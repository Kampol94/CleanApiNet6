using System.Net;

namespace CleanApi.Application.Exceptions
{
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}