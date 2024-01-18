using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerMoveEventTrigger : PointerEventTriggerBase, IPointerMoveHandler
{
    public void OnPointerMove(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
