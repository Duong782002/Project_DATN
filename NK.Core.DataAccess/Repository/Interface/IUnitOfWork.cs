namespace NK.Core.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
