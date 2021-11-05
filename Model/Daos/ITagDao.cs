using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, long>
    {
   
        List<Tag> FindAll(int startIndex = 0, int count = 20);

        Tag FindByName(string tagName);
    }
}
