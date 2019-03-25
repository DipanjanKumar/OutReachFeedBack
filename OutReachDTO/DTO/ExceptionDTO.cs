using System;
using System.ComponentModel.DataAnnotations;

namespace OutReachDTO.DTO
{
    public class ExceptionDTO
    {
        [Required]
        [StringLength(100)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(100)]
        public string ActionrName { get; set; }

        [Required]
        public string ExceptionMessage { get; set; }

        [Required]
        public string ExceptionStackTrace { get; set; }
        [Required]
        public DateTime LogDateTime { get; set; }
    }
}
