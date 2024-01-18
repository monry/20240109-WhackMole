using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class ScrollEventTrigger : PointerEventTriggerBase, IScrollHandler
{
    public void OnScroll(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
