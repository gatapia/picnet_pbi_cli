using cli.Operations;

namespace cli {
  public static class OperationsList { 
    public static readonly Operation[] Operations = {
      new Operation {Op = "import", Impl = new PbixImporter() },
      new Operation {Op = "export", Impl = new PbixExporter() },
    };
  }
}