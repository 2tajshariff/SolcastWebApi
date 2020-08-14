using SolcastWebApi.Models;
using System;
using System.Collections.Generic;  
using System.Linq;  
  
namespace SolcastWebApi.DataAccess  
{  
    public class DataAccessProvider: IDataAccessProvider  
    {  
        private readonly SolcastWebApiContext _context;  
  
        public DataAccessProvider(SolcastWebApiContext context)  
        {  
            _context = context;  
        }  
  
        public void AddUserRecord(User user)  
        {  
            _context.Users.Add(user);  
            _context.SaveChanges();  
        }  
  
        public void UpdateUserRecord(User user)  
        {  
            _context.Users.Update(user);  
            _context.SaveChanges();  
        }  
  
        public void DeleteUserRecord(Guid id)  
        {  
            var entity = _context.Users.FirstOrDefault(t => t.Id == id);  
            _context.Users.Remove(entity);  
            _context.SaveChanges();  
        }  
  
        public User GetUserSingleRecord(Guid id)  
        {  
            return _context.Users.FirstOrDefault(t => t.Id == id);  
        }  
  
        public List<User> GetUserRecords()  
        {  
            return _context.Users.ToList();  
        }  
    }  
}  