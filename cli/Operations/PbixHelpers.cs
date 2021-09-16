using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace cli.Operations
{
  public static class PbixHelpers
  {
    public static bool IsValidSourceFile(FileInfo f)
    {
      if (Constants.BINARIES.Contains(f.Name) || f.Name == Constants.GITIGNORE || f.Directory?.Name == "data") return false;
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

    public static string ConvertJsonToYaml(string contents) { 
      var json = ConvertJTokenToObject(JsonConvert.DeserializeObject<JToken>(contents));
      return new SerializerBuilder().Build().Serialize(json);
    }

    static object ConvertJTokenToObject(JToken token) {
      object GetFormattedValue(object v) =>
        v is not string str || !str.StartsWith('{') ? v
            : JsonConvert.SerializeObject(JsonConvert.DeserializeObject(str), Formatting.Indented).Replace("\r\n", "\n");
      
      return token switch {
          JValue value => GetFormattedValue(value.Value),
          JArray => token.AsEnumerable().Select(ConvertJTokenToObject).ToList(),
          JObject => token.AsEnumerable()
              .Cast<JProperty>()
              .ToDictionary(x => x.Name, x => ConvertJTokenToObject(x.Value)),
          _ => throw new InvalidOperationException("Unexpected token: " + token)
      };
    }
  }
}