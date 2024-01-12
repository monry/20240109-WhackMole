using Microsoft.Extensions.Logging;
using Monry.Toolbox.Attributes;
using Monry.Toolbox.R3.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using ILogger = Microsoft.Extensions.Logging.ILogger;

// ReSharper disable once ArrangeNamespaceBody
namespace Monry.WhackMole.Views.Title
{
    [ConfigureComponent(nameof(_image), ShouldAutoInject = true)]
    public partial class SampleImage : MonoBehaviour
    {
        [SerializeField] private Graphic _image = default!;
        [Inject] private ILogger _logger = default!;

        private void Start()
        {
            _image.OnPointerDownAsObservable()
                .Subscribe(x => _logger.LogInformation("{X}", x))
                .AddTo(this);
        }
    }
}
