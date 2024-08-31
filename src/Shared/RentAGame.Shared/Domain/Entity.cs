namespace RentAGame.Shared.Domain;

public abstract class Entity<T> : IEntity<T>
{
    // Entity'yi tanımlayan Unique Identity(Benzersiz Identity)
    public T Id { get; set; }

    // Her Entity için ortak izleme özellikleri
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}