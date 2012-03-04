using System.Collections.Generic;

namespace CQRS.Domain
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>();
        T Get<T>(int id);
        void Save<T>(T item);
    }
}