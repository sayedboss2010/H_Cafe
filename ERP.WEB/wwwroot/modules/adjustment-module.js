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
    }
}
$("#imageUpload").change(function () {
    readURL(this);
});

//** Adjustment ****************************/
$(document).on('keypress', '#RecieptId', function (e) {
    if (e.which == 13) {
        e.preventDefault();
        GetReciept($(this).val());
    }
    else {
        return isNumberKey(e);
    }
});

$(document).on('change', '#RecieptId', function (e) {

    e.preventDefault();
    GetReciept($(this).val());
});

function GetReciept(recieptId) {
    $.ajax({
        url: "/Treasury/CustodySettlement/GetMoveOutByReciept?recieptId=" + recieptId,
        async: true,
        success: function (data) {
            if (data == -1) {
                alert('رقم الإيصال غير موجود');
                $('#RecieptId').val('');
            }
            else if (data != null) {
                $('#TrMoveOutTreasuryId').val(data.id);

                $('#total').html(data.totalValue);
                $('#paid').html(data.paidValue);
                $('#remain').html(data.remainValue);
            }
        }
    });
}
//************************************/
$(document).on('keypress', '#Value', function (e) {
    return isNumberKeyDecimal(e);
});

$(document).on('blur', '#Value', function () {   
    let value = $(this).val();
    let remain = $('#remain').html();

    if (Number(value) > Number(remain)) {
        jQuery.gritter.add({
            text: "القيمة أكبر من المتبقي",
            class_name: 'growl-warning',
            sticky: false,
            time: '1500',
        });

        $(this).val('');
        $(this).focus();
    }
    else {
        $(this).val(moneyDigits(value));
    }
});
//************************************/
$(document).on('keypress', '#searchRecieptId', function (e) {
    return isNumberKey(e);
});
//** All MoveOut Reciepts ****************************/
$(document).on('click', '#searchBTN', function (e) {
    e.preventDefault();
    LoadIndexData();
});

$(document).on('click', '.btnRefresh', function (e) {
    e.preventDefault();
    LoadIndexData();   
});

