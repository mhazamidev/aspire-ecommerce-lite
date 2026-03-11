using Domain.SeekWork;
using Identity.Infrastructure.Persistence.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence.UOW;

public class UnitOfWork : IUnitOfWork
{
    protected readonly ApplicationDbContext dbContext;
    private readonly ILogger<ApplicationDbContext> logger;

    public UnitOfWork(ApplicationDbContext dbContext, ILogger<ApplicationDbContext> logger)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.logger = logger;
    }

    public int Commit()
    {
        try
        {
            return dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while committing UnitOfWork");
            throw;
        }
    }
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while committing UnitOfWork");
            throw;
        }
    }


    public Task RollbackAsync()
    {
        dbContext.ChangeTracker.Clear();
        return Task.CompletedTask;
    }
}
