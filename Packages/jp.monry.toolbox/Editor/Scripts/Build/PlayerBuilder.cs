using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Monry.Toolbox.Editor.Extensions;
using Monry.Toolbox.Editor.Model;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.OSXStandalone;
using UnityEngine;

namespace Monry.Toolbox.Editor.Build;

public static class PlayerBuilder
{
    private static Dictionary<bool, string> BuildEnvironmentNames { get; } = new()
    {
        [true]  = "development",
        [false] = "production",
    };
    private static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        WriteIndented = true,
    };

    [MenuItem("Build/Player/Build", priority = MenuPriorities.Build_Player_BuildOnly)]
    public static void BuildOnly()
    {
        Build();
    }

    [MenuItem("Build/Player/Build and Run", priority = MenuPriorities.Build_Player_BuildAndRun)]
    public static void BuildAndRun()
    {
        var buildReport = Build();
        if (buildReport.summary.result != BuildResult.Succeeded)
        {
            return;
        }
        Run(buildReport.summary.outputPath);
    }

    private static BuildReport Build()
    {
        PrepareBuild();
        var buildReport = BuildPipeline.BuildPlayer(CreateBuildPlayerOptions());
        File.WriteAllText("build-report.json", JsonSerializer.Serialize(buildReport, JsonSerializerOptions));
        return buildReport;
    }

    private static void Run(string path)
    {
        if (UserBuildSettings.createXcodeProject)
        {
            var xcodeBuildProcess = new Process
            {
                StartInfo =
                {
                    FileName = "xcodebuild",
                    // Arguments = $"-project {path} -scheme Unity-iPhone -destination id={UserBuildSettings.runDeviceId}",
                    UseShellExecute = true,
                },
            };
            return;
        }
        var process = new Process
        {
            StartInfo =
            {
                FileName = path,
                UseShellExecute = true,
            },
        };
        process.Start();
    }

    private static void PrepareBuild()
    {
        EditorUserBuildSettings.macOSXcodeBuildConfig = EditorUserBuildSettings.development ? XcodeBuildConfig.Debug : XcodeBuildConfig.Release;
        EditorUserBuildSettings.iOSXcodeBuildConfig = EditorUserBuildSettings.development ? XcodeBuildConfig.Debug : XcodeBuildConfig.Release;
    }

    private static BuildPlayerOptions CreateBuildPlayerOptions()
    {
        var buildOptions = BuildOptions.None;
        if (EditorUserBuildSettings.development)
        {
            buildOptions |= BuildOptions.Development;
        }
        if (EditorUserBuildSettings.allowDebugging)
        {
            buildOptions |= BuildOptions.AllowDebugging;
        }
        if (EditorUserBuildSettings.connectProfiler)
        {
            buildOptions |= BuildOptions.ConnectWithProfiler;
        }
        return new BuildPlayerOptions
        {
            scenes = EditorBuildSettings.scenes
                .Where(x => x.enabled)
                .Select(x => x.path)
                .ToArray(),
            locationPathName = CreateBuildPath(),
            target = EditorUserBuildSettings.activeBuildTarget,
            options = buildOptions,
        };
    }

    private static string CreateBuildPath() =>
        Path.Combine(
            "Builds",
            BuildEnvironmentNames[EditorUserBuildSettings.development],
            EditorUserBuildSettings.activeBuildTarget.AsCanonicalName(),
            Application.productName,
            UserBuildSettings.createXcodeProject ? string.Empty : ".app"
        );
}
