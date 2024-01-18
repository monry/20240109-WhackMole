using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerEnterEventTrigger : PointerEventTriggerBase, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
