// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using PartnerCode.EfCoreCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace PartnerCode.AppStart
{
    public static class StartupExtensions
    {
        public const string PartnersDbContextHistoryName = "Example-PartnerDbContext";

        public static void RegisterPartner(this IServiceCollection services, IConfiguration configuration)
        {
            //Register any services in this project
            services.RegisterAssemblyPublicNonGenericClasses()
                .Where(c => c.Name.EndsWith("Service"))  //optional
                .AsPublicImplementedInterfaces();

            //Register the retail database to the same database used for individual accounts and AuthP database
            services.AddDbContext<PartnerDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), dbOptions =>
                dbOptions.MigrationsHistoryTable(PartnersDbContextHistoryName)));
        }
    }
}