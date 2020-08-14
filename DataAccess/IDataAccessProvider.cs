using SolcastWebApi.Models;
using System;
using System.Collections.Generic;

namespace SolcastWebApi.DataAccess
{
    public interface IDataAccessProvider{
    void AddUserRecord(User user);  
        void UpdateUserRecord(User user);  
        void DeleteUserRecord(Guid id);  
        User GetUserSingleRecord(Guid id);  
        List<User> GetUserRecords();
    }
}