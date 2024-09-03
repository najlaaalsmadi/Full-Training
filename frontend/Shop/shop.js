debugger;
const url2 = "http://localhost:32189/api/Products";
debugger;

var n = localStorage.getItem("CategoryID");
console.log(n);
const url1 = `http://localhost:32189/api/Products/prodectbycategoryId/${n}`;
debugger;

async function getProducts() {
  var container = document.getElementById("container");

  if (n != null) {
    var token = localStorage.getItem("jwtToken");

    var response = await fetch(url1, {
      method: "GET",
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    var result = await response.json();
    result.forEach((element) => {
      container.innerHTML += ` 
        <div class="col-md-4 mb-4">
          <div class="card" style="width: 100%;">
            <img src="/backend/Backend/img/${element.productImage}" class="card-img-top" alt="${element.productImage} NOT Found image" style="width: 100%; height: 400px; object-fit: cover;" />
            <div class="card-body">
              <h5 class="card-title">${element.productName}</h5>
              <p class="card-text">${element.description}</p>
              <p class="card-text">${element.price}</p>
              <button onclick="saveToLocalStorage(${element.productId})" class="btn btn-primary" style="background-color: navy">تفاصيل المنتج</button>
            </div>
          </div>
        </div>`;
    });
    debugger;
  } else {
    var token = localStorage.getItem("jwtToken");

    var response1 = await fetch(url2, {
      method: "GET",
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    var result1 = await response1.json();
    result1.forEach((element) => {
      container.innerHTML += ` 
        <div class="col-md-4 mb-4">
          <div class="card" style="width: 100%;">
            <img src="/backend/Backend/img/${element.productImage}" class="card-img-top" alt="${element.productImage} NOT Found image" style="width: 100%; height: 400px; object-fit: cover;" />
            <div class="card-body">
              <h5 class="card-title">${element.productName}</h5>
              <p class="card-text">${element.description}</p>
              <p class="card-text">${element.price}</p>
              <button onclick="saveToLocalStorage(${element.productId})" class="btn btn-primary" style="background-color: navy">تفاصيل المنتج</button>
            </div>
          </div>
        </div>`;
    });
  }
}

function saveToLocalStorage(productId) {
  localStorage.setItem("productIdNew", productId);
  window.location.href = "/frontend/Shop/detililes.html";
}

window.onload = function () {
  getProducts();
};
