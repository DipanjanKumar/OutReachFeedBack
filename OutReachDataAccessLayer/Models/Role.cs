using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class Role
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }
        [StringLength(10)]
        public string RoleName { get; set; }    
    }
}
