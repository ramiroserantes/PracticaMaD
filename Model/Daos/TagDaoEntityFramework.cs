using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMad.Model.TagDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.GenericDaoEntityFramework&lt;Es.Udc.DotNet.PracticaMad.Model.Tag, System.Int64&gt;" />
    /// <seealso cref="Es.Udc.DotNet.PracticaMad.Model.TagDao.ITagDao" />
    public class TagDaoEntityFramework :
         GenericDaoEntityFramework<Tag, long>, ITagDao
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDaoEntityFramework"/> class.
        /// </summary>
        public TagDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region ITagDao Members. Specific Operations
        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Tag> FindAll(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = new List<Tag>();

            #region Option 1: Using Linq.

            DbSet<Tag> dbTags = Context.Set<Tag>();

            var result =
                    (from t in dbTags
                     orderby t.tagName ascending
                     select t).Skip(startIndex).Take(count);

            tags = result.ToList<Tag>();

            #endregion Option 1: Using Linq.
            return tags;
        }


        public int FindUsedTags(long tagId)
        {
            DbSet<Tag> tag = Context.Set<Tag>();

            var result = (
                from t in tag
                where t.tagId == tagId
                select t);

            Tag tagr = result.FirstOrDefault();
            
            return tagr.Photo.Count; ;
        }


        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        public Tag FindByName(string tagName)
        {
            Tag tag = null;

            #region Option 1: Using Linq.

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 where t.tagName == tagName
                 select t);

            tag = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (tag == null)
                throw new InstanceNotFoundException(tagName,
                    typeof(Tag).FullName);

            return tag;
        }

        public List<Tag> FindByPhotoTags(long photoId)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            return (
                from t in tags
                where t.Photo.Any(p => p.photoId == photoId)
                select t).ToList();

        }

        public void DeletePhotoTag(long tagId, long photoId)
        {
            DbSet<Tag> tags = Context.Set<Tag>();
            DbSet<Photo> photos = Context.Set<Photo>();

            Tag tag = null;
            Photo photo = null;

            var result =
                (from t in tags
                 where t.tagId == tagId
                 select t);

            var result2 =
                (from u in photos
                 where u.photoId == photoId
                 select u);

            tag = result.FirstOrDefault();
            photo = result2.FirstOrDefault();

            tag.Photo.Remove(photo);
            this.Update(tag);

        }

        public void UpdatePhotoTag(long tagId, long photoId)
        {
            DbSet<Tag> tags = Context.Set<Tag>();
            DbSet<Photo> photos = Context.Set<Photo>();

            Tag tag = null;
            Photo photo = null;

            var result =
                (from t in tags
                 where t.tagId == tagId
                 select t);

            var result2 =
                (from p in photos
                 where p.photoId == photoId
                 select p);

            tag = result.FirstOrDefault();
            photo = result2.FirstOrDefault();

            tag.Photo.Add(photo);
            this.Update(tag);

        }
        #endregion ITagDaoMembers
    }
}
