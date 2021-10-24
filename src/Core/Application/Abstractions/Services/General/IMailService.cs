using CleanApi.Shared.DTOs.General.Requests;
using System.Threading.Tasks;

namespace CleanApi.Application.Abstractions.Services.General
{
    public interface IMailService : ITransientService
    {
        Task SendAsync(MailRequest request);
    }
}