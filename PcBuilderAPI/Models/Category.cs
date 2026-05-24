using System.ComponentModel.DataAnnotations;

namespace PcBuilderAPI.Models
{
    public class Category
    {
        public Category()
        {
            Components = new List<Component>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва категорії обов'язкова")]
        [Display(Name = "Категорії комплектуючих")]
        public string Name { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}