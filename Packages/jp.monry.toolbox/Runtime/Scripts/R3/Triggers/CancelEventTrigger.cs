using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class CancelEventTrigger : BaseEventTriggerBase, ICancelHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        OnBaseEvent.Invoke(eventData);
    }
}
