using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMad.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMad.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.PracticaMad.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMad.Model.TagDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoService
{
    public interface IPhotoService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        IPhotoDao PhotoDao { set; }

        [Inject]
        ICommentDao CommentDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }

        [Transactional]
        void UpdatePhotoDetails(long photoId, string title, string photoDescription);

        PhotoBlock FindAllPhotos(int startIndex = 0, int count = 20);

        PhotoBlock FindAllPhotosByTag(long tagId, int startIndex = 0, int count = 20);

        PhotoBlock FindAllPhotosByUser(long userId, int startIndex = 0, int count = 20);

        PhotoBlock FindAllPhotosByCategoryAndKeyword(string keyword, long categoryId, int startIndex = 0, int count = 20);

        Photo FindPhoto(long photoId);

        [Transactional]
        Comment FindCommentById(long commentId);

        long AddComment(long photoId, long userId, string commentBody);

        void DeleteComment(long commentId);

        void UpdateComment(long commentId, string commentBody);

        [Transactional]
        CommentBlock FindAllPhotoComments(long photoId, int startIndex = 0, int count = 20);

        [Transactional]
        Comment FindCommentByPhotoAndUser(long photoId, long userId);

        [Transactional]
        void GenerateLike(long userProfileId, long photoId);

        [Transactional]
        void DeleteLike(long userProfileId, long photoId);

        long UploadPhoto(long userId, string title, string description, long f, long t,
                        string iso, long wb, long categoryId);

        void DeletePhoto(long photoId);

        long AddTag(string tagName);

        Tag FindTagByName(string tagName);

        TagBlock FindAllTags(int startIndex = 0, int count = 20);

        List<Category> FindAllCategories();

        int getPhotoLikes(long photoId);

    }
}
