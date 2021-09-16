using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace cli.Operations {
  public class PbixExporter : IRun 
  {

    public void Run(CliOptions opts) 
    { 
      if (String.IsNullOrEmpty(opts.Dir)) throw new Exception("the export command requires the -o file_name.pbix parameter");

      var dir = new DirectoryInfo(opts.Dir);
      if (!dir.Exists) { throw new FileNotFoundException($"could not find the specified source directory \"{dir.Name}\""); }

      if (File.Exists(opts.File)) {
        var backup = Directory.CreateDirectory(Path.Combine(dir.FullName, Constants.BACKUP_DIR));
        BackupPbixFile(opts.File, backup.FullName);
        File.Delete(opts.File);
      }
      var datamodel = Path.Combine(dir.FullName, "DataModel");
      if (File.Exists(datamodel)) File.Delete(datamodel);
      File.Copy(Path.Combine(dir.FullName, "data", opts.DataModelName), datamodel);

      using var archive = new ZipFile(opts.File);
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(f => f.Name != Constants.GITIGNORE).
          Where(f => f.Directory?.Name != Constants.DATA_DIR && f.Directory?.Name != Constants.BACKUP_DIR).
          ToList().
          ForEach(f => {
        var name = f.FullName.Replace(dir.FullName + "\\", "");
        
        if (name.EndsWith(".yaml")) { AddYamlFileToArchive(f, archive, name); }
        else if (PbixHelpers.IsValidSourceFile(f)) { AddSourceFileToArchive(f, archive, name); }
        else { AddBinaryFileToArchive(archive, f, dir); }
      });
      archive.Save();
      File.Delete(datamodel);

      Console.WriteLine($"pbix file [{opts.File}] created");
    }

    private void BackupPbixFile(string file, string dir)
    {

      var name = new FileInfo(file).Name;
      var path = Path.Combine(dir, name.Replace(".pbix", $"_{DateTime.Now:yyyyMMdd HHmm}.pbix"));
      File.Delete(path);
      File.Copy(file, path);

      new DirectoryInfo(dir).GetFiles("*.pbix").Where(f => (DateTime.Now - f.CreationTime).TotalDays > 5).ToList().ForEach(f => f.Delete());
    }

    private void AddYamlFileToArchive(FileInfo f, ZipFile archive, string name) {
      var contents = File.ReadAllText(f.FullName, Encoding.UTF8);
      var deserializer = new DeserializerBuilder().Build();
      var yaml = deserializer.Deserialize<dynamic>(contents);
      // these regex replacements are much simpler than using custom converter
      var json = JsonConvert.SerializeObject(yaml, Formatting.None);
      json = Regex.Replace(json, "\\\"([0-9.]+)\\\"", "$1");
      json = Regex.Replace(json, @"\n\s*", "");
      var encoder = PbixHelpers.GetFileEncoding(f);
      var bytes = encoder.GetBytes(json);
      archive.AddEntry(name.Replace(".yaml", ""), bytes);
    }

    private static void AddSourceFileToArchive(FileInfo f, ZipFile archive, string name)
    {
      var contents = File.ReadAllText(f.FullName, Encoding.UTF8);
      contents = PbixHelpers.FormatFileContentsImpl(f, contents, false);
      var encoder = PbixHelpers.GetFileEncoding(f);
      var bytes = encoder.GetBytes(contents);
      archive.AddEntry(name, bytes);
    }

    private static void AddBinaryFileToArchive(ZipFile archive, FileInfo f, DirectoryInfo dir)
    {
      archive.AddFile(f.FullName, f.Directory?.FullName.Replace(dir.FullName, ""));
    }
  }
}