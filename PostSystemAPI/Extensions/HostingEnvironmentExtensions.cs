using System;
using Microsoft.AspNetCore.Hosting;

namespace PostSystemAPI.Extensions;

public static class HostingEnvironmentExtensions
{
    public static bool IsLocalDockerDevelopment(this IWebHostEnvironment environment) =>
        string.Equals(environment.EnvironmentName ?? string.Empty, "LocalDockerDevelopment", StringComparison.OrdinalIgnoreCase);
}