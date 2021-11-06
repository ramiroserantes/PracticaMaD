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
    public class PhotoService : IPhotoService
    {
        [Inject]
        public IPhotoDao PhotoDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        #region IPhotoService Members

        #region Photo Members

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
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

            PhotoDao.Update(photo);
        }

        public PhotoBlock FindAllPhotos(int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindAll(startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        public PhotoBlock FindAllPhotosByTag(long tagId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByTagId(tagId);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        public PhotoBlock FindAllPhotosByCategoryAndKeyword(string keyword, long categoryId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByCategoryAndKeywords(keyword, categoryId,
                startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        public Photo FindPhoto(long photoId)
        {
            return PhotoDao.Find(photoId);
        }

        public PhotoBlock FindAllPhotosByUser(long userId, int startIndex = 0, int count = 20)
        {
            List<Photo> photos = PhotoDao.FindByUserId(userId,
               startIndex, count + 1);

            bool existMorePhotos = (photos.Count == count + 1);

            if (existMorePhotos) photos.RemoveAt(count);

            return new PhotoBlock(photos, existMorePhotos);
        }

        public int getPhotoLikes(long photoId)
        {
            return PhotoDao.findPhotoLikes(photoId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void GenerateLike(long userProfileId, long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            List<Photo> likeListUser = new List<Photo>(userProfile.Photo1);

            if (!likeListUser.Any(u => u.photoId == photoId))
            {
                likeListUser.Add(photo);
                ICollection<Photo> newLikeList = new List<Photo>();
                foreach (Photo p in likeListUser)
                {
                    newLikeList.Add(PhotoDao.Find(p.photoId));
                }

                userProfile.Photo1.Clear();
                userProfile.Photo1 = newLikeList;

                UserProfileDao.Update(userProfile);

                // actualizamos la lista de perfiles que le dieron like a la foto
                List<UserProfile> likeListPhoto = new List<UserProfile>(photo.UserProfile1);

                likeListPhoto.Add(userProfile);
                ICollection<UserProfile> newLikeList2 = new List<UserProfile>();
                foreach (UserProfile p1 in likeListPhoto)
                {
                    newLikeList2.Add(UserProfileDao.Find(p1.userId));
                }

                photo.UserProfile1.Clear();
                photo.UserProfile1 = newLikeList2;

                PhotoDao.Update(photo);
            }

        }

        /// <exception cref="InstanceNotFoundException"/>
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

                // actualizamos la lista de perfiles que le dieron like a la foto
                List<UserProfile> likeListPhoto = new List<UserProfile>(photo.UserProfile1);

                ICollection<UserProfile> newLikeList2 = new List<UserProfile>();
                foreach (UserProfile p1 in likeListPhoto)
                {
                    if (p1.userId != userProfileId)
                        newLikeList2.Add(UserProfileDao.Find(p1.userId));
                }

                photo.UserProfile1.Clear();
                photo.UserProfile1 = newLikeList2;

                PhotoDao.Update(photo);
            }
        }

        public long UploadPhoto(long userId, string title, string description, long f,
                                long t, string iso, long wb, long categoryId)
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

            return newPhoto.photoId;
        }

        public void DeletePhoto(long photoId)
        {
            Photo photo = PhotoDao.Find(photoId);
            PhotoDao.Remove(photo.photoId);

        }

        #endregion Photo Members

        #region Comment Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Comment FindCommentById(long commentId)
        {
            return CommentDao.Find(commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
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


        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteComment(long commentId)
        {
            var comment = CommentDao.Find(commentId);
            CommentDao.Remove(comment.commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, string commentBody)
        {
            Comment comment = CommentDao.Find(commentId);

            comment.commentDescription = commentBody;

            CommentDao.Update(comment);
        }

      

        public CommentBlock FindAllPhotoComments(long photoId, int startIndex = 0, int count = 20)
        {
            List<Comment> comments = CommentDao.FindByPhotoIdOrderByCommentDate(photoId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments) comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        /// <exception cref="InstanceNotFoundException" />
        public Comment FindCommentByPhotoAndUser(long photoId, long userId)
        {
            return CommentDao.FindByPhotoIdAndUserId(photoId, userId);
        }

        #endregion Comment Members

        #region Tag Members

        /// <exception cref="DuplicateInstanceException"/>
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

        /// <exception cref="InstanceNotFoundException"/>
        public Tag FindTagByName(string tagName)
        {
            return TagDao.FindByName(tagName);
        }

        public TagBlock FindAllTags(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = TagDao.FindAll(startIndex, count + 1);

            bool existMoreTags = (tags.Count == count + 1);

            if (existMoreTags) tags.RemoveAt(count);

            return new TagBlock(tags, existMoreTags);
        }

        #endregion Tag Members

        #region Category Members

        public List<Category> FindAllCategories()
        {
            return CategoryDao.FindAll();
        }

        #endregion Category Members

        #endregion IPhotoService Members
    }
}
