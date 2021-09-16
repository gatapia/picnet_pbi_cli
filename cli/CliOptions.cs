using System;
using System.IO;
using CommandLine;

namespace cli {
  public class CliOptions {
    private string _file;

    [Value(0, HelpText = "The operation to perform", MetaName = "operation", Required = true)]
    public string Operation { get; set; }

    [Option('f', "file", HelpText = "The pbix file name to import or destination file for export (will overwrite)", Required = false)]
    public string File {
      get {
        if (_file == null && Operation == "import") {
          var files = Directory.GetFiles(".", "*.pbix");
          if (files.Length == 1) return _file = files[0];
          throw new Exception("the command requires the -f file_name.pbix parameter");
        }

        return _file;
      }
      set {
        _file = value;
        if (string.IsNullOrWhiteSpace(new FileInfo(_file).Extension)) _file = value + ".pbix";
      }
    }

    [Option('d', "dir", HelpText = "The source directory to export or the output directory to overwrite on import", Required = false, Default = "src")]
    public string Dir { get; set; }

    [Option('m', "model", HelpText = "The name of the data model file to use", Required = false, Default = "default")]
    public string DataModelName { get; set; }
  }
}