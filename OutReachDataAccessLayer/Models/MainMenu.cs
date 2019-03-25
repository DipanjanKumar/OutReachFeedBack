using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class MainMenu
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MainMenuId { get; set; }
        [Required]
        [StringLength(20)]
        public string MainMenuName { get; set; }
    }
}

