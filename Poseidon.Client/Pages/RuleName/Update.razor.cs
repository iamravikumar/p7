using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;

namespace Poseidon.Client.Pages.RuleName
{
    public class RuleNameUpdateBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public int Id { get; set; }
        protected RuleNameInputModel RuleNameModel { get; set; }
        protected bool OperationSuccess { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Navigation.BaseUri);

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                RuleNameModel = await httpClient.GetJsonAsync<RuleNameInputModel>($"https://localhost:5001/api/RuleName/{Id}");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected async Task HandleValidSubmit()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Navigation.BaseUri);

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");

                try
                {
                    await httpClient.PutJsonAsync($"https://localhost:5001/api/RuleName/{Id}", RuleNameModel);

                    OperationSuccess = true;
                }
                catch (Exception e)
                {
                    StateHasChanged();
                }
            }
        }

        protected void CancelEdit()
        {
            Navigation.NavigateTo("/RuleName");
        }

        protected void ReturnHome()
        {
            Navigation.NavigateTo("/RuleName");
        }
    }
}