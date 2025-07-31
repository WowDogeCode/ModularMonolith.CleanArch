using System.Reflection;

namespace Products.Infrastructure.Utils
{
    public static class SqlLoader
    {
        public static string LoadSql(string fileName)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sqlFolder = Path.Combine(basePath!, "Sql");

            var fullPath = Path.Combine(sqlFolder, fileName);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"SQL file '{fileName} not found at '{fullPath}'");
            }

            return File.ReadAllText(fullPath);
        }
    }
}
