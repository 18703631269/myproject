// JavaScript Document
;(function($){	
	//focus
	focus_width = $('.gpic').width();
	console.log("focus_width",focus_width);
	li_num = $('.gpic li').length;
    console.log("li_num",li_num);
	ul_width = focus_width * li_num;
	$('.gpic ul').css('width', ul_width + 'px');
	$('.gpic ul li').css('width', focus_width + 'px');	
	$('.gpic ul').append($('.gpic ul').html());
	$('.gpic ul').css('width', (ul_width * 2) + 'px');
	setInterval('f()',5000);	
})(Zepto)

function f(){
	ml = parseInt($('.gpic ul').css('margin-left'));
	$('.gpic ul').animate({'margin-left':(ml - focus_width) + 'px'},'slow','swing',function(){
		ml = parseInt($('.gpic ul').css('margin-left'));		
		if(ml + ul_width == 0){
			$('.gpic ul').css('margin-left', '0px');
		}
	});
}
