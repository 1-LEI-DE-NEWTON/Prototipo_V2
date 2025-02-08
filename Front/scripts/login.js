document.getElementById("login-form").addEventListener("submit", async (event) => {
    event.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
        await login(username, password);
        alert("Login realizado com sucesso!");
        window.location.href = "menu_inicial.html";
    } catch (error) {
        alert("Erro ao fazer login: " + error.message);
    }
});
