const url = "http://localhost:32189/api/Categories";
async function getCategory() {
  var token = localStorage.getItem("jwtToken");

  var response = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: "Bearer " + token,
    },
  });

  console.log(response);
  var result = await response.json();
  console.log(result);
  var container = document.getElementById("container");
  result.forEach((element) => {
    container.innerHTML += `
        <tr>
                <td style="width: 200px;">
                    <img src="/backend/Backend/img/${element.categoryImage}" alt="صورة" style="width: 100%; height: 200px; object-fit: cover;" />
                </td>
                <td>${element.categoryName}</td>
                <td>
                    <a href="/frontend/Admin/UpdateCategories.html" class="btn btn-secondary" onclick="Categories(${element.categoryId})">
                    تعديل الفئة</a>
                    <br><br>
                    <a class="btn btn-danger" onclick="deleteCategories(${element.categoryId})">
                     حذف الفئة</a>
                </td>
                  
            </tr>
    `;
  });
}
function Categories(categoryId) {
  localStorage.setItem("CategoryID", categoryId);
  window.location.href = "/frontend/Admin/UpdateCategories.html";
}
function deleteCategories(categoryId) {
  var url = `http://localhost:32189/api/Categories?id=${categoryId}`;
  fetch(url, { method: "DELETE" }).then(() => {
    alert("تم الحذف بنجاح");
    getCategory();
    window.location.href = "/frontend/Admin/CategoriesAdmin.html";
  });
}
getCategory();
