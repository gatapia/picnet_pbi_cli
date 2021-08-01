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
      ZipFile.CreateFromDirectory(dir.FullName, opts.Output, CompressionLevel.Optimal, false);
      if (1==1) return;
      using (var archive = ZipFile.Open(opts.Output, ZipArchiveMode.Create)) {
        Array.ForEach(dir.GetFiles("*.*", SearchOption.AllDirectories), f => {
          if (f.Name == Constants.GITIGNORE) return;
          if (Constants.BINARIES.Contains(f.Name)) {
            archive.CreateEntryFromFile(f.FullName, f.Name);
            return;
          }
          var entry = archive.CreateEntry(f.Name);

          using var stream = entry.Open();
          using var writer = new StreamWriter(stream);
          
          var contents = File.ReadAllText(f.FullName, Encoding.UTF8);
          var encoded = Encoding.Unicode.GetBytes(contents);
          writer.Write(encoded);
        });
      }
      Console.WriteLine($"pbix file [{opts.Output}] created");
    }
  }
}