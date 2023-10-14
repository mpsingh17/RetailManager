﻿using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;
        private readonly ILoggedInUserModel _loggedInUser;

        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        private void InitializeClient()
        {
            apiClient = new HttpClient();

            string api = ConfigurationManager.AppSettings["api"];

            apiClient.BaseAddress = new Uri(api);

            apiClient
                .DefaultRequestHeaders
                .Accept
                .Clear();

            apiClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response
                        .Content
                        .ReadAsAsync<AuthenticatedUser>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            apiClient
                .DefaultRequestHeaders
                .Clear();

            apiClient
                .DefaultRequestHeaders
                .Accept
                .Clear();

            apiClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            apiClient
                .DefaultRequestHeaders
                .Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await apiClient.GetAsync("/api/User"))
            {
                if ( response.IsSuccessStatusCode )
                {
                    var result = await response
                        .Content?
                        .ReadAsAsync<LoggedInUserModel>();

                    _loggedInUser.Id = result.Id;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.CreatedDate = result.CreatedDate;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
