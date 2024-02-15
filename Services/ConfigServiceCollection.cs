﻿using dotnet_api.Services.ModelServices;
using dotnet_api.Models.Interfaces;
using dotnet_api.Services.SecurityService;

namespace dotnet_api.Services;
public static class ConfigServiceCollection
{
    public static IServiceCollection AddServiceGroup(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        return services;
    }
}
