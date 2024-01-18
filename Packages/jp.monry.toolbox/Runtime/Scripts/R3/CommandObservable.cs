using System;
using Monry.Toolbox.VitalRouter;
using R3;
using VitalRouter;

namespace Monry.Toolbox.R3;

public class CommandObservable<TSource> : Observable<TSource>
{
    public CommandObservable(Observable<TSource> observable, ICommandPublishable commandPublishable)
    {
        Observable = observable;
        CommandPublishable = commandPublishable;
    }

    private Observable<TSource> Observable { get; }
    private ICommandPublishable CommandPublishable { get; }

    public IDisposable SubscribeToPublish<TCommand>()
        where TCommand : ICommand, new()
    {
        return Observable.Subscribe(_ => CommandPublishable.CommandPublisher.Enqueue(new TCommand()));
    }

    public IDisposable SubscribeToPublishWith<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        return Observable.Subscribe(_ => CommandPublishable.CommandPublisher.Enqueue(command));
    }

    public IDisposable SubscribeToPublishWithFactory<TCommand>(Func<TCommand> commandFactory)
        where TCommand : ICommand
    {
        return Observable.Subscribe(_ => CommandPublishable.CommandPublisher.Enqueue(commandFactory.Invoke()));
    }

    public IDisposable SubscribeToPublishWithFactory<TCommand>(Func<TSource, TCommand> commandFactory)
        where TCommand : ICommand
    {
        return Observable.Subscribe(x => CommandPublishable.CommandPublisher.Enqueue(commandFactory.Invoke(x)));
    }

    protected override IDisposable SubscribeCore(Observer<TSource> observer)
    {
        return Observable.Subscribe(observer);
    }
}
