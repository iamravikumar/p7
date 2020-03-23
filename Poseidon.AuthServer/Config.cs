// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Poseidon.AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
                {new ApiResource("poseidon_api", "Poseidon API"),};

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // Postman M2M client
                new Client
                {
                    ClientId = "postman_test_client",
                    ClientSecrets =
                    {
                        new Secret("7c3c1e25-f013-4651-901c-443927a6a90e".Sha256())
                    },
                    RedirectUris = {"http://getpostman.com/oauth2/callback"},
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    AllowedScopes =
                    {
                        "poseidon_api"
                    },
                },
                // interactive ASP.NET Razor Pages client
                new Client
                {
                    ClientId = "poseidon_razor", 
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = {"http://localhost:5002/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "poseidon_api"
                    },

                    AllowOfflineAccess = true
                },
                // Swagger UI
                new Client
                {
                    ClientId = "swagger_ui",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:5001/oauth2-redirect.html"
                    },
                    AllowedCorsOrigins = {"http://localhost:5001"},
                    AllowedScopes = {"poseidon_api"}
                }
            };
    }
}