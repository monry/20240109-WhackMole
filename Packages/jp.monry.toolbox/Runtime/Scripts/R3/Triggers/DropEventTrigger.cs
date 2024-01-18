using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class DropEventTrigger : PointerEventTriggerBase, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
