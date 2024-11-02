using System;
using Hsf_Receitas.Data;
using Hsf_Receitas.Models;
using Hsf_Receitas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hsf_Receitas.Controllers
{
    public class MedicacaoController : Controller
    {
        private readonly ILogger<MedicacaoController> _logger;
        private readonly MedicacaoServices _MedicacaoServices;
        private readonly HSFContext _database;

        public MedicacaoController(ILogger<MedicacaoController> logger, MedicacaoServices medicacaoServices, HSFContext hSFContext)
        {
            _logger = logger;
            _MedicacaoServices = medicacaoServices;
            _database = hSFContext;
        }

        [HttpPost]
        public IActionResult MedicationRegister(Medicacao novaMedicacao)
        {
            try
            {
                _MedicacaoServices.AddMedicacao(novaMedicacao);
                return Json(new { stats = "OK" });
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao adicionar medicamento!" + e.Message);
                return Json(new { stats = "INVALID", message = "Falha ao inserir medicamento!" });
            }
        }

        [HttpPost]
        public IActionResult DeleteMedication(int id)
        {
            try
            {
                var item = _database.Medicamentos.Find(id);

                if (item != null)
                {
                    _MedicacaoServices.DelMedication(item.Id);
                    return Json(new { stats = "OK" });
                }
                else
                {
                    return Json(new { stats = "INVALID" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao excluir medicamento do Receituário!" + e.Message);
                return Json(new { stats = "ERROR", message = "Falha ao excluir medicamento!" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
