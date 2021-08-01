using cli.Operations;

namespace cli {
  public class Operation {
    public string Op { get; set; }
    public IRun Impl { get; set; }
  }
}