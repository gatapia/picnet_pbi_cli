namespace cli.Dao
{
  public class Layout
  {
    public int id { get; set; }
    public string theme { get; set; }
    public Resourcepackage[] resourcePackages { get; set; }
    public Section[] sections { get; set; }
    public string config { get; set; }
    public int layoutOptimization { get; set; }
  }

  public class Resourcepackage
  {
    public Resourcepackage1 resourcePackage { get; set; }
  }

  public class Resourcepackage1
  {
    public string name { get; set; }
    public int type { get; set; }
    public Item[] items { get; set; }
    public bool disabled { get; set; }
  }

  public class Item
  {
    public int type { get; set; }
    public string path { get; set; }
    public string name { get; set; }
  }

  public class Section
  {
    public string name { get; set; }
    public string displayName { get; set; }
    public string filters { get; set; }
    public int ordinal { get; set; }
    public Visualcontainer[] visualContainers { get; set; }
    public string config { get; set; }
    public int displayOption { get; set; }
    public int width { get; set; }
    public int height { get; set; }
  }

  public class Visualcontainer
  {
    public float x { get; set; }
    public float y { get; set; }
    public int z { get; set; }
    public float width { get; set; }
    public float height { get; set; }
    public string config { get; set; }
    public string filters { get; set; }
    public int tabOrder { get; set; }
    public string query { get; set; }
    public string dataTransforms { get; set; }
  }

}