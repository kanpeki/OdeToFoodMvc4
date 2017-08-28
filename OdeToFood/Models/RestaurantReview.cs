﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
	public class RestaurantReview : IValidatableObject
    {
        public int Id { get; set; }   
    
        [Range(1,10)]
        [Required]
        public int Rating { get; set; }
        
        [StringLength(1024)]
        public string Body { get; set; }

        [Display(Name="User Name")]
        [DisplayFormat(NullDisplayText="anonymous")]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Rating < 2 && ReviewerName.ToLower().StartsWith("scott"))
            {
                yield return new ValidationResult("Sorry, Scott, you can't do this");
            }
        }
    }
}