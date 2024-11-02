using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hsf_Receitas.Models
{
    public class Medicacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do medicamento é obrigatório! "), StringLength(80)]
        public string MedicationName { get; set; }

        [Required(ErrorMessage = "Apresentação do medicamento é obrigatória! "), StringLength(40)]
        public string Presentation { get; set; }

        [Required(ErrorMessage = "Via de administração do medicamento é obrigatório! "), StringLength(40)]
        public string RouteOfAdministration { get; set; }

        [Required(ErrorMessage = "Posologia do medicamento é obrigatório! "), StringLength(40)]
        public string Posology { get; set; }

        [Required(ErrorMessage = "Duração do tratamento do medicamento é obrigatório! "), StringLength(40)]
        public string DurationOfTreatment { get; set; }

        public Receituario Receituario { get; set; }

        [ForeignKey("ReceituarioId")]
        public int ReceituarioId { get; set; }
    }
}
