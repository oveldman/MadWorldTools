using MadOffice.Application.Emails;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.General;
using MadOffice.Infrastructure.Emails;
using MadOffice.UI.MauiLogic;
using Radzen;

namespace MadOffice.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMadOfficeServices(this IServiceCollection services)
    {
        services.AddScoped<IDefaultFolder, DefaultFolder>();
        services.AddScoped<System.IO.Abstractions.IFileSystem, System.IO.Abstractions.FileSystem>();
        
        services.AddScoped<IEmailEditor, EmailEditor>();
        services.AddScoped<IEmailFinder, EmailFinder>();
        services.AddScoped<IEmailHelper, EmailHelper>();
        services.AddScoped<IEmailImporter, EmailImporter>();
        services.AddScoped<IEmailReader, EmailReader>();

        services.AddExternPackages();
        
        return services;
    }

    private static void AddExternPackages(this IServiceCollection services)
    {
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<TooltipService>();
        services.AddScoped<ContextMenuService>();
    }
}