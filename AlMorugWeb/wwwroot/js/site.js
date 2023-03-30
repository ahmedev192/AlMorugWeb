// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// burger icon 
let burger = document.getElementById("burger");
let navBar = document.getElementById("navBar");
burger.addEventListener("click", () => {
    navBar.classList.toggle("active");
    if (navBar.classList.contains("active")) {
        navBar.style.left = "0";
    } else {
        navBar.style.left = "-100%";
    }
})


// photo lider
const slides = document.querySelectorAll(".slides img");
const arrowLeft = document.querySelector(".arrow-left");
const arrowRight = document.querySelector(".arrow-right");
const dotsContainer = document.querySelector(".dots");

let currentSlide = 0;
let interval = setInterval(nextSlide, 5000);

function showSlide(n) {
    if (n < 0) {
        currentSlide = slides.length - 1;
    } else if (n >= slides.length) {
        currentSlide = 0;
    } else {
        currentSlide = n;
    }

    slides.forEach((slide) => {
        slide.style.display = "none";
    });

    slides[currentSlide].style.display = "block";

    dotsContainer.innerHTML = "";
    for (let i = 0; i < slides.length; i++) {
        const dot = document.createElement("span");
        dot.classList.add("dot");
        if (i === currentSlide) {
            dot.classList.add("active");
        }
        dotsContainer.appendChild(dot);
    }
}

function nextSlide() {
    showSlide(currentSlide + 1);
}

function prevSlide() {
    showSlide(currentSlide - 1);
}

showSlide(currentSlide);

arrowLeft.addEventListener("click", prevSlide);
arrowRight.addEventListener("click", nextSlide);

dotsContainer.addEventListener("click", (event) => {
    if (event.target.classList.contains("dot")) {
        const dotIndex = Array.from(dotsContainer.children).indexOf(event.target);
        showSlide(dotIndex);
    }
});

// Stop the automatic sliding when the user interacts with the slider
const slider = document.querySelector(".slider");
slider.addEventListener("mouseover", () => {
    clearInterval(interval);
});

slider.addEventListener("mouseleave", () => {
    interval = setInterval(nextSlide, 3000);
});

