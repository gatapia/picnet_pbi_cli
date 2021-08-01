using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace cli.Operations {
  public class PbixImporter : IRun {
    private static readonly string GITIGNORE = ".gitignore";
    private static readonly string[] BINARIES = { "DataModel", "SecurityBindings" };
    
    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.File)) throw new Exception($"the pbiximporter requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File)) { throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\""); }
      var dir = Directory.CreateDirectory("src");
      ZipFile.ExtractToDirectory(opts.File, dir.FullName, true);
      CreateGitIgnoreFile(dir);
      FormatFiles(dir);
    }

    private void CreateGitIgnoreFile(DirectoryInfo dir) { 
      var path = Path.Combine(dir.FullName, GITIGNORE);
      if (File.Exists(path)) return;
      
      var contents = String.Join("\n", BINARIES);
      File.WriteAllText(path, contents);
    }

    private void FormatFiles(DirectoryInfo dir) { 
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(IsValidSourceFile).
          ToList().
          ForEach(f => File.WriteAllText(f.FullName, FormatFileContents(f), Encoding.UTF8));
    }

    private static bool IsValidSourceFile(FileInfo f) { 
      if (BINARIES.Contains(f.Name) || f.Name == GITIGNORE) return false;
      return String.IsNullOrEmpty(f.Extension) || f.Extension == ".json" || f.Extension == ".xml";
    }

    private string FormatFileContents(FileInfo f) {
      Console.WriteLine($"formatting file [{f.Name}]");
      try { 
        return FormatFileContentsImpl(f, File.ReadAllText(f.FullName, Encoding.Unicode)); 
      } catch {
        return FormatFileContentsImpl(f, File.ReadAllText(f.FullName, Encoding.UTF8));
      }
    }

    private static string FormatFileContentsImpl(FileInfo f, string contents) {
      return f.Extension == ".xml" ?
        XDocument.Parse(contents).ToString(SaveOptions.None) :
        JsonConvert.SerializeObject(JsonConvert.DeserializeObject(contents), Formatting.Indented);
    }
  }
}