debugger;
let url = "http://localhost:32189/api/Users/register";
var registerForm1 = document.getElementById("registerForm");

async function registerForm() {
  event.preventDefault();
  debugger;

  var registerForm2 = new FormData(registerForm1);
  let request = await fetch(url, {
    method: "POST",
    body: registerForm2,
  });
  let result = await request.json();

  console.log(request);
  console.log(result);
  Swal.fire({
    icon: "success",
    title: "تم  الاضافة بنجاح",
    text: "تمت الاضافة المستخدم  بنجاح!",
    confirmButtonText: "موافق",
  });
  window.location.href = "/frontend/Index.html";
}
debugger;
