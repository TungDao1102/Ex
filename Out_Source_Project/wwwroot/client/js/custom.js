
(function ($) {
	'use strict';

	//LOADER
	$(window).on('load', function(){
		$("#preloader").removeClass("loader_show");
		$("#preloader").addClass("hide");
		$(".loader").addClass("fadeout");
	})
	$(document).ready(function () {
	
		AOS.init({
			disable: false, // accepts following values: 'phone', 'tablet', 'mobile', boolean, expression or function
			//duration: 1000, // values from 0 to 3000, with step 50ms
			once: true, // whether animation should happen only once - while scrolling down
		})

		// Sidemenu Panel
		ma5menu({
			menu: '.main_menu',
			activeClass: 'current',
			footer: '.slide_navi',
			position: 'right',
			closeOnBodyClick: true
		});
		

		var swiper = new Swiper('.theme_slider_2 .swiper-container', {
			centeredSlides: true,
			resistance: true,
			resistanceRatio: 0.6,
			speed: 1600,
			spaceBetween: 0,
			parallax: true,
			effect: "coverflow",
			controller: {
				inverse: true,
			},
			slidesPerView: '1',
			slideToClickedSlide: true,
			navigation: {
				nextEl: '.swiper-button-next',
				prevEl: '.swiper-button-prev',
			},
			pagination: {
				el: '.swiper-pagination',
				clickable: true,
			},
		});
		
		
		
		

		
		// Youtube
		var $ytvideoTrigger = $(".ytplay_btn");
		$ytvideoTrigger.on("click", function(evt) {  
			$(".ytube_video").addClass("play");
			$("#ytvideo")[0].src += "?autoplay=1";
		});
		
		
		$('.owl_team').owlCarousel({
			loop: false,
			center: false,
			responsiveClass: true,
			autoplayHoverPause: true,
			autoplay: false,
			items: 1,
			margin: 30,
			dots: false,
			nav: true,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			slideSpeed: 400,
			paginationSpeed: 400,
			autoplayTimeout: 3000,
			responsive:{
				0:{
					items:1,
				},
				600:{
					items:2,
					dots: true,
					nav: false,
				},
				1000:{
					items:2,
				},
				1200:{
					items:3,
				}
			}
		});

		$('.owl_service').owlCarousel({
			loop: false,
			center: false,
			responsiveClass: true,
			autoplayHoverPause: true,
			autoplay: false,
			items: 1,
			margin: 30,
			dots: false,
			nav: true,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			slideSpeed: 400,
			paginationSpeed: 400,
			autoplayTimeout: 3000,
			responsive:{
				0:{
					items:1,
				},
				600:{
					items:2,
					dots: true,
					nav: false,
				},
				1000:{
					items:2,
				},
				1200:{
					items:3,
				}
			}
		});


		$('.owl_testimonial1').owlCarousel({
			loop: true,
			center: true,
			responsiveClass: true,
			autoplayHoverPause: true,
			autoplay: false,
			items: 1,
			margin: 30,
			dots: true,
			nav: false,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			slideSpeed: 400,
			paginationSpeed: 400,
			autoplayTimeout: 3000,
			responsive:{
				0:{
					items:1,
				},
				600:{
					items:2,
				},
				1000:{
					items:2,
				},
				1200:{
					items:3,
				}
			}
		});


		$('.owl_testimonial2').owlCarousel({
			loop: true,
			center: true,
			responsiveClass: true,
			autoplayHoverPause: true,
			autoplay: false,
			items: 1,
			margin: 30,
			dots: true,
			nav: false,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			slideSpeed: 400,
			paginationSpeed: 400,
			autoplayTimeout: 3000,
		});


		$('.owl_blog').owlCarousel({
			loop: false,
			center: false,
			responsiveClass: true,
			autoplayHoverPause: true,
			autoplay: false,
			items: 1,
			margin: 30,
			dots: false,
			nav: true,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			slideSpeed: 400,
			paginationSpeed: 400,
			autoplayTimeout: 3000,
			responsive:{
				0:{
					items:1,
				},
				600:{
					items:2,
					dots: true,
					nav: false,
				},
				1000:{
					items:2,
				},
				1200:{
					items:3,
				}
			}
		});


		// accordion
		$(".accordion").on("click",".accordion_tab", function () {
			$(this).next().slideDown();
			$(".accordion_info").not($(this).next()).slideUp();
		});

		$(".accordion").on("click",".item", function () {
			$(this).addClass("active").siblings().removeClass("active");
		});


		// Isotope Portfolio
		var $grid = $('.grid').isotope({
			itemSelector: '.element-item', 
			percentPosition: true,
			layoutMode: 'masonry',
			transformsEnabled: true,
			transitionDuration: "700ms",
			resize: true,
			fitWidth: true,
			columnWidth: '.grid-sizer',
		});

        // bind filter button click
        $('.filters-button-group .button').on( 'click', function() {
            var filterValue = $( this ).attr('data-filter');
            // use filterFn if matches value
            $grid.isotope({ filter: filterValue });
        });

		// change is-checked class on buttons
        $('.filters-button-group').each( function( i, buttonGroup ) {
            var $buttonGroup = $( buttonGroup );
            $buttonGroup.on( 'click', 'button', function() {
                $buttonGroup.find('.is-checked').removeClass('is-checked');
                $( this ).addClass('is-checked');
            });
		});

		$grid.imagesLoaded().progress( function() {
			$grid.isotope('layout');
		});


		// Search width increase
		$('.open_search .search_form .form-control').click(function(e) { 
			$('.open_search .search_form').removeClass('active');
			$(e.target).closest('.open_search .search_form').addClass('active');
		});
		$(document).click(function(e){ 
			if($(e.target).closest('.open_search .search_form').length==0) { 
				$('.open_search .search_form').removeClass('active');  
			}
		});


		
		// Search width increase
		$('.header_search .form-control-submit').click(function(e) { 
			$('.open_search').toggleClass('active');
		});

		

		
		// ========== SHOP PAGE =========== //

		// Select 2
		$('.basic_select').select2({});
		
		// Load More Content
		$(".product_view_list .product_item").slice(0, 6).show();
		$(".product_view_list .load_more").on("click", function(e){
			e.preventDefault();
			$(".product_view_list .product_item:hidden").slice(0, 3).slideDown();
			if($(".product_view_list .product_item:hidden").length == 0) {
				$(".product_view_list .load_more").css('display', 'none');
			}
		});

		// Product Zoom
		$('.product_zoom_button_group > li > a').eq(0).addClass( "selected" );
		$('.product_zoom_container > .product_zoom_info').eq(0).css('display','block');
		$('.product_zoom_button_group').on("click",function(e){
			if($(e.target).is("a")){

				/*Handle Tab Nav*/
				$('.product_zoom_button_group > li > a').removeClass( "selected");
				$(e.target).addClass( "selected");
				
				/*Handles Tab Content*/
				var clicked_index = $("a",this).index(e.target);
				$('.product_zoom_container > .product_zoom_info').css('display','none');
				$('.product_zoom_container > .product_zoom_info').eq(clicked_index).fadeIn();
			}
			$(this).blur();
			return false;
		});

		// Product Carousel
		$('.product_carousel_1').owlCarousel({
			items: 4,
			loop: true,
			margin: 30,
			autoplay: true,
			autoplayTimeout: 4000,
			autoplayHoverPause: true,
			animateIn: 'fadeInDown',
			animateOut: 'fadeOutRight',
			dots: true,
			nav: false,
			navText : ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"],
			center: false,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1,
				},
				480: {
					items: 2,
				},
				768: {
					items: 2,
				},
				992: {
					items: 3,
				},
				1200: {
					items: 4,
				}
			}
		})

		$(function() {
			//var fieldName ="";
			$('.product_quantity_add').click(function(e){
				e.preventDefault();
				//fieldName = $(this).class('.product_quantity_add');
		
				//Fetch qty in the current elements context and since you have used class selector use it.
				var qty = $(this).closest('.cart_content').find('#product_quantity_input');
				//var qty = $(this).closest('tr').find('input[name='+fieldName+']');
		
				var currentVal = parseInt(qty.val());
				if (!isNaN(currentVal)) {
					qty.val(currentVal + 1);
				} else {
					qty.val(0);
				}
		
				//Trigger change event
				qty.trigger('change');
			});
		
			$(".product_quantity_subtract").click(function(e) {
				e.preventDefault();
				//fieldName = $(this).class('.product_quantity_subtract');
		
				//Fetch qty in the current elements context and since you have used class selector use it.
				var qty = $(this).closest('.cart_content').find('#product_quantity_input');
				//var qty = $(this).closest('tr').find('input[name='+fieldName+']');
		
				var currentVal = parseInt(qty.val());
				if (!isNaN(currentVal) && currentVal > 0) {
					qty.val(currentVal - 1);
				} else {
					qty.val(0);
				}
		
				//Trigger change event
				qty.trigger('change');
			});     
		
			//Bind the change event
			$("#product_quantity_input").change(function() {
				var sum = 0;
				var total = 0;
				$('.product_per_price').each(function () {
					var price = $(this);
					var count = price.closest('.cart_content').find('#product_quantity_input');
					sum = (price.html() * count.val());
					total = total + sum;
					price.closest('.cart_content').find('.product_total_price').html(sum + "");
				});
				$('.total_price').html("$" + total + "");
		
			}).change(); //trigger change event on page load
		});


		//caches a jQuery object containing the header element
		var header = $(".header");
		$(window).scroll(function() {
			var scroll = $(window).scrollTop();

			if (scroll >= 50) {
				header.addClass("dark_mode");
			} else {
				header.removeClass("dark_mode");
			}
		});
	});

})(jQuery);



// Hide header on scroll down
var didScroll;
var lastScrollTop = 0;
var navbarHeight = $('header').outerHeight();

$(window).scroll(function(event){
	didScroll = true;
});

setInterval(function() {
	if (didScroll) {
		hasScrolled();
		didScroll = false;
	}
}, 50);

function hasScrolled() {
	var st = $(this).scrollTop();
	

	if (st > lastScrollTop && st > navbarHeight){
		// Scroll Down
		$('header .top_bar').addClass('top-up');
	} else {
		// Scroll Up
		$('header .top_bar').removeClass('top-up');
	}
	
	lastScrollTop = st;
}
// End Sticky Header