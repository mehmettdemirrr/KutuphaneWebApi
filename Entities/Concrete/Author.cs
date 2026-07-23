using Core.Entities;

namespace Entities.Concrete
{
    public class Author : IEntity, ISoftDelete
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool IsDeleted { get; set; }
    }
}