﻿using GeekShoping.IdentityServer.Configuration;
using GeekShoping.IdentityServer.Model;
using GeekShoping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShoping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySQLContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if (_role.FindByIdAsync(IdentityConfiguration.Admin).Result != null)
            {
                return;
            }

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser
            {
                UserName = "WilhelmADM",
                Email = "wilhelmadm@bankai.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (47) 12334-1234",
                FirstName = "Wilhelm",
                LastName = "ZIM"
            };

            _user.CreateAsync(admin, "Will@2024").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var admClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, $"{admin.FirstName}"),
                new Claim(JwtClaimTypes.FamilyName, $"{admin.LastName}"),
                new Claim(JwtClaimTypes.Role, $"{IdentityConfiguration.Admin}"),
            }).Result;

            ApplicationUser client = new ApplicationUser
            {
                UserName = "WilhelmCliente",
                Email = "wilhelmcliente@bankai.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (47) 12334-1234",
                FirstName = "WilhelmCLIENTE",
                LastName = "ZIMcc"
            };

            _user.CreateAsync(client, "Will@2024").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, $"{client.FirstName}"),
                new Claim(JwtClaimTypes.FamilyName, $"{client.LastName}"),
                new Claim(JwtClaimTypes.Role, $"{IdentityConfiguration.Client}"),
            }).Result;
        }
    }
}
