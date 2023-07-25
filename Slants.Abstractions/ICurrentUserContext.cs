namespace Slants
{
    public interface ICurrentUserContext
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}