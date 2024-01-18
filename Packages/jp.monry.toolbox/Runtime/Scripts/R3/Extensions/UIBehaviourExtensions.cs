using Monry.Toolbox.Extensions.Unity;
using Monry.Toolbox.R3.Triggers;
using R3;
using UnityEngine.EventSystems;

namespace Monry.Toolbox.R3.Extensions;

public static class UIBehaviourExtensions
{
    public static Observable<PointerEventData> OnBeginDragAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<BeginDragEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<BaseEventData> OnCancelAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<CancelEventTrigger>();
        return trigger.OnBaseEvent.AsObservable();
    }

    public static Observable<BaseEventData> OnDeselectAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<DeselectEventTrigger>();
        return trigger.OnBaseEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnDragAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<DragEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnDropAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<DropEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnEndDragAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<EndDragEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnInitializePotentialDragAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<InitializePotentialDragEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<AxisEventData> OnMoveAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<MoveEventTrigger>();
        return trigger.OnMoveEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerClickAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerClickEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerDownAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerDownEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerEnterAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerEnterEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerExitAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerExitEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerMoveAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerMoveEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnPointerUpAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<PointerUpEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<PointerEventData> OnScrollAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<ScrollEventTrigger>();
        return trigger.OnPointerEvent.AsObservable();
    }

    public static Observable<BaseEventData> OnSelectAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<SelectEventTrigger>();
        return trigger.OnBaseEvent.AsObservable();
    }

    public static Observable<BaseEventData> OnSubmitAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<SubmitEventTrigger>();
        return trigger.OnBaseEvent.AsObservable();
    }

    public static Observable<BaseEventData> OnUpdateSelectedAsObservable(this UIBehaviour uiBehaviour)
    {
        var trigger = uiBehaviour.gameObject.GetOrAddComponent<UpdateSelectedEventTrigger>();
        return trigger.OnBaseEvent.AsObservable();
    }
}
