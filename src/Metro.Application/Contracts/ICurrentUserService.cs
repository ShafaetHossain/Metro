namespace Metro.Application.Contracts
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}
