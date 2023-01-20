const login = (e) => {
  const email = e.target[0].value;
  const pass = e.target[1].value;
  const loginButton = document.getElementById("loginButt");
  const temp = loginButton.innerHTML;
  loginButton.innerHTML = `<div class="loader"></div>`;
  
};
