using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.CommentDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao&lt;Es.Udc.DotNet.PracticaMad.Model.Comment, System.Int64&gt;" />
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        /// <summary>
        /// Finds the by photo identifier order by comment date.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Comment> FindByPhotoIdOrderByCommentDate(long productId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the by photo identifier and user identifier.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Comment FindByPhotoIdAndUserId(long photoId, long userId);
    }
}
