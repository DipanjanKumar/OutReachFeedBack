using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class User
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string AssociateID { get; set; }
        [Required]
        [StringLength(50)]
        public string AssociateName { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
        [ForeignKey("Event")]
        public string EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
