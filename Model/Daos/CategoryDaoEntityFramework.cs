using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMad.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, long>, ICategoryDao
    {
        #region Public Constructors

        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

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
