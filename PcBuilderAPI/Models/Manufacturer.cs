using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PcBuilderAPI.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Components = new List<Component>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва виробника є обов'язковою")]
        public string Name { get; set; }

        public string? Website { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}