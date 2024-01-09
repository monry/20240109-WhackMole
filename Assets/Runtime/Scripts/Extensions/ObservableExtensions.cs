using System;
using Monry.WhackMole.Extensions.VitalRouter;
using R3;
using VitalRouter;

namespace Monry.WhackMole.Extensions;

public static class ObservableExtensions
{
    public static IDisposable BindCommand<TSource, TCommand>(this Observable<TSource> observable, ICommandPublishable<TCommand> commandPublishable)
        where TCommand : ICommand, new()
    {
        return observable.Subscribe(_ => commandPublishable.CommandPublisher.Enqueue(new TCommand()));
    }

    public static IDisposable BindCommand<TSource, TCommand>(this Observable<TSource> observable, ICommandPublishable<TCommand> commandPublishable, TCommand command)
        where TCommand : ICommand, new()
    {
        return observable.Subscribe(_ => commandPublishable.CommandPublisher.Enqueue(command));
    }

    public static IDisposable BindCommand<TSource, TCommand>(this Observable<TSource> observable, ICommandPublishable<TCommand> commandPublishable, Func<TCommand> commandFactory)
        where TCommand : ICommand, new()
    {
        return observable.Subscribe(_ => commandPublishable.CommandPublisher.Enqueue(commandFactory.Invoke()));
    }

    public static IDisposable BindCommand<TSource, TCommand>(this Observable<TSource> observable, ICommandPublishable<TCommand> commandPublishable, Func<TSource, TCommand> commandFactory)
        where TCommand : ICommand, new()
    {
        return observable.Subscribe(x => commandPublishable.CommandPublisher.Enqueue(commandFactory.Invoke(x)));
    }
}
