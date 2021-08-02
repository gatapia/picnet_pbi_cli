using System.Collections.Generic;

namespace cli.Operations {
  public static class Constants {
    public static readonly string DEFAULT_SRC_DIR = "src";
    public static readonly string GITIGNORE = ".gitignore";
    public static readonly List<string> BINARIES = new() { "DataModel", "SecurityBindings" };
  }
}