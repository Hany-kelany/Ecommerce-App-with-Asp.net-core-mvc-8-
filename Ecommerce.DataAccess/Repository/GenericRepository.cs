
namespace Ecommerce.DataAccess.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext context;

    public GenericRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public T Add(T item)
    {
        if (item == null) throw new ArgumentNullException("item", "item not found");
        context.Set<T>().Add(item);
        context.SaveChanges();
        return item;
    }

    public T Delete(int id)
    {

        var itemfromdb = context.Set<T>().Find(id);
        if (itemfromdb == null)
            throw new ArgumentNullException("itemfom");

        context.Set<T>().Remove(itemfromdb);
        context.SaveChanges();
        return itemfromdb;
    }

    public T Edit(T item, int id)
    {
        var itemfromdb = FindbyId(id);
        context.Entry(itemfromdb).CurrentValues.SetValues(item);
        context.Entry(itemfromdb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return item;
    }

    public T FindbyId(int id)
    {
        var itemfromdb = context.Set<T>().Find(id);
        if (itemfromdb == null)
        {
            throw new ArgumentNullException("itemfromdb");
        }
        return itemfromdb;
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }
}
