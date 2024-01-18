using System.Diagnostics.CodeAnalysis;

namespace Monry.Toolbox.Editor.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public readonly struct MenuPriorities
{
    private const int BasePriority = 1000;
    public const int Build_SwitchEnvironment   = BasePriority              + 1;
    public const int Build_SwitchExportSetting = Build_SwitchEnvironment   + 1;
    public const int Build_Player_BuildOnly    = Build_SwitchExportSetting + 1;
    public const int Build_Player_BuildAndRun  = Build_Player_BuildOnly    + 1;
}
