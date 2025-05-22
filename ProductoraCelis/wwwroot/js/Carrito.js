  /*
fetch("/Home/ActualizarCarrito", {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
    },
  body: JSON.stringify(productosSeleccionados),
}).then(response => {
    if (response.ok) {
        // Limpiar el almacenamiento local
        localStorage.removeItem("productosSeleccionados");
        window.location.href = "/Home/Carrito";
    } else {
        alert("Ocurri� un error al actualizar el carrito.");
    }
});
*/

fetch("/Home/ActualizarCarrito", {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(productosSeleccionados),
})
    .then(response => {
        if (response.ok) {
            localStorage.removeItem("productosSeleccionados");
            window.location.href = "/Home/Carrito";  // Rediriges a la vista del carrito
        } else {
            alert("Ocurri� un error al actualizar el carrito.");
        }
    });


