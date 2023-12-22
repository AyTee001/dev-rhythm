namespace DevRhythm.Shared.Interfaces
{
    public interface IUserInfoProvider
    {
        long? Id { get; }
        void SetUserUid(long? id);
    }
}
