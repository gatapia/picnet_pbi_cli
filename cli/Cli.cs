using System;
using System.Linq;
using System.Reflection;
using cli.Operations;
using CommandLine;

namespace cli
{
  public static class Cli {
    static void Main(string[] args) => Parser.Default.ParseArguments<CliOptions>(args).WithParsed(Run);

    private static void Run(CliOptions opts) {
      var operations = Assembly.GetExecutingAssembly().GetTypes().
          Where(t => t.GetCustomAttribute<OperationAttribute>() != null).
          ToDictionary(t => t.GetCustomAttribute<OperationAttribute>()?.Command, t => t);
      var type = operations[opts.Operation];
      var op = (IRun) Activator.CreateInstance(type);
      op?.Run(opts);
    }
  }
}
