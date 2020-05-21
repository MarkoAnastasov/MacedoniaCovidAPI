using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacedoniaCovidAPIV2.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);

        List<T> GetAll();

        T GetFirstWhere(Func<T, bool> predicate);

        T GetById(int id);

        void Update(T entity);

        void Remove(T entity);

        void SaveEntities();
    }
}
