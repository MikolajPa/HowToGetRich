using System.Globalization;

namespace EnovationAssignment.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        
    }
}
