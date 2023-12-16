namespace DevRhythm.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, long id)
            : base($"Entity {name} (id: {id}) not found.") { }
        public NotFoundException(string name)
            : base($"Entity {name} not found.") { }
    }
}
