namespace cli.Dao
{
  public class Theme
  {
    public string name { get; set; }
    public string[] dataColors { get; set; }
    public string foreground { get; set; }
    public string foregroundNeutralSecondary { get; set; }
    public string foregroundNeutralTertiary { get; set; }
    public string background { get; set; }
    public string backgroundLight { get; set; }
    public string backgroundNeutral { get; set; }
    public string tableAccent { get; set; }
    public string good { get; set; }
    public string neutral { get; set; }
    public string bad { get; set; }
    public string maximum { get; set; }
    public string center { get; set; }
    public string minimum { get; set; }
    public string _null { get; set; }
    public string hyperlink { get; set; }
    public string visitedHyperlink { get; set; }
    public Textclasses textClasses { get; set; }
    public Visualstyles visualStyles { get; set; }
  }

  public class Textclasses
  {
    public Callout callout { get; set; }
    public Title title { get; set; }
    public Header header { get; set; }
    public Label label { get; set; }
  }

  public class Callout
  {
    public int fontSize { get; set; }
    public string fontFace { get; set; }
    public string color { get; set; }
  }

  public class Title
  {
    public int fontSize { get; set; }
    public string fontFace { get; set; }
    public string color { get; set; }
  }

  public class Header
  {
    public int fontSize { get; set; }
    public string fontFace { get; set; }
    public string color { get; set; }
  }

  public class Label
  {
    public int fontSize { get; set; }
    public string fontFace { get; set; }
    public string color { get; set; }
  }

  public class Visualstyles
  {
    public _ _ { get; set; }
    public Scatterchart scatterChart { get; set; }
    public Linechart lineChart { get; set; }
    public Map map { get; set; }
    public Piechart pieChart { get; set; }
    public Donutchart donutChart { get; set; }
    public Pivottable pivotTable { get; set; }
    public Multirowcard multiRowCard { get; set; }
    public Kpi kpi { get; set; }
    public Slicer slicer { get; set; }
    public Waterfallchart waterfallChart { get; set; }
    public Columnchart columnChart { get; set; }
    public Clusteredcolumnchart clusteredColumnChart { get; set; }
    public Hundredpercentstackedcolumnchart hundredPercentStackedColumnChart { get; set; }
    public Barchart barChart { get; set; }
    public Clusteredbarchart clusteredBarChart { get; set; }
    public Hundredpercentstackedbarchart hundredPercentStackedBarChart { get; set; }
    public Areachart areaChart { get; set; }
    public Stackedareachart stackedAreaChart { get; set; }
    public Lineclusteredcolumncombochart lineClusteredColumnComboChart { get; set; }
    public Linestackedcolumncombochart lineStackedColumnComboChart { get; set; }
    public Ribbonchart ribbonChart { get; set; }
    public Group group { get; set; }
    public Basicshape basicShape { get; set; }
    public Shape shape { get; set; }
    public Image image { get; set; }
    public Actionbutton actionButton { get; set; }
    public Textbox textbox { get; set; }
    public Page page { get; set; }
  }

  public class _
  {
    public _1 _1 { get; set; }
  }

  public class _1
  {
    public _2[] _ { get; set; }
    public Line[] line { get; set; }
    public Outline[] outline { get; set; }
    public Plotarea[] plotArea { get; set; }
    public Categoryaxi[] categoryAxis { get; set; }
    public Valueaxi[] valueAxis { get; set; }
    public Title1[] title { get; set; }
    public Linestyle[] lineStyles { get; set; }
    public Wordwrap[] wordWrap { get; set; }
    public Background[] background { get; set; }
    public Outspacepane[] outspacePane { get; set; }
    public Filtercard[] filterCard { get; set; }
  }

  public class _2
  {
    public bool wordWrap { get; set; }
  }

  public class Line
  {
    public int transparency { get; set; }
  }

  public class Outline
  {
    public int transparency { get; set; }
  }

  public class Plotarea
  {
    public int transparency { get; set; }
  }

  public class Categoryaxi
  {
    public bool showAxisTitle { get; set; }
    public string gridlineStyle { get; set; }
  }

  public class Valueaxi
  {
    public bool showAxisTitle { get; set; }
    public string gridlineStyle { get; set; }
  }

  public class Title1
  {
    public bool titleWrap { get; set; }
  }

  public class Linestyle
  {
    public int strokeWidth { get; set; }
  }

  public class Wordwrap
  {
    public bool show { get; set; }
  }

  public class Background
  {
    public bool show { get; set; }
    public int transparency { get; set; }
  }

  public class Outspacepane
  {
    public Backgroundcolor backgroundColor { get; set; }
    public Foregroundcolor foregroundColor { get; set; }
    public int transparency { get; set; }
    public bool border { get; set; }
    public Bordercolor borderColor { get; set; }
  }

  public class Backgroundcolor
  {
    public Solid solid { get; set; }
  }

  public class Solid
  {
    public string color { get; set; }
  }

  public class Foregroundcolor
  {
    public Solid1 solid { get; set; }
  }

