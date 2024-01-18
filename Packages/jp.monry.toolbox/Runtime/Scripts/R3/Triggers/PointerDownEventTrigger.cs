using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerDownEventTrigger : PointerEventTriggerBase, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
