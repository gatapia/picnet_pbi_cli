using System.Text.RegularExpressions;

namespace cli.Operations.Analyser {
  public class PbiMeasure {
    public string MEASURE_NAME { get; set; }
    public string EXPRESSION { get; set; }
    public string MEASUREGROUP_NAME { get; set; }
    public string MEASURE_DISPLAY_FOLDER { get; set; }

    public string GetFullName() { return $"{MEASUREGROUP_NAME}.{MEASURE_NAME}"; }

    public string GetComparableExpression() { return Regex.Replace(EXPRESSION.ToLower(), @"\s+", ""); }

    public string GetComparableName() { return Regex.Replace(MEASURE_NAME.ToLower(), @"\s+", ""); }
  }
}