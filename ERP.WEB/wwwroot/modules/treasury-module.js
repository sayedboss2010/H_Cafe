let lang = "ar";

function readURL(input) {
    if (input?.files[0]) {
        let reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);

        $('.btnPrint').css('display', 'inline-block');
    }
    else {
        $('.btnPrint').css('display', 'none');
    }
}
$("#imageUpload").change(function () {
    readURL(this);
});

$(document).on('keypress', '#Value', function (e) {
    return isNumberKeyDecimal(e);
});

//** MoveOut *******************************/
$(document).on("change", "#TrMoveOutTreasuryTypeId", function () {
    let currency = $(this).val();

    if (Number(currency) == 1) {
        $('#otherWithdrawDiv').css('display', 'block');
    } else {
        $('#otherWithdrawDiv').css('display', 'none');
    }
});

$(document).on('click', '.btnPrint', function (e) {
    e.preventDefault();

    let headContent = `<title>invoice</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link href="https://fonts.googleapis.com/css2?family=Cairo:wght@300;400;500;600&display=swap" rel="stylesheet">
    <style>
        body {
            -webkit-print-color-adjust: exact;
            font-family: 'Cairo', sans-serif;
        }

        .container {
            margin: 10% auto;
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 20px;
        }

        .loges {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

            .loges img {
                width: 100px;
            }

        .header {
            text-align: center;
            text-decoration: underline;
            font-size: 16px;
            font-weight: 900;
        }

        .money {
            display: flex;
            align-items: center;
            margin-top: 5px;
            margin-left: 20px;
        }

            .money p {
                margin: 3px;
                text-align: center;
            }

                .money p span {
                    display: block;
                    width: 40px;
                    height: 40px;
                    border: 1px solid #313030;
                    margin: 5px 0;
                    border-radius: 10px;
                }

        ul {
            list-style: none;
            text-align: right;
            direction: rtl;
            padding: 0;
            font-size: 12px;
            font-weight: 700;
        }

            ul li {
                display: flex;
                align-items: baseline;
                margin: 15px 0;
            }

                ul li p {
                    margin: 0px 0 0px 10px;
                    flex: none;
                }

                ul li span {
                    display: inline-flex;
                    width: 100%;
                    height: 100%;
                    border-bottom: 1px dashed #a69d9d;
                }
    </style>`;

    let printcontent = `<section>
        <div class="container">
            <div class="loges">
                <img src="/logo/logo-6.png" />
                <img src="/logo/ar-logo.png" />
            </div>
            <h2 class="header">
                إيصال استلام نقدية / شيك
            </h2>
            <div class="money">

                <p>
                    جنيه
                    <span style=" width: 60px;"></span>
                </p>
                <p>
                    قرش
                    <span></span>
                </p>
            </div>
            <ul>
                <li>
                    <p> استلمت أنــــــــــا :</p> <span></span>

                </li>
                <li>
                    <p> من السيــــــــــــد :</p> <span></span>
                </li>
                <li>
                    <p> مبلغ وقدره فقط :</p> <span></span>
                </li>
                <li>
                    <p> نقــــدا / شـــــيك :</p> <span></span><p> شيك رقم :</p> <span></span><p> مسحوب على بنك :</p> <span></span>
                </li>
                <li>
                    <p> وذلك قيمـــــــــة :</p> <span></span>

                </li>
                <li>
                    <p> التاريـــــــــــــــــخ :  <strong> &nbsp;  &nbsp; &nbsp;   / &nbsp;  &nbsp;  &nbsp; / &nbsp;  &nbsp;  &nbsp;  20 &nbsp;  &nbsp; &nbsp;   </strong>  </p>
                </li>
            </ul>
            <p style="font-size:14px;margin: 0px 40px 0 0px;text-align: center;direction: rtl;"> وهذا ايصال منا بذلك ..,</p>
            <p style="font-size:14px; text-align: left; margin: 8px 0 0 0; margin-left: 28%;">التوقيع</p>

        </div>
    </section>`;

    let newWindow = window.open('', '', 'width=100, height=100'),
        document = newWindow.document.open(),
        pageContent =
            '<!DOCTYPE html>' +
            '<html>' +
            '<head>' + headContent +
            '</head>' +
            '<body>' + printcontent + '</body></html>';
    document.write(pageContent);
    document.close();
    newWindow.moveTo(0, 0);
    newWindow.resizeTo(screen.width, screen.height);
    $(newWindow.window).on("load", function () {
        setTimeout(function () {
            newWindow.print();
            newWindow.close();
        }, 1500);
    });
});

