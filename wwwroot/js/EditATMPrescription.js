function EditATMPrescription() {

  let properties = {
    Id: $("#atm-up-prescriptionId").val(),
    PacientName: $("#atm-up-pacientName").val(),
    DoctorName: $("#atm-up-doctorName").val(),
    DateOfMedicalAppoiment: $("#atm-up-dataAppoiment").val(),
  };

  $.post("/AtestadoMedico/ATMCompletePrescription?id=" + properties.Id, properties)

    .done(function (output) {
      if (output.stats == "OK") {

        setTimeout(function () {
          $(".alerta")
            .html(
              '<div class="alert alert-success"> Receita atualizada com Sucesso! </div>'
            )
            .fadeOut(5000);
        }, 80);

      } else if (output.stats == "INVALID") {
        $(".alerta").html('<div class="alert alert-danger"> Não foi possível salvar esta receita. Tente novamente mais tarde!</div>');

      }
    })

    .fail(function () {
      alert("Falha ao salvar receita!");
    });
}

$(document).ready(function () {
  $("#atm-up-receita-form").submit(function (e) {
    e.preventDefault();
    EditATMPrescription();
  });
});