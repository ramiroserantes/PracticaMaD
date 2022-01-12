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
using System.Text;

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

        public PhotoBlock FindAllPhotosByKeyword(string keyword, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByKeywords(keyword, startIndex, count + 1);

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

            List<Photo> likeListUser = UserProfileDao.FindByLikedPhotos(userProfileId);

            bool containsItem = likeListUser.Any(p => p.photoId == photoId);
            if (!containsItem)
            {
                UserProfileDao.UpdateLikePhoto(userProfileId, photoId);
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

            List<Photo> likeListUser = UserProfileDao.FindByLikedPhotos(userProfileId);

            if (likeListUser.Any(u => u.photoId == photoId))
            {
                UserProfileDao.DeleteLikePhoto(userProfileId, photoId);
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
        /// 


        public long UploadPhoto(string userName, string title, string description, long f,
                                long t, string iso, long wb, long categoryId, long userId, Image newImage)
        {


            Photo newPhoto = new Photo
            {
                userName = userName,
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

            newPhoto.link = "C:/EntregaMaD/PracticaMaD/Web/Images/" + newPhoto.photoId.ToString() + ".jpg";
            newImage.Save(newPhoto.link, System.Drawing.Imaging.ImageFormat.Png);

            PhotoDao.Update(newPhoto);

            return newPhoto.photoId;
        }

        /// <summary>
        /// Deletes the photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeletePhoto(long photoId, long userId)
        {
            Photo photo = PhotoDao.Find(photoId);
            
            if (userId == UserProfileDao.FindByPhotoId(photoId))
            {
                File.Delete(photo.link);
                PhotoDao.Remove(photo.photoId);
            }
            

        }

        #endregion Photo Members

        #region Comment Members

        

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
        public long AddTag(string tagName)
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

        public void AddPhotoTag(long tagId, long photoId) {

            Photo p = PhotoDao.Find(photoId);

            Tag t = TagDao.Find(tagId);

            p.Tag.Add(t);
        }

        public int GetUsedTag(long TagId) {

            return TagDao.FindUsedTags(TagId);
        
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
