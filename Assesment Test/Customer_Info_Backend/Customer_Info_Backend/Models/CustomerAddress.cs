using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Info_Backend.Models
{
    public class CustomerAddresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,ForeignKey("Customers")]
        public int CustomerId { get; set; }
        [StringLength(500),Column(TypeName = "nvarchar")]
        public string CustomerAddress { get; set; }

    }
}
