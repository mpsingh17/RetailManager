﻿using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMWPFUserInterface.EventModels;
using RMWPFUserInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _events;
        private string _userName;
        private string _password;
        private string _errorMessage;


        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

		#region Properties

		public string UserName
		{
			get { return _userName; }
			set 
			{ 
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
		}

		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public bool IsErrorVisible
		{
			get 
			{
				bool output = false;

				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}

				return output;
			}
		}


		public string ErrorMessage
        {
			get { return _errorMessage; }
			set 
			{ 
				_errorMessage = value; 
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}


		public bool CanLogIn
		{
			get
			{
				bool output = false;

				if (UserName?.Length > 0 && Password?.Length > 0)
					output = true;

				return output;
			}
		}

        #endregion


        public async Task LogIn()
		{
			try
			{
				ErrorMessage = string.Empty;
				var result = await _apiHelper.Authenticate(UserName, Password);

				// Capture more information about the logged in user.
				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

				await _events.PublishOnUIThreadAsync(new LogOnEvent());

			}
			catch (Exception ex)
			{

				ErrorMessage = ex.Message;
            }
        }


	}
}
