using System;

namespace Stone.Sdk.Persistence
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

    public abstract class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; protected set; } = false;
    }
}