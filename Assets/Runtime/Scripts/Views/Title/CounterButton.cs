using Monry.WhackMole.Commands;
using Monry.WhackMole.Extensions;
using Monry.WhackMole.Extensions.VitalRouter;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VitalRouter;

// ReSharper disable once ArrangeNamespaceBody
namespace Monry.WhackMole.Views.Title
{
    public class CounterButton : MonoBehaviour, ICommandPublishable<ButtonClickedCommand>
    {
        [SerializeField] private Button _button = default!;
        [Inject] public ICommandPublisher CommandPublisher { get; set; } = default!;
        private int _counter = 0;

        private void Start()
        {
            _button.OnClickAsObservable()
                .BindCommand(this, () => new ButtonClickedCommand(++_counter))
                .AddTo(this);
        }
    }
}
