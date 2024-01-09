using Microsoft.Extensions.Logging;
using Monry.WhackMole.Presenters;
using Monry.WhackMole.Views.Title;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;
using ZLogger.Unity;

namespace Monry.WhackMole.LifetimeScopes;

public class TitleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ILoggerFactory>(
            _ =>
                LoggerFactory.Create(
                    logging =>
                    {
                        logging.AddZLoggerUnityDebug();
                    }),
            Lifetime.Singleton
        );
        builder.RegisterVitalRouter(routingBuilder =>
        {
            routingBuilder.Map<TitlePresenter>();
        });
        builder.Register<CounterButton.Processor>(Lifetime.Singleton);
    }
}
