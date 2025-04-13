using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? ProductName { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Unit { get; set; }
    }
}
