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
    public T Get(Guid id)
    {
        return entities.Single(s => s.Id == id);
    }
    public void Insert(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        entities.Add(entity);
        context.SaveChanges();
    }
    public void Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.SaveChanges();
    }
    public void Delete(Guid id)
    {
        var entity = entities.SingleOrDefault(s => s.Id == id);
        if (entity == null) throw new ArgumentException(nameof(id));
        entities.Remove(entity);
        context.SaveChanges();
    }
}