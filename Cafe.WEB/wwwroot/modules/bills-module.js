let lang = "ar";
$(document).on('keypress', '#AcidNum', function (e) {
    return isNumberKey(e);
});

$('#RecieveShipment').change(function () {
    if ($(this).is(':checked')) {
        $('#reciveNotesDiv').show();
    } else {
        $('#reciveNotesDiv').hide();
    }
});    

function readURL(input) {
    if (input?.files[0]) {
        let reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);       
    }   
}
$("#imageUpload").change(function () {
    readURL(this);
});

$(document).on('click', '#searchBTN', function (e) {
    e.preventDefault();
    LoadIndex(1);
});

$(document).on('click', '#btnRefresh', function (e) {
    e.preventDefault();
    LoadIndex(1);
});

$(document).on('click', '.btnModalClose', function () {
    $("#detailsModal").modal("hide");
});

$(document).on("click", ".btnDetails", function (e) {
    let id = $(this).data("id");
    e.preventDefault();
    
    $("#detailsModal").html($(".dtails-container[data-id='" + id + "']").html());
    $("#detailsModal").appendTo('body').modal("show");
});

$(document).on("click", ".btnDelete", function (e) {
    let id = $(this).data("id");
    e.preventDefault();

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/ShipmentBills/Bills/DeleteBill?id=' + id,
                    async: true,
                    success: function (result) {
                        if (result == 1) {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                                class_name: 'growl-success',
                                sticky: false,
                                time: '1500',
                            });

                            let page = $('.pagination').find('li.active').find('span').html();
                            LoadIndex(page);

                        } else {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى"
                                    : "Error happend please try again later",
                                class_name: 'growl-warning',
                                sticky: false,
                                time: '1500',
                            });
                        }
                    },
                    error: function (xhr) {
                        jQuery.gritter.add({
                            position: lang == "ar" ? 'top-left' : 'top-right',
                            text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
                            class_name: 'growl-warning',
                            sticky: false,
                            time: '1500',
                        });
                    }
                });
            }
        }
    });
});

function LoadIndex(page) {
    let search = $('#searchString').val();

    $.ajax({
        url: '/ShipmentBills/Bills/Index?searchString=' + search + '&page=' + page,
        async: true,
        success: function (result) {
            $('#divdata').html(result);
        },
        error: function (xhr) {
            jQuery.gritter.add({
                position: lang == "ar" ? 'top-left' : 'top-right',
                text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
                class_name: 'growl-warning',
                sticky: false,
                time: '1500',
            });
        }
    });
}