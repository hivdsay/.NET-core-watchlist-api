using System.Linq.Expressions;
using WatchList.Entities.Abstract;

namespace WatchList.Core.WatchListBaseEntity.Abstract;

//generic repository pattern
//her entity için özel repo interfaceleri bunun üzerinden türetiliyor.
//Bütün entity’ler için ortak olan CRUD işlemlerini tanımlar.
   public interface IBaseRepository<TEntity> where TEntity : class, IEntity, new()
    {
        //CRUD
        Task<TEntity> AddAsync(TEntity entity); //Yeni bir kayıt ekler ve eklenen entity’yi döner.

        Task AddBulkAsync(List<TEntity> entities); //Birden fazla kaydı topluca ekler. Toplu film ekleme

        Task UpdateAsync(TEntity entity); //Var olan kaydı günceller ve güncellenmiş entity’yi döner.

        Task DeleteAsync(TEntity entity); //Silme işlemi entity’yi veritabanından kaldırır, artık geri döndürülecek bir güncel entity yok

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
      

        Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> predicate);
        //Eğer predicate olmasa, metod neyi kontrol edeceğini bilemez

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null);
        //Belirli bir koşulu sağlayan entity sayısını döndürmek
        //= null burada esneklik sağlar, filtre vermezsen tüm kayıtları say, filtre verirsen sadece filtrelenmiş kayıtları say.
        
        Task<TEntity?> GetByIdAsync(int id);
    }
