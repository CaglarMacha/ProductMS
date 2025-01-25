using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string propertyName, object value)
        {
            // Eğer değer geçerli değilse, sorguyu olduğu gibi döndür
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return query;
            }

            // Property name'i ile ifade oluşturulacak
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);

            // Eğer property ve değer uyuyorsa, "p.Property == value" ifadesini oluştur
            var equalExpression = Expression.Equal(property, constant);

            // Bu ifadeyi bir lambda ifadesine dönüştür
            var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

            // Filtreyi IQueryable'a uygula
            return query.Where(lambda);
        }

        // Bu metot, birden fazla koşul içeren filtreler için kullanılır
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, object> filters)
        {
            foreach (var filter in filters)
            {
                query = query.ApplyFilter(filter.Key, filter.Value);
            }

            return query;
        }
    }

}
