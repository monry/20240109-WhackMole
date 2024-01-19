using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        Converters =
        {
            new IgnoreObsoleteConverter(),
        },
    };

    private class IgnoreObsoleteConverter : JsonConverter<BuildReport>
    {
        public override BuildReport Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BuildReport value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("summary");
            JsonSerializer.Serialize(writer, value.summary, options);
            writer.WritePropertyName("steps");
            JsonSerializer.Serialize(writer, value.steps, options);
            // 以下のプロパティが返す型の中にも NoSupportedException を throw する輩がいるので、一旦無視する
            // writer.WritePropertyName("strippingInfo");
            // JsonSerializer.Serialize(writer, value.strippingInfo, options);
            // writer.WritePropertyName("packedAssets");
            // JsonSerializer.Serialize(writer, value.packedAssets, options);
            // writer.WritePropertyName("scenesUsingAssets");
            // JsonSerializer.Serialize(writer, value.scenesUsingAssets, options);
            writer.WriteEndObject();
        }
    }

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
        // 開発ビルド関連
        EditorUserBuildSettings.development = BuildContext.Development;
        EditorUserBuildSettings.allowDebugging = BuildContext.AllowDebugging;
        EditorUserBuildSettings.connectProfiler = BuildContext.ConnectWithProfiler;

        // iOS 関連
        EditorUserBuildSettings.iOSXcodeBuildConfig = EditorUserBuildSettings.development ? XcodeBuildConfig.Debug : XcodeBuildConfig.Release;

        // Android 関連
        EditorUserBuildSettings.buildAppBundle = BuildContext.BuildAndroidAppBundle;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = BuildContext.ExportAsGoogleAndroidProject;
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.androidBuildType = EditorUserBuildSettings.development ? AndroidBuildType.Debug : AndroidBuildType.Release;

        // macOS 関連
        EditorUserBuildSettings.macOSXcodeBuildConfig = EditorUserBuildSettings.development ? XcodeBuildConfig.Debug : XcodeBuildConfig.Release;
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
            $"{Application.productName}{ResolveExtension()}"
        );

    private static string ResolveExtension() =>
        EditorUserBuildSettings.activeBuildTarget switch
        {
            BuildTarget.StandaloneOSX when !UserBuildSettings.createXcodeProject
                => ".app",
            BuildTarget.Android when !EditorUserBuildSettings.exportAsGoogleAndroidProject
                => EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk",
            _
                => string.Empty,
        };
}
