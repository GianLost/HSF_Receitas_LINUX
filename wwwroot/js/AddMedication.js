function AddMedications() {

  let properties = {
    Id: $("#medicationId").val(),
    MedicationName: $("#medicationName").val(),
    Presentation: $("#presentation").val(),
    RouteOfAdministration: $("input[type=radio][name=administration]:checked").val(),
    Posology: $("#posology").val(),
    DurationOfTreatment: $("#durationTreatment").val(),
    ReceituarioId: $(".addRec-recId").val(),
  };

  if (properties.RouteOfAdministration == "outros") {
    properties.RouteOfAdministration = $("#other").val();
  }

  $.post("/Medicacao/MedicationRegister", properties)

    .done(function (output) {

      if (output.stats == "OK") {

        alert("Medicação: " + properties.MedicationName + " - " + properties.Presentation + " cadastrada com sucesso!")
        location.reload();

      } else if (output.stats == "INVALID") {
        $(".alerta").html('<div class="alert alert-danger"> Não foi possível cadastrar essa medicação. Tente mais tarde!</div>');

      }

    })

    .fail(function () {
      alert("Falha ao adicionar medicamento!");
    });
}

$("#other").click(function (e) {
  if (!$('#outros').is(':checked')) {
    e.preventDefault();
    $('#outros').attr('checked', 'true');
  }
});

$(document).ready(function () {
  $(".buttonPOST").click(function (e) {
    e.preventDefault();
    AddMedications();
  });
});