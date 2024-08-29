namespace RentAGame.Shared.Domain;

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
public interface IEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}