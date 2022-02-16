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

  $(window).scroll(function () {
    if (window.scrollY >= 350) {
      $(".nav").slideDown();
      $(".nav").addClass("sticky-top");
    } else if (window.scrollY < 350) {
      $(".nav").slideUp("fast");
    }
    if (window.scrollY <= 100) {
      $(".nav").addClass("d-none");
    } else {
      $(".nav").removeClass("d-none");
    }
  });

  $(".cards-button").click(function () {
    $(this).addClass("active").siblings("button").removeClass("active");
    if ($(this).hasClass("services")) {
      $(this)
        .parent()
        .siblings(".tab-body")
        .children(".services-body")
        .removeClass("d-none")
        .siblings()
        .addClass("d-none");
    } else if ($(this).hasClass("shop")) {
      $(this)
        .parent()
        .siblings(".tab-body")
        .children(".shop-body")
        .removeClass("d-none")
        .siblings()
        .addClass("d-none");
    } else if ($(this).hasClass("product")) {
      $(this)
        .parent()
        .siblings(".tab-body")
        .children(".product-body")
        .removeClass("d-none")
        .siblings()
        .addClass("d-none");
    }
  });

});



let flowerList = document.querySelector('#flower-list');
let deleteFlower = document.querySelector(".fa-times");



function productCount(basket) {
  let countElement = document.querySelector(".countProduct");
  let count = 0;

  basket?.forEach((p) => {
    count += p.count;
  });
  countElement.innerText = count;
}

function totalPrice(basket) {
  let priceElement = document.querySelector(".totalPrice");
  let ElementPrices = document.querySelector(".totalPriceList");
  let total = basket?.reduce((total, p) => {
    return (total += p.price * p.count);
  }, 0);
  priceElement.innerText = total ?? 0;
  ElementPrices.innerText = total ?? 0;
}



Refresh();


function deleteItemFromLocalStorage(id) {
  let basket = JSON.parse(localStorage.getItem("basket"));
  let newBasket = basket.filter((p) => p.id != id);
  localStorage.setItem("basket", JSON.stringify(newBasket));
  Refresh();
}


function addItemToBasketList(basket) {
  let basketArray = basket?.map((p) => {
    return `<div class="flower-item">
    <div class="itemImg">
      <img src="${p.image}" />
    </div>
    <div class="flower-body">
      <div class="flower-title">
        <p class="">${p.flower}</p>
      </div>
      <div class="flower-text">
        <span class="count">${p.count}</span>
        x
        <span class="price">${p.price}</span>
      </div>
    </div>
    <i class="fas fa-times" onclick="deleteItemFromLocalStorage(${p.id})"></i>
  </div>`
  });


  if(basketArray && basketArray?.length > 0){
    flowerList.innerHTML = basketArray.join("");
  }
  else{
    flowerList.innerHTML = '<div class="text-center">No products in the cart.</div>';
  }
}

function Refresh() {
  let basket = JSON.parse(localStorage.getItem("basket"));
  addItemToBasketList(basket);
  productCount(basket);
  totalPrice(basket);
}
