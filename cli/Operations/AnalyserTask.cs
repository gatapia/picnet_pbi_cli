using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cli.Operations.Analyser;

namespace cli.Operations {
  [Operation("analyze")]
  public class AnalyserTask : IRun {

    private static readonly string[] PBI_DIRS = {
      @"C:\Users\guidot\AppData\Local\Microsoft\Power BI Desktop\AnalysisServicesWorkspaces",
      @"C:\Users\guidot\AppData\Local\Microsoft\Power BI Desktop SSRS\AnalysisServicesWorkspaces"
    };

    public void Run(CliOptions opts) {
      var layoutfile = Path.Combine(opts.Dir, "Report", "Layout");
      if (!File.Exists(layoutfile)) throw new Exception($"the specified directory '{opts.Dir}' does not contain the expected Report\\Layout file");
      var layout = File.ReadAllText(layoutfile);
      var runningpbis = GetSsasPortsAndDb();
      if (!runningpbis.Any()) throw new Exception("no running Power BI instances found.  Please start the report that matches the code in the current directory.");
      if (runningpbis.Count() > 1) throw new Exception("multiple running Power BI instances found.  Please ensure only the single report that matches the code in the current directory is running.");
      var pbi = runningpbis.Single();
      
      new Analyse(pbi.port, pbi.dbname, layout).Go();
    }

    

    private List<(int port, string dbname)> GetSsasPortsAndDb()
    {
      return PBI_DIRS.SelectMany(d =>
      {
        return Directory.GetDirectories(d).
            Select(workspace => Path.Combine(workspace, "Data")).
            Where(Directory.Exists).
            Select(datadir =>
        {
          var portfile = Path.Combine(datadir, "msmdsrv.port.txt");
          if (!File.Exists(portfile)) return (-1, null);
          var porttxt = File.ReadAllText(portfile, Encoding.Unicode).Trim();
          var port = Int32.Parse(porttxt);
          var datafile = Directory.GetFiles(datadir, "*.db.xml").SingleOrDefault();
          return datafile == null ? 
            (-1, null) : 
            (port, Path.GetFileName(datafile).Split('.').First());
        });
      }).Where(pn => pn.port > 0 && pn.Item2 != null).ToList();
    }
  }
}