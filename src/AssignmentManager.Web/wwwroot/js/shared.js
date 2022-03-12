window.addEventListener('DOMContentLoaded', (event) => {
    // Menu button
    const menuBtn = document.getElementsByClassName("mobile_menu")[0];
    const nav = document.getElementsByTagName("nav")[0];
    const cornerGraphic = document.getElementsByClassName("corner_graphic_mobile")[0]
    menuBtn.addEventListener("click", () => {
        nav.classList.toggle("nav--mobile");
        cornerGraphic.classList.toggle("corner_graphic_mobile--emerge");
        menuBtn.classList.toggle("mobile_menu--active");
    });

    // Local storage
    if (localStorage.getItem("notFirstTime") === null) {
        window.location.href = '/Classes/Welcome';
        localStorage.setItem("notFirstTime", false);
    }
});