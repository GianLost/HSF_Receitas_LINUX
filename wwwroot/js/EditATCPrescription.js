function EditATCPrescription() {

  let properties = {
    Id: $("#atc-up-prescriptionId").val(),
    PacientName: $("#atc-up-pacientName").val(),
    DoctorName: $("#atc-up-doctorName").val(),
    DateOfMedicalAppoiment: $("#atc-up-dataAppoiment").val(),
  };

  $.post("/AtestadoComparecimento/ATCCompletePrescription?id=" + properties.Id, properties)

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
  $("#atc-up-receita-form").submit(function (e) {
    e.preventDefault();
    EditATCPrescription();
  });
});