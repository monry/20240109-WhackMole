using System;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Monry.Toolbox.VitalRouter;

public interface ICommandPublishable
{
    ICommandPublisher CommandPublisher { get; }
    ICommandPublishable<TCommand> As<TCommand>() where TCommand : ICommand =>
        (ICommandPublishable<TCommand>)this;
}

public interface ICommandPublishable<TCommand> : ICommandPublishable where TCommand : ICommand
{

    UniTask PublishAsync() => PublishAsync(CreateCommand());
    UniTask PublishAsync(TCommand command) => CommandPublisher.PublishAsync(command);

    TCommand CreateCommand() => throw new NotImplementedException();
}
