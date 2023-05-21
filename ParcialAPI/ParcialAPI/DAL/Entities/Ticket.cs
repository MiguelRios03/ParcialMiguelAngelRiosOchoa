using System.ComponentModel.DataAnnotations;

namespace ParcialAPI.DAL.Entities
{
    public class Ticket
    {
        [Key]
        public Guid ID { get; set; }

        [Display(Name ="Fecha uso")]
        public DateTime? UseDate { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio.")]
        public Boolean IsUsed { get; set; }

        [Display(Name = "Entrada")]
        [MaxLength(10, ErrorMessage ="El campo {0} debe tener máximo {1} caracteres.")]
        public string? EntranceGate { get; set; }
    }
}
