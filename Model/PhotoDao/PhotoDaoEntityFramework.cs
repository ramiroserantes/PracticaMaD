﻿using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.PhotoDao
{
    public class PhotoDaoEntityFramework :
        GenericDaoEntityFramework<Photo, long>, IPhotoDao
    {
        #region Public Constructors
        public PhotoDaoEntityFramework()
        {
        }
        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations
        public Photo FindById(string photoId)
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

        public List<Photo> FindByTagId(long tagId, int startIndex = 0, int count = 20)
        {
            List<Photo> photo = null;

            #region Option 1: Using Linq.

            DbSet<Photo> photos = Context.Set<Photo>();

            var result =
                (from p in photos
                 where p.Comments.Any(c => c.Tags.Any(t => t.tagId == tagId))
                 orderby p.title ascending
                 select p).Skip(startIndex).Take(count);

            photo = result.ToList();


            #endregion Option 1: Using Linq.

            return photo;

        }

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

        #endregion  IUserProfileDao Members. Specific Operations
    }
}