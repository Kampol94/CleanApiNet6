using System;

namespace CleanApi.Domain.Contracts
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        Guid? DeletedBy { get; set; }
    }
}