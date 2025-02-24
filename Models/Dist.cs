using System.ComponentModel.DataAnnotations;

namespace RKAS.Models
{
    public class Dist
    {
        [Key]
        public string ?Did{get;set;}
        public string ?Sid{get;set;}
        public string ?DName{get;set;}
    }
}