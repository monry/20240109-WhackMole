using Monry.Toolbox.VitalRouter;
using R3;

namespace Monry.Toolbox.R3.Extensions;

public static class ObservableExtensions
{
    public static CommandObservable<TSource> BindCommandPublisher<TSource>(this Observable<TSource> observable, ICommandPublishable commandPublishable)
    {
        return new CommandObservable<TSource>(observable, commandPublishable);
    }
}
