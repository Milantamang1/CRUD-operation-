using CMS.DbEntity;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class BlogCategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        public BlogCategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var list = new List<BlogCategoryModel>();
            var dbRecord = _dbContext.BlogCategory.ToList();
            foreach (var item in dbRecord)
            {
                var model = new BlogCategoryModel
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    CreatedOn = item.CreatedDate
                };
                list.Add(model);

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new BlogCategoryModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(BlogCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // var model = new BlogCategoryModel();
            var dbEntity = new BlogCategory
            {
                Name = model.Name,
                CreatedDate = DateTime.Now,
            };
            _dbContext.Add(dbEntity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int categoryId)
        {
            var dbEntity = _dbContext.BlogCategory.FirstOrDefault(e => e.CategoryId == categoryId);
            var model = new BlogCategoryModel();
            if (dbEntity != null)
            {
                model.Name = dbEntity.Name;
                model.CategoryId = dbEntity.CategoryId;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BlogCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // var model = new BlogCategoryModel();
            var dbEntity = _dbContext.BlogCategory.FirstOrDefault(e => e.CategoryId == model.CategoryId);
            if (dbEntity != null)
            {
                dbEntity.Name = model.Name;
                _dbContext.Update(dbEntity);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int categoryId)
        {
            var dbEntity = _dbContext.BlogCategory.FirstOrDefault(e => e.CategoryId == categoryId);
            if (dbEntity != null)
            {
                _dbContext.Remove(dbEntity);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}