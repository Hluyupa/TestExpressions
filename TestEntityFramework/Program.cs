using System.Linq.Expressions;
using TestEntityFramework.Models;

namespace TestEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyDbContext())
            {
                ParameterExpression param = Expression.Parameter(typeof(User), "user");
                Expression propertyName = Expression.Property(param, nameof(User.UserName));
                Expression targetValue = Expression.Constant("mGrayson");
                Expression equalsExpression = Expression.NotEqual(propertyName, targetValue);
                Expression<Func<User, bool>> lambda = Expression.Lambda<Func<User, bool>>(equalsExpression, param);
                var query = context.Users.Where(lambda);
                foreach (var user in query)
                {
                    Console.WriteLine($"User: {user.UserName}");
                }
            }
            Console.ReadLine();
        }
    }
}