  public class Solid1
  {
    public string color { get; set; }
  }

  public class Bordercolor
  {
    public Solid2 solid { get; set; }
  }

  public class Solid2
  {
    public string color { get; set; }
  }

  public class Filtercard
  {
    public string id { get; set; }
    public int transparency { get; set; }
    public Foregroundcolor1 foregroundColor { get; set; }
    public bool border { get; set; }
  }

  public class Foregroundcolor1
  {
    public Solid3 solid { get; set; }
  }

  public class Solid3
  {
    public string color { get; set; }
  }

  public class Scatterchart
  {
    public _3 _ { get; set; }
  }

  public class _3
  {
    public Bubble[] bubbles { get; set; }
    public General[] general { get; set; }
    public Fillpoint[] fillPoint { get; set; }
    public Legend[] legend { get; set; }
  }

  public class Bubble
  {
    public int bubbleSize { get; set; }
  }

  public class General
  {
    public bool responsive { get; set; }
  }

  public class Fillpoint
  {
    public bool show { get; set; }
  }

  public class Legend
  {
    public bool showGradientLegend { get; set; }
  }

  public class Linechart
  {
    public _4 _ { get; set; }
  }

  public class _4
  {
    public General1[] general { get; set; }
    public Smallmultipleslayout[] smallMultiplesLayout { get; set; }
  }

  public class General1
  {
    public bool responsive { get; set; }
  }

  public class Smallmultipleslayout
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Map
  {
    public _5 _ { get; set; }
  }

  public class _5
  {
    public Bubble1[] bubbles { get; set; }
  }

  public class Bubble1
  {
    public int bubbleSize { get; set; }
  }

  public class Piechart
  {
    public _6 _ { get; set; }
  }

  public class _6
  {
    public Legend1[] legend { get; set; }
    public Label1[] labels { get; set; }
  }

  public class Legend1
  {
    public bool show { get; set; }
    public string position { get; set; }
  }

  public class Label1
  {
    public string labelStyle { get; set; }
  }

  public class Donutchart
  {
    public _7 _ { get; set; }
  }

  public class _7
  {
    public Legend2[] legend { get; set; }
    public Label2[] labels { get; set; }
  }

  public class Legend2
  {
    public bool show { get; set; }
    public string position { get; set; }
  }

  public class Label2
  {
    public string labelStyle { get; set; }
  }

  public class Pivottable
  {
    public _8 _ { get; set; }
  }

  public class _8
  {
    public _9[] _ { get; set; }
  }

  public class _9
  {
    public bool showExpandCollapseButtons { get; set; }
  }

  public class Multirowcard
  {
    public _10 _ { get; set; }
  }

  public class _10
  {
    public Card[] card { get; set; }
  }

  public class Card
  {
    public int outlineWeight { get; set; }
    public bool barShow { get; set; }
    public int barWeight { get; set; }
  }

  public class Kpi
  {
    public _11 _ { get; set; }
  }

  public class _11
  {
    public Trendline[] trendline { get; set; }
  }

  public class Trendline
  {
    public int transparency { get; set; }
  }

  public class Slicer
  {
    public _12 _ { get; set; }
  }

  public class _12
  {
    public General2[] general { get; set; }
  }

  public class General2
  {
    public bool responsive { get; set; }
  }

  public class Waterfallchart
  {
    public _13 _ { get; set; }
  }

  public class _13
  {
    public General3[] general { get; set; }
  }

  public class General3
  {
    public bool responsive { get; set; }
  }

  public class Columnchart
  {
    public _14 _ { get; set; }
  }

  public class _14
  {
    public General4[] general { get; set; }
    public Legend3[] legend { get; set; }
    public Smallmultipleslayout1[] smallMultiplesLayout { get; set; }
  }

  public class General4
  {
    public bool responsive { get; set; }
  }

  public class Legend3
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout1
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Clusteredcolumnchart
  {
    public _15 _ { get; set; }
  }

  public class _15
  {
    public General5[] general { get; set; }
    public Legend4[] legend { get; set; }
    public Smallmultipleslayout2[] smallMultiplesLayout { get; set; }
  }

  public class General5
  {
    public bool responsive { get; set; }
  }

  public class Legend4
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout2
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Hundredpercentstackedcolumnchart
  {
    public _16 _ { get; set; }
  }

  public class _16
  {
    public General6[] general { get; set; }
    public Legend5[] legend { get; set; }
    public Smallmultipleslayout3[] smallMultiplesLayout { get; set; }
  }

  public class General6
  {
    public bool responsive { get; set; }
  }

  public class Legend5
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout3
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Barchart
  {
    public _17 _ { get; set; }
  }

  public class _17
  {
    public General7[] general { get; set; }
    public Legend6[] legend { get; set; }
    public Smallmultipleslayout4[] smallMultiplesLayout { get; set; }
  }

  public class General7
  {
    public bool responsive { get; set; }
  }

  public class Legend6
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout4
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Clusteredbarchart
  {
    public _18 _ { get; set; }
  }

