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

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{

    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.PhotoService.IPhotoService" />
    public class PhotoService : IPhotoService
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

        #region IPhotoService Members

        #region Photo Members

        /// <summary>
        /// Updates the photo details.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="photoDescription">The photo description.</param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException">
        /// </exception>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <exception cref="DuplicateInstanceException"></exception>
        [Transactional]
        public void UpdatePhotoDetails(long photoId, string title, string photoDescription)
        {
            Photo photo = PhotoDao.Find(photoId);

            if (!photo.title.Equals(title))
            {
                try
                {
                    PhotoDao.FindByTitle(title);

                    throw new DuplicateInstanceException(title,
                    typeof(Photo).FullName);
                }
                catch (InstanceNotFoundException)
                {
                    photo.title = title;
                }
            }

            if (!photo.photoDescription.Equals(photoDescription))
            {
                try
                {
                    PhotoDao.FindByPhotoDescription(photoDescription);

                    throw new DuplicateInstanceException(photoDescription,
                    typeof(Photo).FullName);
                }
                catch (InstanceNotFoundException)
                {
                    photo.photoDescription = photoDescription;
                }
            }

            PhotoDao.Update(photo);
        }

        /// <summary>
        /// Finds all photos.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public PhotoBlock FindAllPhotos(int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindAll(startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        /// <summary>
        /// Finds all photos by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public PhotoBlock FindAllPhotosByTag(long tagId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByTagId(tagId);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        /// <summary>
        /// Finds all photos by category and keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public PhotoBlock FindAllPhotosByCategoryAndKeyword(string keyword, long categoryId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByCategoryAndKeywords(keyword, categoryId,
                startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        /// <summary>
        /// Finds the photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        public Photo FindPhoto(long photoId)
        {
            return PhotoDao.Find(photoId);
        }

        /// <summary>
        /// Finds all photos by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public PhotoBlock FindAllPhotosByUser(long userId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByUserId(userId,
               startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        /// <summary>
        /// Gets the photo likes.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        public int GetPhotoLikes(long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);

            return photo.UserProfile1.Count();
        }

        /// <summary>
        /// Generates the like.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="photoId">The photo identifier.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void GenerateLike(long userProfileId, long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            List<Photo> likeListUser = new List<Photo>(userProfile.Photo1);

            bool containsItem = likeListUser.Any(p => p.photoId == photoId);
            if (!containsItem)
            {
                likeListUser.Add(photo);
                ICollection<Photo> newLikeList = new List<Photo>(likeListUser);
              
                userProfile.Photo1.Clear();
                userProfile.Photo1 = newLikeList;

                UserProfileDao.Update(userProfile);

            }

        }

        /// <summary>
        /// Deletes the like.
        /// </summary>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="photoId">The photo identifier.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public void DeleteLike(long userProfileId, long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            List<Photo> likeListUser = new List<Photo>(userProfile.Photo1);

            if (likeListUser.Any(u => u.photoId == photoId))
            {
                ICollection<Photo> newLikeList = new List<Photo>();
                foreach (Photo p in likeListUser)
                {
                    if (p.photoId != photoId)
                        newLikeList.Add(PhotoDao.Find(p.photoId));
                }

                userProfile.Photo1.Clear();
                userProfile.Photo1 = newLikeList;

                UserProfileDao.Update(userProfile);

            }
        }

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
        public long UploadPhoto(string title, string description, long f,
                                long t, string iso, long wb, long categoryId, long userId)
        {
            Photo newPhoto = new Photo
            {
                title = title,
                photoDescription = description,
                photoDate = System.DateTime.Now,
                f = f,
                t = t,
                iso = iso,
                wb = wb,
                categoryId = categoryId,
                userId = userId,
            };

            PhotoDao.Create(newPhoto);

            newPhoto.link = @"c:\Resource\" + newPhoto.photoId.ToString();

            PhotoDao.Update(newPhoto);

            return newPhoto.photoId;
        }

        /// <summary>
        /// Deletes the photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        public void DeletePhoto(long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);
            PhotoDao.Remove(photo.photoId);

        }

        #endregion Photo Members

        #region Comment Members

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

        #endregion Comment Members

        #region Tag Members

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.DuplicateInstanceException"></exception>
        /// <exception cref="DuplicateInstanceException"></exception>
        public long AddTag(string tagName, long userId)
        {
            Tag tag = new Tag();
            try
            {
                TagDao.FindByName(tagName);

                throw new DuplicateInstanceException(tagName,
                    typeof(Tag).FullName);
            }
            catch (InstanceNotFoundException)
            {
                tag.tagName = tagName;
                tag.userId = userId;

                TagDao.Create(tag);
            }
            return tag.tagId;
        }

        /// <summary>
        /// Finds the name of the tag by.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Tag FindTagByName(string tagName)
        {
            return TagDao.FindByName(tagName);
        }

        /// <summary>
        /// Finds all tags.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public TagBlock FindAllTags(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = TagDao.FindAll(startIndex, count + 1);

            bool existMoreTags = (tags.Count == count + 1);

            if (existMoreTags) tags.RemoveAt(count);

            return new TagBlock(tags, existMoreTags);
        }

        #endregion Tag Members

        #region Category Members

        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> FindAllCategories()
        {
            return CategoryDao.FindAll();
        }

        #endregion Category Members

        #endregion IPhotoService Members
    }
}
