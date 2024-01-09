using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monry.WhackMole.Commands;
using VitalRouter;
using ZLogger;

namespace Monry.WhackMole.Presenters;

[Routes]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public partial class TitlePresenter
{
    private readonly ILogger<TitlePresenter> _logger;

    public TitlePresenter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TitlePresenter>();
    }

    public async UniTask On(ButtonClickedCommand buttonClickedCommand)
    {
        await UniTask.Delay(1000);
        _logger.Counter(buttonClickedCommand.Counter);
    }
}

public static partial class LogExtensions
{
    [ZLoggerMessage(LogLevel.Information, "Button Clicked: {Counter}")]
    public static partial void Counter(this ILogger<TitlePresenter> logger, int counter);
}
