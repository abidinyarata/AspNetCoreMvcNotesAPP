using AspNetCoreMvcNotesAPP.Context;
using AspNetCoreMvcNotesAPP.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreMvcNotesAPP.DataAccess
{
    public class CategoryRepository : IRepository<Category>
    {
        private DatabaseContext _db = new DatabaseContext();

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category Insert(Category category)
        {
            _db.Categories.Add(category);
            if (_db.SaveChanges() > 0)
            {
                return category;
            }

            return null;
        }

        public bool IsExistsByCategoryName(string categoryName)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());
        }

        public bool IsExistsByCategoryName(string categoryName, int excludeCategoryId)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower() && c.Id != excludeCategoryId);
        }

        public bool IsExistsByCategoryDesc(string desc)
        {
            return _db.Categories.Any(c => c.Desc.ToLower() == desc.ToLower());
        }

        public Category GetById(int id)
        {
            return _db.Categories.Find(id);
        }

        public Category Update(int id, Category category)
        {
            Category categoryDb = GetById(id);

            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.Desc = category.Desc;
                categoryDb.IsHidden = category.IsHidden;

                if (_db.SaveChanges() > 0)
                    return categoryDb;
            }

            return null;
        }

        public int Delete(int id)
        {
            Category category = GetById(id);

            if (category == null)
            {
                return 1;
            }

            _db.Categories.Remove(category);

            return _db.SaveChanges();
        }
    }
}
