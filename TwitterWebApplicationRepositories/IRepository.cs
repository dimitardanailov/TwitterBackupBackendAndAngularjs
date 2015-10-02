using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TwitterWebApplicationRepositories
{
    /// <summary>
    /// We will use IRepository to describe main database queries
    /// 
    /// <see cref="http://blog.sapiensworks.com/post/2012/03/05/The-Generic-Repository-Is-An-Anti-Pattern.aspx/"/>
    /// <see cref="https://irepository.codeplex.com/"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/ff649690.aspx"/>
    /// <see cref="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">Build where clause for query</param>
        /// <param name="orderBy">Build order by clause for query</param>
        /// <param name="includeProperties">Storing, which fields should to be returned.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetEntities(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        );

        /// <summary>
        /// Get Record by record id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Create a new Item
        /// </summary>
        /// <param name="item"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete item by Entity reference
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// Change state of entity
        /// </summary>
        /// <param name="entity"></param>
        void Detach(TEntity entity);
    }
}
