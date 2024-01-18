using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace Monry.Toolbox.Editor.Internal.CsprojModifier.Features;

[UsedImplicitly]
public class AddNullableFeature : ICsprojModifierFeature
{
    public int Priority => 0;

    public bool ShouldModify(string path, string content) =>
        true;

    public string OnGeneratedCSProject(string path, string content)
    {
        var doc = XDocument.Parse(content);
        if (doc.Root == null)
        {
            return content;
        }
        var defaultNamespace = doc.Root.GetDefaultNamespace();
        var firstPropertyGroup = doc.Root.Elements().First(x => x.Name.LocalName == "PropertyGroup");
        if (firstPropertyGroup.Elements().All(x => x.Name.LocalName != "Nullable"))
        {
            firstPropertyGroup.Add(new XElement(defaultNamespace + "Nullable", "enable"));
        }
        content = doc.ToString();
        return content;
    }
}
