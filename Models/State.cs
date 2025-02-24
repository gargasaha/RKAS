using System.ComponentModel.DataAnnotations;

namespace RKAS.Models
{
    public class State
    {
        [Key]
        public string ?Sid{get;set;}
        public string ?Sname{get;set;}
    }
}