function LoadIndexData() {
    let recieptId = $('#searchRecieptId').val();
    let destName = $('#searchDestName').val();
    let currency = $('#drpCurrency').val();
    let setteled = $('#drpSetteled').val();
    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/Treasury/CustodySettlement/Index?recieptId=' + recieptId + '&destination=' + destName
        + '&currencyId=' + currency + '&isSetteled=' + setteled + '&fromDate=' + fromdate
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
}
//****************************************/
$(document).on('click', '.btnPrint', function (e) {
    e.preventDefault();

    let recieptId = $('#searchRecieptId').val();
    let destName = $('#searchDestName').val();
    let currency = $('#drpCurrency').val();
    let setteled = $('#drpSetteled').val();
    let fromdate = $('#txtDateFrom').val();
    let todate = $('#txtDateTo').val();

    let uri = '/Treasury/CustodySettlement/Print?recieptId=' + recieptId + '&destination=' + destName
        + '&currencyId=' + currency + '&isSetteled=' + setteled + '&fromDate=' + fromdate
        + '&todate=' + todate;

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            if (result != -1) {
                printData(result);

                $('#searchRecieptId').val(recieptId ?? "");
                $('#searchDestName').val(destName ?? "");
                $('#drpCurrency').val(currency);
                $('#drpSetteled').val(setteled);
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
    let lang = "ar";

    let printcontent = `
                <br />
                <table class="table table-bordered align-items-center mb-0">
                    <thead>
                        <tr>
                            <th style="text-align:center">${lang == "ar" ? "رقم الإيصال" : "Reciept Number"}</th>
                            <th style="text-align:center">${lang == "ar" ? "موجه إلي" : "Destination"}</th>
                            <th style="text-align:center">${lang == "ar" ? "المبلغ الإجمالي " : "Total Amount"}</th>
                            <th style="text-align:center">${lang == "ar" ? "المبلغ المسوى" : "Setteled Amount"}</th>
                            <th style="text-align:center">${lang == "ar" ? "المبلغ المتبقي" : "Remain Amount"}</th>
                        </tr>
                    </thead>
                    <tbody>`;

    for (const item of result) {
        printcontent += `<tr>
                            <td style="text-align:center">
                                <span>رقم الإيصال : ${item.receiptId}</span><br />`;

        if (item.moveOutDate != null) {
            let date = new Date(item.moveOutDate);
            printcontent += `  <span>التاريخ : ${date.toLocaleDateString()}</span><br />`;
        }

        printcontent += ` <span>نوع الصرف : `;
        if (item.typeId == 1) {
            printcontent += `<span>${item.typeText}</span>`;
        } else {
            printcontent += `<span>${item.typeName}</span>`;
        }
        printcontent += `</span></td>`;

        printcontent += `<td>${item.destintionMoveout}</td>`;
        printcontent += `<td>
                            <span>${parseFloat(item.totalValue.toFixed(2))}</span><br />
                            <span>العملة : ${item.currencyName}</span>
                        </td>`;
        printcontent += `<td class="text-success">${parseFloat(item.paidValue.toFixed(2))}</td>`;

        let remainVal = item.paidValue == 0
            ? item.totalValue
            : item.totalValue - item.paidValue;

        printcontent += `<td class="text-danger">${parseFloat(remainVal.toFixed(2))}</td>`;

        printcontent += `</tr>`;
    }

    printcontent += `</tbody>
                </table>`;

    let restorepage = document.body.innerHTML;

    document.body.innerHTML = printcontent;
    window.print();

    document.body.innerHTML = restorepage;
}
//****************************************/

$(document).on('click', '.btnPrintAdj', function (e) {
    e.preventDefault();
    let reciept = $(this).data('reciept');

    let restorepage = document.body.innerHTML;

    let printcontent = `<nav aria-label="breadcrumb">
                            <h4 class="font-weight-bolder mb-0">تسويات الإيصال رقم : ${reciept}</h4>
                        </nav>`;

    printcontent += $('#printDiv').html();

    document.body.innerHTML = printcontent;
    window.print();

    document.body.innerHTML = restorepage;
});

//********************************************/
$(document).on('change', '.btnUploadReciept', function (e) {
    e.preventDefault();

    let id = $(this).data('id');

    let data = new FormData();
    let files = $(this)[0].files;

    for (const element of files) {
        let file = element;
        data.append("files", file);
        break;
    }
    $.ajax({
        type: "POST",
        url: '/Treasury/CustodySettlement/Create?id=' + id,        
        contentType: false,
        processData: false,
        data: data,
        async: true,
        success: function (e) {            
            if (e == "") {
                jQuery.gritter.add({
                    position: lang == "ar" ? 'top-left' : 'top-right',
                    text: lang == "ar" ? "حدث خطأ من فضلك حاول مرة أخرى" : "Error happend please try again later",
                    class_name: 'growl-warning',
                    sticky: false,
                    time: '1500',
                });
            }
            else {
                e = e.split("http:").join("https:");
                $("#recieptImg_" + id).attr("src", e);

                jQuery.gritter.add({
                    position: lang == "ar" ? 'top-left' : 'top-right',
                    text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                    class_name: 'growl-success',
                    sticky: false,
                    time: '1500'
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
$(document).on('click', '.btnPrintReciept', function (e) {
    e.preventDefault();

    let recieptId = $(this).data('id');

    let uri = '/Treasury/CustodySettlement/GetMoveOutByReciept?recieptId=' + recieptId;

    $.ajax({
        url: uri,
        async: true,
        success: function (result) {
            if (result != -1) {
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
                
                let mon1 = '';
                let mon2 = '';
                if (result.totalValueStr.includes('.') == -1) {
                    mon1 = result.totalValueStr;
                    mon2 = '00';
                } else {
                    let value = result.totalValueStr.split('.');
                    mon1 = value[0];
                    mon2 = value[1];
                }

                let reason = '';
                if (result.trMoveOutTreasuryTypeId == 1) {
                    reason = result.trMoveOutTreasuryTypeText
                } else {
                    reason = result.typeName;
                }

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
                                <span style=" width: 60px;">${mon1}</span>
                            </p>
                            <p>
                                قرش
                                <span>${mon2}</span>
                            </p>
                        </div>
                        <ul>
                            <li>
                                <p> استلمت أنــــــــــا :</p> <span>${result.destintionMoveout}</span>

                            </li>
                            <li>
                                <p> من السيــــــــــــد :</p> <span></span>
                            </li>
                            <li>
                                <p> مبلغ وقدره فقط :</p> <span>${result.totalValue}  ${result.currencyName}</span >
                            </li>
                            <li>
                                <p> نقــــدا / شـــــيك :</p> <span></span><p> شيك رقم :</p> <span></span><p> مسحوب على بنك :</p> <span></span>
                            </li>
                            <li>
                                <p> وذلك قيمـــــــــة :</p> <span>${reason}</span>

                            </li>
                            <li>
                                <p> التاريـــــــــــــــــخ :
                                    <strong> ${new Date(result.moveOutDate).toLocaleDateString()} </strong>  
                                </p>
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
            }
            else {
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

//*************************************/
$(document).on('click', '.btnDeleteAdj', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    let recieptNum = $(this).data('reciptnum');


    bootbox.confirm({
        size: "large",
        message: lang == "ar" ? "هل أنت متأكد؟" : "Are you sure?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: '/Treasury/CustodySettlement/DeleteAdj?recieptNum=' + id,
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