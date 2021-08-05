using System;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace cli.Operations {
  public class PbixExporter : IRun 
  {

    public void Run(CliOptions opts) 
    { 
      if (String.IsNullOrEmpty(opts.Dir)) throw new Exception("the export command requires the -o file_name.pbix parameter");

      var dir = new DirectoryInfo(opts.Dir);
      if (!dir.Exists) { throw new FileNotFoundException($"could not find the specified source directory \"{dir.Name}\""); }
      
      if (File.Exists(opts.File)) File.Delete(opts.File);
      var datamodel = Path.Combine(dir.FullName, "DataModel");
      if (File.Exists(datamodel)) File.Delete(datamodel);
      File.Copy(Path.Combine(dir.FullName, "data", opts.DataModelName), datamodel);

      using var archive = new ZipFile(opts.File);
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(f => f.Name != Constants.GITIGNORE).
          Where(f => f.Directory?.Name != Constants.DATA_DIR).
          ToList().
          ForEach(f => {
        var name = f.FullName.Replace(dir.FullName + "\\", "");
        Console.WriteLine($"adding file to archive [{name}]");

        if (PbixHelpers.IsValidSourceFile(f)) { AddSourceFileToArchive(f, archive, name); }
        else { AddBinaryFileToArchive(archive, f, dir); }
      });
      archive.Save();
      File.Delete(datamodel);

      Console.WriteLine($"pbix file [{opts.File}] created");
    }

    private static void AddBinaryFileToArchive(ZipFile archive, FileInfo f, DirectoryInfo dir)
    {
      archive.AddFile(f.FullName, f.Directory?.FullName.Replace(dir.FullName, ""));
    }

    private static void AddSourceFileToArchive(FileInfo f, ZipFile archive, string name)
    {
      var contents = File.ReadAllText(f.FullName, Encoding.UTF8);
      contents = PbixHelpers.FormatFileContentsImpl(f, contents, false);
      var encoder = PbixHelpers.GetFileEncoding(f);
      var bytes = encoder.GetBytes(contents);
      archive.AddEntry(name, bytes);
    }
  }
}