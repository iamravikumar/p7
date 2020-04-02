using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poseidon.Client.Pages.RuleName
{
    public class CreateBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public int Id { get; set; }
        protected RuleNameInputModel RuleNameModel { get; set; }
        protected bool OperationSuccess { get; set; } = false;

        protected override void OnInitialized()
        {
            RuleNameModel = new RuleNameInputModel();
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
                    await httpClient.PostJsonAsync("https://localhost:5001/api/RuleName", RuleNameModel);

                    OperationSuccess = true;

                    StateHasChanged();
                }
                catch (Exception)
                {
                    StateHasChanged();
                }
            }
        }

        protected void CancelCreate()
        {
            Navigation.NavigateTo("/RuleName");
        }

        protected void ReturnHome()
        {
            Navigation.NavigateTo("/RuleName");
        }
    }
}
