using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMad.Model.PhotoDao
{
    /// <seealso cref="Es.Udc.DotNet.ModelUtil.Dao.IGenericDao&lt;Es.Udc.DotNet.PracticaMad.Model.Photo, System.Int64&gt;" />
    public interface IPhotoDao : IGenericDao<Photo, long>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        Photo FindById(long photoId);

        /// <summary>
        /// Finds the by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        Photo FindByTitle(string title);
        /// <summary>
        /// Finds the by photo description.
        /// </summary>
        /// <param name="photoDescription">The photo description.</param>
        /// <returns></returns>
        Photo FindByPhotoDescription(string photoDescription);

        /// <summary>
        /// Finds the by tag identifier.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Photo> FindByTagId(long tagId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Photo> FindByUserId(long userId, int startIndex = 0, int count = 20);
        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Photo> FindAll(int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds the by category and keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Photo> FindByCategoryAndKeywords(string keywords, long categoryId, int startIndex = 0, int count = 20);

    }
}