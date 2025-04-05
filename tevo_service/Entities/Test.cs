using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class Test
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TestId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string TestName { get; set; }

    }
}
