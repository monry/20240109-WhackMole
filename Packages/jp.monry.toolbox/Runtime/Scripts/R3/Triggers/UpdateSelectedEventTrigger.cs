using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class UpdateSelectedEventTrigger : BaseEventTriggerBase, IUpdateSelectedHandler
{
    public void OnUpdateSelected(BaseEventData eventData)
    {
        OnBaseEvent.Invoke(eventData);
    }
}
