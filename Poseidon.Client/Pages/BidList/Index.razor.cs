using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;


namespace Poseidon.Client.Pages.BidList
{
    public class BidListBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected List<BidListInputModel> BidListModels { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Navigation.BaseUri);

            var tokenResult = await AuthenticationService.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");

                try
                {
                    BidListModels = await httpClient.GetJsonAsync<List<BidListInputModel>>("https://localhost:5001/api/bidlist");
                }
                catch (Exception)
                {
                    BidListModels = new List<BidListInputModel>();
                }
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        protected void Edit(int id)
        {
            Navigation.NavigateTo($"/bidlist/edit/{id}");
        }

        protected void Delete(int id)
        {
            Navigation.NavigateTo($"/bidlist/delete/{id}");
        }
    }
}