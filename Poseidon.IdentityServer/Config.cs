// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Poseidon.Client
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("role", new[] { "role" })
            };


        public static IEnumerable<ApiResource> Apis()
        {
            var apiResources = new ApiResource[1];

            var resource = new ApiResource
            {
                Name = "poseidon_api",
                DisplayName = "Poseidon API",
                UserClaims = new List<string> { "role" },
                ApiSecrets = new List<Secret> { new Secret("apisecret".Sha256()) }
            };


            return apiResources;
        }

        public static IEnumerable<IdentityServer4.Models.Client> Clients =>
            new IdentityServer4.Models.Client[]
            {
                // Postman M2M client
                new IdentityServer4.Models.Client
                {
                    ClientId = "postman_test_client",
                    ClientSecrets =
                    {
                        new Secret("7c3c1e25-f013-4651-901c-443927a6a90e".Sha256())
                    },
                    RedirectUris = { "http://getpostman.com/oauth2/callback" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    AllowedScopes =
                    {
                        "poseidon_api"
                    },
                },
                // Swagger UI
                new IdentityServer4.Models.Client
                {
                    ClientId = "swagger_ui",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "https://localhost:5001/oauth2-redirect.html"
                    },
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowedScopes = { "poseidon_api" }
                },
                // Blazor/WASM client
                new IdentityServer4.Models.Client
                {
                    ClientId = "poseidon_client",
                    ClientName = "Poseidon Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:5002/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5002/authentication/logged-out" },
                    AllowedCorsOrigins = { "https://localhost:5002" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "poseidon_api",
                        "roles",
                        "role"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = false
                }
            };
    }
}