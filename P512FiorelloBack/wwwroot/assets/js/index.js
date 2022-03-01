const sortClick = (e, name)=>{
  $(e).siblings().css({color: "#000000"})
  $(e).css({color: "#f34f3f"})
  const targets = document.querySelectorAll(`.flowers .flowers-item`).forEach(item => item.style.display = 'block');
  if(name)
  document.querySelectorAll(`.flowers .flowers-item:not(.${name})`).forEach(element => {
    element.style.display = "none"
  });
}


$(document).ready(function () {
  $(".fa-bars").click(function () {
    $(".side-menu").css({ width: "100%", transition: "0.3s" });
    $(".side-menu-item").addClass("animate__fadeInRight");
  });
  $(".fa-times").click(function () {
    $(".side-menu").css({
      width: "0px",
      padding_left: "15px",
    });
    $(".side-menu-item").removeClass("animate__fadeInRight");
  });

  $(".categories").click(function () {
    $(".categories-dropdown").slideToggle();
  });

  $(window).scroll(function () {
    if (window.scrollY >= 650) {
      $(".nav").slideDown();
      $(".nav").addClass("sticky-top");
    } else if (window.scrollY < 650) {
      $(".nav").slideUp("fast");
    }
    if (window.scrollY <= 100) {
      $(".nav").addClass("d-none");
    } else {
      $(".nav").removeClass("d-none");
    }
  });

  $(window).scroll(function () {
    if (window.scrollY <= 554) {
      $('.toTheTop').addClass('d-none');
    } else if(window.scrollY > 554){
      $('.toTheTop').removeClass('d-none');
    }
  });

  $('.side-menu-item .clickable').click(function(){
    $(this).parent().children('ul').slideToggle();
    $(this).parent().siblings('.side-menu-item').children('ul').slideUp();
  });


  $('.shopSideDropdown').click(function(){
    $(this).children('ul').slideToggle();

  })
  const headerImages = [...document.querySelectorAll(".header-slider img")];

  $(".main-slider").owlCarousel({
    nav: true,
    responsive: {
      1024: {
        nav: false,
      },
    },
    loop: true,
    dots: false,
    autoplay: true,
    autoplayTimeout: 3000,
    items: 1,
  });

  $(".header-slider").owlCarousel({
    nav: true,
    loop: true,
    autoplay: true,
    animateOut: "fadeOut",
    animateTimeout: 5000,
    mouseDrag: false,
    touchDrag: false,
    responsive: {
      768: {
        nav: true,
      },
    },
    items: 1,
    onChange: (para) => {
      const index = para.item.index - 1;
      headerImages.forEach((img, i) => {
        if (i === index) {
          $(img).css({ transform: "scale(1.03)" });
        } else {
          $(img).css({ transform: "scale(1.0)" });
        }
      });
    },
  });

  $(".owl-next")
    .children()
    .html('<i class="fas fa-long-arrow-alt-right fa-2x"></i>');
  $(".owl-prev")
    .children()
        .html('<i class="fas fa-long-arrow-alt-left fa-2x"></i>');

    $(document).on("keyup", "#search-input", function(){

        var searchStr = $(this).val();
        $.ajax({
            type: "GET",
            url: "/Flower/search/?searchStr=" + searchStr,

            success: function (response) {
                $("#search-result li:not(:first-child)").remove();
                $("#search-result").append(response);
            }
        });
    });
});



let addToCart = document.querySelectorAll(".addToCart");
let flowerList = document.querySelector('#flower-list');
let deleteFlower = document.querySelector(".fa-times");




//if (!localStorage.getItem("basket")) {
//  localStorage.setItem("basket", JSON.stringify([]));
//}

//addToCart.forEach((addBtn) => {
//  addBtn.addEventListener("click", function (e) {
//    if (!localStorage.getItem("basket")) {
//      localStorage.setItem("basket", JSON.stringify([]));
//    }

//    let basket = JSON.parse(localStorage.getItem("basket"));

//    let price = this.nextElementSibling.children[0].innerText;
//    let flower = this.parentElement.previousElementSibling.innerText;
//    let image =
//      this.parentElement.parentElement.previousElementSibling.children[0].src;
//    let id = this.getAttribute("data-id");

//    let existProduct = basket.find((p) => p.id == id);

//    if (!existProduct) {
//      let product = {
//        id,
//        price,
//        flower,
//        image,
//        count: 1,
//      };
//      basket.push(product);
//    } else {
//      existProduct.count++;
//    }

//    localStorage.setItem("basket", JSON.stringify(basket));
//    Refresh();
//  });
//});

//function productCount(basket) {
//  let countElement = document.querySelector(".countProduct");
//  let count = 0;

//  basket?.forEach((p) => {
//    count += p.count;
//  });
//  countElement.innerText = count;
//}

//function totalPrice(basket) {
//  let priceElement = document.querySelector(".totalPrice");
//  let ElementPrices = document.querySelector(".totalPriceList");
//  let total = basket?.reduce((total, p) => {
//    return (total += p.price * p.count);
//  }, 0);
//  priceElement.innerText = total ?? 0;
//  ElementPrices.innerText = total ?? 0;
//}



//Refresh();


//function deleteItemFromLocalStorage(id) {
//  let basket = JSON.parse(localStorage.getItem("basket"));
//  let newBasket = basket.filter((p) => p.id != id);
//  localStorage.setItem("basket", JSON.stringify(newBasket));
//  Refresh();
//}


//function addItemToBasketList(basket) {
//  let basketArray = basket?.map((p) => {
//    return `<div class="flower-item">
//    <div class="itemImg">
//      <img src="${p.image}" />
//    </div>
//    <div class="flower-body">
//      <div class="flower-title">
//        <p class="">${p.flower}</p>
//      </div>
//      <div class="flower-text">
//        <span class="count">${p.count}</span>
//        x
//        <span class="price">${p.price}</span>
//      </div>
//    </div>
//    <i class="fas fa-times" onclick="deleteItemFromLocalStorage(${p.id})"></i>
//  </div>`
//  });


//  if(basketArray && basketArray?.length > 0){
//    flowerList.innerHTML = basketArray.join("");
//  }
//  else{
//    flowerList.innerHTML = '<div class="text-center">No products in the cart.</div>';
//  }
//}

//function Refresh() {
//  let basket = JSON.parse(localStorage.getItem("basket"));
//  addItemToBasketList(basket);
//  productCount(basket);
//  totalPrice(basket); 
//}


//window.addEventListener('scroll', function(){
//  if(window.scrollY <= 554){

//  }
//})