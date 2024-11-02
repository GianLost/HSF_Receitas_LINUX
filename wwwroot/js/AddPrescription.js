function AddPrescription() {

  let properties = {
    Id: $("#prescriptionId").val(),
    PacientName: $("#pacientName").val(),
    DoctorName: $("#doctorName").val(),
    DateOfMedicalAppoiment: $("#dataAppoiment").val(),
  };

  $.post("/Receituario/Prescription", properties)

    .done(function (output) {
      if (output.stats == "OK") {

        $(location).attr('href', '/Receituario/CompletePrescription?id=' + output.id);

      } else if (output.stats == "INVALID") {
        setTimeout(function () {
          $(".alerta").html('<div class="alert alert-danger"> Não foi possível salvar esta receita. Tente novamente mais tarde!</div>').fadeOut(5000);
        }, 80);
      }

    })

    .fail(function () {
      alert("Falha ao salvar receita!");
      localtion.reload();
    });
}

$(document).ready(function () {
  $("#receita-form").submit(function (e) {
    e.preventDefault();
    AddPrescription();
  });
});