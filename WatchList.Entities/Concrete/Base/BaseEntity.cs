using WatchList.Entities.Abstract;

namespace WatchList.Entities.Concrete.Base;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}

//Abstract Class: Hem "ne yapacağını" hem "nasıl yapacağını" söyler.