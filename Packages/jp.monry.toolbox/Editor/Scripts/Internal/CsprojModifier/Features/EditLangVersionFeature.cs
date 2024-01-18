using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Monry.Toolbox.Editor.Internal.CsprojModifier.Features;

[UsedImplicitly]
public class EditLangVersionFeature : ICsprojModifierFeature
{
    public int Priority => 0;

    public bool ShouldModify(string path, string content) =>
        true;

    public string OnGeneratedCSProject(string path, string content) =>
        Regex.Replace(
            content,
            "<PropertyGroup>\n    <LangVersion>([^<]+)</LangVersion>",
            "<PropertyGroup>\n    <LangVersion>11</LangVersion>"
        );
}
