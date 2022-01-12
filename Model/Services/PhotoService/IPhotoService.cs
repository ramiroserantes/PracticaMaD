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

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{
    public interface IPhotoService
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
        /// Updates the photo details.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="photoDescription">The photo description.</param>
        [Transactional]
        void UpdatePhotoDetails(long photoId, string title, string photoDescription);

        /// <summary>
        /// Finds all photos.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        PhotoBlock FindAllPhotos(int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds all photos by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        PhotoBlock FindAllPhotosByTag(long tagId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds all photos by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        PhotoBlock FindAllPhotosByUser(long userId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds all photos by category and keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        PhotoBlock FindAllPhotosByCategoryAndKeyword(string keyword, long categoryId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds all photos by keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        PhotoBlock FindAllPhotosByKeyword(string keyword, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        Photo FindPhoto(long photoId);

     

        /// <summary>
        /// Generates the like.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="photoId">The photo identifier.</param>
        [Transactional]
        void GenerateLike(long userProfileId, long photoId);

        /// <summary>
        /// Deletes the like.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="photoId">The photo identifier.</param>
        [Transactional]
        void DeleteLike(long userProfileId, long photoId);

        /// <summary>
        /// Uploads the photo.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="f">The f.</param>
        /// <param name="t">The t.</param>
        /// <param name="iso">The iso.</param>
        /// <param name="wb">The wb.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Transactional]
        long UploadPhoto(string userName, string title, string description, long f, long t,
                        string iso, long wb, long categoryId, long userId, Image newImage);

        /// <summary>
        /// Deletes the photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        void DeletePhoto(long photoId, long userId);

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        long AddTag(string tagName);

        void AddPhotoTag(long tagId, long photoId);
        /// <summary>
        /// Finds the name of the tag by.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        Tag FindTagByName(string tagName);

        /// <summary>
        /// Finds all tags.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        TagBlock FindAllTags(int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        List<Category> FindAllCategories();

        /// <summary>
        /// Gets the photo likes.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        int GetPhotoLikes(long photoId);

        int GetUsedTag(long TagId);

        List<Tag> FindByPhotoTags(long photoId);

        void DeleteTag(long tagId, long photoId);

    }
}
