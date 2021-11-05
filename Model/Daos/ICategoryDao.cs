﻿using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMad.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        List<Category> FindAll();
    }
}