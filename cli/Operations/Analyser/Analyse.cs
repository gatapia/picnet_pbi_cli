using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;

namespace cli.Operations.Analyser
{
  public class Analyse
  {
    private int port;
    private string dbname;
    private string layout;

    public Analyse(int port, string dbname, string layout)
    {
      this.port = port;
      this.layout = layout;
      this.dbname = dbname;
    }

    public void Go()
    {
      var measures = GetReportMeasures(port, dbname);
      var unused = GetUnusedMeasures(measures);
      var duplicates = GetDuplicateMeasures(measures);
      new AnalysisPrinter(measures, unused, duplicates).Print();
    }

    private List<PbiMeasure> GetUnusedMeasures(List<PbiMeasure> measures)
    {
      return measures.Where(m => !IsMeasureUsed(m, measures)).ToList();
    }

    private bool IsMeasureUsed(PbiMeasure m, List<PbiMeasure> all)
    {
      return layout.IndexOf(m.GetFullName(), StringComparison.OrdinalIgnoreCase) >= 0 ||
             layout.IndexOf($@"\""{m.MEASURE_NAME}\""", StringComparison.OrdinalIgnoreCase) >= 0 ||
             all.Any(m2 => m2 != m && m2.GetComparableExpression().IndexOf($"[{m.GetComparableName()}]", StringComparison.OrdinalIgnoreCase) >= 0);
    }

    private List<List<PbiMeasure>> GetDuplicateMeasures(List<PbiMeasure> measures)
    {
      var expressions = GetAllMeasureExpressions(measures);
      return expressions.Where(kvp => kvp.Value.Count > 1).Select(kvp => kvp.Value).ToList();
    }

    private static Dictionary<string, List<PbiMeasure>> GetAllMeasureExpressions(List<PbiMeasure> measures)
    {
      var duplicates = new Dictionary<string, List<PbiMeasure>>();
      measures.ForEach(m =>
      {
        var exp = m.GetComparableExpression();
        if (duplicates.ContainsKey(exp))
        {
          duplicates[exp].Add(m);
        }
        else
        {
          duplicates[exp] = new List<PbiMeasure> {m};
        }
      });
      return duplicates;
    }

    private List<PbiMeasure> GetReportMeasures(int port, string dbname)
    {
      using var conn = new AdomdConnection($"Data Source=localhost:{port};Catalog={dbname}");
      conn.Open();

      var mdx =
        @"SELECT MEASURE_NAME, EXPRESSION, MEASUREGROUP_NAME, MEASURE_DISPLAY_FOLDER FROM $SYSTEM.MDSCHEMA_MEASURES";
      var cmd = new AdomdCommand(mdx, conn);
      var reader = cmd.ExecuteReader();
      var results = new List<PbiMeasure>();
      while (reader.Read())
      {
        var name = Convert.ToString(reader["MEASURE_NAME"]);
        if (name == "__Default measure") continue;
        results.Add(new PbiMeasure
        {
          MEASURE_NAME = name,
          EXPRESSION = Convert.ToString(reader["EXPRESSION"]),
          MEASUREGROUP_NAME = Convert.ToString(reader["MEASUREGROUP_NAME"]),
          MEASURE_DISPLAY_FOLDER = Convert.ToString(reader["MEASURE_DISPLAY_FOLDER"]),
        });
      }

      return results;
    }
  }
}