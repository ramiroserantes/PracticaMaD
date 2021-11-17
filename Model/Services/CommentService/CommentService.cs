using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Ninject;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Es.Udc.DotNet.PracticaMad.Model.CommentService
{


        /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.PhotoService.IPhotoService" />
        public class CommentService : ICommentService
        {

            /// <summary>
            /// Sets the photo DAO.
            /// </summary>
            /// <value>
            /// The photo DAO.
            /// </value>
            [Inject]
            public IPhotoDao PhotoDao { private get; set; }

            /// <summary>
            /// Sets the user profile DAO.
            /// </summary>
            /// <value>
            /// The user profile DAO.
            /// </value>
            [Inject]
            public IUserProfileDao UserProfileDao { private get; set; }

            /// <summary>
            /// Sets the comment DAO.
            /// </summary>
            /// <value>
            /// The comment DAO.
            /// </value>
            [Inject]
            public ICommentDao CommentDao { private get; set; }

            /// <summary>
            /// Sets the tag DAO.
            /// </summary>
            /// <value>
            /// The tag DAO.
            /// </value>
            [Inject]
            public ITagDao TagDao { private get; set; }

            /// <summary>
            /// Sets the category DAO.
            /// </summary>
            /// <value>
            /// The category DAO.
            /// </value>
            [Inject]
            public ICategoryDao CategoryDao { private get; set; }
            /// <summary>
            /// Finds the comment by identifier.
            /// </summary>
            /// <param name="commentId">The comment identifier.</param>
            /// <returns></returns>
            /// <exception cref="InstanceNotFoundException"></exception>
            [Transactional]
        public Comment FindCommentById(long commentId)
        {
            return CommentDao.Find(commentId);
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commentBody">The comment body.</param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public long AddComment(long photoId, long userId, string commentBody)
        {
            Comment comment = new Comment
            {
                commentDescription = commentBody,
                commentDate = System.DateTime.Now,
                userId = userId,
                photoId = photoId
            };

            CommentDao.Create(comment);

            return comment.commentId;
        }


        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void DeleteComment(long commentId)
        {
            var comment = CommentDao.Find(commentId);
            CommentDao.Remove(comment.commentId);
        }

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentBody">The comment body.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void UpdateComment(long commentId, string commentBody)
        {
            Comment comment = CommentDao.Find(commentId);

            comment.commentDescription = commentBody;

            CommentDao.Update(comment);
        }



        /// <summary>
        /// Finds all photo comments.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public CommentBlock FindAllPhotoComments(long photoId, int startIndex = 0, int count = 20)
        {
            List<Comment> comments = CommentDao.FindByPhotoIdOrderByCommentDate(photoId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments) comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        /// <summary>
        /// Finds the comment by photo and user.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Comment FindCommentByPhotoAndUser(long photoId, long userId)
        {
            return CommentDao.FindByPhotoIdAndUserId(photoId, userId);
        }
    }
}
