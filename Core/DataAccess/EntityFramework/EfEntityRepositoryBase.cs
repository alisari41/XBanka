using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {//AddedEntity =Eklenmeye çalışan entity
                var addedEntity = context.Entry(entity);//Gönderilen entity Context'e abone ediyorum
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {//deletedEntity =Silmeye çalışan entity
                var deletedEntity = context.Entry(entity);//Gönderilen entity Context'e abone ediyorum
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {//using disposibıl petirbul gibiNesnenin hayatını sonlandırmasını using bloğu bittiği halde silinmesini sağlıyor
                return context.Set<TEntity>().SingleOrDefault(filter);//verdiğimiz filter'a göre o data'nın gelmesini sağlıyorum
            }
        }
        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {//using disposibıl petirbul gibiNesnenin hayatını sonlandırmasını using bloğu bittiği halde silinmesini sağlıyor
                return filter == null
                    ? context.Set<TEntity>().ToList()//Filter boş mu boş ise tamamını getir
                    : context.Set<TEntity>().Where(filter).ToList();//aksi taktirde filter'a göre listeye çevir
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {//updatedEntity =Güncellemeye çalışan entity
                var updatedEntity = context.Entry(entity);//Gönderilen entity Context'e abone ediyorum
                updatedEntity.State = EntityState.Modified;//Modified Güncelleme yapar.  
                context.SaveChanges();
            }
        }
    }
}
