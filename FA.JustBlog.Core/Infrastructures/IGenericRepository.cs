using System.Linq.Expressions;

namespace FA.JustBlog.Core.Infrastructures
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Change state of entity to added
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);

        /// <summary>
        ///  Change state of entities to added
        /// </summary>
        /// <param name="entities"></param>
        void CreateRange(List<TEntity> entities);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(params object[] ids);

        /// <summary>
        /// Change state of entity to modified
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Change state of entities to modified
        /// </summary>
        /// <param name="entity"></param>
        void UpdateRange(List<TEntity> entities);

        /// <summary>
        /// Get all <paramref name="TEntity"></paramref> from database
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includePropertie"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includePropertie = "");

        /// <summary>
        /// Get <paramref name="TEntity"></paramref> from database 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        TEntity? GetEntityById(params object[] primaryKey);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        //IEnumerable<TEntity> GetPaging(IOrderedEnumerable<TEntity> orderBy, int currentPage = PageConfig.CurentPage, int pageSize = PageConfig.PageSize, string filter = null);
    }
}