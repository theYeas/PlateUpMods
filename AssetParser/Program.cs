using AssetParser;
using AssetParser.Models;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

internal class Program
{
    /* 
     * Hacked together to generate a mapping of Assets
     *  - content to be copied into AssetId.cs and AssetGroups.cs
     */
    private static void Main(string[] args)
    {
        string path = args[0];
        if (!Directory.Exists(path))
        {
            Console.WriteLine("Invalid director.");
            return;
        }
        var deserializer = new DeserializerBuilder()
          .IgnoreUnmatchedProperties()
          .Build();

        var sourceFolder = Path.Join(path, "MonoScript");
        Dictionary<string, string> Sources = new Dictionary<string, string>();
        foreach(string d in Directory.GetDirectories(sourceFolder))
        {
            if (d.Contains("Kitchen"))
            {
                string[] sources = Directory.GetFiles(d, "*.cs.meta", SearchOption.AllDirectories);
                foreach (string s in sources)
                {
                    var meta = deserializer.Deserialize<Meta>(new StreamReader(s));
                    if (meta != null)
                    {
                        Sources.Add(meta.guid, Path.GetFileName(s).Split(".")[0]);
                    }
                }
            }
        }

        var assetFolder = Path.Join(path, "MonoBehaviour");
        string[] assets = Directory.GetFiles(assetFolder, "*.asset", SearchOption.AllDirectories);
        List<AssetMapEntry> Assets = new List<AssetMapEntry>();
        foreach(string a in assets)
        {

            var yaml = new YamlStream();
            yaml.Load(new StreamReader(a));

            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            var monobehavior = (YamlMappingNode)mapping.Children[0].Value;

            if (monobehavior.Children.Keys.Contains(new YamlScalarNode("ID")))
            {
                var id = monobehavior.Children[new YamlScalarNode("ID")];
                var name = monobehavior.Children[new YamlScalarNode("m_Name")];
                var m_script = (YamlMappingNode)monobehavior.Children[new YamlScalarNode("m_Script")];
                var guid = m_script.Children[new YamlScalarNode("guid")];
                string t = "Unknown";
                Sources.TryGetValue(guid.ToString(), out t);
                string named = safeName(name.ToString());
                string rootName = named;
                int count = 2;
                while (Assets.FindIndex(a => a.Name == named) > -1)
                {
                    named = rootName + "_" + count;
                    count++;
                }

                Assets.Add(new AssetMapEntry()
                {
                    ID = int.Parse(id.ToString()),
                    Name = named,
                    Type = t
                });
            }
        }
        string outputPath = Path.Combine(Path.GetTempPath() + "assets.txt");
        var file = File.Create(outputPath);
        StreamWriter streamWriter = new StreamWriter(file);
        streamWriter.WriteLine("public enum AssetId");
        streamWriter.WriteLine("{");
        foreach (var asset in Assets)
        {
            streamWriter.WriteLine("\t" + asset.Name + " = " + asset.ID + ",");
        }
        streamWriter.WriteLine("}");
        streamWriter.Flush();
        streamWriter.Close();

        outputPath = Path.Combine(Path.GetTempPath() + "assets2.txt");
        file = File.Create(outputPath);
        streamWriter = new StreamWriter(file);

        foreach (var type in Assets.Select(x => x.Type).Distinct())
        {
            streamWriter.WriteLine("public static readonly List<AssetId> " + type + " = new List<AssetId>() {");
            foreach (var asset in Assets.Where(a => a.Type == type))
            {
                streamWriter.WriteLine("\tAssetId." + asset.Name + ",");
            }
            streamWriter.WriteLine("};");
            streamWriter.Flush();
        }

        string safeName(string name)
        {
            int startNum = -1;
            if (int.TryParse(name.Substring(0,1), out startNum) && startNum > -1)
            {
                name = "_" + name;
            }
            return name.ToString().Replace(" ", "").Replace("-", "");
        }
    }
}