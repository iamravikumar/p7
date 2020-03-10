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
                // Interactive ASP.NET Core Razor Pages client
                new Client
                {
                    ClientId = "poseidon_client",
                    ClientSecrets =
                    {
                        new Secret("7c3c1e25-f013-4651-901c-443927a6a90e".Sha256()) 
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
//                    RequireConsent = false,
                    AllowedScopes =
                    {
//                        IdentityServerConstants.StandardScopes.OpenId,
//                        IdentityServerConstants.StandardScopes.Profile,
                        "poseidon_api"
                    },
//                    RequirePkce = true,
//                    RedirectUris = { "http://localhost:5002/signin-oidc"},
//                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
//                    AllowOfflineAccess = true
                },
            };
    }
}