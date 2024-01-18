using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Triggers;

public sealed class SubmitEventTrigger : BaseEventTriggerBase, ISubmitHandler
{
    public void OnSubmit(BaseEventData eventData)
    {
        OnBaseEvent.Invoke(eventData);
    }
}
