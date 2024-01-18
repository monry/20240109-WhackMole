using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class BeginDragEventTrigger : PointerEventTriggerBase, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
