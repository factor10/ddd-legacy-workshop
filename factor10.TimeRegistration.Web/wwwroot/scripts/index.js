setCurrentDateInDatePicker();
function setCurrentDateInDatePicker() {
  var dateControl = document.getElementById("input-date");
  dateControl.value = new Date().toISOString().substring(0, 10);
}

getAllConsultantsAndAddToUi();
function getAllConsultantsAndAddToUi() {
  httpGet("consultants")
    .then(response => response.json())
    .then(response => {
      var consultants = response;
      var consultantsElement = document.getElementById("select-consultant");
      consultants.forEach(consultant => {
        consultantsElement.innerHTML += `<option value="${consultant.id}">${consultant.person.fullName}</option>`;
      });
    });
}

function displayRegistrations() {
  var consultantId = document.getElementById("select-consultant").value;
  var date = document.getElementById("input-date").value;
    
  if (consultantId.length > 0) {
      httpGet("days/" + consultantId + "/" + date + "/registrations")
        .then(response => response.json())
        .then(response => {
          var day = response;
          var registrationsElement = document.getElementById("registrations");
          registrationsElement.innerHTML = "";
          registrationsElement.innerHTML += getDayDisplayElement(day);
        });
  }
}

function getDayDisplayElement(day) {
  var registrationsHtml = "";
  var totalDurationMinutes = 0;
  day.registrations.forEach(registration => {
    registrationsHtml += getRegistrationDisplayElement(registration);
    totalDurationMinutes += registration.duration.minutes;
  });
  return `
    <div class="day" cy="day">
        <dl>
            <dt>Total duration</dt>
            <dd cy="total-duration">${totalDurationMinutes} minutes</dd>

            <!--<dt>Registrations</dt>-->
            <dd><ul>${registrationsHtml}</ul></dd>
        </dl>
    </div>`;
}

function getRegistrationDisplayElement(registration) {
  return `
    <li class="registration" cy="registration">
      <span cy="activity-name">${registration.activity}</span>
      @
      <span cy="project-name">${registration.projectSnapshot.name}</span>
      <br />
      <span cy="duration">${registration.duration.minutes} minutes</span>
    </li>`;
}

document.getElementById("button-add-registration").addEventListener("click", function() {
    addRegistration();
});
document.getElementById("select-consultant").addEventListener("change", function() {
    updateProjectSelect();
    displayRegistrations();
});
document.getElementById("input-date").addEventListener("blur", function() {
    displayRegistrations();
});

function addRegistration() {
  hideError();
  var consultantId = document.getElementById("select-consultant").value;
  var date = document.getElementById("input-date").value;
  var projectName = document.getElementById("select-project").value;
  var activity = document.getElementById("input-activity").value;
  var duration = document.getElementById("input-duration").value;
  var data = {
    projectName,
    activity,
    duration
  };  
  httpPost("days/" + consultantId + "/" + date + "/registrations", data)
    .then(response => {
      if (response.status === 200) {
        displayRegistrations();
        return;
      }
      return response.json();
    })
    .then(json => {
      if (json && json.error) {
        showError(json.error);
      }
    });
}

function updateProjectSelect() {
  var consultantId = document.getElementById("select-consultant").value;
  httpGet("consultants/" + consultantId + "/projects")
    .then(response => response.json())
    .then(response => {
      var projects = response;
      var projectsElement = document.getElementById("select-project");
      projectsElement.innerHTML = projectsElement.getElementsByTagName(
        "option"
      )[0].outerHTML;
      projects.forEach(project => {
        projectsElement.innerHTML += `<option value="${project.name}">${project.name}</option>`;
      });
    });
}

var warningElement = document.getElementById("warning");
function showError(message) {
  warningElement.innerText = message;
  warningElement.style.display = "";
}
function hideError() {
  warningElement.innerText = "";
  warningElement.style.display = "none";
}

function httpGet(path) {
  return fetch(path, getOptions("GET"));
}

function httpPost(path, data) {
  return fetch(path, getOptions("POST", data));
}

function getOptions(verb, data) {
  var options = {
    dataType: "json",
    method: verb,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    }
  };
  if (data) {
    options.body = JSON.stringify(data);
  }
  return options;
}
