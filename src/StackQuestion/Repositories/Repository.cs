using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackQuestion.Repositories
{
    public interface Repository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll { get; }
        T GetById(int id);
        Task<T> Create(T item);
        Task<T> Update(T item);
    }
}
