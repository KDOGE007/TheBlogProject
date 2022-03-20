using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Models
{
    public class Tag
    {
        //Primary key
        public int Id { get; set; }

        //Foreign key
        public int PostId { get; set; }
        public string AuthorId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = ("The {0} must be at least {2} and no more than {1} characters long"),MinimumLength = 2)]
        public string Text { get; set; }

        //Navigation properties
        //Parents
        public virtual Post Post { get; set; }
        public virtual IdentityUser Author { get; set; }
    }
}
