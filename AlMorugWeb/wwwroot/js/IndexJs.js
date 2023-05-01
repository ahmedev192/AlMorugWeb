// burger icon 
let burger = document.getElementById("burger");
let navBar = document.getElementById("navBar");
burger.addEventListener("click" , () =>{
  navBar.classList.toggle("active");
  if(navBar.classList.contains("active")){
      navBar.style.right ="50%";
  }else{
      navBar.style.right ="150%" ;
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


// for projects
var count = 0;
var target = parseInt(document.querySelector(".countnum").innerHTML); // set your desired target number here
function incrementCount() {
  count++;
  document.querySelector(".count").innerHTML = count;
  if (count < target) {
    setTimeout(incrementCount, 25);
  }
}

function startCounter() {
  if (window.pageYOffset >= 1500) {
    setTimeout(incrementCount, 25);
    window.removeEventListener('scroll', startCounter);
  }
}

window.addEventListener('scroll', startCounter);


// for real estat 
var targetreal = parseInt(document.querySelector(".countrealnum").innerHTML) ; // set your desired target number here
var countReal = 0;
function incrementCountReal() {
  countReal++;
  document.querySelector(".countreal").innerHTML = countReal;
  if (countReal < targetreal) {
    setTimeout(incrementCountReal, 25);
  }
}

function startCounterReal() {
  console.log(window.pageYOffset);
    if (window.pageYOffset >= 1500) {
    setTimeout(incrementCountReal, 25);
    window.removeEventListener('scroll', startCounterReal);
  }
}

window.addEventListener('scroll', startCounterReal);

const currentYear = new Date().getFullYear();
document.getElementById("currentYear").innerHTML = currentYear;
