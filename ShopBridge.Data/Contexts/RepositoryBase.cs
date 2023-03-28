using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShopBridge.Data.Contexts
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ShopBridgeContext _shopBridgeContext { get; set; }
        public RepositoryBase(ShopBridgeContext shopBridgeContext)
        {
            _shopBridgeContext = shopBridgeContext;
        }
        public IQueryable<T> FindAll() => _shopBridgeContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _shopBridgeContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _shopBridgeContext.Set<T>().Add(entity);
        public void Update(T entity) => _shopBridgeContext.Set<T>().Update(entity);
        public void Delete(T entity) => _shopBridgeContext.Set<T>().Remove(entity);
    }
}
