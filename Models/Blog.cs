using Microsoft.AspNetCore.Http;
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
    public class Blog
    {
        //Primary Key
        public int Id { get; set; }
        //Foreign key
        public string AuthorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime? Updated { get; set; }

        [DisplayName("Blog Image")]
        public byte[] ImageData { get; set; }
        [DisplayName("Image Type")]
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        //Navigation properties
        //Parents
        public virtual BlogUser Author { get; set; }
        //Children
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    }
}
