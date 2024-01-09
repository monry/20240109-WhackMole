using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

#if !UNITY_2023_1_OR_NEWER
namespace UnityEditor.iOS.Xcode;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class PBXProjectExtensions
{
    private static Type PBXProjectType { get; } = typeof(PBXProject);
    private static Type PBXProjectDataType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBXProjectData, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXProjectSectionType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXProjectSection, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXObjectDataType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXObjectData, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXElementType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXElement, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXElementArrayType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXElementArray, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;
    private static Type PBXElementDictType { get; } = Type.GetType("UnityEditor.iOS.Xcode.PBX.PBXElementDict, UnityEditor.iOS.Extensions.Xcode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")!;

    private static MethodInfo PBXElement_AsArrayMethod { get; } = PBXElementType.GetMethod("AsArray", PredefinedBindingFlags.PublicInstance)!;
    private static MethodInfo PBXElementArray_AddStringMethod { get; } = PBXElementArrayType.GetMethod("AddString", PredefinedBindingFlags.PublicInstance)!;
    private static PropertyInfo PBXElementDict_Indexer { get; } = PBXElementDictType.GetProperties(PredefinedBindingFlags.PublicInstance).First(x => x.GetIndexParameters().Select(y => y.ParameterType).SequenceEqual(new [] { typeof(string) }));
    private static MethodInfo PBXElementDict_CreateArrayMethod { get; } = PBXElementDictType.GetMethod("CreateArray", PredefinedBindingFlags.PublicInstance)!;
    private static MethodInfo PBXElementDict_RemoveMethod { get; } = PBXElementDictType.GetMethod("Remove", PredefinedBindingFlags.PublicInstance)!;

    public static void ClearKnownRegions(this PBXProject pbxProject)
    {
        var properties = pbxProject.GetPBXProjectObjectDataProperties();
        PBXElementDict_RemoveMethod.Invoke(
            properties,
            new object[] { "knownRegions" }
        );
        PBXElementDict_CreateArrayMethod.Invoke(
            properties,
            new object[] { "knownRegions" }
        );
    }

    public static void AddKnownRegion(this PBXProject pbxProject, string region)
    {
        var properties = pbxProject.GetPBXProjectObjectDataProperties();
        var knownRegionsItem = PBXElementDict_Indexer
            .GetValue(
                properties,
                new object[] { "knownRegions" }
            );
        var knownRegionsArray = PBXElement_AsArrayMethod.Invoke(
            knownRegionsItem,
            null
        );
        PBXElementArray_AddStringMethod.Invoke(
            knownRegionsArray,
            new object[] { region }
        );
    }

    private static object GetPBXProjectObjectDataProperties(this PBXProject pbxProject)
    {
        var project = PBXProjectType
            .GetField("m_Data", PredefinedBindingFlags.InternalInstance)!
            .GetValue(pbxProject)!;
        var projectSection = PBXProjectDataType
            .GetField("project", PredefinedBindingFlags.PublicInstance)!
            .GetValue(project)!;
        var projectObjectData = PBXProjectSectionType
            .GetProperty("project", PredefinedBindingFlags.PublicInstance)!
            .GetValue(projectSection)!;
        var properties = PBXObjectDataType
            .GetField("m_Properties", PredefinedBindingFlags.InternalInstance)!
            .GetValue(projectObjectData)!;
        return properties;
    }

    private readonly struct PredefinedBindingFlags
    {
        public const BindingFlags PublicInstance   = BindingFlags.Public    | BindingFlags.Instance;
        public const BindingFlags InternalInstance = BindingFlags.NonPublic | BindingFlags.Instance;
    }
}
#endif
