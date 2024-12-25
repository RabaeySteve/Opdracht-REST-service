namespace FitnessManagement.API.Exceptions {
    public class MapperException : Exception {
        public MapperException(string? message) : base(message) {
        }

        public MapperException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
