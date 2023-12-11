namespace DevRhythm.Core.Entities.Base
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
