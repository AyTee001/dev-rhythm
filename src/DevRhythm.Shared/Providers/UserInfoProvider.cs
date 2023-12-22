using DevRhythm.Shared.Interfaces;

namespace DevRhythm.Shared.Providers
{
    public class UserInfoProvider : IUserInfoProvider
    {
        public long? Id { get; private set; }

        public void SetUserUid(long? id)
        {
            Id = id;
        }
    }
}
