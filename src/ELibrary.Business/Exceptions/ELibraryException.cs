using ELibrary.Core.Common;

namespace ELibrary.Business.Exceptions
{
    public class ELibraryException : Exception
    {
        public Errors Error { get; }

        public ELibraryException(Errors error) : base(error.message)
        {
            Error = error;
        }
    }
}
