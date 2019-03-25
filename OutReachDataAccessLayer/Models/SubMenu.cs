using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class SubMenu
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubMenuId { get; set; }

        [Required]
        [StringLength(20)]
        public string SubMenuName { get; set; }

        [Required]
        [StringLength(100)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(100)]
        public string ActionrName { get; set; }

        [Required]
        [ForeignKey("MainMenu")]
        public int MainMenuId { get; set; }
        public virtual MainMenu MainMenu { get; set; }        
    }
}
