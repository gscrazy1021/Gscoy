$(document).ready(function(){
	var label= $('label');
	var content= $('.content');
	$('.content').not(":first").hide();
	$('label').on("click",function(){
		var labelClicked = $(this);
		var labelContent = labelClicked.next();
		if(labelContent.is(":visible")) {
			return;
		}
		content.slideUp("normal");
		labelContent.slideDown("slow");
	});
});