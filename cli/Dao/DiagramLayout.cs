namespace cli.Dao
{

  public class DiagramLayout
  {
    public string version { get; set; }
    public Diagram[] diagrams { get; set; }
    public string selectedDiagram { get; set; }
    public string defaultDiagram { get; set; }
  }

  public class Diagram
  {
    public int ordinal { get; set; }
    public Scrollposition scrollPosition { get; set; }
    public Node[] nodes { get; set; }
    public string name { get; set; }
    public float zoomValue { get; set; }
    public bool pinKeyFieldsToTop { get; set; }
    public bool showExtraHeaderInfo { get; set; }
    public bool hideKeyFieldsWhenCollapsed { get; set; }
  }

  public class Scrollposition
  {
    public int x { get; set; }
    public int y { get; set; }
  }

  public class Node
  {
    public Location location { get; set; }
    public string nodeIndex { get; set; }
    public Size size { get; set; }
    public int zIndex { get; set; }
  }

  public class Location
  {
    public float x { get; set; }
    public float y { get; set; }
  }

  public class Size
  {
    public float height { get; set; }
    public float width { get; set; }
  }

}