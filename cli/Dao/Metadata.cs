namespace cli.Dao
{
  public class Metadata
  {
    public int Version { get; set; }
    public object[] AutoCreatedRelationships { get; set; }
    public string CreatedFrom { get; set; }
    public string CreatedFromRelease { get; set; }
  }


  public class Settings
  {
    public int Version { get; set; }
    public Reportsettings ReportSettings { get; set; }
    public Queriessettings QueriesSettings { get; set; }
  }

  public class Reportsettings
  {
  }

  public class Queriessettings
  {
    public bool TypeDetectionEnabled { get; set; }
    public bool RelationshipImportEnabled { get; set; }
    public bool RunBackgroundAnalysis { get; set; }
    public string Version { get; set; }
  }
  
  public class VersionClass { 
    public decimal Version { get; set; }
  }


}