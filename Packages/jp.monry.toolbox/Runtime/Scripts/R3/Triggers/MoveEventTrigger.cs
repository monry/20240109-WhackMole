using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class MoveEventTrigger : MonoBehaviour, IMoveHandler
{
    public UnityEvent<AxisEventData> OnMoveEvent { get; } = new();

    public void OnMove(AxisEventData eventData)
    {
        OnMoveEvent.Invoke(eventData);
    }
}
