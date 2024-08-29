namespace RentAGame.Shared.Domain;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}