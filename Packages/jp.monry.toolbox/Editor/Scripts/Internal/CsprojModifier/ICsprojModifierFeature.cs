namespace Monry.Toolbox.Editor.Internal.CsprojModifier;

public interface ICsprojModifierFeature
{
    int Priority { get; }

    bool ShouldModify(string path, string content);

    string OnGeneratedCSProject(string path, string content);
}
