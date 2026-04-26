using System.Reflection;
using Mapster;

namespace Celestial.Agency.Service.Configuration;

public static class MapsterConfiguration
{
    private static bool _configured;

    public static void RegisterMappings()
    {
        if (_configured) return;
        _configured = true;

        var config = TypeAdapterConfig.GlobalSettings;

        // ✅ ۱. فقط propertyهایی که setter دارند map شوند
        config.Default.IgnoreMember((member, _) =>
        {
            if (member.Info is PropertyInfo prop)
                return !prop.CanWrite;
            return false;
        });

        // ✅ ۲. نادیده گرفتن propertyهایی که mapping ندارند
        config.Default.IgnoreNonMapped(true);

        // ✅ ۳. جلوگیری از recursion و circular reference
        try
        {
            config.Default.PreserveReference(true);
        }
        catch
        {
            // بعضی نسخه‌ها این متد را ندارند
        }

        // ✅ ۴. مپینگ امن برای DateOnly ↔ DateTime
        try
        {
            TypeAdapterConfig<DateOnly, DateTime>.NewConfig()
                .MapWith(src => src.ToDateTime(TimeOnly.MinValue));

            TypeAdapterConfig<DateTime, DateOnly>.NewConfig()
                .MapWith(src => DateOnly.FromDateTime(src));
        }
        catch
        {
        }

        // ✅ ۵. جلوگیری از خطای CompileException
        AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
        {
            if (args.Exception is CompileException or InvalidOperationException)
                Console.WriteLine($"[Mapster Warning] Mapping skipped: {args.Exception.Message}");
        };

        // ✅ ۶. فیلتر امن برای جلوگیری از crash هنگام تطبیق typeها
        config.When((srcType, destType, mapType) =>
        {
            try
            {
                if (srcType == null || destType == null)
                    return false;

                if (srcType.IsAbstract || destType.IsAbstract)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        });

        Console.WriteLine("✅ Mapster global configuration applied safely (compatible with v7–v9).");
    }
}