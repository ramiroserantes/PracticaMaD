using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.TagDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao&lt;Es.Udc.DotNet.PracticaMad.Model.Tag, System.Int64&gt;" />
    public interface ITagDao : IGenericDao<Tag, long>
    {

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Tag> FindAll(int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        Tag FindByName(string tagName);

        int FindUsedTags(long tagId);
       
    }
}
