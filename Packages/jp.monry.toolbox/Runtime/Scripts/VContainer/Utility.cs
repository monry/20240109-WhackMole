using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VContainer.Unity;

namespace Monry.Toolbox.VContainer;

public static class VContainerUtility
{
    public static bool TryAddAutoInjectGameObjects(GameObject gameObject)
    {
        var lifetimeScope = gameObject.scene.GetRootGameObjects()
            .Select(x => x.GetComponent<LifetimeScope>())
            .FirstOrDefault(x => x != default);
        if (lifetimeScope == default)
        {
            return false;
        }
        var autoInjectGameObjects = lifetimeScope.GetType()
            .GetField("autoInjectGameObjects", BindingFlags.Instance | BindingFlags.NonPublic)?
            .GetValue(lifetimeScope) as List<GameObject> ?? new List<GameObject>();
        if (autoInjectGameObjects.Contains(gameObject))
        {
            return false;
        }
        autoInjectGameObjects.Add(gameObject);
        return true;
    }
}
