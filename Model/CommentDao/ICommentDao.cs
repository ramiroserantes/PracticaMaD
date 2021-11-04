using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        List<Comment> FindByPhotoIdOrderByCommentDate(long productId, int startIndex = 0, int count = 20);

        Comment FindByPhotoIdAndUserId(long photoId, long userId);
    }
}
