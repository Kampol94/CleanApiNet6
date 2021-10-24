using MassTransit;
using System;

namespace CleanApi.Domain.Contracts
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }

        protected BaseEntity()
        {
            Id = NewId.Next().ToGuid();
        }
    }
}