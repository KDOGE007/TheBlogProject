using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be atleast {2} and no more than {1} characters long", MinimumLength = 1)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be atleast {2} and no more than {1} characters long", MinimumLength = 1)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public byte[] Image { get; set; }
        public string ContentType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and no more than {1} characters long", MinimumLength = 1)]
        public string FacebookUrl { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} and no more than {1} characters long", MinimumLength = 1)]
        public string TwitterUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //Navigation property
        //Children
        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
