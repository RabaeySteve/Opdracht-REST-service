using FitnessManagement.BL.Intefaces;
using FitnessManagement.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDataLayerProvider {
    public class FitnessRepositories {
        public IMemberRepository MemberRepository { get; }
        public IEquipmentRepository EquipmentRepository { get; }
        public ITimeSlotRepository TimeSlotRepository { get; }
        public IReservationRepository ReservationRepository { get; }

        public IProgramRepository ProgramRepository { get; }

        public FitnessRepositories(string connectionString, RepositoryType repositoryType) {

			try {
                switch(repositoryType) {
                    case RepositoryType.EFCore:
                        MemberRepository = new MemberRepositoryEF(connectionString);
                        EquipmentRepository = new EquipmentRepository(connectionString);
                        TimeSlotRepository = new TimeSlotRepository(connectionString);
                        ReservationRepository = new ReservationRepository(connectionString);
                        ProgramRepository = new ProgramRepository(connectionString);
                        break;
                    
                    default: throw new Exception();
                }
			} catch (Exception ex) {

				throw;
			}
        }
    }
}
