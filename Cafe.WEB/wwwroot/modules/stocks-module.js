let lang = "ar";

$(document).on('keypress', '#txtPhone', function (e) {
    return isNumberKey(e);
});
$(document).on('click', '.btnRefresh', function (e) {
    e.preventDefault();

    $('#SearchString').val('');
    $('#mainDrp').val(0);
    $('.searchDiv').removeClass('is-filled');

    LoadIndex();
});

$(document).on('click', '.btnModalClose', function () {
    $("#addStockModal").modal("hide");
});

$(document).on('click', '.btnAdd', function (e) {
    e.preventDefault();

    lang = $(this).data('lang');

    $('.divTxt').removeClass('is-filled');

    $('#txtName').val('');
    $('#txtAdd').val('');
    $('#txtPhone').val('');
    $('#txtEmail').val('');
    $('#mainDrpModal').val(0);

    $('#modalTitle').html(lang == "ar" ? "إضافة" : "Add");
    $('#stockId').val(0);
    $("#addStockModal").appendTo('body').modal("show");
});

$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    lang = $(this).data('lang');

    $.ajax({
        url: '/Stocks/GetById?id=' + id,
        async: true,
        success: function (result) {
            if (result != null && result.name != "") {
                $('#txtName').val(result.name);
                $('#txtAdd').val(result.address);
                $('#txtPhone').val(result.phone);
                $('#txtEmail').val(result.email);

                let main = 0;
                if (result.mainStockId != null && result.mainStockId > 0 && result.mainStockId != id) {
                    main = result.mainStockId;
                }

                $('#mainDrpModal').val(main);

                $('.divTxt').addClass('is-filled');

                $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");
                $('#stockId').val(id);
                $("#addStockModal").appendTo('body').modal("show");
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

$(document).on('click', '#btnAddEditStock', function (e) {
    e.preventDefault();

    let id = $('#stockId').val();
    let name = $('#txtName').val();
    let address = $('#txtAdd').val();
    let email = $('#txtEmail').val();
    let phone = $('#txtPhone').val();
    let mainSt = $('#mainDrpModal').val();

    if (name.trim() == "" || address.trim() == "") {
        $('.alertEmpty').css("display", "block");
        return;
    }

    //check if mail is true
    if (email.trim() != "" && !validateEmail(email)) {
        alert(lang == "ar" ? "تأكد من إدخال بريد إلكتروني صحيح" : "Enter a valid Email Address");
        return false;
    }

    $('.alertEmpty').css("display", "none");

    let uri = id == 0 ? '/Stocks/Add' : '/Stocks/Edit';

    let obj = {
        Id: id,
        Name: name,
        Address: address,
        Email: email,
        Phone: phone,
        MainStockId: mainSt
    };

    $.ajax({
        url: uri,
        type: "POST",
        async: true,
        data: obj,
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
                FillMainStocks();

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

            $("#addStockModal").modal("hide");
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
                    url: '/Stocks/Delete?id=' + id,
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
                            FillMainStocks();

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
    let mainId = $('#mainDrp').val();

    $.ajax({
        url: '/Stocks/PrintData?term=' + term + '&mainStock=' + mainId,
        async: true,
        success: function (result) {
            let restorepage = document.body.innerHTML;

            document.body.innerHTML = GetPrintString(result);
            window.print();

            document.body.innerHTML = restorepage;

            $('#SearchString').val(term);
            $('#mainDrp').val(mainId);
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
                    url: '/Stocks/ActivateStock?id=' + id + '&isActive=' + active,
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
                            FillMainStocks();
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
function GetPrintString(result) {
    let name = lang == "ar" ? "الاسم" : "Name";
    let addr = lang == "ar" ? "العنوان" : "Address";
    let contact = lang == "ar" ? "وسائل الإتصال" : "Contact Data";
    let mainStr = lang == "ar" ? "مخزن رئيسي" : "Main Stock";
    let branStr = lang == "ar" ? "مخزن فرعي" : "Branch Stock";

    let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th style="text-align:center">${name}</th>
                            <th style="text-align:center">${addr}</th>
                            <th style="text-align:center">${contact}</th>
                        </tr>
                    </thead>
                    <tbody>`;

    for (const item of result) {
        let mainBr = `<br />`;
        if (item.mainStockId != null) {
            if (item.mainStockId == item.id) {
                mainBr = `<span class="mainstock">${mainStr}</span>`;
            }
            else {
                mainBr = `<span class="branchstock">${branStr}</span>`;
            }
        }

        let mail = "";
        if (item.email != null && item.email != "") {
            mail = `<span>
                                <i class="fa fa-envelope" style="color:#3aaf85"></i>
                                &nbsp;&nbsp;${item.email}
                            </span>
                            <br />`;
        }

        let phone = "";
        if (item.phone != null && item.phone != "") {
            phone = `<span>
                                <i class="fa fa-phone" style="color: #55acee;"></i>
                                &nbsp;&nbsp;${item.phone}
                            </span>`;
        }

        printcontent += `<tr>
                            <td style="text-align:center">
                                <span>${item.name}</span><br />
                                ${mainBr}
                            </td>
                            <td style="text-align:center">${item.address}</td>
                            <td style="text-align:center">
                                 ${mail} ${phone}
                            </td>
                        </tr>`;
    }

    printcontent += `</tbody>
                </table>`;

    return printcontent;
}
function LoadIndex() {
    let uri = '/Stocks/Index';

    let term = $('#SearchString').val();
    let mainId = $('#mainDrp').val();

    if (term != "" || mainId != 0) {
        uri = '/Stocks/Search?term=' + term + '&mainStock=' + mainId;
    }

    $.ajax({
        url: uri,
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

function FillMainStocks() {
    let main = $('#mainDrp').val();

    $.ajax({
        url: '/Stocks/GetMainStocks',
        async: true,
        success: function (result) {
            $('.dropMain').empty();
            let exist = false;
            $.each(result, function (index, value) {
                if (value.id == main) {
                    exist = true;
                }

                $('.dropMain').append($("<option />").val(value.id).text(value.nameAr));
            });

            if (exist) {
                $('#mainDrp').val(main);
                LoadIndex();
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