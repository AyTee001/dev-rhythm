using DevRhythm.Core.Entities.Base;

namespace DevRhythm.Core.Entities
{
    public class Tag (string name) : Entity
    {
        public string Name { get; set; } = name;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
