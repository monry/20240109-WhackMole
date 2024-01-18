using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class EndDragEventTrigger : PointerEventTriggerBase, IEndDragHandler
{
    public void OnEndDrag(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
