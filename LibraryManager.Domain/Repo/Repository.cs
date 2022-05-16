using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Domain.Repo;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DbContext context;
    private DbSet<T> entities;

    public Repository(DbContext context)
    {
        this.context = context;
        entities = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return entities.AsQueryable();
    }

    public Task<T> Get(Guid id)
    {
        return entities.SingleAsync(s => s.Id == id);
    }

    public async Task<Guid> Insert(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        entities.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        var entity = entities.SingleOrDefault(s => s.Id == id);
        if (entity == null) throw new ArgumentException(nameof(id));
        entities.Remove(entity);
        context.SaveChanges();
    }
}