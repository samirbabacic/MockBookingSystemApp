using MockBookingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Contracts
{
    public interface IDAL
    {
        T GetById<T>(object id) where T : BaseEntity;

        T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

        IQueryable<T> GetAll<T>() where T : BaseEntity;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        void Insert<T>(T entity) where T : BaseEntity;
        void Replace<T>(T entity) where T : BaseEntity;
        void Delete<T>(object objectId);
    }
}
