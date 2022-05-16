namespace LibraryManager.Domain;

public interface IRepository <T> where T : BaseEntity {  
    IQueryable<T> GetAll();  
    T Get(Guid id);  
    void Insert(T entity);  
    void Update(T entity);  
    void Delete(Guid id);  
} 

