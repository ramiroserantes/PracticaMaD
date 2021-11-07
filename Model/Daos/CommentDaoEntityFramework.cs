using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Es.Udc.DotNet.PracticaMad.Model.CommentDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework&lt;Es.Udc.DotNet.PracticaMad.Model.Comment, System.Int64&gt;" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.CommentDao.ICommentDao" />
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDaoEntityFramework"/> class.
        /// </summary>
        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICommentDao Members. Specific Operations

        /// <summary>
        /// Finds the by photo identifier order by comment date.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
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

        /// <summary>
        /// Finds the by photo identifier and user identifier.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
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
