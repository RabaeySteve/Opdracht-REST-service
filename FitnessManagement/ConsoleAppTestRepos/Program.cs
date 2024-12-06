using FitnessBL.Models;
using FitnessDataLayerProvider;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF;
using System.Configuration;
using System.Xml;
namespace ConsoleAppTestRepos {
    internal class Program {
        static void Main(string[] args) {

            string dataLayer = ConfigurationManager.AppSettings["DataLayer"];
            string connectionString = ConfigurationManager.ConnectionStrings["EFconnection"].ConnectionString;


            FitnessRepositories repos = FitnessDatalayerFactory.GeefRepositories(connectionString, RepositoryType.EFCore);
            FitnessManagementContext ctx = new FitnessManagementContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            // MemberTesten

            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("MemberTesten");
            Console.WriteLine("-------------------------------------------------------------------------------------\n");
            List<string> interessesSteve = new List<string> { "Lopen", "Fietsen" };
            DateTime dateSteve = new DateTime(2002, 02, 24);
           
            DateTime dataGert = new DateTime(2004, 01, 20);

            Member memberSteve = new Member("Steve", "Rabaey", "steve@hotmail.com", "Gent", dateSteve, interessesSteve, Member.MemberType.Gold);
            Member memberGert = new Member("Gert", "Jan", null, "Kortrijk", dataGert, null, null);

            repos.MemberRepository.AddMember(memberSteve);
            repos.MemberRepository.AddMember(memberGert);
            Member steveDB = repos.MemberRepository.GetMember(1);
            Console.WriteLine($"Name Steve : {steveDB.FirstName}");
            steveDB.FirstName = "Ronaldo";
            repos.MemberRepository.UpdateMember(steveDB);
            Console.WriteLine($"Name Ronaldo : {steveDB.FirstName}\n");

            Console.WriteLine("Alle members:");
            IEnumerable<Member> allMembers = repos.MemberRepository.GetMembers();
            foreach (Member member in allMembers) {
                Console.WriteLine(member);
            }
            Console.WriteLine();
            repos.MemberRepository.DeleteMember(1);
            allMembers = repos.MemberRepository.GetMembers();
            bool isMemberTest = repos.MemberRepository.IsMember(1);
            Console.WriteLine("Moet False zijn: " + isMemberTest);
            Console.WriteLine("Alle members:");
            foreach (Member member in allMembers) {
                Console.WriteLine(member);
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("EquipmentTesten");
            Console.WriteLine("-------------------------------------------------------------------------------------\n");
            
            Equipment bike1 = new Equipment(Equipment.EquipmentType.bike);
            Equipment bike2 = new Equipment(Equipment.EquipmentType.bike);
            Equipment treadmill1 = new Equipment(Equipment.EquipmentType.treadmill);
            Equipment treadmill2 = new Equipment(Equipment.EquipmentType.treadmill);
            repos.EquipmentRepository.AddEquipment(bike1);
            repos.EquipmentRepository.AddEquipment(bike2);
            repos.EquipmentRepository.AddEquipment(treadmill1);
            repos.EquipmentRepository.AddEquipment(treadmill2);
            //Equipment bike1DB = repos.EquipmentRepository.GetEquipment(1);
            Equipment bike2DB = repos.EquipmentRepository.GetEquipment(2);
            //Equipment treadmill1DB = repos.EquipmentRepository.GetEquipment(3);
            Equipment treadmill2DB = repos.EquipmentRepository.GetEquipment(4);

            repos.EquipmentRepository.SetMaintenance(bike2DB.EquipmentId, true);
            Console.WriteLine("all");
            IEnumerable<Equipment> allEquipment = repos.EquipmentRepository.GetAllEquipment();
            foreach (Equipment equipment in allEquipment) {
                Console.WriteLine(equipment);
            }
            Console.WriteLine("All available");
            IEnumerable<Equipment> allAvEquipment = repos.EquipmentRepository.GetAvailableEquipment();
            foreach (Equipment equipment in allAvEquipment) {
                Console.WriteLine(equipment);
            }


            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("TimeSlotTest");
            Console.WriteLine("-------------------------------------------------------------------------------------\n");

            List<TimeSlot> timeSlots = new List<TimeSlot>();

            for (int startTime = 8; startTime <= 21; startTime++) {
                timeSlots.Add(new TimeSlot(startTime));
            }

            foreach (TimeSlot timeSlot in timeSlots) {
                repos.TimeSlotRepository.addTimeSlot(timeSlot);
            }
            IEnumerable<TimeSlot> allTimeSlots = repos.TimeSlotRepository.GetAllTimeSlots();
            foreach (TimeSlot timeSlotDB in allTimeSlots) {
                Console.WriteLine(timeSlotDB);
            }

        }
    }
}
