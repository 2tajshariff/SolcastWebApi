using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolcastWebApi.Models{
    public class User{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public Guid Id{get;set;}
        public string Name{get;set;}
        public string Email{get;set;}
    }
}