  public class _18
  {
    public General8[] general { get; set; }
    public Legend7[] legend { get; set; }
    public Smallmultipleslayout5[] smallMultiplesLayout { get; set; }
  }

  public class General8
  {
    public bool responsive { get; set; }
  }

  public class Legend7
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout5
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Hundredpercentstackedbarchart
  {
    public _19 _ { get; set; }
  }

  public class _19
  {
    public General9[] general { get; set; }
    public Legend8[] legend { get; set; }
    public Smallmultipleslayout6[] smallMultiplesLayout { get; set; }
  }

  public class General9
  {
    public bool responsive { get; set; }
  }

  public class Legend8
  {
    public bool showGradientLegend { get; set; }
  }

  public class Smallmultipleslayout6
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Areachart
  {
    public _20 _ { get; set; }
  }

  public class _20
  {
    public General10[] general { get; set; }
    public Smallmultipleslayout7[] smallMultiplesLayout { get; set; }
  }

  public class General10
  {
    public bool responsive { get; set; }
  }

  public class Smallmultipleslayout7
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Stackedareachart
  {
    public _21 _ { get; set; }
  }

  public class _21
  {
    public General11[] general { get; set; }
    public Smallmultipleslayout8[] smallMultiplesLayout { get; set; }
  }

  public class General11
  {
    public bool responsive { get; set; }
  }

  public class Smallmultipleslayout8
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Lineclusteredcolumncombochart
  {
    public _22 _ { get; set; }
  }

  public class _22
  {
    public General12[] general { get; set; }
    public Smallmultipleslayout9[] smallMultiplesLayout { get; set; }
  }

  public class General12
  {
    public bool responsive { get; set; }
  }

  public class Smallmultipleslayout9
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Linestackedcolumncombochart
  {
    public _23 _ { get; set; }
  }

  public class _23
  {
    public General13[] general { get; set; }
    public Smallmultipleslayout10[] smallMultiplesLayout { get; set; }
  }

  public class General13
  {
    public bool responsive { get; set; }
  }

  public class Smallmultipleslayout10
  {
    public int backgroundTransparency { get; set; }
    public string gridLineType { get; set; }
  }

  public class Ribbonchart
  {
    public _24 _ { get; set; }
  }

  public class _24
  {
    public General14[] general { get; set; }
  }

  public class General14
  {
    public bool responsive { get; set; }
  }

  public class Group
  {
    public _25 _ { get; set; }
  }

  public class _25
  {
    public Background1[] background { get; set; }
  }

  public class Background1
  {
    public bool show { get; set; }
  }

  public class Basicshape
  {
    public _26 _ { get; set; }
  }

  public class _26
  {
    public Background2[] background { get; set; }
    public General15[] general { get; set; }
    public Visualheader[] visualHeader { get; set; }
  }

  public class Background2
  {
    public bool show { get; set; }
  }

  public class General15
  {
    public bool keepLayerOrder { get; set; }
  }

  public class Visualheader
  {
    public bool show { get; set; }
  }

  public class Shape
  {
    public _27 _ { get; set; }
  }

  public class _27
  {
    public Background3[] background { get; set; }
    public General16[] general { get; set; }
    public Visualheader1[] visualHeader { get; set; }
  }

  public class Background3
  {
    public bool show { get; set; }
  }

  public class General16
  {
    public bool keepLayerOrder { get; set; }
  }

  public class Visualheader1
  {
    public bool show { get; set; }
  }

  public class Image
  {
    public _28 _ { get; set; }
  }

  public class _28
  {
    public Background4[] background { get; set; }
    public General17[] general { get; set; }
    public Visualheader2[] visualHeader { get; set; }
    public Lockaspect[] lockAspect { get; set; }
  }

  public class Background4
  {
    public bool show { get; set; }
  }

  public class General17
  {
    public bool keepLayerOrder { get; set; }
  }

  public class Visualheader2
  {
    public bool show { get; set; }
  }

  public class Lockaspect
  {
    public bool show { get; set; }
  }

  public class Actionbutton
  {
    public _29 _ { get; set; }
  }

  public class _29
  {
    public Visualheader3[] visualHeader { get; set; }
  }

  public class Visualheader3
  {
    public bool show { get; set; }
  }

  public class Textbox
  {
    public _30 _ { get; set; }
  }

  public class _30
  {
    public General18[] general { get; set; }
    public Visualheader4[] visualHeader { get; set; }
  }

  public class General18
  {
    public bool keepLayerOrder { get; set; }
  }

  public class Visualheader4
  {
    public bool show { get; set; }
  }

  public class Page
  {
    public _31 _ { get; set; }
  }

  public class _31
  {
    public Outspace[] outspace { get; set; }
    public Background5[] background { get; set; }
  }

  public class Outspace
  {
    public Color color { get; set; }
  }

  public class Color
  {
    public Solid4 solid { get; set; }
  }

  public class Solid4
  {
    public string color { get; set; }
  }

  public class Background5
  {
    public int transparency { get; set; }
  }

}