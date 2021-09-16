using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace cli.Operations {
  public class PbixImporter : IRun {
    public string Command => "import";

    public void Run(CliOptions opts) {
      if (string.IsNullOrEmpty(opts.File))
        throw new Exception("the import command requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File))
        throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\"");
      var dir = Directory.CreateDirectory(opts.Dir);
      ZipFile.ExtractToDirectory(opts.File, dir.FullName, true);
      CreateGitIgnoreFile(dir);
      FormatFiles(dir);
      MoveDataModelFile(dir, opts.DataModelName);

      Console.WriteLine($"source directory [{dir.Name}] created and all PBIX files extracted");
    }

    private void CreateGitIgnoreFile(DirectoryInfo dir) {
      var path = Path.Combine(dir.FullName, Constants.GITIGNORE);
      if (File.Exists(path)) return;

      var contents = string.Join("\n", Constants.BINARIES) + "\ndata\\\nbackups\\";
      File.WriteAllText(path, contents);
    }

    private void FormatFiles(DirectoryInfo dir) {
      dir.GetFiles("*.*", SearchOption.AllDirectories).Where(PbixHelpers.IsValidSourceFile).ToList()
          .ForEach(WriteFileContents);
    }

    private void WriteFileContents(FileInfo f) {
      var encoder = PbixHelpers.GetFileEncoding(f);
      var contents = File.ReadAllText(f.FullName, encoder);
      if (ShouldConvertToYaml(contents)) {
        var formatted = PbixHelpers.ConvertJsonToYaml(contents);
        File.WriteAllText(f.FullName + ".yaml", formatted, Encoding.UTF8);
        f.Delete();
      } else {
        var formatted = PbixHelpers.FormatFileContentsImpl(f, contents, true);
        File.WriteAllText(f.FullName, formatted, Encoding.UTF8);
      }
    }

    private bool ShouldConvertToYaml(string contents) {
      if (!Constants.USE_YAML_FOR_EMBEDDED_JSON) return false;
      return contents.IndexOf(":\"{", StringComparison.Ordinal) >= 0;
    }

    private void MoveDataModelFile(DirectoryInfo dir, string name) {
      var datadir = dir.CreateSubdirectory(Constants.DATA_DIR);
      var dest = Path.Combine(datadir.FullName, name);
      if (File.Exists(dest)) File.Delete(dest);
      File.Move(Path.Combine(dir.FullName, "DataModel"), dest);
    }
  }
}