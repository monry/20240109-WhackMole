using System;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEditor.OSXStandalone;
using UnityEngine;

namespace Monry.Toolbox.Editor.Build;

// ReSharper disable once PartialTypeWithSinglePart
public partial class PostprocessBuild : IPostprocessBuildWithReport
{
    public int callbackOrder { get; }

    private PBXProject? _pbxProject;
    private PlistDocument? _plistDocument;
    private BuildReport? _buildReport;
    private PBXProject PBXProject => _pbxProject ??= new PBXProject();
    private PlistDocument PlistDocument => _plistDocument ??= new PlistDocument();
    private BuildReport BuildReport
    {
        // ReSharper disable once UnusedMember.Local
        get => _buildReport != default ? _buildReport : throw new NullReferenceException();
        set => _buildReport = value;
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        // Create Xcode Project が無効な場合は処理しない
        if (!UserBuildSettings.createXcodeProject)
        {
            return;
        }

        BuildReport = report;
        PBXProject.ReadFromFile(Path.Combine(report.summary.outputPath, $"{Application.productName}.xcodeproj", "project.pbxproj"));
        PlistDocument.ReadFromFile(Path.Combine(report.summary.outputPath, Application.productName, "Info.plist"));

        PBXProject.WriteToFile(Path.Combine(report.summary.outputPath, $"{Application.productName}.xcodeproj", "project.pbxproj"));
        PlistDocument.WriteToFile(Path.Combine(report.summary.outputPath, Application.productName, "Info.plist"));
    }
}
