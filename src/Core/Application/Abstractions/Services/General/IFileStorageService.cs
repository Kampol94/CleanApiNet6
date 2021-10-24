using CleanApi.Domain.Enums;
using CleanApi.Shared.DTOs.General.Requests;
using System.Threading.Tasks;

namespace CleanApi.Application.Abstractions.Services.General
{
    public interface IFileStorageService : ITransientService
    {
        public Task<string> UploadAsync<T>(FileUploadRequest request, FileType supportedFileType)
        where T : class;
    }
}