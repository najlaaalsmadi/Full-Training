debugger;
async function addCategories() {
  event.preventDefault();
  debugger;

  var category1 = document.getElementById("createCategoriesForm");
  var category2 = new FormData(category1);
  let url = "http://localhost:32189/api/Categories";
  let request = await fetch(url, {
    method: "POST",
    body: category2,
  });
  debugger;

  console.log(request);
  let result = await request.json();
  console.log(result);
  Swal.fire({
    icon: "success",
    title: "تم  الاضافة بنجاح",
    text: "تمت الاضافة الفئة بنجاح!",
    confirmButtonText: "موافق",
  });
  window.location.href = "/frontend/Admin/CategoriesAdmin.html";
}
debugger;
