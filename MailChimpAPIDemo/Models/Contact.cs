using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace MailChimpAPIDemo.Models
{
    public class Contact
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Email]
        public string EmailAddress { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}