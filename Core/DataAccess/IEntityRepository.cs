using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {//Generic Repository. Kısıtlama koyuyorum
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);//Filter (fitre gönderilmesse hepsini listele)
        void Add(T entity);//Gönderdiğim T'yi veri tabanına ekleyecek
        void Update(T entity);
        void Delete(T entity);
    }
}
