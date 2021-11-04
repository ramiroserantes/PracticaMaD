using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.PhotoDao
{
    public interface IPhotoDao : IGenericDao<Photo, long>
    {
        Photo FindById(long photoId);

        Photo FindByTitle(string title);

        List<Photo> FindByTagId(long tagId, int startIndex = 0, int count = 20);

        List<Photo> FindAll(int startIndex = 0, int count = 20);
    }
}