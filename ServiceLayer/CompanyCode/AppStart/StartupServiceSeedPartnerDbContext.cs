﻿// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthPermissions.AdminCode;
using PartnerCode.EfCoreClasses;
using PartnerCode.EfCoreCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RunMethodsSequentially;

namespace PartnerCode.AppStart
{
    /// <summary>
    /// If there are no RetailOutlets in the RetailDbContext it seeds the RetailDbContext with RetailOutlets and gives each of them some stock
    /// </summary>

    public class StartupServiceSeedPartnerDbContext : IStartupServiceToRunSequentially
    {
        public int OrderNum { get; } //runs after migration of the InvoicesDbContext

        public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
        {
            var context = scopedServices.GetRequiredService<PartnerDbContext>();
            var numInvoices = await context.Companies.IgnoreQueryFilters().CountAsync();
            if (numInvoices == 0)
            {
                var authTenantAdmin = scopedServices.GetRequiredService<IAuthTenantAdminService>();

                var seeder = new SeedPartnerDbContext(context);
                await seeder.SeedInvoicesForAllTenantsAsync(authTenantAdmin.QueryTenants().ToArray());
            }
        }

    }
}