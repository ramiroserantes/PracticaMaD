using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.CategoryDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao&lt;Es.Udc.DotNet.PracticaMad.Model.Category, System.Int64&gt;" />
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        List<Category> FindAll();
    }
}
