using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Es.Udc.DotNet.PracticaMad.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        #region Public Constructors

        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICommentDao Members. Specific Operations

        public List<Comment> FindByPhotoIdOrderByCommentDate(long photoId, int startIndex = 0, int count = 20)
        {
            List<Comment> comments = null;

            #region Option 1: Using Linq.

            DbSet<Comment> commentsFound = Context.Set<Comment>();

            var result =
                (from c in commentsFound
                 where c.photoId == photoId
                 orderby c.commentDate descending
                 select c).Skip(startIndex).Take(count);

            comments = result.ToList();

            #endregion Option 1: Using Linq.

            if (comments == null)
                throw new InstanceNotFoundException(photoId,
                    typeof(Comment).FullName);

            return comments;
        }

        public Comment FindByPhotoIdAndUserId(long photoId, long userId)
        {
            Comment comment = null;

            #region Option 1: Using Linq.

            DbSet<Comment> commentsFound = Context.Set<Comment>();

            var result =
                (from c in commentsFound
                 where (c.photoId == photoId && c.userId == userId)
                 select c);

            comment = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (comment == null)
                throw new InstanceNotFoundException(photoId,
                    typeof(Comment).FullName);

            return comment;
        }

        #endregion ICommentDao Members. Specific Operations
    }
}
