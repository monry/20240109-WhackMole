using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public abstract class PointerEventTriggerBase : MonoBehaviour
{
    public UnityEvent<PointerEventData> OnPointerEvent { get; } = new();
}
