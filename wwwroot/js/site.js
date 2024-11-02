function inserirRemedios() { // Captura o Receituario id e o passa para o campo correspondente no formulário para registro de uma nova medicação.
  var medicId = document.querySelector(".addRec-prescriptionId").value;
  document.querySelector(".addRec-recId").value = medicId;
}

function ATM() { // na View de Receituário + ATM esta função passa para o formulário de adicionar um novo ATM o nome do paciente a receber o atestado médico
  var pacientName = document.getElementById("atm-up-pacientName").value;
  document.getElementById("atm-pacientName").value = pacientName;
}

/* Mostrar dados do paciente em tempo real na vizualização do Receituário */

function ShowPrescriptionPacient() {
  var pacientName = document.querySelector(".s-pacientName").value;
  document.querySelector('#nameP').textContent = pacientName;
}

function ShowPrescriptionData() {
  var dataAppoiment = new Date(document.querySelector(".s-dataAppoiment").value);
  document.querySelector('#dataA').textContent = dataAppoiment.toLocaleDateString('pt-BR', { timeZone: 'UTC' });
}

function ShowPrescriptionMedic() {
  var doctorName = document.querySelector(".s-doctorName").value;
  document.querySelector('#nameM').textContent = doctorName;
}
