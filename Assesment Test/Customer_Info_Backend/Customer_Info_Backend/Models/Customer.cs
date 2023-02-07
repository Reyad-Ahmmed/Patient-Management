using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Customer_Info_Backend.Models
{
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required,ForeignKey("Countries")]
        public int CountryId { get; set; }
        [StringLength(100),Column(TypeName = "nvarchar")]
        public string CustomerName { get; set; }
        [StringLength(100), Column(TypeName = "nvarchar")]
        public string FatherName { get; set; }
        [StringLength(100), Column(TypeName = "nvarchar")]
        public string MotherName { get; set;}
        public int MaritalStatus { get; set; }
        [Column(TypeName = "nvarchar(max)"),AllowNull]
        public string CustomerPhoto { get; set; }
    }
}
