debugger;

var n = localStorage.getItem("productIdNew");
debugger;

const url = `http://localhost:32189/api/Products/byIDProduct/${n}`;
debugger;

async function getdetililes() {
  var response = await fetch(url);
  console.log(response);
  var result = await response.json();
  console.log(result);
  var container = document.getElementById("container");
  debugger;

  container.innerHTML = `
       <div class="container mt-5">
  <div class="row">
    <div class="col-md-6">
      <!-- صورة المنتج -->
      <div class="product-image">
        <img src="/backend/Backend/img/${result.productImage}" class="img-fluid" alt="صورة المنتج" style="width: 100%; height: 400px; object-fit: cover;" />
      </div>
    </div>
    
    <div class="col-md-6">
      <!-- تفاصيل المنتج -->
      <div class="product-details">
        <h3 class="product-title">${result.productName}</h3>
        <p class="product-description">${result.description}</p>
        <div class="product-price">
          <span class="price">${result.price}</span>
        </div>
        <label>الكمية</label>
          <input id="cartQuantity" type="number">
            <div class="mt-4">
        <button onclick="add()" class="btn btn-primary">اضافة الى السلة</button>
        </div>
      </div>
    </div>
  </div>
</div>

    `;
}

getdetililes();
const url11 = "http://localhost:32189/api/CartItem";
async function add() {
  try {
    localStorage.setItem("cartID", 1);
    const CartID = localStorage.getItem("cartID");
    const input = document.getElementById("cartQuantity");
    const response = {
      cartId: CartID,
      productId: localStorage.getItem("productIdNew"),
      Quantity: input.value,
    };
    const responseData = await fetch(url11, {
      method: "POST",
      body: JSON.stringify(response),
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (!responseData.ok) throw new Error("Network response was not ok");
    // Handle success, e.g., display a success message
  } catch (error) {
    console.error("Error adding to cart:", error);
  }
}
