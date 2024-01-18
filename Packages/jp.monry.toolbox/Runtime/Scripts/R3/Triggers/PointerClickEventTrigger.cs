using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerClickEventTrigger : PointerEventTriggerBase, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
