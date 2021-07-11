const replyButton = document.querySelectorAll(".reply-btn");
const submitButton = document.getElementById("submit-btn");
const popup = document.getElementById("popup");

replyButton.forEach(btn => {
    btn.addEventListener("click", () => {
        popup.style.display = "grid";
    })
})

popup.addEventListener("click", () => {
    popup.style.display = "none";
})