using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;

namespace Poseidon.Client.Pages.Trade
{
    public class TradeBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected List<TradeInputModel> TradeModels { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Navigation.BaseUri)
            };

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");

                try
                {
                    TradeModels = 
                    await httpClient.GetJsonAsync<List<TradeInputModel>>("https://localhost:5001/api/Trade");
                }
                catch (Exception)
                {
                    TradeModels = new List<TradeInputModel>();
                }
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected void Edit(int id)
        {
            Navigation.NavigateTo($"/Trade/edit/{id}");
        }

        protected void Delete(int id)
        {
            Navigation.NavigateTo($"/Trade/delete/{id}");
        }
    }
}