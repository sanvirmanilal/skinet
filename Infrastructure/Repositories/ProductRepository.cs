using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository(StoreContext storeContext) : BaseRepository<Product>(storeContext)
{

}
