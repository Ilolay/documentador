$j = jQuery;
if (typeof (window.$) === 'undefined') { window.$ = jQuery; }
$j(document).ready(function () {   
	// Apply fancybox on all images
	//$j("a[href$='gif']").fancybox();
	//$j("a[href$='jpg']").fancybox();
	//$j("a[href$='png']").fancybox();
	                     
    $j('#menu-main-navigation li').hover(function () {
        $j(this).find('ul:first').css({'visibility': 'visible', 'display': 'none'}).slideDown('fast');
    }, function () {
        $j(this).find('ul:first').css({visibility: 'hidden'});
    });
    
    $j('li.comment').each(function () {
		var reply = $j(this).find('.reply:first');
		$j(this).remove('.reply:first');
		$j(this).find('.vcard:first').prepend(reply);
	});	
    
    $j('#commentform p').each(function () {
        var inputBox = $j(this).find('input');
		$j(this).remove('input');        
        $j(this).prepend(inputBox)
    });
    
    cancelReply = $j('#cancel-comment-reply-link');
	$j('#cancel-comment-reply-link').remove();
	$j('#respond').prepend(cancelReply);

    $j('li.comment div.first').hover(function () {
        $j(this).find('.reply:first').show();
    }, function () {
        $j(this).find('.reply:first').hide();
    });        

    var firstItem = $j('.main-menu').find('li:first');
    if (firstItem.find('strong').length == 0) {        
        var content = firstItem.find('a').text();
        firstItem.find('a').html('<strong>' + content + '</strong>');
    }
    
    $j('.footer-wrap .menu li').each(function (index) {
        $j(this).append('<span class="sep">|</span>');    
    });

    $j('div.footer .rightlnk .sep:last').css({'display': 'none'});
    
    //InitSliderNivo();
    
    //InitSliderKwicks();
    
    InitCufon();
});

$j(window).load(function() {
	//InitSliderNivo();
});

function InitCufon() {

    // Font replacement
    Cufon.replace('.logo .title', { fontFamily: 'Zurich Cn BT' }); 
    Cufon.replace('.main-menu li strong', { fontFamily: 'Zurich Cn BT', textShadow: '#333333 -1px -2px' }); 

    Cufon.replace('h1', { fontFamily: 'Zurich Cn BT' }); 
    Cufon.replace('h2', { fontFamily: 'Zurich Cn BT' }); 
    Cufon.replace('h3', { fontFamily: 'Zurich Cn BT' }); 
    Cufon.replace('h4', { fontFamily: 'Zurich Cn BT' });

    Cufon.replace('h1 .thin', { fontFamily: 'Zurich LtCn BT' }); 
    Cufon.replace('h2 .thin', { fontFamily: 'Zurich LtCn BT' }); 
    Cufon.replace('h3 .thin', { fontFamily: 'Zurich LtCn BT' }); 
    Cufon.replace('h4 .thin', { fontFamily: 'Zurich LtCn BT' });

    Cufon.replace('.stamp strong', { fontFamily: 'Zurich Cn BT' }); 
    Cufon.replace('.stamp.gold strong', { textShadow: '#584324 -1px -2px' }); 
    Cufon.replace('.stamp.blue strong', { textShadow: '#32B6D9 -1px -2px' }); 

    Cufon.replace('.bigbut .title', { fontFamily: 'Zurich Cn BT', textShadow: '#333333 -1px -2px'});
    Cufon.replace('.bigbut.blue .title', { textShadow: '#32B6D9 -1px -2px' }); 
    Cufon.replace('.bigbut.purple .title', { textShadow: '#AC5F92 -1px -2px' });
    
    Cufon.replace('.portfolio-website .website-name', { fontFamily: 'Zurich Cn BT' }); 

}

//function InitSliderNivo() {
//	$j('#slider-nivo .slides').nivoSlider({
//		effect:'fade', //Specify sets like: 'fold,fade,sliceDown'
//		slices:15,
//		animSpeed:500, //Slide transition speed
//		pauseTime:3000,
//		startSlide:0, //Set starting Slide (0 index)
//		directionNav:false, //Next & Prev
//		directionNavHide:true, //Only show on hover
//		controlNav:true, //1,2,3...
//		controlNavThumbs:false, //Use thumbnails for Control Nav
//        controlNavThumbsFromRel:false, //Use image rel for thumbs
//		controlNavThumbsSearch: '.jpg', //Replace this with...
//		controlNavThumbsReplace: '_thumb.jpg', //...this in thumb Image src
//		keyboardNav:true, //Use left & right arrows
//		pauseOnHover:true, //Stop animation while hovering
//		manualAdvance:false, //Force manual transitions
//		captionOpacity:1, //Universal caption opacity
//		beforeChange: function(){
//        },
//		afterChange: function(){
//        },
//		slideshowEnd: function(){} //Triggers after all slides have been shown
//	});
//}

//function InitSliderKwicks() {
//    $j('#slider-kwicks ul').kwicks({
//            max: 800,
//            spacing: 0,
//			duration: 1000
//    });
//    $j('#slider-kwicks  li').hover(function () {
//            $j('#slider-kwicks ul li').not($j(this)).find('img').stop().show()
//            $j('#slider-kwicks ul li').not($j(this)).find('.kwicks-caption').stop().hide();
//        },
//        function (){
//            $j('#slider-kwicks ul li').not($j(this)).find('img').stop().fadeTo('slow', 1);
//            $j('#slider-kwicks ul li').not($j(this)).find('.kwicks-caption').stop().show();
//    });    
//}