using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace cli.Operations
{
  public static class PbixHelpers
  {
    public static bool IsValidSourceFile(FileInfo f)
    {
      if (Constants.BINARIES.Contains(f.Name) || f.Name == Constants.GITIGNORE) return false;
      return String.IsNullOrEmpty(f.Extension) || f.Extension == ".json" || f.Extension == ".xml";
    }

    public static Encoding GetFileEncoding(FileInfo f)
    {
      return f.Extension == ".xml" ? Encoding.UTF8 :
          f.Extension == ".json" ? Encoding.ASCII : 
          new UnicodeEncoding(false, false);
    }

    public static string FormatFileContentsImpl(FileInfo f, string contents, bool pretty)
    {
      if (f.Extension == ".xml" ) {
        var xml = XDocument.Parse(contents).ToString(pretty ? SaveOptions.None : SaveOptions.DisableFormatting);
        return pretty ? xml : "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + xml;
      }
      return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(contents), pretty ? Formatting.Indented : Formatting.None);
    }
  }
}