function deleteMedication(idItem) {

  let properties = {
    Id: $(".delete-Id").val(),
  };

  properties.Id = idItem;

  console.log("Id a ser excluído: " + properties.Id)

  $.post("/Medicacao/DeleteMedication", properties)

    .done(function (output) {
      if (output.stats == "OK") {

        alert("Medicação excluída com suesso !");

      } else if (output.stats == "INVALID") {
        alert("Falha ao tentar excluir a medicação. Tente novamente em alguns instantes !");
      }

      location.reload();

    })

    .fail(function () {
      alert("Falha na conexão. Erro ao excluir medicação, tente novamente em alguns instantes!");
    });
}

$(document).ready(function () {
  $(".medication-delete").submit(function (e) {
    e.preventDefault();
    deleteMedication(idItem);
  });
});
