using CarService.Models.Entities;

namespace CarService.DL.MyStaticDB
{
    internal static class StaticDB
    {
        internal static List<Customer> Customers { get; set; } = new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                Name = "John Doe",
                Email = "jd@xxx.com"
            },
            new Customer()
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "js@xxx.com"
            }
        };
    }
}