using System.ComponentModel.DataAnnotations;

namespace PcBuilderAPI.Models
{
    public class BuildComponent
    {
        [Key]
        public int Id { get; set; }

        public int BuildId { get; set; }
        public virtual Build Build { get; set; }

        public int ComponentId { get; set; }
        public virtual Component Component { get; set; }

        public int Quantity { get; set; }
    }
}