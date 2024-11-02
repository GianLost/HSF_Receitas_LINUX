function AddATCPrescription() {

  let properties = {
    Id: $("#atc-prescriptionId").val(),
    PacientName: $("#atc-pacientName").val(),
    DoctorName: $("#atc-doctorName").val(),
    DateOfMedicalAppoiment: $("#atc-dataAppoiment").val(),
  };

  $.post("/Receituario/Prescription", properties)

    .done(function (output) {
      if (output.stats == "OK") {

        $(location).attr('href', '/AtestadoComparecimento/ATCCompletePrescription?id=' + output.id);

      } else if (output.stats == "INVALID") {
        $(".alerta").html('<div class="alert alert-danger"> Não foi possível salvar esta receita. Tente novamente mais tarde!</div>');

      }
    })

    .fail(function () {
      alert("Falha ao salvar receita!");
    });
}

$(document).ready(function () {
  $("#atc-receita-form").submit(function (e) {
    e.preventDefault();
    AddATCPrescription();
  });
});