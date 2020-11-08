using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Channels;

namespace POLYCLINIC.BLL.Infrastructure
{
    public class LiveData<TEntity> where TEntity : class
    {
        public event EventHandler TableChanged;

        private DbSet<TEntity> dbSet;

        public LiveData(DbSet<TEntity> dbSet)
        {
            this.dbSet = dbSet;
        }

        public List<TEntity> List => dbSet.ToList();

        public virtual TEntity Add(TEntity entity)
        {
            TEntity result = dbSet.Add(entity);
            OnPropertyChanged();
            return result;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public virtual TEntity Remove(TEntity entity)
        {
            var result = dbSet.Remove(entity);
            OnPropertyChanged();
            return result;
        }

        public virtual DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            var result = dbSet.SqlQuery(sql, parameters);
            OnPropertyChanged();
            return result;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public virtual void Refresh()
        {
            OnPropertyChanged();
        }

        private void OnPropertyChanged()
        {
            TableChanged?.Invoke(this, new EventArgs());
        }
    }
}
