using System;

namespace Monry.Toolbox.Editor.Build;

public static class BuildContext
{
    /// <summary>
    /// Development Build とするかどうか
    /// <remarks>Default: true</remarks>
    /// </summary>
    public static bool Development { get; }                  = Environment.GetEnvironmentVariable("BUILD_DEVELOPMENT") != "false";
    /// <summary>
    /// ConnectWithProfiler を有効にするかどうか
    /// <remarks>Default: true</remarks>
    /// </summary>
    public static bool ConnectWithProfiler { get; }          = Environment.GetEnvironmentVariable("BUILD_CONNECT_WITH_PROFILER") != "false";
    /// <summary>
    /// AllowDebugging を有効にするかどうか
    /// <remarks>Default: false</remarks>
    /// </summary>
    public static bool AllowDebugging { get; }               = Environment.GetEnvironmentVariable("BUILD_ALLOW_DEBUGGING") == "true";
    /// <summary>
    /// Apple Developer Team ID を上書きする場合の値
    /// <remarks>Default: (empty)</remarks>
    /// </summary>
    public static string AppleDeveloperTeamId { get; }       = Environment.GetEnvironmentVariable("APPLE_DEVELOPER_TEAM_ID") ?? string.Empty;
    /// <summary>
    /// Android App Bundle としてビルドするかどうか
    /// <remarks>Default: false</remarks>
    /// </summary>
    public static bool BuildAndroidAppBundle { get; }     = Environment.GetEnvironmentVariable("BUILD_ANDROID_APP_BUNDLE") == "true";
    /// <summary>
    /// Android Project としてエクスポートするかどうか
    /// <remarks>Default: false</remarks>
    /// </summary>
    public static bool ExportAsGoogleAndroidProject { get; } = Environment.GetEnvironmentVariable("BUILD_EXPORT_GOOGLE_ANDROID_PROJECT") == "true";

    // NOTE: 以下の設定を利用する場合は https://github.com/baba-s/UniAndroidExternalTools/blob/master/Editor/AndroidExternalTools.cs を参考にすると良さそう
    /// <summary>
    /// Android SDK Path を上書きする場合の値
    /// <remarks>Default: (empty)</remarks>
    /// </summary>
    public static string AndroidSdkPath { get; }             = Environment.GetEnvironmentVariable("BUILD_ANDROID_SDK_PATH") ?? string.Empty;
    /// <summary>
    /// Android NDK Path を上書きする場合の値
    /// <remarks>Default: (empty)</remarks>
    /// </summary>
    public static string AndroidNdkPath { get; }             = Environment.GetEnvironmentVariable("BUILD_ANDROID_NDK_PATH") ?? string.Empty;
    /// <summary>
    /// JDK Path を上書きする場合の値
    /// <remarks>Default: (empty)</remarks>
    /// </summary>
    public static string JdkPath { get; }                    = Environment.GetEnvironmentVariable("BUILD_JDK_PATH") ?? string.Empty;
}
