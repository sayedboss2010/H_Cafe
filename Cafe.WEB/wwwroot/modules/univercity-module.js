let lang = "ar";

$(document).on('click', '.btnRefresh', function (e) {
    e.preventDefault();

    $('#SearchString').val('');  
    $('.searchDiv').removeClass('is-filled');  

    LoadIndex();
});

$(document).on('click', '.btnModalClose', function () {
    $("#addUnivercityModal").modal("hide");
});

$(document).on('click', '.btnAdd', function (e) {
    e.preventDefault();

    lang = $(this).data('lang');

    $('#txtArName').val('');  
    $('.divArName').removeClass('is-filled');  
    
    $('#txtEnName').val('');   
    $('.divEnName').removeClass('is-filled');   

    $('.drpSelectFac').val(null).trigger('change');

    $('#modalTitle').html(lang == "ar" ? "إضافة" : "Add");
    $('#univercityId').val(0);
    $("#addUnivercityModal").appendTo('body').modal("show");
});

$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();

    let id = $(this).data('id');
    lang = $(this).data('lang');

    $.ajax({
        url: '/Univercity/GetById?id=' + id,
        async: true,
        success: function (result) {
            debugger;
            if (result != null && result.nameAr != "") {
                $('#txtArName').val(result.nameAr); 
                $('.divArName').addClass('is-filled'); 

                $('#txtEnName').val(result.nameEn);
                $('.divEnName').addClass('is-filled');  

                if (result.facsIds != null && result.facsIds.length > 0) {                   
                    $('.drpSelectFac').val(result.facsIds).trigger('change');
                }
                else {
                    $('.drpSelectFac').val(null).trigger('change');
                }

                $('#modalTitle').html(lang == "ar" ? "تعديل" : "Edit");
                $('#univercityId').val(id);
                $("#addUnivercityModal").appendTo('body').modal("show");
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

$(document).on('click', '#btnAddEditUniv', function (e) {
    e.preventDefault();
    
    let id = $('#univercityId').val();
    let arName = $('#txtArName').val();
    let enName = $('#txtEnName').val();
    let facs = $('.drpSelectFac').val();
    
    if (facs == undefined) facs = "";
    
    if (arName == "" || enName == "") {
        $('.alertEmpty').css("display", "block");
        return;
    }
    
    $('.alertEmpty').css("display", "none");

    let uri = id == 0 ? '/Univercity/Add?arName=' + arName + '&enName=' + enName + '&facs=' + facs
        : '/Univercity/Edit?id=' + id + '&arName=' + arName + '&enName=' + enName + '&facs=' + facs;
    
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

            $("#addUnivercityModal").modal("hide");
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
                    url: '/Univercity/Delete?id=' + id,
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
        url: '/Univercity/PrintData?term=' + term,
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

//***********************************************************************/
function LoadIndex() {
    let uri = '/Univercity/Index';

    let term = $('#SearchString').val();    
    if (term != "") {
        uri = '/Univercity/Search?term=' + term;
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