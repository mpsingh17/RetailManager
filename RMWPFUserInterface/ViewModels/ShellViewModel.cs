using Caliburn.Micro;

namespace RMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly LoginViewModel _loginVM;

        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            ActivateItemAsync(_loginVM);
        }
    }
}
