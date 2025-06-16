let lang = "ar";

$(document).on('keypress', '#txtToEg', function (e) {
    return isNumberKeyDecimal(e);
});

$(document).on('click', '.btnRefresh', function (e) {
    e.preventDefault();

    $('#SearchString').val('');
    $('.searchDiv').removeClass('is-filled');

    LoadIndex();
});

$(document).on('click', '.btnModalClose', function () {
    $("#addCurrencyModal").modal("hide");
});

$(document).on('click', '.btnAdd', function (e) {
    e.preventDefault();

    lang = $(this).data('lang');

    $('#txtName').val('');
    $('.divName').removeClass('is-filled');

    $('#txtToEg').val('');
    $('.divToEgp').removeClass('is-filled');

    $('#modalTitle').html(lang == "ar" ? "إضافة" : "Add");
    $('#currId').val(0);
    $("#addCurrencyModal").appendTo('body').modal("show");
});

$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    lang = $(this).data('lang');

    $.ajax({
        url: '/Treasury/Currency/GetById?id=' + id,
        async: true,
        success: function (result) {
            debugger;
            if (result != null && result.currencyName != "") {
                $('#txtName').val(result.currencyName);
                $('.divName').addClass('is-filled');

                $('#txtToEg').val(result.covertToEgp);
                $('.divToEgp').addClass('is-filled');

                $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");
                $('#currId').val(id);
                $("#addCurrencyModal").appendTo('body').modal("show");
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

$(document).on('click', '#btnAddEditCurr', function (e) {
    e.preventDefault();

    let id = $('#currId').val();
    let name = $('#txtName').val();
    let toegp = $('#txtToEg').val();

    if (name == "") {
        $('.alertEmpty').css("display", "block");
        return;
    }

    $('.alertEmpty').css("display", "none");

    let uri = id == 0 ? '/Treasury/Currency/Add?curName=' + name + '&toEg=' + toegp
        : '/Treasury/Currency/Edit?id=' + id + '&curName=' + name + '&toEg=' + toegp;

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

            $("#addCurrencyModal").modal("hide");
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
                    url: '/Treasury/Currency/Delete?id=' + id,
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
        url: '/Treasury/Currency/PrintData?term=' + term,
        async: true,
        success: function (result) {

            let name = lang == "ar" ? "الاسم" : "Name";
            let convert = lang == "ar" ? "التحويل للمصري" : "Convert to EGP";

            let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th style="text-align:center">${name}</th>
                            <th style="text-align:center">${convert}</th>
                        </tr>
                    </thead>
                    <tbody>`;

            for (const item of result) {
                printcontent += `<tr>
                            <td style="text-align:center">${item.currencyName}<br />
                                <span class="mainstock">${item.isActive}</span>
                            <td style="text-align:center">${item.covertToEgp}</td>
                        </tr>`;
            }

            printcontent += `</tbody>
                </table>`;

            let restorepage = document.body.innerHTML;

            document.body.innerHTML = printcontent;
            window.print();

            document.body.innerHTML = restorepage;

            $('#SearchString').val(term);
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

$(document).on('click', '.btnActive', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    let active = $(this).data('active');

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Treasury/Currency/ActivateCurrency?id=' + id + '&isActive=' + active,
                    async: true,
                    success: function (result) {
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

//***********************************************************************/
function LoadIndex() {
    let uri = '/Treasury/Currency/Index';

    let term = $('#SearchString').val();
    if (term != "") {
        uri = '/Treasury/Currency/Search?term=' + term;
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