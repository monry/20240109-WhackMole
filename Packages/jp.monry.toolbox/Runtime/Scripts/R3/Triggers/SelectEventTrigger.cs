using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class SelectEventTrigger : BaseEventTriggerBase, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        OnBaseEvent.Invoke(eventData);
    }
}
