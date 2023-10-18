using Caliburn.Micro;
using RMWPFUserInterface.EventModels;
using System.Threading;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly IEventAggregator _events;
        private readonly SimpleContainer _container;
        private readonly SalesViewModel _salesViewModel;

        public ShellViewModel(IEventAggregator events, SimpleContainer container, SalesViewModel salesViewModel)
        {
            _events = events;
            _container = container;
            _salesViewModel = salesViewModel;

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(_container.GetInstance<LoginViewModel>());
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesViewModel);
        }
    }
}
