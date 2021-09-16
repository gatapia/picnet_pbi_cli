using System.Collections.Generic;

namespace cli.Operations {
  public static class Constants {
    public static readonly string DATA_DIR = "data";
    public static readonly string BACKUP_DIR = "backups";
    public static readonly string GITIGNORE = ".gitignore";
    public static readonly List<string> BINARIES = new() { "DataModel", "SecurityBindings" };
  }
}