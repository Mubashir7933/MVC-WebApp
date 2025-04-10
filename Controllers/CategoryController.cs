using Microsoft.AspNetCore.Mvc;
using MyWebApp.DataAccess.Data;    // for ApplicationDbContext
using MyWebApp.Models;  // for Category model
using MyWebApp.DataAccess.Repositories.IRepository;

namespace MyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
         
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: CategoryController
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll(c => true);
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
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
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
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == id);
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
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();  
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
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == id);
        if(categoryFromDb==null)
        {
            return NotFound();
        }
            return View (categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
        Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);    
            if(obj == null )
            {
            return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save(); 
               TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction ("Index");
            
        }
    }
}
