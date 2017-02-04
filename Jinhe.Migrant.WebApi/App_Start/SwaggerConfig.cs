using System.Web.Http;
using WebActivatorEx;
using Jinhe.Migrant.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System.IO;

namespace Jinhe.Migrant.WebApi
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            httpConfiguration.EnableSwagger("docs/{apiVersion}", c =>
            {
                c.Schemes(new[] { "http", "https" });
                c.SingleApiVersion("v1", "")
                 .Description("");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteProperties();
                c.UseFullTypeNameInSchemaIds();

                var searchFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
                foreach (var file in Directory.EnumerateFiles(searchFolder, "*.dll", SearchOption.AllDirectories))
                {
                    var commentsFile = $"{file.Substring(0, file.Length - 3)}xml";
                    if (File.Exists(commentsFile))
                    {
                        c.IncludeXmlComments(commentsFile);
                    }
                }
            }).EnableSwaggerUi("sandbox/{*assetPath}");
        }
    }
}
