using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;    // for ApplicationDbContext
using MyWebApp.Models;  // for Category model

namespace MyApp.Namespace
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: CategoryController
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);  // Also passing the list to the view
        }
        public IActionResult Create()
        {
            return View ();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString()){
                ModelState.AddModelError("Name", "The display order and name cannot be the same");
            }
            if(ModelState.IsValid){
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Created Successfully";
            return RedirectToAction ("Index","Category");
            }
              TempData["Error"] = "Failed to create category.";
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
            return NotFound();
        }
        Category? categoryFromDb = _db.Categories.Find(id);
        if(categoryFromDb==null)
        {
            return NotFound();
        }
            return View (categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid){

            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Updated Successfully";
            return RedirectToAction ("Index");
            }
             TempData["Error"] = "Failed to update category.";
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id==null || id==0){
            return NotFound();
        }
        Category? categoryFromDb = _db.Categories.Find(id);
        if(categoryFromDb==null)
        {
            return NotFound();
        }
            return View (categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
        Category? obj = _db.Categories.Find(id);
            if(obj == null )
            {
            return NotFound();
                }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
               TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction ("Index");
            
        }
    }
}
