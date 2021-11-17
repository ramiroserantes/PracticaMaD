using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Ninject;
using System.Collections.Generic;
using System.Drawing;


namespace Es.Udc.DotNet.PracticaMad.Model.CommentService
{
    public interface ICommentService
    {
        /// <summary>
        /// Sets the user profile DAO.
        /// </summary>
        /// <value>
        /// The user profile DAO.
        /// </value>
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Sets the photo DAO.
        /// </summary>
        /// <value>
        /// The photo DAO.
        /// </value>
        [Inject]
        IPhotoDao PhotoDao { set; }

        /// <summary>
        /// Sets the comment DAO.
        /// </summary>
        /// <value>
        /// The comment DAO.
        /// </value>
        [Inject]
        ICommentDao CommentDao { set; }

        /// <summary>
        /// Sets the tag DAO.
        /// </summary>
        /// <value>
        /// The tag DAO.
        /// </value>
        [Inject]
        ITagDao TagDao { set; }

        /// <summary>
        /// Sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        [Inject]
        ICategoryDao CategoryDao { set; }
        /// <summary>
        /// Finds the comment by identifier.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        [Transactional]
        Comment FindCommentById(long commentId);

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commentBody">The comment body.</param>
        /// <returns></returns>
        long AddComment(long photoId, long userId, string commentBody);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        void DeleteComment(long commentId);

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentBody">The comment body.</param>
        void UpdateComment(long commentId, string commentBody);

        /// <summary>
        /// Finds all photo comments.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [Transactional]
        CommentBlock FindAllPhotoComments(long photoId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the comment by photo and user.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Transactional]
        Comment FindCommentByPhotoAndUser(long photoId, long userId);
    }
}
