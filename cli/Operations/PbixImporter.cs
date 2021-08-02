using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace cli.Operations {
  public class PbixImporter : IRun {

    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.File)) throw new Exception($"the pbiximporter requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File)) { throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\""); }
      var dir = Directory.CreateDirectory(opts.Output ?? Constants.DEFAULT_SRC_DIR);
      ZipFile.ExtractToDirectory(opts.File, dir.FullName, true);
      CreateGitIgnoreFile(dir);
      FormatFiles(dir);
    }

    private void CreateGitIgnoreFile(DirectoryInfo dir) { 
      var path = Path.Combine(dir.FullName, Constants.GITIGNORE);
      if (File.Exists(path)) return;
      
      var contents = String.Join("\n", Constants.BINARIES);
      File.WriteAllText(path, contents);
    }

    private void FormatFiles(DirectoryInfo dir) { 
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(PbixHelpers.IsValidSourceFile).
          ToList().
          ForEach(f => File.WriteAllText(f.FullName, FormatFileContents(f), Encoding.UTF8));
    }

    private string FormatFileContents(FileInfo f) {
      var encoder = PbixHelpers.GetFileEncoding(f);
      var contents = File.ReadAllText(f.FullName, encoder);
      return PbixHelpers.FormatFileContentsImpl(f, contents, true);
    }
  }
}