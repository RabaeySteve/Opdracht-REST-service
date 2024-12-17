using FitnessManagement.EF;

namespace ConsoleAppTestDBModel {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            string connectionString = @"Data Source=laptop-le0hu5an\sqlexpress;Initial Catalog=GymTestOG;Integrated Security=True;Trust Server Certificate=True";

            FitnessManagementContext ctx = new FitnessManagementContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
    }
}
