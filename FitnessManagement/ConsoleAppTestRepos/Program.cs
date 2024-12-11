using FitnessBL.Models;
using FitnessDataLayerProvider;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using FitnessManagement.EF;
using System.Configuration;
using System.Xml;
using static FitnessManagement.BL.Models.Program;

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
            //repos.MemberRepository.DeleteMember(1);
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

            TimeSlot timeSlot8 = repos.TimeSlotRepository.GetTimeSlot(8);
            TimeSlot timeSlot9 = repos.TimeSlotRepository.GetTimeSlot(9);
            TimeSlot timeSlot18 = repos.TimeSlotRepository.GetTimeSlot(18);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Reservation");
            Console.WriteLine("-------------------------------------------------------------------------------------\n");

            Member memberJohn = new Member("John", "Doe", "john.doe@gmail.com", "Brugge", new DateTime(1990, 3, 22), new List<string> { "Swimming" }, Member.MemberType.Silver);
   
            repos.MemberRepository.AddMember(memberJohn);
            Member gertDB = repos.MemberRepository.GetMember(2);
            Member johnDB = repos.MemberRepository.GetMember(3);
            
            Reservation reservationGert1 = new Reservation(new DateTime(2024, 12, 13),1, bike2DB, timeSlot8, gertDB);
            Reservation reservationGert2 = new Reservation(new DateTime(2024, 12, 14),2, bike2DB, timeSlot9, gertDB);
            Reservation reservationjohn1 = new Reservation(new DateTime(2024, 12, 14),3, treadmill2DB, timeSlot18, johnDB);
            Reservation reservationsteve = new Reservation(new DateTime(2024, 12, 11), 4, bike2DB, timeSlot18, steveDB);

            repos.ReservationRepository.AddReservation(reservationGert1);
            repos.ReservationRepository.AddReservation(reservationGert2);
            repos.ReservationRepository.AddReservation(reservationjohn1);
            repos.ReservationRepository.AddDubbleRes(reservationsteve);

            Console.WriteLine("\nAll Reservations:");
            List<Reservation> allReservations = repos.ReservationRepository.GetAll();
            foreach (var reservation in allReservations) {
                Console.WriteLine(reservation);
            }

            
            Console.WriteLine("\nReservations for Gert:");
            List<Reservation> gertReservations = repos.ReservationRepository.GetReservationsMember(gertDB.MemberId);
            foreach (var reservation in gertReservations) {
                Console.WriteLine(reservation);
            }

            
            Console.WriteLine("\nReservations for Gert on 13/12/2024:");
            List<Reservation> gertReservationsDate = repos.ReservationRepository.GetReservationsMemberDate(gertDB.MemberId, new DateTime(2024, 12, 13));
            foreach (var reservation in gertReservationsDate) {
                Console.WriteLine(reservation);
            }

            
            Console.WriteLine("\nGet Reservation by ID:");
            Reservation specificReservation = repos.ReservationRepository.GetReservation(1);
            Console.WriteLine(specificReservation);

            
            Console.WriteLine("\nDelete Reservation:");
            Console.WriteLine("Id2 moet weg zijn");
            Reservation Gert2 = repos.ReservationRepository.GetReservation(2);
            repos.ReservationRepository.DeleteReservation(Gert2);
            allReservations = repos.ReservationRepository.GetAll();
            foreach (var reservation in allReservations) {
                Console.WriteLine(reservation);
            }

            
            Console.WriteLine("\nUpdate Reservation:");
            Reservation jhonUpdate = repos.ReservationRepository.GetReservation(3);
            jhonUpdate.Date = new DateTime(2024, 12, 15);
            repos.ReservationRepository.UpdateReservation(jhonUpdate);
            Console.WriteLine(repos.ReservationRepository.GetReservation(jhonUpdate.ReservationId));

            Console.WriteLine("\nUpdate DubbleReservation:");
            Reservation reservationsteveUpdate = repos.ReservationRepository.GetReservation(4);
            reservationsteveUpdate.Date = new DateTime(2024, 12, 16);
            repos.ReservationRepository.UpdateDubbleRes(reservationsteveUpdate);
            Console.WriteLine(repos.ReservationRepository.GetReservation(reservationsteveUpdate.ReservationId));

            Console.WriteLine("\nDelete DubbleReservation:");
            Console.WriteLine("Id4 en Id5 moet weg zijn");
            Reservation reservationsteveDelete = repos.ReservationRepository.GetReservation(4);
            repos.ReservationRepository.DeleteDubbleRes(reservationsteveDelete);
            allReservations = repos.ReservationRepository.GetAll();
            foreach (var reservation in allReservations) {
                Console.WriteLine(reservation);
            }

            Console.WriteLine("\n-------------------------------------------------------------------------------------");
            Console.WriteLine("Reservation");
            Console.WriteLine("-------------------------------------------------------------------------------------\n");

            FitnessManagement.BL.Models.Program start2Run = new FitnessManagement.BL.Models.Program("1", "Start2Run", new DateTime(2024, 12, 13), ProgramTarget.beginner, 20);
            FitnessManagement.BL.Models.Program FTPBoost = new FitnessManagement.BL.Models.Program("2", "FTPBoost", new DateTime(2024, 12, 16), ProgramTarget.advanced, 30);

            repos.ProgramRepository.AddProgram(start2Run);
            repos.ProgramRepository.AddProgram(FTPBoost);
            IEnumerable<FitnessManagement.BL.Models.Program> programs = repos.ProgramRepository.GetAll();
            foreach (FitnessManagement.BL.Models.Program program in programs) {
                Console.WriteLine(program);
            }


            repos.ProgramRepository.GetProgramByProgramCode("1");

            repos.MemberRepository.AddProgram(2, "1");
            Member gertMetProgram = repos.MemberRepository.GetMember(2);
            Console.WriteLine(gertMetProgram);
        }
    }
}
