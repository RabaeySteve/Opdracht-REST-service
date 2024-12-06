using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDataLayerProvider {
    public class FitnessDatalayerFactory {
        public static FitnessRepositories GeefRepositories(string connectionString, RepositoryType repositoryType) {
            return new FitnessRepositories(connectionString, repositoryType);
        }
    }
}
