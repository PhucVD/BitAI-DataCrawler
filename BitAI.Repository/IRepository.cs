using System;
using System.Linq;
using System.Linq.Expressions;

namespace BitAI.Repository
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteById(int id);

        T GetById(int id);

        IQueryable<T> GetList();

        IQueryable<T> GetList(Expression<Func<T, bool>> filter);

        IQueryable<T> GetList(Expression<Func<T, object>>[] includes, Expression<Func<T, bool>> filter);

        int ExecuteCommand(string sql);

        int Save();
    }
}
