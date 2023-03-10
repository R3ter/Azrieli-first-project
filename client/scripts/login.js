const login = async (e) => {
  const email = e.target[0].value;
  const pass = e.target[1].value;
  const loginButton = document.getElementById("loginButt");
  const error = document.getElementById("messageError");
  const temp = loginButton.innerHTML;
  loginButton.innerHTML = `<div class="loader"></div>`;
  fetch("https://localhost:7133/api/account/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username: email, password: pass }),
  }).then(async (e) => {
    loginButton.innerHTML = temp;
    if (await e.json()) {
      localStorage.setItem("user", email);
      window.location.href = "./pages/Department.html";
    } else {
      error.innerHTML = "Username or password is incorrect";
    }
  });
};

const getDepartmentData = async (e) => {
  const departments = await fetch("https://localhost:7133/api/Department");
  const tbody = document.getElementById("tbody");
  const text = await (
    await departments.json()
  )
    .map(({ department: { id, name }, employee }) => {
      console.log(employee);
      return ` <tr style="cursor: default">
    <td class="column1" style="cursor: default">
      ${id}
    </td>
    <td class="column2">${name}</td>
    <td class="column3">${
      employee ? employee?.firstName + " " + employee?.lastName : "no manager"
    }</td>
    <td class="column4">
      <button
      onclick="window.location.href = './EditPage.html?name=${name}&managerID=${
        employee?.id
      }&section=department&id=${id}'";
      style="
      padding: 10px;
      background-color: rgb(80, 197, 218);
      border-radius: 10px;
      "
      >
      Edit
      </button>
      </td>
      <td class="column5">
      <button
      onclick="deleteDepartment(${id})"
        style="
          padding: 10px;
          background-color: rgb(218, 80, 87);
          border-radius: 10px;
        "
      >
        Remove
      </button>
    </td>
  </tr>`;
    })
    .join("");
  tbody.innerHTML = text;
};
const getEmployeeData = async () => {
  const departments = await fetch("https://localhost:7133/api/Employee");
  const tbody = document.getElementById("tbodyEmployee");

  const text = await (
    await departments.json()
  )
    .map(({ id, firstName, lastName, startWorkYear, department }) => {
      return ` <tr style="cursor: default">
    <td class="column1" style="cursor: default">
      ${id}
    </td>
    <td class="column2">${firstName}</td>
    <td class="column3">${lastName}</td>
    <td class="column4">${startWorkYear}</td>
    <td class="column5">${department.name}</td>
    <td class="column6">
      <button
        style="
          padding: 10px;
          background-color: rgb(80, 197, 218);
          border-radius: 10px;
        "
        onclick="window.location.href = './EditPage.html?FirstName=${firstName}&LastName=${lastName}&StartWorkYear=${startWorkYear}&DepartmentID=${department?.id}&section=employee&id=${id}'";
      >
        Edit
      </button>
    </td>
    <td class="column7">
      <button
      onclick='deleteEmployee(${id})'
        style="
          padding: 10px;
          background-color: rgb(218, 80, 87);
          border-radius: 10px;
        "
      >
        Remove
      </button>
    </td>
  </tr>`;
    })
    .join("");
  tbody.innerHTML = text;
};
const getShiftData = async () => {
  const departments = await fetch("https://localhost:7133/api/shift");
  const tbody = document.getElementById("tbodyShifts");

  const text = await (
    await departments.json()
  )
    .map(
      ({
        id,
        employee: { firstName, lastName },
        shift: { date, startTime, endTime },
      }) => {
        return ` <tr style="cursor: default">
    <td class="column1" style="cursor: default">
      ${id}
    </td>
    <td class="column2">${firstName}</td>
    <td class="column3">${lastName}</td>
    <td class="column4">${date}</td>
    <td class="column5">${startTime}</td>
    <td class="column6">${endTime}</td>
    <td class="column7">
      <button
        style="
          padding: 10px;
          background-color: rgb(80, 197, 218);
          border-radius: 10px;
        "
      >
        Edit
      </button>
    </td>
    <td class="column8">
      <button
        style="
          padding: 10px;
          background-color: rgb(218, 80, 87);
          border-radius: 10px;
        "
      >
        Remove
      </button>
    </td>
  </tr>`;
      }
    )
    .join("");
  tbody.innerHTML = text;
};
const onloadNav = async () => {
  const a = await localStorage.getItem("user");
  console.log(a);
  if (a) {
    document.getElementById("welcome").innerText = "Hello " + a;
  } else {
    window.location.href = "./../index.html";
  }
};
const deleteEmployee = async (id) => {
  const departments = await fetch("https://localhost:7133/api/employee", {
    method: "DELETE",
    headers: {
      Accept: "application/json, text/plain, */*",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: JSON.stringify({
      id,
    }),
  });
  window.location.href = "./Employee.html";
};
const deleteDepartment = async (id) => {
  const departments = await fetch("https://localhost:7133/api/department", {
    method: "DELETE",
    headers: {
      Accept: "application/json, text/plain, */*",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: JSON.stringify({
      id,
    }),
  });
  window.location.href = "./Department.html";
};
const addDepartment = async (e) => {
  e.preventDefault();
  const name = document.getElementById("name").value;
  const managerID = document.getElementById("manager").value;
  if (name == "" || managerID == "") {
    return false;
  }
  e.target.innerHTML = `Please wait....`;
  e.target.disabled = true;
  const departments = await fetch("https://localhost:7133/api/department", {
    method: "POST",
    headers: {
      Accept: "application/json, text/plain, */*",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: JSON.stringify({
      name,
      managerID,
    }),
  });
  await departments.json();
  window.location.href = "./Department.html";
};
const editDepartment = async (e) => {
  e.preventDefault();
  const name = document.getElementById("name").value;
  const managerID = document.getElementById("manager").value;
  if (name == "" || managerID == "") {
    return false;
  }
  e.target.innerHTML = `Please wait....`;
  e.target.disabled = true;
  const departments = await fetch("https://localhost:7133/api/department", {
    method: "POST",
    headers: {
      Accept: "application/json, text/plain, */*",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: JSON.stringify({
      name,
      managerID,
    }),
  });
  await departments.json();
  window.location.href = "./Department.html";
};
