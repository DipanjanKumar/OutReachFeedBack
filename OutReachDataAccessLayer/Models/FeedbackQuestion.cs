using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class FeedbackQuestion
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionNumber { get; set; }

        [StringLength(200)]
        public string QuestionText { get; set; }
    }
}
