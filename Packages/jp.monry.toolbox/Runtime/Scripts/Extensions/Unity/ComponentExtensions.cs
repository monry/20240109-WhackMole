using UnityEngine;

namespace Monry.Toolbox.Extensions.Unity;

public static class ComponentExtensions
{
    public static TComponent GetOrAddComponent<TComponent>(this Component self) where TComponent : Component
    {
        return self.gameObject.TryGetComponent<TComponent>(out var component) ? component : self.gameObject.AddComponent<TComponent>();
    }
}
