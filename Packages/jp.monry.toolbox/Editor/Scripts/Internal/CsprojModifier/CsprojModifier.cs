using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Monry.Toolbox.Editor.Internal.CsprojModifier;

public class CsprojModifier : AssetPostprocessor
{
    private static IEnumerable<ICsprojModifierFeature> Features { get; set; }

    static CsprojModifier()
    {
        Features = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x =>
                x.GetTypes()
                    .Where(y =>
                        typeof(ICsprojModifierFeature).IsAssignableFrom(y) &&
                        !y.IsInterface &&
                        !y.IsAbstract
                    )
            )
            .Select(x => (ICsprojModifierFeature)Activator.CreateInstance(x))
            .OrderByDescending(x => x.Priority)
            .ToArray();
    }

    public static string OnGeneratedCSProject(string path, string content) =>
        Features
            .Where(x => x.ShouldModify(path, content))
            .Aggregate(
                content,
                (current, feature) => feature.OnGeneratedCSProject(path, current)
            );
}
