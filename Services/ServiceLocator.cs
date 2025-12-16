using Microsoft.Extensions.DependencyInjection;
using RedFocus.ViewModel;

namespace RedFocus.Services;

/// <summary>
/// 依赖注入服务定位器
/// </summary>
public static class ServiceLocator
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    /// 配置服务
    /// </summary>
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<ILanguageService, LanguageService>();
        services.AddSingleton<ITimerService, TimerService>();

        services.AddTransient<TimerConfigViewModel>();
        services.AddTransient<TimerViewModel>();
        services.AddTransient<ThemeSelectorViewModel>();
        services.AddTransient<OptionsViewModel>();

        services.AddSingleton<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
        return _serviceProvider;
    }

    /// <summary>
    /// 获取服务
    /// </summary>
    public static T GetService<T>() where T : class
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("服务提供者尚未初始化，请先调用 ConfigureServices()");
        }
        return _serviceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// 尝试获取服务
    /// </summary>
    public static T? TryGetService<T>() where T : class
    {
        return _serviceProvider?.GetService<T>();
    }
}
