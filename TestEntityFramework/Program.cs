using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestEntityFramework.ApplicationModels;
using TestEntityFramework.Extensions;
using TestEntityFramework.Models;

namespace TestEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyDbContext())
            {
                var sortOrder = new List<SortItem>
                {
                    new SortItem { PropertyName = "LastName", Direction = SortDirection.Descending },
                    new SortItem { PropertyName = "Age", Direction = SortDirection.Ascending }
                };

                var sortedUsers = context.Users
                    .AsQueryable()
                    .ApplySorting(sortOrder)
                    .ToList();

                foreach (var user in sortedUsers)
                {
                    Console.WriteLine($"{user.LastName} {user.Age}");
                }
            }
            Console.ReadLine();
        }
    }
}
