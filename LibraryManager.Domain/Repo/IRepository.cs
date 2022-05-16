namespace LibraryManager.Domain;

public interface IRepository<T> where T : BaseEntity {  
    IQueryable<T> GetAll();
    Task<T> Get(Guid id);  
    Task<Guid> Insert(T entity);  
    Task Update(T entity);
    Task Delete(Guid id);  
} 

