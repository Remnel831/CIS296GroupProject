﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Incident
    {
		public int IncidentID { get; set; }
        [Required(ErrorMessage = "Please enter the title of the incident.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please describe what is wrong.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the date the the incident happened.")]
        public DateTime DateOpened { get; set; } = DateTime.Now;
        public DateTime? DateClosed { get; set; } = null;

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerID { get; set; }                   // foreign key property
        [ValidateNever]
        public Customer Customer { get; set; } = null!;       // navigation property

        [Required(ErrorMessage = "Please select a product.")]
        public int ProductID { get; set; }                    // foreign key property
        [ValidateNever]
        public Product Product { get; set; } = null!;         // navigation property

        public int TechnicianID { get; set; } = -1;               // foreign key property 
        [ValidateNever]
        public Technician Technician { get; set; } = null!;   // navigation property

		
	}
}