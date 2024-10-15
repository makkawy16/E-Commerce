

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        [Required]  
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        public string? Img { get; set; }
        [Required]
        public string? Price { get; set; }

        [Required]
        [DisplayName("Category")]
        [ValidateNever] 
        public int CategoryId { get; set; } 
        public Category? Category { get; set; }      



       

       }
    }


