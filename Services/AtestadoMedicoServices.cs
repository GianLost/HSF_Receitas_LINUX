using System.Collections.Generic;
using System.Linq;
using Hsf_Receitas.Data;
using Hsf_Receitas.Models;

namespace Hsf_Receitas.Services
{
    public class AtestadoMedicoServices
    {
        public void AddATM(AtestadoMedico novoAtestado)
        {
            using HSFContext database = new HSFContext();

            database.AtestadosMedico.Add(novoAtestado);
            database.SaveChanges();
        }

        public List<AtestadoMedico> ListATM()
        {
            using HSFContext database = new HSFContext();
            return database.AtestadosMedico.ToList();
        }

        public List<AtestadoMedico> ListATMPrescriptions(int id)
        {
            using HSFContext database = new HSFContext();

            Receituario rec = new Receituario();

            List<AtestadoMedico> foundATM = database.AtestadosMedico
                .Where(m => m.ReceituarioId == id)
                .ToList();

            return foundATM;
        }
    }
}
