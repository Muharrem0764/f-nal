using fınal.Models;
using fınal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace fınal.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TodoListAjax()
        {
            var todoModels = _context.Todos.Select(x => new TodoModel()
            {
                Id = x.Id,
                Title = x.Title,
                Status = x.Status,
            }).ToList();

            return Json(todoModels);
        }
        public IActionResult TodoByIdAjax(int id)
        {
            var todoModel = _context.Todos.Where(s => s.Id == id).Select(x => new TodoModel()
            {
                Id = x.Id,
                Title = x.Title,
                Status = x.Status,
            }).SingleOrDefault();

            return Json(todoModel);
        }

        [HttpPost]
        public IActionResult TodoAddEditAjax(TodoModel model)
        {
            var sonuc = new SonucModel();
            if (model.Id == 0)
            {

                if (_context.Todos.Count(c => c.Title == model.Title) > 0)
                {
                    sonuc.Status = false;
                    sonuc.Message = "Girilen Başlık Kayıtlıdır!";
                    return Json(sonuc);
                }

                var todo = new Todo();
                todo.Title = model.Title;
                todo.Status = model.Status;
                _context.Todos.Add(todo);
                _context.SaveChanges();
                sonuc.Status = true;
                sonuc.Message = "İşlem Eklendi";
            }
            else
            {
                var todo = _context.Todos.FirstOrDefault(x => x.Id == model.Id);
                todo.Status = model.Status;
                todo.Title = model.Title;
                _context.SaveChanges();
                sonuc.Status = true;
                sonuc.Message = "İşlem Güncellendi";
            }

            return Json(sonuc);
        }
        public IActionResult TodoRemoveAjax(int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();

            var sonuc = new SonucModel();
            sonuc.Status = true;
            sonuc.Message = "İşlem Silindi";
            return Json(sonuc);
        }
    }
}

