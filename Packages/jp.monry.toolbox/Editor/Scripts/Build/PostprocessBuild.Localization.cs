#if USE_LOCALIZATION
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Monry.Toolbox.Editor.Internal.Xcode;
using Newtonsoft.Json;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Monry.Toolbox.Editor.Build;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public partial class PostprocessBuild
{
    private static TableReference TableReference => "Application";
    private static Dictionary<LocalizationEntryKind, TableEntryReference> TableEntryReferences { get; } = new()
    {
        [LocalizationEntryKind.AppName]                = "AppName",
        [LocalizationEntryKind.AppNameShort]           = "AppNameShort",
        [LocalizationEntryKind.CameraUsageDescription] = "CameraUsageDescription",
    };
    private static Dictionary<LocalizationEntryKind, string> InfoPlistKeys { get; } = new()
    {
        [LocalizationEntryKind.AppName]                = "CFBundleDisplayName",
        [LocalizationEntryKind.AppNameShort]           = "CFBundleName",
        [LocalizationEntryKind.CameraUsageDescription] = "NSCameraUsageDescription",
    };

    private XCStringsData? _xcstrings;
    private XCStringsData XcStrings => _xcstrings ??= new XCStringsData();

    private static Type PBXProjectType { get; } = typeof(PBXProject);
    private static Type PBXProjectDataType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBXProjectData, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXFileReferenceDataType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXFileReferenceData, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXGroupDataType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXGroupData, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXKnownSectionBaseType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.KnownSectionBase`1, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXKnownSectionBaseType_PBXFileReferenceData { get; } = PBXKnownSectionBaseType.MakeGenericType(PBXFileReferenceDataType);
    private static Type PBXKnownSectionBaseType_PBXGroupData { get; } = PBXKnownSectionBaseType.MakeGenericType(PBXGroupDataType);
    private static Type PBXGUIDType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXGUID, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type GUIDListType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.GUIDList, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;

    private static Type LocalizationSettingsType { get; } = typeof(LocalizationSettings);

    private void ProcessLocalization()
    {
        PrepareProcessLocalization();
        // ビルド中は LocalizationSettings が使えないので LocalizationEditorSettings からもらう
        // https://forum.unity.com/threads/localization-in-postprocessbuild-app-tracking-transparency.1280444/#post-8122055
        var locales = LocalizationEditorSettings.GetLocales();
        foreach (var locale in locales)
        {
            ProcessLocalization(locale);
        }
        SaveXcstrings();
    }

    private void PrepareProcessLocalization()
    {
        // 都度クリアしないと English が多重追加されてしまう
        PBXProject.ClearKnownRegions();
        // ゴリッゴリの Reflection で PBXProject にローカライズ情報を追加する
        // AddInfoPlistVariantGroup();
        SetupXcstrings();

        PlistDocument.root.SetBoolean("LSHasLocalizedDisplayName", true);
    }

    private void SetupXcstrings()
    {
        // LocalizationSettings.ProjectLocale だと読み込みのタイミング的な問題か何かで null になるので Reflection でフィールドの値を取得する
        XcStrings.SourceLanguage = ((Locale)LocalizationSettingsType
            .GetField(
                "m_ProjectLocale",
                BindingFlags.Instance | BindingFlags.NonPublic
            )!
            .GetValue(LocalizationSettings.Instance))
            .Identifier
            .Code;
        foreach (var key in InfoPlistKeys.Values)
        {
            XcStrings.Strings[key] = new XCStringsData.StringData
            {
                ExtractionState = ExtractionState.Manual,
            };
        }
    }

    private void SaveXcstrings()
    {
        File.WriteAllText(
            Path.Combine(BuildReport.summary.outputPath, Application.productName, "InfoPlist.xcstrings"),
            JsonConvert.SerializeObject(XcStrings, XCStringsData.DefaultSerializerSettings)
        );
        // ゴリッゴリの Reflection で PBXProject に InfoPlist.xcstrings の情報を追加する
        AddXcstringsFileReference();
    }

    private void ProcessLocalization(Locale locale)
    {
        // まずは PBXProject にローカライズ情報を追加する
        PBXProject.AddKnownRegion(locale.Identifier.Code);

        foreach (var localizationEntryKind in InfoPlistKeys.Keys)
        {
            var infoPlistKey = InfoPlistKeys[localizationEntryKind];
            var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(
                TableReference,
                TableEntryReferences[localizationEntryKind],
                locale
            );
            XcStrings.Strings[infoPlistKey].Localizations[locale.Identifier.Code] = new XCStringsData.StringData.LocalizationData
            {
                StringUnit = new XCStringsData.StringData.LocalizationData.StringUnitData
                {
                    State = LocalizationState.Translated,
                    Value = localizedString,
                },
            };
        }
    }

    private void AddXcstringsFileReference()
    {
        var fileRefs = PBXProjectDataType
            .GetField("fileRefs", PredefinedBindingFlags.InternalInstance)!
            .GetValue(
                PBXProjectType
                    .GetField("m_Data", PredefinedBindingFlags.InternalInstance)!
                    .GetValue(PBXProject)
            );
        var fileRefObjects = (IEnumerable)PBXKnownSectionBaseType_PBXFileReferenceData
            .GetMethod("GetObjects", PredefinedBindingFlags.PublicInstance)!
            .Invoke(
                fileRefs,
                null
            );
        // ReSharper disable once LoopCanBeConvertedToQuery
        // 既に登録済の場合は何もしない
        foreach (var fileRefObject in fileRefObjects)
        {
            var name = PBXFileReferenceDataType
                .GetField("name", PredefinedBindingFlags.PublicInstance)!
                .GetValue(fileRefObject);
            if ((string)name != "InfoPlist.xcstrings")
            {
                continue;
            }
            return;
        }

        // InfoPlist.xcstrings の PBXFileReference を作成する
        var pbxFileReferenceData = Activator.CreateInstance(PBXFileReferenceDataType)!;
        var guid = GenerateGUID("PBXFileReference", "InfoPlist.xcstrings");
        PBXFileReferenceDataType
            .GetField("guid", PredefinedBindingFlags.PublicInstance)!
            .SetValue(pbxFileReferenceData, guid);
        PBXFileReferenceDataType
            .GetField("name", PredefinedBindingFlags.PublicInstance)!
            .SetValue(pbxFileReferenceData, "InfoPlist.xcstrings");
        PBXFileReferenceDataType
            .GetProperty("path", PredefinedBindingFlags.PublicInstance)!
            .SetValue(pbxFileReferenceData, "InfoPlist.xcstrings");
        PBXFileReferenceDataType
            .GetMethod("SetPropertyString", PredefinedBindingFlags.InternalInstance)!
            .Invoke(pbxFileReferenceData, new object[] { "isa", "PBXFileReference" });
        PBXFileReferenceDataType
            .GetMethod("SetPropertyString", PredefinedBindingFlags.InternalInstance)!
            .Invoke(pbxFileReferenceData, new object[] { "fileEncoding", "4" });
        PBXFileReferenceDataType
            .GetField("m_LastKnownFileType", PredefinedBindingFlags.InternalInstance)!
            .SetValue(pbxFileReferenceData, "text.json.xcstrings");
        PBXFileReferenceDataType
            .GetField("tree", PredefinedBindingFlags.PublicInstance)!
            .SetValue(pbxFileReferenceData, PBXSourceTree.Group);

        // PBXFileReference を PBXProject に追加する
        PBXKnownSectionBaseType_PBXFileReferenceData
            .GetMethod("AddEntry", PredefinedBindingFlags.PublicInstance)!
            .Invoke(
                fileRefs,
                new[] { pbxFileReferenceData }
            );
        var groupObjects = (IEnumerable)PBXKnownSectionBaseType_PBXGroupData
            .GetMethod("GetObjects", PredefinedBindingFlags.PublicInstance)!
            .Invoke(
                PBXProjectDataType
                    .GetField("groups", PredefinedBindingFlags.InternalInstance)!
                    .GetValue(
                        PBXProjectType
                            .GetField("m_Data", PredefinedBindingFlags.InternalInstance)!
                            .GetValue(PBXProject)
                    ),
                null
            );
        object? targetGroupObject = default;
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var groupObject in groupObjects)
        {
            var name = PBXGroupDataType
                .GetField("name", PredefinedBindingFlags.PublicInstance)!
                .GetValue(groupObject);
            if ((string)name != Application.productName)
            {
                continue;
            }
            targetGroupObject = groupObject;
            break;
        }

        // PBXFileReference を Application.productName な Group の子要素に追加する
        GUIDListType
            .GetMethod("AddGUID", PredefinedBindingFlags.PublicInstance)!
            .Invoke(
                PBXGroupDataType
                    .GetField("children", PredefinedBindingFlags.PublicInstance)!
                    .GetValue(
                        targetGroupObject
                        // PBXProjectDataType
                        //     .GetMethod("GroupsGetByName", PredefinedBindingFlags.InternalInstance)!
                        //     .Invoke(
                        //         PBXProjectType
                        //             .GetField("m_Data", PredefinedBindingFlags.InternalInstance)!
                        //             .GetValue(PBXProject),
                        //         new object[] { Application.productName }
                        //     )
                    ),
                new[] { guid }
            );

        // InfoPlist.xcstrings の PBXBuildFile を作成する
        // var pbxBuildFileData = Activator.CreateInstance(PBXBuildFileType)!;
        PBXProject.AddFileToBuild(PBXProject.GetUnityMainTargetGuid(), guid as string);
    }

    private static object GenerateGUID(string typeName, string path)
    {
        return PBXGUIDType
            .GetMethod("Generate", PredefinedBindingFlags.PublicStatic)!
            .Invoke(null, new object[] { $"{typeName} {path}" });
    }

    private readonly struct PredefinedBindingFlags
    {
        public const BindingFlags PublicInstance   = BindingFlags.Public    | BindingFlags.Instance;
        public const BindingFlags InternalInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public const BindingFlags PublicStatic     = BindingFlags.Public    | BindingFlags.Static;
    }

    private enum LocalizationEntryKind
    {
        AppName,
        AppNameShort,
        CameraUsageDescription,
    }
}
#endif
