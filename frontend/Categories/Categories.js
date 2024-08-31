const url = "http://localhost:32189/api/Categories";
async function getCategory() {
  var response = await fetch(url);
  console.log(response);
  var result = await response.json();
  console.log(result);
  var container = document.getElementById("container");
  result.forEach((element) => {
    container.innerHTML += `
        <div class="col-md-4 mb-4">
          <div class="card" style="width: 100%;">
           <img src="/backend/Backend/img/${element.categoryImage}" class="card-img-top" alt="صورة" style="width: 100%; height: 200px; object-fit: cover;" />
        <div class="card-body">
          <h5 class="card-title">${element.categoryName} </h5>
                    <a href="/frontend/Shop/shop.html" class="btn btn-primary" style="background-color: navy" onclick="setCategoryID(${element.categoryId})">
           اذهب إلى المنتجات </a
          >
        </div>
      </div> </div>
    `;
  });
}
function setCategoryID(categoryId) {
  localStorage.setItem("CategoryID", categoryId);
}
getCategory();
