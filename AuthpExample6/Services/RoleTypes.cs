using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using System.Linq.Expressions;

namespace AuthpExample6.Services
{
    public static class RoleTypesExtensions
    {
        public static IQueryable<RoleTypes> AsQueryable(this Enum enumValue)
        {
            var type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Argument is not an Enum");
            }

            var values = Enum.GetValues(type).OfType<Enum>();
            var parameter = Expression.Parameter(type, "x");
            var memberExpression = Expression.Convert(parameter, typeof(RoleTypes));
            var lambda = Expression.Lambda<Func<Enum, RoleTypes>>(memberExpression, parameter);

            return values.Select(lambda.Compile()).AsQueryable();
        }
    }
}
