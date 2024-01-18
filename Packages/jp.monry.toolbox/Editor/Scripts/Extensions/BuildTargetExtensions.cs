using UnityEditor;

namespace Monry.Toolbox.Editor.Extensions;

public static class BuildTargetExtensions
{
    public static string AsCanonicalName(this BuildTarget buildTarget)
    {
        return buildTarget switch
        {
            BuildTarget.StandaloneOSX       => "macOS",
            BuildTarget.StandaloneWindows   => "Windows",
            BuildTarget.StandaloneWindows64 => "Windows",
            BuildTarget.Android             => "Android",
            BuildTarget.iOS                 => "iOS",
            BuildTarget.WebGL               => "WebGL",
            _ => "Unknown",
        };
    }
}
