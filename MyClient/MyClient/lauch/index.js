var server = document.getElementById("server");

server.addEventListener("click", function () {
    window.location.href = "http://localhost:8080/stats";
});

var age = document.getElementById("age");

age.addEventListener("click", function () {
    window.location.href = "http://localhost:8080/age";
});

var pertinente = document.getElementById("pertinente");

pertinente.addEventListener("click", function () {
    window.location.href = "http://localhost:8080/scenario";
});