using Hsf_Receitas.Models;
using Hsf_Receitas.Data;
using System.Collections.Generic;
using System.Linq;

namespace Hsf_Receitas.Services
{
    public class ReceituarioServices
    {
        public void AddReceita(Receituario novaReceita)
        {
            HSFContext database = new HSFContext();
            database.Receitas.Add(novaReceita);
            database.SaveChanges();
        }

        public void EditReceita(Receituario editreceita)
        {
            using HSFContext database = new HSFContext();

            Receituario rec = database.Receitas.Find(editreceita.Id);

            rec.PacientName = editreceita.PacientName;
            rec.DoctorName = editreceita.DoctorName;
            rec.DateOfMedicalAppoiment = editreceita.DateOfMedicalAppoiment;

            database.SaveChanges();
        }

        public List<Receituario> ListPrescription()
        {
            using HSFContext database = new HSFContext();
            return database.Receitas.ToList();
        }

        public List<Receituario> ListPrescriptionsForId(int id)
        {
            using HSFContext database = new HSFContext();

            List<Receituario> prescriptionList = new List<Receituario>();

            Receituario foundPrescription = database.Receitas
                .Where(p => p.Id == id)
                .SingleOrDefault();

            prescriptionList.Add(foundPrescription);

            return prescriptionList;
        }

        public Receituario SearchForId(int id)
        {
            using HSFContext dataBase = new HSFContext();
            return dataBase.Receitas.Find(id);
        }
    }
}
