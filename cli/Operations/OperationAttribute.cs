using System;

namespace cli.Operations {
  public class OperationAttribute : Attribute {
    public string Command { get; }

    public OperationAttribute(string command) { Command = command; }
  }
}