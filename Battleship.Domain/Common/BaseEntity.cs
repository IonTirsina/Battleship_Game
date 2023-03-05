namespace Battleship.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get ; set; }
        public DateTime DateUpdated { get; set; }
    }
}
