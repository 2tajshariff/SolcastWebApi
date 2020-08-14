using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using SolcastWebApi.DataAccess;  
using SolcastWebApi.Models;
using System;  
using System.Collections.Generic;
using System.Linq;

namespace SolcastWebApi.Controllers  
{  
    //[Route("api/[controller]")]  
    public class UsersController : ControllerBase  
    {  
        private readonly IDataAccessProvider _dataAccessProvider;  
  
        public UsersController(IDataAccessProvider dataAccessProvider)  
        {  
            _dataAccessProvider = dataAccessProvider;  
        }  
  
        [HttpGet]  
        [Route("[controller]/GetUser")]
        public IEnumerable<User> Get()  
        {  
            return _dataAccessProvider.GetUserRecords();  
        }  
          
        [HttpPost]  
        [Route("[controller]/CreateUser")]
        public IActionResult Create([FromBody]User user)  
        {  
            if (ModelState.IsValid)  
            {  
                // Guid obj = Guid.NewGuid();  
                // User.Id = obj;  
                _dataAccessProvider.AddUserRecord(user);  
                return Ok();  
            }  
            return BadRequest();  
        }  
  
        [HttpGet("{id}")]  
        public User Details(Guid id)  
        {  
            return _dataAccessProvider.GetUserSingleRecord(id);  
        }  
  
        [HttpPut]  
        public IActionResult Edit([FromBody]User user)  
        {  
            if (ModelState.IsValid)  
            {  
                _dataAccessProvider.UpdateUserRecord(user);  
                return Ok();  
            }  
            return BadRequest();  
        }  
  
        [HttpDelete("{id}")]  
        public IActionResult DeleteConfirmed(Guid id)  
        {  
            var data = _dataAccessProvider.GetUserSingleRecord(id);  
            if (data == null)  
            {  
                return NotFound();  
            }  
            _dataAccessProvider.DeleteUserRecord(id);  
            return Ok();  
        }  
    }  
}  