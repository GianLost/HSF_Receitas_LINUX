function AddATM() {

  let properties = {
    Id: $("#atmId").val(),
    PacientName: $("#atm-pacientName").val(),
    MedicUnity: $("#medicUnity").val(),
    CID: $("#atm-cid").val(),
    RestDays: $("#restDays").val(),
    ReceituarioId: $("#atm-receituarioId-medic").val(),
  };

  $.post("/AtestadoMedico/ATMRegister", properties)

    .done(function (output) {
      if (output.stats == "OK") {

        alert("Atestado de: " + properties.PacientName + " gerado com sucesso!")
        $(location).attr('href', '/AtestadoMedico/ATMCompletePrescription?id=' + parseInt(properties.ReceituarioId));

      } else if (output.stats == "INVALID") {
        setTimeout(function () {
          $(".alerta").html('<div class="alert alert-danger"> Não foi possível cadastrar essa medicação. Tente mais tarde!</div>').fadeOut(5000);
        }, 80);
      }
    })

    .fail(function () {
      alert("Falha ao adicionar medicamento!");
    });
}

$(document).ready(function () {
  $(".atmButtonPOST").click(function (e) {
    e.preventDefault();
    AddATM();
  });
});