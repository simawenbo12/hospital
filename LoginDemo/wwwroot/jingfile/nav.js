

//$(document).ready(function(){
//	document.getElementById('li1').className= "l1";
//	document.getElementById('li2').className= "l2"; 
//	document.getElementById('li3').className= "l2"; 
//	//document.getElementById('li4').className= "l2"; 
//	document.getElementById('li5').className= "l2"; 
//	//document.getElementById('li6').className= "l2"; 

//});


function changeBody(index){    

switch(index){  
case 1:{
document.getElementById('li1').className= "l1";  
document.getElementById('li2').className= "l2"; 
document.getElementById('li3').className= "l2"; 
//document.getElementById('li4').className= "l2"; 
document.getElementById('li5').className= "l2"; 
//document.getElementById('li6').className= "l2"; 
 
document.getElementById('iDBody1').style.display = "";    
document.getElementById('iDBody2').style.display = "none";    
document.getElementById('iDBody3').style.display = "none";    
//document.getElementById('iDBody4').style.display = "none";    
document.getElementById('iDBody5').style.display = "none";    
//document.getElementById('iDBody6').style.display = "none";  
break;  
}    
case 2:{ 
document.getElementById('li1').className= "l2";  
document.getElementById('li2').className= "l1"; 
document.getElementById('li3').className= "l2"; 
//document.getElementById('li4').className= "l2"; 
document.getElementById('li5').className= "l2"; 
//document.getElementById('li6').className= "l2";  
  
document.getElementById('iDBody1').style.display = "none";    
document.getElementById('iDBody2').style.display = "";    
document.getElementById('iDBody3').style.display = "none";    
//document.getElementById('iDBody4').style.display = "none";    
document.getElementById('iDBody5').style.display = "none";    
//document.getElementById('iDBody6').style.display = "none";  
break;  
} 
case 3:{  
document.getElementById('li1').className= "l2";  
document.getElementById('li2').className= "l2"; 
document.getElementById('li3').className= "l1"; 
//document.getElementById('li4').className= "l2"; 
document.getElementById('li5').className= "l2"; 
//document.getElementById('li6').className= "l2";  
  
document.getElementById('iDBody1').style.display = "none";    
document.getElementById('iDBody2').style.display = "none";    
document.getElementById('iDBody3').style.display = "";    
//document.getElementById('iDBody4').style.display = "none";    
document.getElementById('iDBody5').style.display = "none";    
//document.getElementById('iDBody6').style.display = "none";  
break;  
} 
case 4:{  
document.getElementById('li1').className= "l2";  
document.getElementById('li2').className= "l2"; 
document.getElementById('li3').className= "l2"; 
//document.getElementById('li4').className= "l1"; 
document.getElementById('li5').className= "l2"; 
//document.getElementById('li6').className= "l2"; 
  
document.getElementById('iDBody1').style.display = "none";    
document.getElementById('iDBody2').style.display = "none";    
document.getElementById('iDBody3').style.display = "none";    
//document.getElementById('iDBody4').style.display = "";    
document.getElementById('iDBody5').style.display = "none";    
//document.getElementById('iDBody6').style.display = "none";  
break;  
} 
case 5:{  
document.getElementById('li1').className= "l2";  
document.getElementById('li2').className= "l2"; 
document.getElementById('li3').className= "l2"; 
//document.getElementById('li4').className= "l2"; 
document.getElementById('li5').className= "l1"; 
//document.getElementById('li6').className= "l2"; 


document.getElementById('iDBody1').style.display = "none";    
document.getElementById('iDBody2').style.display = "none";    
document.getElementById('iDBody3').style.display = "none";    
//document.getElementById('iDBody4').style.display = "none";    
document.getElementById('iDBody5').style.display = "";    
//document.getElementById('iDBody6').style.display = "none";  
break;  
} 
case 6:{   
document.getElementById('li1').className= "l2";  
document.getElementById('li2').className= "l2"; 
document.getElementById('li3').className= "l2"; 
//document.getElementById('li4').className= "l2"; 
document.getElementById('li5').className= "l2"; 
//document.getElementById('li6').className= "l1"; 
 
document.getElementById('iDBody1').style.display = "none";    
document.getElementById('iDBody2').style.display = "none";    
document.getElementById('iDBody3').style.display = "none";    
//document.getElementById('iDBody4').style.display = "none";    
document.getElementById('iDBody5').style.display = "none";    
//document.getElementById('iDBody6').style.display = "";  
break;  
} 

 }    
}

(function(){
	
	var navHover = function(){
		var subtop = $('.j-subtop'), subbottom = $('.j-subnav'),subtopA = $('.j-subtop a'),crt,subshow;

		$('#J_Nav li').mouseenter(function(){
			if(!$(this).hasClass('j-crt')){
				$(this).addClass('hover');
			}
		});
		$('#J_Nav li').mouseleave(function(){
			$(this).removeClass('hover');
		});

		function setCurrentMenu(){
			subtopA.each(function(){
				if($(this).hasClass('j-crt')){
					crt = $(this).parent().attr('tab');
				}
			})
		}

		setCurrentMenu();

		function showSubmenu(classname){
			subbottom.each(function(){
				if($(this).hasClass(classname)){
					$(this).show();
				}else{
					$(this).hide();
				}
			});
		}
		
		function hideSubmenu(){
			subbottom.each(function(){
				if($(this).hasClass(crt)){
					$(this).show();
				}else{
					$(this).hide();
				}
			});
		}

		subtop.mouseenter(function(e){		
			if($(this).hasClass('shouye')){
				$('#J_SubNav').addClass('m-subnav-w');
			}else{
				$('#J_SubNav').removeClass('m-subnav-w');
			}
			if($('#J_SubNav').css('display') === 'none'){
				subshow = true;
				$('#J_SubNav').show();
				$('#innerid').slideDown(500);
			}
			var name = $(this).attr('tab');
			showSubmenu(name);
		});
		
		$('#J_Header').mouseleave(function(){
			hideSubmenu();
			$('#J_SubNav').removeClass('m-subnav-w');
			if(subshow){
				$('#J_SubNav').hide();
				$('#innerid').slideUp(50);
			}
		});
		
	};
	
	$().ready(function(){
		navHover();	
		
		
	});
	
	
	

})();
