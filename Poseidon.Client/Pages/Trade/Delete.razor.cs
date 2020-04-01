using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poseidon.Client.Pages.Trade
{
    public class DeleteBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        protected TradeInputModel TradeModel { get; set; }
        protected bool OperationSuccess { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Navigation.BaseUri);

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                TradeModel = await httpClient.GetJsonAsync<TradeInputModel>($"https://localhost:5001/api/Trade/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected void CancelDelete()
        {
            Navigation.NavigateTo("/Trade");
        }

        protected void ReturnHome()
        {
            Navigation.NavigateTo("/Trade");
        }

        protected async Task Delete(int id)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Navigation.BaseUri);

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                TradeModel = await httpClient.GetJsonAsync<TradeInputModel>($"https://localhost:5001/api/Trade/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }

            try
            {
                var response = await httpClient.DeleteAsync($"https://localhost:5001/api/Trade/{id}");

                if (response.IsSuccessStatusCode)
                {
                    OperationSuccess = true;
                    StateHasChanged();
                }

            }
            catch (Exception)
            {
                StateHasChanged();
                throw;
            }
        }
    }
}
