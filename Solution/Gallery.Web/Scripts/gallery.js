/// <reference path="~/Scripts/jquery-1.8.2-vsdoc.js" />


////////////////////////////////////////////////////////////////////////////////////////////////
/*--------------------------------------------------------------------------------------------*/
$(document).ready(function() {
	setupThumbs();
	cropThumbs();

	setupMainPhoto();
	resizeMainPhoto();
});

/*--------------------------------------------------------------------------------------------*/
$(window).resize(function() {
	cropThumbs();
	resizeMainPhoto();
});


////////////////////////////////////////////////////////////////////////////////////////////////
/*--------------------------------------------------------------------------------------------*/
function setupThumbs() {
	$("img.thumb").css("display", "none");
	var thumbs = $("img.thumb").toArray();
	
	for ( var i = 0 ; i < thumbs.length ; ++i ) {
		var t = thumbs[i];
		
		if ( isImageLoaded(t) ) {
			onThumbLoad(t);
		}
		else {
			$(t).load(function() { onThumbLoad(this); });
		}
	}
}

/*--------------------------------------------------------------------------------------------*/
function onThumbLoad(pThumb) {
	captureImageSize(pThumb);
	resizeThumb(pThumb);
}

/*--------------------------------------------------------------------------------------------*/
function cropThumbs() {
	var thumbs = $("img.thumb").toArray();

	for ( var i = 0 ; i < thumbs.length ; ++i ) {
		resizeThumb(thumbs[i]);
	}
}

/*--------------------------------------------------------------------------------------------*/
function isImageLoaded(pImage) {
	var img = $(pImage).get(0);
	console.log(img.complete+" - "+img.readyState);
	return (img.complete || img.readyState);
}

/*--------------------------------------------------------------------------------------------*/
function captureImageSize(pThumb) {
	$(pThumb).css("display", "inherit");
	$(pThumb).css("position", "absolute");
	$(pThumb).data("origW", $(pThumb).width());
	$(pThumb).data("origH", $(pThumb).height());
	$(pThumb).css("position", "inherit");
	
	var imgW = $(pThumb).data("origW");
	var imgH = $(pThumb).data("origH");
	//console.log(imgW+"/"+imgH+" ... "+$(pThumb).width()+"/"+$(pThumb).height());
}

/*--------------------------------------------------------------------------------------------*/
function resizeThumb(pThumb) {
	var par = $(pThumb).parent();
	var crop = $(par).parent();

	var cropW = $(crop).width();
	$(crop).css("height", cropW+"px");
	
	var imgW = $(pThumb).data("origW");
	var imgH = $(pThumb).data("origH");
	if ( !imgW ) { return ; }

	if ( imgH/imgW < 1.0 ) {
		$(pThumb).height(cropW);

		var mx = -(imgW*(cropW/imgH)-cropW)/2;
		$(par).css("margin-left", mx+"px");
		$(par).css("margin-right", mx+"px");
		$(par).css("margin-top", "0px");
	}
	else {
		$(par).css("margin-top", -(cropW*0.08)+"px");
		$(par).css("margin-left", "0px");
		$(par).css("margin-right", "0px");
	}
}


////////////////////////////////////////////////////////////////////////////////////////////////
/*--------------------------------------------------------------------------------------------*/
function setupMainPhoto() {
	var main = $("#mainPhoto");
	$(main).css("display", "none");
	
	if ( isImageLoaded(main) ) {
		onMainLoad(main);
	}
	else {
		$(main).load(function() { onMainLoad(this); });
	}
}

/*--------------------------------------------------------------------------------------------*/
function onMainLoad(pMain) {
	captureImageSize(pMain);
	resizeMainPhoto();
}

/*--------------------------------------------------------------------------------------------*/
function resizeMainPhoto() {
	var main = $("#mainPhoto");
	var par = $(main).parent();
	var parW = $(par).width();
	$(par).height(parW);

	var imgW = $(main).data("origW");
	var imgH = $(main).data("origH");
	if ( !imgW ) { return ; }

	var margT = 0;
	var margL = 0;
	
	if ( imgH/imgW < 1.0 ) { //landscape
		margT = (parW-$(main).height())/2;
	}
	else {
		$(main).height(parW);
		margL = (parW-$(main).width())/2;
	}

	$(main).css("padding", margT+"px "+margL+"px "+margT+"px "+margL+"px");
}
