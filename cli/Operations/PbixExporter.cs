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
      if (String.IsNullOrEmpty(opts.Output)) throw new Exception("the pbiximporter requires the -o file_name.pbix parameter");

      var dir = new DirectoryInfo(opts.File ?? Constants.DEFAULT_SRC_DIR);
      if (!dir.Exists) { throw new FileNotFoundException($"could not find the specified source directory \"{dir.Name}\""); }
      
      if (File.Exists(opts.Output)) File.Delete(opts.Output);

      using var archive = new ZipFile(opts.Output);
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(f => f.Name != Constants.GITIGNORE).
          ToList().
          ForEach(f => {
        var name = f.FullName.Replace(dir.FullName + "\\", "");
        Console.WriteLine($"adding file to archive [{name}]");

        if (PbixHelpers.IsValidSourceFile(f)) { AddSourceFileToArchive(f, archive, name); }
        else { AddBinaryFileToArchive(name, archive, f, dir); }
      });
      archive.Save();
      Console.WriteLine($"pbix file [{opts.Output}] created");
    }

    private static void AddBinaryFileToArchive(string name, ZipFile archive, FileInfo f, DirectoryInfo dir)
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