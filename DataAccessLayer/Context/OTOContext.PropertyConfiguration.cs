using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public partial class OTOContext
    {
        private static void ConfigureDecimalProperties(ModelBuilder modelBuilder)
        {
            var decimalProperties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in decimalProperties)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }

        private static void ConfigureUnicodeProperties(ModelBuilder modelBuilder)
        {
            var unicodeProperties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string) && p.IsUnicode().HasValue);

            foreach (var property in unicodeProperties)
            {
                property.SetIsUnicode(false);
            }
        }
    }
}
