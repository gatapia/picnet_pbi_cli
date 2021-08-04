using CommandLine;

namespace cli {
  public class CliOptions {
    [Value(0, HelpText = "The operation to perform", MetaName = "operation", Required = true)] public string Operation { get; set; }
    [Option('f', "file", HelpText = "The pbix file name to import", Required = false)] public string File { get; set; }
    [Option('o', "output", HelpText = "The output file or directory to create or update", Required = false)] public string Output { get; set; }
    [Option('d', "data-name", HelpText = "The name of the data model file to use", Required = false, Default = "default")] public string DataModelName { get; set; }
  }
}