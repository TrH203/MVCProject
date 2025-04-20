using JokeMVCApp.Data;
using JokeMVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JokeMVCApp.Controllers;

public class BooksController: Controller{
    private readonly AppDbContext _context;
    public BooksController(AppDbContext context)
    {
        _context = context;
    }
    public ViewResult Index(){
        IEnumerable<Books> objCategoryList = _context.Books;
        return View(objCategoryList);
    }

    //GET
    [Authorize]
    public ViewResult Create(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Books obj){
        if (obj.DisplayOrder.ToString() == obj.Name){
            ModelState.AddModelError("CustomError", "The DisplayOrder cannot = Name");
        }
        if(ModelState.IsValid){
            _context.Books.Add(obj);
            _context.SaveChanges();
            TempData["Success"] = "Create Sucessfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    //GET
    [Authorize]
    public IActionResult Edit(int? id){
        if (id == null || id<=0){
            return NotFound();
        }
        var bookFromDb = _context.Books.Find(id);

        if (bookFromDb == null){
            return NotFound();
        }
        else{
            return View(bookFromDb);
        }

    }

    [HttpPost,Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Books obj){
        if (obj.DisplayOrder.ToString() == obj.Name){
            ModelState.AddModelError("CustomError", "The DisplayOrder cannot = Name");
        }
        if(ModelState.IsValid){
            _context.Books.Update(obj);
            _context.SaveChanges();
            TempData["Success"] = "Edit Sucessfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    //GET
    [Authorize]
    public IActionResult Delete(int? id){
        if (id == null || id <= 0){
            return NotFound();
        }
        var bookFromDb = _context.Books.Find(id);

        if (bookFromDb == null){
            return NotFound();
        }
        else{
            return View(bookFromDb);
        }
    }

    [HttpPost,Authorize,ActionName("Delete")]
    public IActionResult Delete(Books obj){
        var book = _context.Books.Find(obj.Id);
        if (book == null){
            return NotFound();
        }
        _context.Books.Remove(book);
        _context.SaveChanges();
        TempData["Success"] = "Delete Sucessfully";
        return RedirectToAction("Index");
    }
    public IActionResult Search(){
        return View(new BooksSearchViewModel());
    }
    [HttpGet,ActionName("SearchQuery")]
    public IActionResult Search(string? query){
        var results = _context.Books.Where(b => !string.IsNullOrEmpty(query) && b.Name.Contains(query)).ToList();

        var model = new BooksSearchViewModel
        {
            Name = query,
            Results = results
        };

        return View("Search",model);
    }

    [HttpPost]
    public IActionResult Search(BooksSearchViewModel model){
        model.Results = _context.Books.Where(b => b.Name.Contains(model.Name)).ToList();
        return View(model);
    }
}