using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cli.Operations.Analyser
{
  public class AnalysisPrinter
  {
    private readonly List<PbiMeasure> measures;
    private readonly List<PbiMeasure> unused;
    private readonly List<List<PbiMeasure>> duplicates;

    public AnalysisPrinter(List<PbiMeasure> measures, List<PbiMeasure> unused, List<List<PbiMeasure>> duplicates)
    {
      this.duplicates = duplicates;
      this.unused = unused;
      this.measures = measures;
    }

    public void Print()
    {
      var msg = GetAnalysisMessage();
      Console.WriteLine(msg);
    }

    private string GetAnalysisMessage()
    {
      var msg = new StringBuilder("Analysis Results\n================\n\n");
      msg.AppendLine("All Measures\n------------\n");
      measures.ForEach(m => { msg.Append(" - ").AppendLine(m.GetFullName()); });
      if (unused.Any())
      {
        msg.AppendLine("\n\nUnused Measures\n---------------\n");
        unused.ForEach(m => { msg.Append(" - ").AppendLine(m.GetFullName()); });
      }
      else
      {
        msg.AppendLine("\n\nNo unused measures");
      }

      if (duplicates.Any())
      {
        msg.AppendLine("\n\nDuplicate Measures\n------------------\n");
        duplicates.ForEach(dups =>
        {
          msg.Append(" - ").Append(String.Join(", ", dups.Select(m => m.GetFullName())))
            .AppendLine(" have the same expression");
        });
      }
      else
      {
        msg.AppendLine("\n\nNo duplicate measures");
      }
      return msg.ToString();
    }
  }
}