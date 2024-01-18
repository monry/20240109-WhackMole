using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerUpEventTrigger : PointerEventTriggerBase, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
