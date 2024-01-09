using Cysharp.Threading.Tasks;
using VitalRouter;

namespace Monry.WhackMole.Extensions.VitalRouter;

public interface ICommandPublishable<in TCommand> where TCommand : ICommand, new()
{
    ICommandPublisher CommandPublisher { get; }

    UniTask PublishAsync() => PublishAsync(new TCommand());
    UniTask PublishAsync(TCommand command) => CommandPublisher.PublishAsync(command);
}
