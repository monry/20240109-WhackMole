using Monry.WhackMole.Commands;
using Monry.WhackMole.Extensions;
using Monry.WhackMole.Extensions.VitalRouter;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VitalRouter;

namespace Monry.WhackMole.Views.Title;

public class CounterButton : MonoBehaviour
{
    [SerializeField] private Button _button = default!;
    [Inject] private Processor _processor = default!;
    private int _counter = 0;

    private void Start()
    {
        _button.OnClickAsObservable()
            .BindCommand(_processor, () => new ButtonClickedCommand(++_counter))
            .AddTo(this);
    }

    public class Processor : ICommandPublishable<ButtonClickedCommand>
    {
        public ICommandPublisher CommandPublisher { get; }

        public Processor(ICommandPublisher commandPublisher)
        {
            CommandPublisher = commandPublisher;
        }
    }
}
