using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Common
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class AppSettingHelper
    {
        static IConfiguration Configuration { get; set; }
        static string ContentPath { get; set; }

        public AppSettingHelper()
        {
            string path = "appsettings.json";
            Configuration = new ConfigurationBuilder().SetBasePath(ContentPath).Add(new JsonConfigurationSource
            {
                Path = path,
                Optional = false,
                ReloadOnChange = true
            }).Build();
        }
        public AppSettingHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 获取指定属性值
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    if (Configuration != null)
                    {
#pragma warning disable CS8603 // 可能返回 null 引用。
                        return Configuration[string.Join(":", sections)];
#pragma warning restore CS8603 // 可能返回 null 引用。
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        /// <summary>
        /// 获取指定对象值
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> app<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}