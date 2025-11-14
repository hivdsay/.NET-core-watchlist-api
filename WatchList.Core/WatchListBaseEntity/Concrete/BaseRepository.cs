using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WatchList.Core.WatchListBaseEntity.Abstract;
using WatchList.Entities.Abstract;

namespace WatchList.Core.WatchListBaseEntity.Concrete;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, new()
//new() → Parametresiz bir constructor’ı olmalı. Yani bu repository sadece benim belirlediğim entity sınıfları için çalışır.
{
    //Dependency Injection (bağımlılık enjeksiyonu)
    //_context adında, sadece okunabilir ve sadece repository sınıfı içinde kullanılacak bir DbContext değişkeni var.
    protected readonly DbContext _context; 
    //Dışarıdan hangi DbContext gelecekse repository içine veriliyor.
    // Repository kendi içinde _context üzerinden işlemlerini yapıyor.
    public BaseRepository(DbContext context)
    {// repository’i oluştururken ona hangi veritabanı bağlamını kullanacağını söylüyorsun
        _context = context;
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    { //Parametre olarak gelen entity nesnesini EF Core context’ine ekliyor.
        await _context.Set<TEntity>().AddAsync(entity); //DbContext içindeki ilgili tabloyu bul. yeni kaydı veritabanına eklemeye hazırlar.
        return entity;
    }

    public async Task AddBulkAsync(List<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
      
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(()=>_context.Set<TEntity>().Update(entity));
         
    }

    public async Task DeleteAsync(TEntity entity)
    {
       await Task.Run(()=>_context.Set<TEntity>().Remove(entity));
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query = query.Where(predicate);

        if (includeProperties != null)
        {
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
        }
        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties != null)
        {
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await (predicate == null ? _context.Set<TEntity>().CountAsync() : _context.Set<TEntity>().CountAsync(predicate));
    }
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
}