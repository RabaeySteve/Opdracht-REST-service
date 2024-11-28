using FitnessManagement.EF;

namespace ConsoleAppTestDBModel {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            string connectionString = @"Data Source=LAPTOP-LE0HU5AN\SQLEXPRESS;Initial Catalog=GymTest;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            FitnessManagementContext ctx = new FitnessManagementContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
    }
}
