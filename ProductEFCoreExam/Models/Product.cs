using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductEFCoreExam.Models
{

    internal class Product
    {


        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }

    }
}
