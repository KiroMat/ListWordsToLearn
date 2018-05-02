using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ListWordsToLearn.Common.DB
{
    public interface IRepositoryDB<T> where T : BaseModel
    {
        T GetById(int id);
        bool Insert(T entity);
        bool Update(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        bool Remove(T entity);
        bool DropCollection(string name);
    }
}
