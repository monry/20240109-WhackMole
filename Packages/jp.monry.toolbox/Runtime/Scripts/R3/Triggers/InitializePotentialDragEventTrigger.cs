using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class InitializePotentialDragEventTrigger : PointerEventTriggerBase, IInitializePotentialDragHandler
{
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        OnPointerEvent.Invoke(eventData);
    }
}
