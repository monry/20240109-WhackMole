using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public abstract class BaseEventTriggerBase : MonoBehaviour
{
    public UnityEvent<BaseEventData> OnBaseEvent { get; } = new();
}
