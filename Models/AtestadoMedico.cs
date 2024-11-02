using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hsf_Receitas.Models
{
    public class AtestadoMedico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " Nome do paciente é obrigatório! "), StringLength(80)]
        public string PacientName { get; set; }

        [Required(ErrorMessage = " Unidade médica é obrigatória! "), StringLength(40)]
        public string MedicUnity { get; set; }

        [Required(ErrorMessage = " CID é obrigatório! "), StringLength(20)]
        public string CID { get; set; }

        public int RestDays { get; set; }

        public Receituario Receituario { get; set; }

        [ForeignKey("ReceituarioId")]
        public int ReceituarioId { get; set; }
    }
}
