using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace cli.Operations {
  public class PbixDebugger : IRun {

    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.File)) throw new Exception("the debug command requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File)) { throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\""); }
      using var archive = ZipFile.Open(opts.File, ZipArchiveMode.Read);
      Console.WriteLine($"archive [{opts.File}] mode[{archive.Mode}] entries[{archive.Entries.Count}]");
      Console.WriteLine("Name\tAttributes\tCompressed\tCrc32\tLength");
      archive.Entries.
          Select(e => $"{e.Name}\t{e.ExternalAttributes}\t{e.CompressedLength}\t{e.Crc32}\t{e.Length}").
          OrderBy(s => s).
          ToList().
          ForEach(Console.WriteLine);
    }
  }
}