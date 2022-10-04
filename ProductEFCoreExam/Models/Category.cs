using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProductEFCoreExam.Models;
using static System.Console;

namespace ProductEFCoreExam.Models
{
   
    internal class Category
    {
        
        [MaxLength(50)]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }

        


        public ICollection<Product> Products { get; set; } = new List<Product>();
        

    }
}
