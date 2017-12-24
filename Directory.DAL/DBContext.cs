namespace Directory.DAL
{
    using System.Data.Entity;
    using Directory.Common.Models;

    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection")
        {

        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
