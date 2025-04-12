using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClientId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? ClientName { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? ClientSurname { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? ClientTelNo { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string ClientAdres { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string? ClientRequestMilk { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? ClientDeliverMilk { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? ClientPrice { get; set; }
    }
}
