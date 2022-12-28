﻿using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.IRepositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    IList<Category> GetAllCategories();
}
