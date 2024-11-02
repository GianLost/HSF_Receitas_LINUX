function EditPrescription() {

  let properties = {
    Id: $("#up-prescriptionId").val(),
    PacientName: $("#up-pacientName").val(),
    DoctorName: $("#up-doctorName").val(),
    DateOfMedicalAppoiment: $("#up-dataAppoiment").val(),
  };

  $.post("/Receituario/CompletePrescription?id=" + properties.Id, properties)

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
  $("#up-receita-form").submit(function (e) {
    e.preventDefault();
    EditPrescription();
  });
});