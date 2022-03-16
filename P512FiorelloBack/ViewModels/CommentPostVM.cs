using System.ComponentModel.DataAnnotations;

namespace P512FiorelloBack.ViewModels
{
    public class CommentPostVM
    {
        [Required, MaxLength(500), MinLength(10)]
        public string Description { get; set; }
    }
}
