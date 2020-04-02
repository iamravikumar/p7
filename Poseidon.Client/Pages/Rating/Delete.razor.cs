using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poseidon.Client.Pages.Rating  
{
    public class DeleteBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        protected RatingInputModel RatingModel { get; set; }
        protected bool OperationSuccess { get; set; } = false;

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
                RatingModel = await httpClient.GetJsonAsync<RatingInputModel>($"https://localhost:5001/api/Rating/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected void CancelDelete()
        {
            Navigation.NavigateTo("/Rating");
        }

        protected void ReturnHome()
        {
            Navigation.NavigateTo("/Rating");
        }

        protected async Task Delete(int id)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Navigation.BaseUri)
            };

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                RatingModel = await httpClient.GetJsonAsync<RatingInputModel>($"https://localhost:5001/api/Rating/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }

            try
            {
                var response = await httpClient.DeleteAsync($"https://localhost:5001/api/Rating/{id}");

                if (response.IsSuccessStatusCode)
                {
                    OperationSuccess = true;
                    StateHasChanged();
                }

            }
            catch (Exception)
            {
                StateHasChanged();
            }
        }
    }
}
