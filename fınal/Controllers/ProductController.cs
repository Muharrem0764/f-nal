using Microsoft.AspNetCore.Mvc;
using fınal.Models;
using fınal.ViewModels;

namespace final.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            var productsModel = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Status = x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName

            }).ToList();
            return View(productsModel);
        }
        public IActionResult Detail(int id)
        {
            var productModel = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Status = x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName

            }).SingleOrDefault(x => x.Id == id);
            return View(productModel);

        }
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(ProductModel model)
        {
            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Status = model.Status;
            product.CategoryId = model.CategoryId;
            product.CategoryName = model.CategoryName;
            

                _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var productModel = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Status = x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
               


            }).SingleOrDefault(x => x.Id == id);
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Status = model.Status;
            product.CategoryId = model.CategoryId;
            product.CategoryName = model.CategoryName;

            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var productModel = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Status = x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName

            }).SingleOrDefault(x => x.Id == id);
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Delete(ProductModel model)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == model.Id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult ChangeStatus(int id, bool st)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            product.Status = st;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
       
        
        public IActionResult PageList(string searchText = "", int page = 1, int size = 6)
        {
            var products = _context.Products.Where(s => s.Name.ToLower().Contains(searchText.ToLower())).AsQueryable();

            int pageskip = (page - 1) * size;
            var productModels = products.Skip(pageskip).Take(size).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToList();
            int recordCount = products.Count();
            int pageCount = (int)Math.Ceiling(Convert.ToDecimal(recordCount / size));


            ViewBag.PageCount = pageCount;
            ViewBag.RecordCount = recordCount;
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.SearchText = searchText;

            return View(productModels);
        }
    }
}