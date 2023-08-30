using Microsoft.AspNetCore.Mvc;
using TodoAppWeb.Models;

namespace TodoAppWeb
{
    [Controller]
    public class ToDoController : Controller
    {
        private readonly ToDoContext toDoContext;

        public ToDoController(ToDoContext toDoContext)
        {
            this.toDoContext = toDoContext;
        }

        // Create an HTTP Get endpoint to return a List of ToDos from a Database
        // The endpoint should return a 200
        // The endpoint should return the ToDos as JSON
        [HttpGet]
        public ActionResult Index()
        {
            var todos = this.toDoContext.ToDos.ToList();

            // Filter all todos except completed ones
            todos = todos.Where(todo => todo.IsComplete == false).ToList();

            return View(todos);
        }

        // Create an HTTP Get endpoint to return a ToDo from a Database by Id
        // The endpoint should return a 404 if the ToDo is not found
        // The endpoint should return a 200 if the ToDo is found
        // The endpoint should return the ToDo as JSON
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = this.toDoContext.ToDos.Find(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPut]
        public IActionResult Update(int id, ToDo todo)
        {
            var todoToUpdate = this.toDoContext.ToDos.Find(id);

            if (todoToUpdate == null)
            {
                return NotFound();
            }

            // update Name of todoToUpdate entity
            todoToUpdate.Name = todo.Name;
            
            this.toDoContext.SaveChanges();
            return NoContent();
        }

        [HttpPost("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var todoToDelete = this.toDoContext.ToDos.Find(id);

            if (todoToDelete == null)
            {
                return NotFound();
            }

            this.toDoContext.ToDos.Remove(todoToDelete);
            this.toDoContext.SaveChanges();

            // redirect to Index action
            return RedirectToAction("Index");
        }

        // Create an Http Post method Complete
        // The endpoint should return a 404 if the ToDo is not found
        // The endpoint should return a 200 if the ToDo is found
        // The endpoint should return the ToDo as JSON
        [HttpPost("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var todo = this.toDoContext.ToDos.Find(id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = true;
            this.toDoContext.SaveChanges();

            // redirect to Index action
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(ToDo todo)
        {
            // Add validation for case if todo is null
            if (todo == null)
            {
                return BadRequest();
            }

            // Add timestamp to todo Name
            todo.Name = $"{todo.Name} - {DateTime.Now}";
            this.toDoContext.ToDos.Add(todo);
            this.toDoContext.SaveChanges();

            // Replace return result with Redirect to Index
            return RedirectToAction("Index");
        }
    }
}
