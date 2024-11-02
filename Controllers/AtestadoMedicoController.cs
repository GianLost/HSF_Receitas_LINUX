using System;
using System.Collections.Generic;
using System.IO;
using FastReport.Export.PdfSimple;
using Hsf_Receitas.Models;
using Hsf_Receitas.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hsf_Receitas.Controllers
{
    public class AtestadoMedicoController : Controller
    {
        private readonly ILogger<ReceituarioController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly MedicacaoServices _MedicacaoServices;
        private readonly ReceituarioServices _ReceituarioServices;
        private readonly AtestadoMedicoServices _AtestadoMedicoServices;

        public AtestadoMedicoController(
            ILogger<ReceituarioController> logger,
            IWebHostEnvironment environment,
            ReceituarioServices receituarioServices,
            MedicacaoServices medicacaoServices,
            AtestadoMedicoServices atestadoMedicoServices
        )
        {
            _logger = logger;
            _environment = environment;
            _ReceituarioServices = receituarioServices;
            _MedicacaoServices = medicacaoServices;
            _AtestadoMedicoServices = atestadoMedicoServices;
        }

        public IActionResult ATMRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ATMRegister(AtestadoMedico novoATM)
        {
            try
            {
                _AtestadoMedicoServices.AddATM(novoATM);
                return Json(new { stats = "OK" });
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao adicionar atestado médico!" + e.Message);
                return Json(new { stats = "INVALID", message = "Falha ao gerar atestado médico!" });
            }
        }

        public IActionResult ATMPrescription()
        {
            return View();
        }

        public IActionResult ATMCompletePrescription(int id)
        {
            Receituario buscaReceita =  _ReceituarioServices.SearchForId(id);
            return View(buscaReceita);
        }

        [HttpPost]
        public IActionResult ATMCompletePrescription(Receituario editReceita)
        {
            try
            {
                _ReceituarioServices.EditReceita(editReceita);
                return Json(new { stats = "OK" });
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao completar o receituário + atestado médico !" + e.Message);
                return Json(new{stats = "INVALID", message = "falha ao salvar alterações na receita do atestado médico!"});
            }
        }

        [HttpGet]
        public IActionResult ATMCreateReport()
        {
            try
            {
                string reportFile = Path.Combine(_environment.WebRootPath, @"Print_Files\ATM_Report.frx");

                FastReport.Report r = new FastReport.Report();

                ICollection<Medicacao> medicationList = _MedicacaoServices.ListMedication();
                ICollection<Receituario> prescriptionList = _ReceituarioServices.ListPrescription();
                ICollection<AtestadoMedico> atmList = _AtestadoMedicoServices.ListATM();

                r.Report.Dictionary.RegisterBusinessObject(
                    medicationList,
                    "medicationList",
                    10,
                    true
                );
                r.Report.Dictionary.RegisterBusinessObject(
                    prescriptionList,
                    "prescriptionList",
                    10,
                    true
                );
                r.Report.Dictionary.RegisterBusinessObject(atmList, "atmList", 10, true);

                r.Report.Save(reportFile);

                return Ok($"OK! {reportFile}");
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao rerar report de receituário + Atestado médico via FastReporter !"+ e.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ATMPrescriptionPrintToPDF(int id)
        {
            try
            {
                string reportFile = Path.Combine(_environment.WebRootPath, @"Print_Files/Rec_Atm.frx");

                FastReport.Report r = new FastReport.Report();

                ICollection<Receituario> prescriptionList = _ReceituarioServices.ListPrescriptionsForId(id);
                ICollection<Medicacao> medicationList = _MedicacaoServices.ListMedicationPrescriptions(id);
                ICollection<AtestadoMedico> atmList = _AtestadoMedicoServices.ListATMPrescriptions(id);

                r.Report.Load(reportFile);
                r.Report.Dictionary.RegisterBusinessObject(
                    prescriptionList,
                    "prescriptionList",
                    10,
                    true
                );
                r.Report.Dictionary.RegisterBusinessObject(
                    medicationList,
                    "medicationList",
                    10,
                    true
                );
                r.Report.Dictionary.RegisterBusinessObject(atmList, "atmList", 10, true);
                r.Prepare();

                PDFSimpleExport pdfExport = new PDFSimpleExport();
                using MemoryStream ms = new MemoryStream();

                pdfExport.Export(r, ms);

                ms.Flush();

                return File(ms.ToArray(), "application/pdf");
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao gerar Receita + atestado médico em PDF !" + e.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
