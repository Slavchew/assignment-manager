﻿window.addEventListener('DOMContentLoaded', (event) => {
    // Header

    // Menu button
    const menuBtn = document.getElementsByClassName("menu_btn")[0];
    const nav = document.getElementsByClassName("nav")[0];
    const cornerGraphic = document.getElementsByClassName("corner_graphic_mobile")[0]
    menuBtn.addEventListener("click", () => {
        nav.classList.toggle("nav--mobile");
        cornerGraphic.classList.toggle("corner_graphic_mobile--menu_btn_active");
        menuBtn.classList.toggle("menu_btn--active");
    });
});