using System;
using System.Collections.Generic;
using System.Linq;
using Hsf_Receitas.Data;
using Hsf_Receitas.Models;

namespace Hsf_Receitas.Services
{
    public class AtestadoComparecimentoServices
    {
        public void AddATC(AtestadoComparecimento novoAtestadoMedico)
        {
            using HSFContext database = new HSFContext();

            database.AtestadosComparecimento.Add(novoAtestadoMedico);
            database.SaveChanges();
        }

        public List<AtestadoComparecimento> ListATC()
        {
            using HSFContext database = new HSFContext();
            return database.AtestadosComparecimento.ToList();
        }

        public List<AtestadoComparecimento> ListATCPrescriptions(int id)
        {
            using HSFContext database = new HSFContext();

            Receituario rec = new Receituario();

            List<AtestadoComparecimento> foundATC = database.AtestadosComparecimento
                .Where(m => m.ReceituarioId == id)
                .ToList();

            return foundATC;
        }
    }
}
