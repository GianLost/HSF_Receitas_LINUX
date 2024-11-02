using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hsf_Receitas.Models;

namespace Hsf_Receitas.Models
{
    public class AtestadoComparecimento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " Nome do paciente é obrigatório! "), StringLength(80)]
        public string PacientName { get; set; }

        [Required(ErrorMessage = " Turno é obrigatório! "), StringLength(20)]
        public string Turno { get; set; }

        public Receituario Receituario { get; set; }

        [ForeignKey("ReceituarioId")]
        public int ReceituarioId { get; set; }
    }
}
