﻿using Microsoft.AspNetCore.Mvc;
using WebApiDemo1225.Dtos;
using WebApiDemo1225.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo1225.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<StudentDto> Get()
        {
            var items = _studentService.GetAll();
            var dataToReturn = items.Select(s =>
            {
                return new StudentDto
                {
                    Id = s.Id,
                    Age = s.Age,
                    Fullname = s.Fullname,
                    Score = s.Score,
                    SeriaNo = s.SeriaNo
                };
            });
            return dataToReturn;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public StudentDto Get(int id)
        {
            var item=_studentService.Get(id);
            return new StudentDto
            {
                 Age = item.Age,
                  Fullname = item.Fullname,
                   Score = item.Score,
                    Id = item.Id,
                     SeriaNo= item.SeriaNo
                      
            };
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
