using cli.Operations;

namespace cli {
  public static class OperationsList { 
    public static readonly Operation[] Operations = {
      new() {Op = "import", Impl = new PbixImporter() },
      new() {Op = "export", Impl = new PbixExporter() },
      new() {Op = "data", Impl = new PbixDataExtract() },
      new() {Op = "debug", Impl = new PbixDebugger() },
    };
  }
}