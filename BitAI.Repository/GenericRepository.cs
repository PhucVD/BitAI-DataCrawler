using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BitAI.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly DbContext _context;
        protected readonly DbSet<TEntity> dbSet;

        #endregion

        #region Constructor

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this.dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void DeleteById(int id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual TEntity GetById(int id)
        {
            TEntity entity = dbSet.Find(id);
            return entity;
        }

        public virtual IQueryable<TEntity> GetList()
        {
            return dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            var query = dbSet.AsQueryable();
            return query.Where(filter);
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, object>>[] includes, Expression<Func<TEntity, bool>> filter)
        {
            var query = dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query.Where(filter);
        }

        public virtual int ExecuteCommand(string sql)
        {
            int numRowsAffected = _context.Database.ExecuteSqlCommand(sql);
            return numRowsAffected;
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        #endregion

    }
}
