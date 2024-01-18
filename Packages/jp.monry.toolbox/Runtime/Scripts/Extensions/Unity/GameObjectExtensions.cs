using UnityEngine;

namespace Monry.Toolbox.Extensions.Unity;

public static class GameObjectExtensions
{
    public static TComponent GetOrAddComponent<TComponent>(this GameObject gameObject) where TComponent : Component
    {
        return gameObject.TryGetComponent<TComponent>(out var component) ? component : gameObject.AddComponent<TComponent>();
    }
}
