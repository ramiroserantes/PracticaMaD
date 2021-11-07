using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework&lt;Es.Udc.DotNet.PracticaMad.Model.Photo, System.Int64&gt;" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.PhotoDao.IPhotoDao" />
    public class PhotoDaoEntityFramework :
        GenericDaoEntityFramework<Photo, long>, IPhotoDao
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoDaoEntityFramework"/> class.
        /// </summary>
        public PhotoDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public Photo FindById(long photoId)
        {
            Photo photo = null;

            #region Option 1: Using Linq.
            DbSet<Photo> userProfiles = Context.Set<Photo>();

            var result =
                (from u in userProfiles
                 where u.photoId == photoId
                 select u);

            photo = result.FirstOrDefault();

            #endregion Option 1: Using Linq.
            if (photo == null)
                throw new InstanceNotFoundException(photoId,
                    typeof(Photo).FullName);

            return photo;
        }


        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Photo> FindByUserId(long userId, int startIndex = 0, int count = 20) 
        {
            List<Photo> photos = null;

            #region Option 1: Using Linq.
            DbSet<Photo> userPhotos = Context.Set<Photo>();

            var result =
                (from p in userPhotos 
                 where p.userId == userId
                 orderby p.photoDate descending
                 select p).Skip(startIndex).Take(count);

            #endregion Option 1: Using Linq.

            photos = result.ToList();

            return photos;
        }
        /// <summary>
        /// Finds the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public Photo FindByTitle(string title)
        {
            Photo photo = null;

            #region Option 1: Using Linq.

            DbSet<Photo> userProfiles = Context.Set<Photo>();

            var result =
                (from u in userProfiles
                 where u.title == title
                 select u);

            photo = result.FirstOrDefault();

            #endregion Option 1: Using Linq.
            if (photo == null)
                throw new InstanceNotFoundException(title,
                    typeof(Photo).FullName);

            return photo;
        }

        /// <summary>
        /// Finds the by photo description.
        /// </summary>
        /// <param name="photoDescription">The photo description.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public Photo FindByPhotoDescription(string photoDescription)
        {
            Photo photo = null;

            #region Option 1: Using Linq.

            DbSet<Photo> userProfiles = Context.Set<Photo>();

            var result =
                (from u in userProfiles
                 where u.photoDescription == photoDescription
                 select u);

            photo = result.FirstOrDefault();

            #endregion Option 1: Using Linq.
            if (photo == null)
                throw new InstanceNotFoundException(photoDescription,
                    typeof(Photo).FullName);

            return photo;
        }

        /// <summary>
        /// Finds the by tag identifier.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Photo> FindByTagId(long tagId, int startIndex = 0, int count = 20)
        {
            List<Photo> photo = null;

            #region Option 1: Using Linq.

            DbSet<Photo> photos = Context.Set<Photo>();

            var result =
                (from p in photos
                 where p.Tag.Any(t => t.tagId == tagId)
                 orderby p.title ascending
                 select p).Skip(startIndex).Take(count);

            photo = result.ToList();


            #endregion Option 1: Using Linq.

            return photo;

        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Photo> FindAll(int startIndex, int count)
        {

            #region Option 1: Using Linq.
            DbSet<Photo> photos = Context.Set<Photo>();

            var result =
                (from p in photos
                 orderby p.photoDate ascending
                 select p).Skip(startIndex).Take(count);
            #endregion Option 1: Using Linq.

            return result.ToList();
        }

        /// <summary>
        /// Finds the by category and keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Photo> FindByCategoryAndKeywords(string keywords, long categoryId, int startIndex = 0, int count = 20)
        {
            List<Photo> photo = null;

            #region Option 1: Using Linq.

            DbSet<Photo> photos = Context.Set<Photo>();

            var result =
                (from p in photos
                where ((p.title.ToLower().Contains(keywords.ToLower()) ||
                (p.photoDescription.ToLower().Contains(keywords.ToLower())) &&
                (p.categoryId == categoryId)))
                orderby p.photoDate descending
                select p).Skip(startIndex).Take(count);

            #endregion Option 1: Using Linq.

            photo = result.ToList();

            return photo;

        }
        #endregion  IUserProfileDao Members. Specific Operations


    }

  
}