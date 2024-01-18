using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class PointerExitEventTrigger : PointerEventTriggerBase, IPointerExitHandler
{
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
