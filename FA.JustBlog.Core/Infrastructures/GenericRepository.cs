using FA.JustBlog.Core.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.Infrastructures;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    internal JustBlogContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(JustBlogContext injected)
    {
        context = injected;
        dbSet = context.Set<TEntity>();
    }

    public virtual void Create(TEntity entity) => dbSet.Add(entity);

    public void CreateRange(List<TEntity> entities) => dbSet.AddRange(entities);

    public void Delete(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
            dbSet.Attach(entity);

        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(params object[] ids)
    {
        TEntity? entity = dbSet.Find(ids);
        if (entity is null)
            throw new ArgumentException($"{string.Join(";", ids)} not exist in the {typeof(TEntity).Name} table");

        Delete(entity);
    }

    public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate) => dbSet.Where(predicate);

    public virtual IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includePropertie = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter is not null)
            query = query.Where(filter);

        foreach (string? includeProperty in includePropertie
            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy is not null)
            return orderBy(query).ToList();
        else
            return query.ToList();
    }

    public virtual TEntity? GetEntityById(params object[] primaryKey) => dbSet.Find(primaryKey);

    public virtual void Update(TEntity entity) => dbSet.Update(entity);

    public void UpdateRange(List<TEntity> entities) => dbSet.UpdateRange(entities);
}
