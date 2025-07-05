let lang = "ar";

$(document).on('click', '.btnRefresh', function (e) {
    e.preventDefault();

    $('#SearchString').val('');
    $('.searchDiv').removeClass('is-filled');

    LoadIndex();
});

$(document).on('click', '.btnModalClose', function () {
    $("#addTabelModal").modal("hide");
});

$(document).on('click', '.btnAdd', function (e) {

    e.preventDefault();

    lang = $(this).data('lang');

    $('#txtArName').val('');
    $('.divArName').removeClass('is-filled');

    $('#txtEnName').val('');
    $('.divEnName').removeClass('is-filled');

    $('#txtphoneNumber').val('');
    $('.divPhoneNumber').removeClass('is-filled');


    //$('.drpSelectFac').val(null).trigger('change');

    $('#modalTitle').html(lang == "ar" ? "إضافة" : "Add");
    $('#TableId').val(0);
    $("#addTabelModal").appendTo('body').modal("show");
});

$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    lang = $(this).data('lang');

    $('#TableId').val(id);

    $.ajax({
        url: '/Table/GetById?id=' + id,
        async: true,
        success: function (result) {
            //for (const key in result) {
            //    if (result.hasOwnProperty(key)) {
            //        alert(`${key}: ${result[key]}`);
            //    }
            //}
            debugger;
            if (result != null && result.nameAr != "") {
                $('#txtNote').val(result.notes);
                $('.divArName').addClass('is-filled');

              
                $('#mainDrpLocations').val(result.locationID);


                $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");

                $("#addTabelModal").appendTo('body').modal("show");
            }
        },
        error: function (xhr) {
            jQuery.gritter.add({
                position: lang == "ar" ? 'bottom-left' : 'bottom-right',
                text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
                class_name: 'growl-warning',
                sticky: false,
                time: '1500',
            });
        }
    });
});

$
    (document).on('click', '#btnAddEditTable', function (e) {
        e.preventDefault();

        let id = $('#TableId').val();
        let Notes = $('#txtNote').val();
      
        let LocationID = $('#mainDrpLocations').val();

        if (Notes == "" ) {
            $('.alertEmpty').css("display", "block");
            return;
        }

        $('.alertEmpty').css("display", "none");

        let uri = id == 0 ? '/Table/Add?Notes=' + Notes + '&mainDrpLocations=' + LocationID
            : '/Table/Edit?id=' + id + '&Notes=' + Notes + '&mainDrpLocations=' + LocationID

        $.ajax({
            url: uri,
            async: true,
            success: function (result) {

                if (result > 0) {
                    jQuery.gritter.add({
                        position: lang == "ar" ? 'top-left' : 'top-right',
                        text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                        class_name: 'growl-success',
                        sticky: false,
                        time: '1500'
                    });

                    LoadIndex();
                } else if (result == -1) {
                    jQuery.gritter.add({
                        position: lang == "ar" ? 'top-left' : 'top-right',
                        text: lang == "ar" ? "الاسم موجود مسبقا" : "Name Exist",
                        class_name: 'growl-warning',
                        sticky: false,
                        time: '1500',
                    });
                } else {
                    jQuery.gritter.add({
                        position: lang == "ar" ? 'top-left' : 'top-right',
                        text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
                        class_name: 'growl-warning',
                        sticky: false,
                        time: '1500',
                    });
                }

                $("#addTabelModal").modal("hide");
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
    });

$(document).on('click', '.btnDelete', function (e) {
    e.preventDefault();

    let id = $(this).data('id');

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Table/Delete?id=' + id,
                    async: true,
                    success: function (result) {
                        debugger;
                        if (result) {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                                class_name: 'growl-success',
                                sticky: false,
                                time: '1500',
                            });

                            LoadIndex();
                        } else {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
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
$(document).on('click', '.btnDelete', function (e) {
    e.preventDefault();

    let id = $(this).data('id');

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Table/Delete?id=' + id,
                    async: true,
                    success: function (result) {
                        debugger;
                        if (result) {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                                class_name: 'growl-success',
                                sticky: false,
                                time: '1500',
                            });

                            LoadIndex();
                        } else {
                            jQuery.gritter.add({
                                position: lang == "ar" ? 'top-left' : 'top-right',
                                text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
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
$(document).on('click', '.btnPrint', function () {
    let term = $('#SearchString').val();

    $.ajax({
        url: '/Table/PrintData?term=' + term,
        async: true,
        success: function (result) {

            let arName = lang == "ar" ? "الاسم باللغة العربية" : "Arabic Name";
            let enName = lang == "ar" ? "الاسم باللغة الإنجليزية" : "English Name";

            let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th style="text-align:center">${arName}</th>
                            <th style="text-align:center">${enName}</th>
                        </tr>
                    </thead>
                    <tbody>`;

            for (const item of result) {
                printcontent += `<tr>
                            <td style="text-align:center">${item.nameAr}</td>
                            <td style="text-align:center">${item.nameEn}</td>
                        </tr>`;
            }

            printcontent += `</tbody>
                </table>`;

            let restorepage = document.body.innerHTML;

            document.body.innerHTML = printcontent;
            window.print();

            document.body.innerHTML = restorepage;
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
});

$(document).on('click', '#searchBTN', function (e) {
    e.preventDefault();
    LoadIndex();
});

//***********************************************************************/
function LoadIndex() {
    let uri = '/Table/Index';

    let term = $('#SearchString').val();
    if (term != "") {
        uri = '/Table/Search?term=' + term;
    }

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            debugger;
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