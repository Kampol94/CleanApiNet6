using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleanApi.Application.Abstractions.Services.General
{
    public interface IJobService : ITransientService
    {
        string Enqueue(Expression<Func<Task>> methodCall);
    }
}