$(function(){
	//控制banner按钮动画
	$(".leftbtn").hover(function(){
		$(this).children("a").animate({left:'50%'},300);	
	},function(){
		$(this).children("a").animate({left:'35%'},300);		
	})	
	$(".rightbtn").hover(function(){
		$(this).children("a").animate({right:'50%'},300);	
	},function(){
		$(this).children("a").animate({right:'35%'},300);		
	})
	//加载客服动画
	 $('.kefu-list ul li').hover(function(){
		$(this)	.addClass('animated fadeInDown'); 
	},function(){
		$(this)	.removeClass('animated fadeInDown'); 	
	})
	//内容一按钮动画
	$(".c1-right ul li div.c1right").hover(function(){
		$(this).animate({left:'15px'},300);	
	},function(){
		$(this).animate({left:'0px'},300);		
	})
	//内容二按钮动画
	$(".c2-main ul li").hover(function(){
		$(this).animate({top:'-15px'},300);	
	},function(){
		$(this).animate({top:'0px'},300);		
	})
	//去除边距
	$(".c2-main ul li:last-of-type").css("margin-right","0px")
	$(".c3-tops ul li:last-of-type").css("margin-right","0px")
	$(".c4-slide .hdd ul li:last-of-type").css("margin-bottom","0px")
	$(".c3r-main div.c3r-list:last-of-type").css("margin-bottom","0px")
	//内容三按钮动画
	$(".c3-tops ul li").hover(function(){
		$(this).animate({top:'-15px'},300);	
	},function(){
		$(this).animate({top:'0px'},300);		
	})
	//FAQ
	$(".sideMenu h3").click(function(){
		$(this).siblings("ul").slideDown();	
		$(this).addClass("on");	
		$(this).parents().siblings().children("h3").removeClass("on");	
		$(this).parents().siblings().children("ul").slideUp();	
	})
	//文本框焦点
	$(document).ready(function() {  
		$(".input-text").val("搜索医生、文章等....");  
		textFill( $('.input-text') );  
		$(".input-text2").val("请输入科室名称...");  
		textFill( $('.input-text2') );  
	});  
	function textFill(input){ //input focus text function  
		var originalvalue = input.val();  
		input.focus( function(){  
			if( $.trim(input.val()) == originalvalue ){
				input.val(''); 
			}  
		}).blur( function(){  
			if( $.trim(input.val()) == '' ){ 
				input.val(originalvalue); 
			}  
		});  
	}
	//
})


