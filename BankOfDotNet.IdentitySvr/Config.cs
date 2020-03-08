using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace BankOfDotNet.IdentitySvr
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Dhanraj",
                    Password = "gehlot"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Gehlot",
                    Password = "dhanraj"
                }
            };
        }

        public static IEnumerable<ApiResource> GetAllApiResx()
        {
            return new List<ApiResource>
            {
                new ApiResource ("bankOfDotNetApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "bankOfDotNetApi"
                    }
                },

                // Resource Owner Password Grant Type
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "bankOfDotNetApi"
                    }
                }
            };
        }
    }
}