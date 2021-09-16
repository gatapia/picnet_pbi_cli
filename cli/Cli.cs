using System;
using System.Linq;
using System.Reflection;
using cli.Operations;
using CommandLine;

namespace cli {
  public static class Cli {
    private static void Main(string[] args) { Parser.Default.ParseArguments<CliOptions>(args).WithParsed(Run); }

    private static void Run(CliOptions opts) {
      var operations = Assembly.GetExecutingAssembly().
          GetTypes().
          Where(t => typeof(IRun).IsAssignableFrom(t) && t.IsClass).
          Select(t => (IRun) Activator.CreateInstance(t)).
          ToDictionary(r => r?.Command, r => r);
      operations[opts.Operation].Run(opts);
    }
  }
}