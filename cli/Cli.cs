using System.Linq;
using CommandLine;

namespace cli
{
  public static class Cli
  {

    static void Main(string[] args)
    {
      Parser.Default.ParseArguments<CliOptions>(args).WithParsed(Run);

    }

    private static void Run(CliOptions opts) { 
      var op = OperationsList.Operations.Single(o => o.Op == opts.Operation);
      op.Impl.Run(opts);
    }
  }
}
