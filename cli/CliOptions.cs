using CommandLine;

namespace cli {
  public class CliOptions {
    [Value(0, HelpText = "The operation to perform", MetaName = "operation", Required = true)] public string Operation { get; set; }
    [Option('f', "file", HelpText = "The file name to import", Required = false)] public string File { get; set; }
    [Option('o', "output", HelpText = "The file name to export", Required = false)] public string Output { get; set; }
  }
}