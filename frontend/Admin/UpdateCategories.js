// تحديث الفئة
async function updateCategories() {
  event.preventDefault();

  // الحصول على الفورم وعناصره
  var categoryForm = document.getElementById("updateCategoriesForm");
  var formData = new FormData(categoryForm);

  // الحصول على معرف الفئة
  let n = localStorage.getItem("CategoryID");
  let url = `http://localhost:32189/api/Categories/${n}`;

  // إرسال طلب التعديل
  let request = await fetch(url, {
    method: "PUT",
    body: formData,
  });

  // عرض النتائج وتأكيد التعديل
  let result = await request.json();

  Swal.fire({
    icon: "success",
    title: "تم التعديل بنجاح",
    text: "تمت تعديل الفئة بنجاح!",
    confirmButtonText: "موافق",
  });

  // إعادة التوجيه إلى صفحة الإدارة
  window.location.href = "/frontend/Admin/CategoriesAdmin.html";
}
