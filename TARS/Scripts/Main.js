
dateTime = function () {
	var now = new Date();
	var hours = now.getHours();
	var greet;

	if (hours < 12) {
		greet = "Good Morning,";
	} else if (hours < 16) {
		greet = "Good Afternoon,";
	} else {
		greet = "Good Evening,";
	}

	$('#time').html(greet);
};


$(document).ready(function () {
	dateTime();
	$('#wait').hide();

	$("#toggleIcon").click(function () {
		$(".admin_nav").toggleClass("Admin_mob");

	})
	$(".admin_nav li").click(function () {
		$(".admin_nav").toggleClass("Admin_mob");

	})



	$("#Dashboard").click(function () {
		
		$.ajax({
			url: "/Admin/Home",
			success: function (result) {
				$('#body_container').html(result);
			}
		});
	});

	$("#Add_Employee").click(function () {
		$.ajax({
			url: "/Admin/Add_Employee",
			//beforeSend: function () { $('#wait').show(); },
			//complete: function () { $('#wait').hide(); },
			success: function (result) {
				$('#body_container').html(result);
			}
		});
	});


	$("#Emp").click(function () {
		
		
		$.ajax({

			url: "/Admin/Our_Employee",
			success: function (result) {
				$('#body_container').html(result);

			}


			
		})
		
	});

	$("#Parcels").click(function () {

		$.ajax({
			url: "/Admin/Parcels",
			success: function (result) {
				$('#body_container').html(result);
			}
		});
	});


	$("#Pricing").click(function () {

		$.ajax({
			url: "/Admin/Pricing",
			success: function (result) {
				$('#body_container').html(result);
			}
		});
	});


	$("#Center").click(function () {

		$.ajax({
			url: "/Admin/Center",
			success: function (result) {
				$('#body_container').html(result);
			}
		});
	});



	$("#track_btn").click(function () {
		var searchTxt = $("#track_value").val();


		$.ajax({
			url: "/Home/Tracking",
			data: {
				search: searchTxt
			},

		})
			.done(function (result) {
				$('body').html(result);
			})

	});




	$("#centerbtn").click(function () {
		


		$.ajax({
			url: "/Home/Center",
			method: "POST",
			data: {
				city: $("#city_value").val(),
				area: $("#area_value").val()
			},

		})
			.done(function (result) {
				$('body').html(result);
				$("#city_value").val("");
				$("#area_value").val("");
			})

	});



})





function PreviewImage() {
	var oFReader = new FileReader();
	oFReader.readAsDataURL(document.getElementById("file").files[0]);

	oFReader.onload = function (oFREvent) {
		document.getElementById("text").src = oFREvent.target.result;
	};
};