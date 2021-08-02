using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using cli.Dao;

namespace cli.Operations {
  public class PbixExporter : IRun {
    
    
    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.Output)) throw new Exception($"the pbiximporter requires the -o file_name.pbix parameter");
      var dir = new DirectoryInfo(opts.File ?? Constants.DEFAULT_SRC_DIR);
      if (!dir.Exists) { throw new FileNotFoundException($"could not find the specified source directory \"{dir.Name}\""); }
      
      if (File.Exists(opts.Output)) File.Delete(opts.Output);
      
      using (var archive = ZipFile.Open(opts.Output, ZipArchiveMode.Create)) {
        Array.ForEach(dir.GetFiles("*.*", SearchOption.AllDirectories), f => {
          if (f.Name == Constants.GITIGNORE) return;
          var name = f.FullName.Replace(dir.FullName + "\\", "");
          if (!PbixHelpers.IsValidSourceFile(f)) {
            Console.WriteLine($"adding binary entry [{name}]");
            archive.CreateEntryFromFile(f.FullName, name, CompressionLevel.Optimal);
            return;
          }
          var contents = File.ReadAllText(f.FullName, Encoding.UTF8);
          contents = PbixHelpers.FormatFileContentsImpl(f, contents, false);
          var encoder = PbixHelpers.GetFileEncoding(f);

          var entry = archive.CreateEntry(name, CompressionLevel.Optimal);
          using var stream = entry.Open();
          using var writer = new StreamWriter(stream, encoder);

          // var encoded = encoder.GetBytes(contents);
          Console.WriteLine($"adding text entry [{name}] encoding [{encoder.GetType().Name}]");
          writer.Write(contents);
        });
      }
      Console.WriteLine($"pbix file [{opts.Output}] created");
      Directory.Delete(opts.Output + "dir", true);
      ZipFile.ExtractToDirectory(opts.Output, opts.Output + "dir");
    }
  }
}