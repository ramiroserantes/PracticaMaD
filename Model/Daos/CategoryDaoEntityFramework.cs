using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMad.Model.CategoryDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework&lt;Es.Udc.DotNet.PracticaMad.Model.Category, System.Int64&gt;" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.CategoryDao.ICategoryDao" />
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, long>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDaoEntityFramework"/> class.
        /// </summary>
        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        public List<Category> FindAll()
        {
            List<Category> tags = new List<Category>();

            #region Option 1: Using Linq.

            DbSet<Category> dbCategories = Context.Set<Category>();

            var result =
                    (from c in dbCategories
                     orderby c.categoryType ascending
                     select c);

            tags = result.ToList<Category>();

            #endregion Option 1: Using Linq.

            return tags;
        }

        #endregion ICategoryDao Members. Specific Operations
    }
}
