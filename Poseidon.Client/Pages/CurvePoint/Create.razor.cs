using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Poseidon.Shared.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poseidon.Client.Pages.CurvePoint
{
    public class CreateBase : ComponentBase
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public int Id { get; set; }
        protected CurvePointInputModel CurvePointModel { get; set; }

        protected override void OnInitialized()
        {
            CurvePointModel = new CurvePointInputModel();
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
                    await httpClient.PostJsonAsync("https://localhost:5001/api/curvepoint", CurvePointModel);

                    Navigation.NavigateTo("/curvepoint");
                }
                catch (Exception e)
                {
                    StateHasChanged();
                }
            }
        }

        protected void CancelCreate()
        {
            Navigation.NavigateTo("/curvepoint");
        }
    }
}
