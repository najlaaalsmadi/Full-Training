debugger;

async function getProducts() {
  const n1 = Number(localStorage.getItem("cartID"));

  if (!n1) {
    console.error("cartID is not set in localStorage.");
    return;
  }

  const url1 = `http://localhost:32189/api/CartItem/${n1}`;
  debugger;

  try {
    const response = await fetch(url1);
    if (!response.ok) throw new Error("Network response was not ok");

    const result = await response.json();
    console.log("API response:", result);

    const container = document.getElementById("tbody");
    debugger;

    if (container) {
      container.innerHTML = result
        .map((item) => {
          const product = item.productRequestDTO || {};

          return `<tr>
                        <td>${product.productName}</td>
                        <td>${product.price}</td>
                        <td>${item.quantity}</td>
                        <td>${item.quantity * product.price}</td>
                        <td><a href="#" onclick="removeItem(${
                          item.cartItemId
                        })"style="color: white;" class="btn btn-danger">حذف</a>
                        <BR>      <BR>
<a href="#" class="quantity-btn" onclick="updateItem(${item.cartItemId}, ${
            item.quantity + 1
          })">
  <span class="quantity-icon">+</span>
</a>
<span class="quantity-label">${item.quantity}</span> <!-- عرض الكمية الحالية -->
<a href="#" class="quantity-btn" onclick="updateItem(${item.cartItemId}, ${
            item.quantity - 1
          })">
  <span class="quantity-icon">−</span>
</a>

</td>

                    </tr>`;
        })
        .join("");
    } else {
      console.error("Container element with ID 'tbody' not found.");
    }
  } catch (error) {
    console.error("Error fetching product data:", error);
  }
}

getProducts();
async function removeItem(cartItemId) {
  const url = `http://localhost:32189/api/CartItem?id=${cartItemId}`;

  try {
    const response = await fetch(url, {
      method: "DELETE",
    });

    if (!response.ok) throw new Error("Network response was not ok");

    // عند نجاح الحذف، قم بتحديث الجدول لعرض السلة الجديدة بعد الحذف
    console.log("Item removed successfully.");
    getProducts(); // قم باستدعاء getProducts لتحديث الجدول
  } catch (error) {
    console.error("Error removing item:", error);
  }
}
async function updateItem(cartItemId, newQuantity) {
  const url = `http://localhost:32189/api/CartItem/${cartItemId}`;

  const data = {
    quantity: newQuantity,
  };

  try {
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error("Failed to update item. Network response was not ok.");
    }

    console.log(`Item with ID ${cartItemId} updated successfully.`);
    // إعادة تحميل المنتجات أو تحديث الواجهة بشكل ديناميكي
    getProducts();
  } catch (error) {
    console.error("Error updating item:", error);
  }
}
