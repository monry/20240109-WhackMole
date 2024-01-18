using Monry.Toolbox.Editor.Model;
using UnityEditor;

namespace Monry.Toolbox.Editor.Build;

[InitializeOnLoad]
public static class EnvironmentSwitcher
{
    private const string MenuPath = "Build/Development";

    static EnvironmentSwitcher()
    {
        EditorApplication.delayCall += Initialize;
    }

    private static void Initialize()
    {
        Menu.SetChecked(MenuPath, EditorUserBuildSettings.development);
        EditorApplication.delayCall -= Initialize;
    }

    [MenuItem(MenuPath, priority = MenuPriorities.Build_SwitchEnvironment)]
    public static void SwitchBuildEnvironment()
    {
        Menu.SetChecked(MenuPath, !Menu.GetChecked(MenuPath));
        var isDevelopment = Menu.GetChecked(MenuPath);
        EditorUserBuildSettings.development = isDevelopment;
        EditorUserBuildSettings.allowDebugging = isDevelopment;
        EditorUserBuildSettings.connectProfiler = isDevelopment;
    }
}
