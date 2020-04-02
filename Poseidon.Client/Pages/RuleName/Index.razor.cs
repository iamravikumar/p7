using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;


namespace Poseidon.Client.Pages.RuleName
{
    public class RuleNameBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected List<RuleNameInputModel> RuleNameModels { get; set; }

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
                    RuleNameModels = await httpClient.GetJsonAsync<List<RuleNameInputModel>>("https://localhost:5001/api/RuleName");
                }
                catch (Exception)
                {
                    RuleNameModels = new List<RuleNameInputModel>();
                }
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected void Edit(int id)
        {
            Navigation.NavigateTo($"/RuleName/edit/{id}");
        }

        protected void Delete(int id)
        {
            Navigation.NavigateTo($"/RuleName/delete/{id}");
        }
    }
}