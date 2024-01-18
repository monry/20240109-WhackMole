using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class DeselectEventTrigger : BaseEventTriggerBase, IDeselectHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        OnBaseEvent.Invoke(eventData);
    }
}
