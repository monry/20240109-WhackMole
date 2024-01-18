using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class DragEventTrigger : PointerEventTriggerBase, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
