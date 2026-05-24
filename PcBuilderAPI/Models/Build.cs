using System.ComponentModel.DataAnnotations;

namespace PcBuilderAPI.Models
{
    public class Build
    {
        public Build()
        {
            BuildComponents = new List<BuildComponent>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва збірки є обов'язковою")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<BuildComponent> BuildComponents { get; set; }
    }
}