using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;

namespace Poseidon.Client
{
    public class BidListSingleBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public int Id { get; set; }
        protected BidListInputModel BidListModel { get; set; }
        protected bool OperationSuccess { get; set; } = false;
        protected bool OperationError { get; set; } = false;

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
                BidListModel = await httpClient.GetJsonAsync<BidListInputModel>($"https://localhost:5001/api/bidlist/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected async Task HandleValidSubmit()
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
                    await httpClient.PutJsonAsync($"https://localhost:5001/api/bidlist/{Id}", BidListModel);

                    OperationSuccess = true;
                }
                catch (Exception)
                {
                    OperationError = true;

                    StateHasChanged();
                }
            }
        }

        protected void CancelEdit()
        {
            Navigation.NavigateTo("/bidlist");
        }

        protected void ReturnHome()
        {
            Navigation.NavigateTo("/bidlist");
        }
    }
}