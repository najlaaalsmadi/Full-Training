async function loginForm() {
  event.preventDefault(); // Prevent form submission default behavior

  let url = "http://localhost:32189/api/Users/login";

  // Collect the form data
  var formData = new FormData(document.getElementById("loginForm"));

  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  let result = await response.json();
  // Check if the response is okay and parse JSON
  if (response.ok) {
    window.location.href = "/frontend/Index.html";
    localStorage.setItem("jwtToken", result.token);
  } else {
    alert("can not login");
  }
}
