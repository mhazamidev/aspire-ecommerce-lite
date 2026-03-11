namespace Domain.SeekWork;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    int Commit();
    Task RollbackAsync();
}
