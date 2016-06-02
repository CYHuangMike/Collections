
    $(window).load(function(){
        $('#cubeSpinner').impulseslider({
            height: 130,
            width: 420,
            pauseTime: 5000,
            transitionDuration: 2000,
            transitionEffect: "linear",
            autostart: true,
            rightSelector: ".right1",
            leftSelector: ".left1",
            directionRight: true,
            containerSelector: "#cubeCarousel",
            spinnerSelector: "#cubeSpinner"
        });
    });
