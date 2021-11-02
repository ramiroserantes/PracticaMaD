using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.PhotoDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.PhotoService
{
    public interface IPhotoService
    {

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

        PhotoBlock FindAllPhotosByCategory(long categoryId, int startIndex = 0, int count = 20);

        Photo FindPhoto(long photoId);

        [Transactional]
        Comment FindCommentById(long commentId);

        long AddComment(long photoId, long userId, string commentBody);

        long AddComment(long photoId, long userId, string commentBody, List<long> tags);

        void DeleteComment(long commentId);

        void UpdateComment(long commentId, string commentBody);

        void UpdateComment(long commentId, string commentBody, List<long> newTags);

        [Transactional]
        CommentBlock FindAllPhotoComments(long photoId, int startIndex = 0, int count = 20);

        [Transactional]
        Comment FindCommentByPhotoAndUser(long photoId, long userId);

        long AddTag(string tagName);

        Tag FindTagByName(string tagName);

        TagBlock FindAllTags(int startIndex = 0, int count = 20);

        List<Category> FindAllCategories();

    }
}
