namespace AuthExample.Infrastructure.Interfaces;
public interface IBaseRepository<T> where T : class
{
    // exemplo de como poderia "obrigar" implementar metodos
    //Task<T> AddAsync(T entity);
    //Task<T> UpdateAsync(T entity);
    //Task<T> DeleteAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
}