//** All Duration *******************************/
$(document).on('click', '#searchBTNDuration', function (e) {
    e.preventDefault();

    LoadDuration(1);
});

function LoadDuration(page) {
    let currency = $('#drpCurrency').val();
    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/Treasury/Treasury/AllTreasuryDuration?currencyId=' + currency + '&fromDate=' + fromdate
        + '&todate=' + todate + '&page=' + page;

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
$(document).on('click', '.btnPrintDuration', function (e) {
    e.preventDefault();
    let currency = $('#drpCurrency').val();
    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/Treasury/Treasury/PrintDuration?currencyId=' + currency + '&fromDate=' + fromdate
        + '&todate=' + todate;

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            if (result != -1) {
                printData(result);
               
                $('#drpCurrency').val(currency);               
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

function printData(data) {  
    let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th>رقم الأيصال</th>
                            <th>البيان</th>
                            <th>التاريخ</th>
                            <th>صرف</th>
                            <th>إيداع</th>
                            <th>ملاحظات</th>
                        </tr>
                    </thead>
                    <tbody>`;
    debugger;
    let result = data.result;
    for (const item of result) {
        printcontent += `<tr>
                            <td>${item.recieptNumber}</td>
                            <td>${item.employeeName}</td>`;

        if (item.date != null) {
            let date = new Date(item.date);
            printcontent += `  <td>${date.toLocaleDateString()}</td>`;
        }
        else {
            printcontent += `  <td></td>`;
        }

        if (item.value < 0) {
            printcontent += `<td class="text-danger">${item.valueDigit}</td>`;
        } else {
            printcontent += `  <td></td>`;
        }

        if (item.value > 0) {
            printcontent += `<td>${item.valueDigit}</td>`;
        } else {
            printcontent += `  <td></td>`;
        }        

        printcontent += `<td>
                            ${item.moveType}
                            <br /><span>${item.notes}</span>
                        </td>`;        

        printcontent += `</tr>`;
    }

    printcontent += `</tbody>
                       <tfoot class="custom-tfoot">
                            <tr>
                                <td colspan='3' style="text-align:center">الإجمالي</td>
                                <td style="text-align:center">${data.withdrawTot}</td>
                                <td style="text-align:center">${data.dipositeTot}</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan='3' style="text-align:center">الرصيد الكلي</td>
                                <td colspan='2' style="text-align:center">
                                    ${data.totalAmount}
                                </td>
                                <td></td>
                            </tr>
                        </tfoot>
                </table>`;

    let restorepage = document.body.innerHTML;

    document.body.innerHTML = printcontent;
    window.print();

    document.body.innerHTML = restorepage;
}        

$(document).on('click', '.btnDeleteMoveOut', function (e) {
    e.preventDefault();

    let recieptNum = $(this).data('id');

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Treasury/Treasury/DeleteMoveOut?recieptNum=' + recieptNum,
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

                            let page = $('.pagination').find('li.active').find('span').html();
                            LoadDuration(page);
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

$(document).on('click', '.btnDeleteMoveIn', function (e) {
    e.preventDefault();

    let recieptId = $(this).data('id');

    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Treasury/Treasury/DeleteMoveIn?recieptId=' + recieptId,
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

                            var page = $('.pagination').find('li.active').find('span').html();
                            LoadDuration(page);
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