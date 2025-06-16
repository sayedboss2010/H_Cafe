let lang = "ar";

$(document).on('click', '.btnModalClose', function () {
    $("#addCostModal").modal("hide");
});

$(document).on('click', '.btnAdd', function (e) {
    e.preventDefault();

    $('#txtName').val('');
    $('.divName').removeClass('is-filled');

    $('#txtCost').val('');
    $('.divCost').removeClass('is-filled');

    $('#modalTitle').html(lang == "ar" ? "إضافة" : "Add");
    $('#costId').val(0);
    $("#addCostModal").appendTo('body').modal("show");
});

$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();

    let id = $(this).data('id');

    if (id != 0) {
        $.ajax({
            url: '/ShipmentBills/Bills/GetCostById?id=' + id,
            async: true,
            success: function (result) {

                if (result != null && result.name != "") {
                    $('#txtName').val(result.name);
                    $('.divName').addClass('is-filled');

                    $('#txtCost').val(moneyDigits(result.cost));
                    $('.divCost').addClass('is-filled');

                    $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");
                    $('#costId').val(id);
                    $("#addCostModal").appendTo('body').modal("show");
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
    }
    else {
        let name = $(this).data('name');

        $('#txtName').val(name);
        $('.divName').addClass('is-filled');

        $('#txtCost').val();
        $('.divCost').addClass('is-filled');

        $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");
        $('#costId').val(id);
        $("#addCostModal").appendTo('body').modal("show");
    }
});

$(document).on('click', '#btnAddEditCost', function (e) {
    e.preventDefault();

    let id = $('#costId').val();
    let billId = $('#billIdHdn').val();
    let name = $('#txtName').val();
    let cost = $('#txtCost').val();

    if (name == "" || cost == "") {
        $('.alertEmpty').css("display", "block");
        return;
    }

    $('.alertEmpty').css("display", "none");

    let uri = id == 0
        ? '/ShipmentBills/Bills/AddCost?name=' + name + '&cost=' + cost + '&billId=' + billId
        : '/ShipmentBills/Bills/EditCost?id=' + id + '&name=' + name + '&cost=' + cost;

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            $("#addCostModal").modal("hide");

            if (result > 0) {
                jQuery.gritter.add({
                    position: lang == "ar" ? 'top-left' : 'top-right',
                    text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                    class_name: 'growl-success',
                    sticky: false,
                    time: '1500'
                });

                window.location.reload();

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
                    url: '/ShipmentBills/Bills/DeleteCost?id=' + id,
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

                            window.location.reload();
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

$(document).on('click', '#searchBTNCost', function (e) {
    e.preventDefault();

    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/ShipmentBills/Bills/AllCostsReport?fromDate=' + fromdate
        + '&todate=' + todate + '&page=1';

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
});

$(document).on('click', '.btnPrint', function (e) {
    e.preventDefault();

    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/ShipmentBills/Bills/PrintAllCostReport?fromDate=' + fromdate
        + '&todate=' + todate;

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            if (result != -1) {
                printData(result);

                $('#txtDateFrom').val(fromdate ?? "");
                $('#txtDateTo').val(todate ?? "");
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

});

function printData(result) {

    let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th>البيان</th>
                            <th>التكلفة</th>
                        </tr>
                    </thead>
                    <tbody>`;

    let totalCost = 0;
    for (const item of result) {
        totalCost += item.totalCost;
        printcontent += `<tr>
                            <td>${item.name}</td>
                            <td>${item.totalCostDigit}</td>`;        
        printcontent += `</tr>`;
    }

    printcontent += `</tbody>
                     <tfoot class="custom-tfoot">
                            <tr>
                                <td>الإجمالي</td>
                                <td>${moneyDigits(totalCost.toString())}</td>
                            </tr>
                        </tfoot>
                </table>`;

    let restorepage = document.body.innerHTML;

    document.body.innerHTML = printcontent;
    window.print();

    document.body.innerHTML = restorepage;
}     