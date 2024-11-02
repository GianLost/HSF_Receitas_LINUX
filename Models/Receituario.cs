using System;
using System.ComponentModel.DataAnnotations;

namespace Hsf_Receitas.Models
{
    public class Receituario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " Nome do paciente é obrigatório! "), StringLength(80)]
        public string PacientName { get; set; }

        [Required(ErrorMessage = " Nome do médico é obrigatório! "), StringLength(80)]
        public string DoctorName { get; set; } [ Required(ErrorMessage = "O campo Data do atendimento obrigatório"), DataType(DataType.Date) ]
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfMedicalAppoiment { get; set; }
    }
}
