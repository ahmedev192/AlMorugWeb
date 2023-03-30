// search animation 
const searchIcon = document.querySelector(".fa-search");

searchIcon.addEventListener("mouseenter", () => {
  searchIcon.style.transform = "rotate(45deg)";
});

searchIcon.addEventListener("mouseleave", () => {
  searchIcon.style.transform = "rotate(0deg)";
});

let glass = document.getElementById("galssSlider") ; 

let showPtn = document.querySelectorAll(".product-button-show");




showPtn.forEach( (element) => {
    
    // get the card which is clicked 
    element.addEventListener('click' , () =>{
    // parentNode.parentNode.parentNode => the parent = product div 
        let productDiv = element.parentNode.parentNode.parentNode ;
        // now we have array of imgs 
        let imgs = productDiv.querySelectorAll("img") ;
        glass.style.display="block"; 
        
        let imgescontainer = document.getElementById("imgescontainer");
        console.log(imgescontainer.innerHTML);
        imgescontainer.innerHTML="" ;

        for(let i=0 ; i< imgs.length ; i++){
        imgescontainer.innerHTML += `

                <!-- Slides -->
          
                <div class="swiper-slide">
                    <img src=" ${imgs[i].src}" alt="" />
                </div>
        `
    }
    })


});


let closeptn = document.getElementById("colse") ;
closeptn.addEventListener("click" , () => {
glass.style.display="none"
});