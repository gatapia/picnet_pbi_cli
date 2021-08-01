using System;
using System.IO;
using System.IO.Compression;

namespace cli.Operations {
  public class PbixExporter : IRun {
    
    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.Output)) throw new Exception($"the pbiximporter requires the -o file_name.pbix parameter");
      var dir = opts.File ?? Constants.DEFAULT_SRC_DIR;
      if (!Directory.Exists(dir)) { throw new FileNotFoundException($"could not find the specified source directory \"{dir}\""); }
      ZipFile.CreateFromDirectory(dir, opts.Output, CompressionLevel.Fastest, false);
      Console.WriteLine($"pbix file [{opts.Output}] created");
    }
  }
}