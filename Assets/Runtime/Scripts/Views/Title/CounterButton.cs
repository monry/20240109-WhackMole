using Microsoft.Extensions.Logging;
using Monry.Toolbox.Attributes;
using Monry.Toolbox.R3.Extensions;
using Monry.WhackMole.Commands;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VitalRouter;
using ILogger = Microsoft.Extensions.Logging.ILogger;

// ReSharper disable once ArrangeNamespaceBody
namespace Monry.WhackMole.Views.Title
{
    [Routes]
    [CommandPublishable]
    [ConfigureComponent(nameof(_button))]
    public partial class CounterButton : MonoBehaviour
    {
        [SerializeField] private Button _button = default!;
        [Inject] private ILogger _logger = default!;
        private int _counter;

        private void Start()
        {
            _button.OnClickAsObservable()
                .BindCommandPublisher(this)
                .SubscribeToPublishWithFactory(CommandFactory)
                .AddTo(this);
        }

        public void On(ButtonClickedCommand buttonClickedCommand)
        {
            _logger.LogInformation("Button Clicked (inner): {Counter}", buttonClickedCommand.Counter);
        }

        private ButtonClickedCommand CommandFactory() => new(++_counter);
    }
}
