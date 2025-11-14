namespace WatchList.Entities.Abstract;

public interface IEntity
{
    int Id { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
    bool IsActive { get; set; }
}
//Interface: Sadece "ne yapacağını" söyler