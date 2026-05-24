using System.ComponentModel.DataAnnotations;

namespace PcBuilderAPI.Models
{
    public class Component
    {
        public Component()
        {
            BuildComponents = new List<BuildComponent>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва комплектуючого є обов'язковою")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути більшою за 0")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }

        public virtual ICollection<BuildComponent> BuildComponents { get; set; }
    }
}