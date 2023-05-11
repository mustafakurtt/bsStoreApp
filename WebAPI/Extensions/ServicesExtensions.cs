﻿using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

namespace WebAPI.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}