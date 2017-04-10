using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        #region Private Member(s)
        private readonly ITodoRepository _todoRepository;
        #endregion

        #region Constructor
        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        #endregion

        #region Public Member(s)
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _todoRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }
        #endregion


    }
}
