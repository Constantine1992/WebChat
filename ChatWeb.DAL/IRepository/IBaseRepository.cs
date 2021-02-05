using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.DAL.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
