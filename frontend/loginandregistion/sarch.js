async function getsarch() {
  let n = document.getElementById("userId").value;
  console.log(n);
  let url = `http://localhost:32189/api/Users/${n}`;

  let response = await fetch(url);
  console.log(response);

  let result = await response.json();
  console.log(result);
  let container = document.getElementById("Data");
  container.innerHTML = `
        <h1>${result[0].email}</h1>
         <p>${result[0].userId}</p>
         <p>${result[0].username}</p>
      

      `;
}
