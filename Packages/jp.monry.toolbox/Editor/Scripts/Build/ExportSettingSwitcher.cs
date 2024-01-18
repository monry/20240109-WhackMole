using Monry.Toolbox.Editor.Model;
using UnityEditor;
using UnityEditor.OSXStandalone;

namespace Monry.Toolbox.Editor.Build;

[InitializeOnLoad]
public static class ExportSettingSwitcher
{
    private const string MenuPath = "Build/Create Xcode Project";

    static ExportSettingSwitcher()
    {
        EditorApplication.delayCall += Initialize;
    }

    private static void Initialize()
    {
        Menu.SetChecked(MenuPath, UserBuildSettings.createXcodeProject);
        EditorApplication.delayCall -= Initialize;
    }

    [MenuItem(MenuPath, priority = MenuPriorities.Build_SwitchExportSetting)]
    public static void SwitchExportSetting()
    {
        Menu.SetChecked(MenuPath, !Menu.GetChecked(MenuPath));
        var isExportSetting = Menu.GetChecked(MenuPath);
        // Create Xcode Project の値を切り替える
        UserBuildSettings.createXcodeProject = isExportSetting;
    }
}
