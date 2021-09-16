namespace cli.Operations {
  public interface IRun {
    public string Command { get; }
    void Run(CliOptions opts);
  }
}