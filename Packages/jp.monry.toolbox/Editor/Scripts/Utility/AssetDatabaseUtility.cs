using UnityEngine;

namespace Monry.Toolbox.Editor.Utility;

public static class AssetDatabaseUtility
{
    public static string GetAssetPathFromFullPath(string fullPath)
    {
        return fullPath.Replace(Application.dataPath, "Assets");
    }
}
