namespace Core.Entities
{
    public interface ISoftDelete : IEntity
    {
        bool IsDeleted { get; set; }
    }
}
