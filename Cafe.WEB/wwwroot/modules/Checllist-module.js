let lang = "ar";




$(document).on('change', '#EquipmentTypeID', function (e) {

    var EquipmentTypeID = $(this).val();
    $("#EquipmentID").empty();

    $.ajax({
        url: "/Checllist/GetEquipment",
        async: false,
        data: { EquipmentTypeID: EquipmentTypeID },
        success: function (res) {

            var statesSelect = $('#EquipmentID');
            statesSelect.empty();

            $("#EquipmentID").append("<option value='0'> ----------  </option>");

            $.each(res, function (index, state) {
                statesSelect.append($('<option/>', {
                    value: state.value,
                    text: state.text
                }));
            });

           
      
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


$(document).on('click', '.edit-btn', function (e) {
    e.preventDefault();


    var LocationID = $('#LocationID').val();
    var EquipmentTypeID = $('#EquipmentTypeID').val();
    var EquipmentID = $('#EquipmentID').val();
    var PlanID = $('#PlanID').val();
    
    var Check = 1;
    if (Check == 1) {
        if (LocationID == 0 || EquipmentTypeID == 0 || EquipmentID == 0 || PlanID == 0 ) {




            if (LocationID == 0) {
                CheckInputs = 0;

                $('#LocationID').css("border", "red solid 1px");
            }
            else {
                $('#LocationID').css("border", "#C2C2C2 solid 1px");
            }

            if (EquipmentTypeID == 0) {
                CheckInputs = 0;

                $('#EquipmentTypeID').css("border", "red solid 1px");
            }
            else {
                $('#EquipmentTypeID').css("border", "#C2C2C2 solid 1px");
            }
            if (EquipmentID == 0) {
                CheckInputs = 0;

                $('#EquipmentID').css("border", "red solid 1px");
            }
            else {
                $('#EquipmentID').css("border", "#C2C2C2 solid 1px");
            }
            if (PlanID == 0) {
                CheckInputs = 0;

                $('#PlanID').css("border", "red solid 1px");
            }
            else {
                $('#PlanID').css("border", "#C2C2C2 solid 1px");
            }
           
            if (CheckInputs == 0) {

                alert(lang == "ar" ? "يجب ادخال البيانات" : "Please Enter all data");
            }
            

        }
        else {

            $.ajax({
                type: "Get",
                url: "/Checllist/datalist",
                data: { LocationID: LocationID, EquipmentTypeID: EquipmentTypeID, EquipmentID: EquipmentID },

                async: false,
                success: function (res) {

                    /*  $("#tbl").empty();*/
                    $('#tbl thead tr th').empty();
                    $('#tbl tbody').empty();
                    $('#resultDiv').css('display', 'none')
                    $('#resultNo').css('display', 'none')

                    if (res.checklist.length > 0 && res.equipment.length > 0) {
                        $('#resultDiv').css('display', 'block');

                        let headerRow = "<th> المعدة </th>";

                        for (var i = 0; i < res.checklist.length; i++) {

                            headerRow += "<th><input type='hidden' value=" + res.checklist[i].checkListID + ">" + res.checklist[i].checkItem + "</th>";
                        }

                        $('#tableHeaderRow').html(headerRow);



                        for (var i = 0; i < res.equipment.length; i++) {

                            $("#tbl tbody").append("<tr><td class='text-center'  style='padding:20px !important; font-weight: bold;'>" + res.equipment[i].equipmentName + "</td></tr>");

                        }


                        var columnCount = $('#tbl thead th').length;

                        // تعديل كل صف في tbody
                        var c = 1;
                        $('#tbl tbody tr').each(function () {
                            var firstCellText = $(this).find('td').text().trim(); // اسم المعدة
                            var newRowHtml = '<td>' + firstCellText + '</td>'; // أول خلية

                            // باقي الأعمدة: checkboxes
                            for (var i = 1; i < columnCount; i++) {

                                const table = document.getElementById('tbl');
                                const secondHeaderCell = table.tHead.rows[0].cells[i];
                                const hiddenInput = secondHeaderCell.getElementsByTagName('input')[0];


                                newRowHtml += '<td>   <label class="ok">  <input type="radio"  name="col' + c + '_' + hiddenInput.value + '" value="1"> سليمة </label>    <label class="fault">  <input type="radio"  name="col' + c + '_' + hiddenInput.value + '" value="0"> بها عطل </label>   </td>';

                            }
                            c++;

                            // استبدال محتوى الصف
                            $(this).html(newRowHtml);



                            //$("#tblproduct TBODY TR").each(function () {

                            //    var row = $(this);
                            //    var product = {};
                            //    product.FKProductID = row.find("TD").eq(2).find('INPUT').val();//.html();

                            //    products.push(product);
                            //});


                        });
                    }
                    else {

                        $('#resultNo').css('display', 'block')


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



$(document).on('change', 'input[type="radio"]', function () {

    var val = $(this).val();
    var id = $(this).prop('name');
    var checklistid = id.split('_')[1]


    var LocationID = $('#LocationID').val();
    var EquipmentTypeID = $('#EquipmentTypeID').val();
    var EquipmentID = $('#EquipmentID').val();
    var PlanID = $('#PlanID').val();

    if (val == 1) {


        $.ajax({
            type: "Post",
            url: "/Checllist/savadata",
            data: { checklistid: checklistid, LocationID: LocationID, EquipmentID: EquipmentID, PlanID: PlanID },

            async: false,
            success: function (res) {

                if (res == 1) {

                    var selectedRadio = $('input[name="' + id + '"]');

                    if (selectedRadio.length > 0) {
                        var value = selectedRadio.val();
                        var td = selectedRadio.closest('td');     // نحصل على قيمة الراديو
                        var label = selectedRadio.closest('label');           // أقرب عنصر label

                        label.remove(); /*replaceWith('<span>سليمة</span>');*/
                        td.append('<span>سليمة</span>');
                    }

                  

                    jQuery.gritter.add({
                        position: lang == "ar" ? 'top-left' : 'top-right',
                        text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                        class_name: 'growl-warning',
                        sticky: false,
                        time: '1500',
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



    }
    else {
        $('#checklistid').val(checklistid);
        $('#checklistName').val(id);
        $('#tblproduct tbody').empty();

        $("#addUnivercityModal").appendTo('body').modal("show");

        $(document).on('click', '.btnModalClose', function () {
            $('#tblproduct tbody').empty();

            $("#addUnivercityModal").modal("hide");

            $('input[name="'+id+'"]').prop('checked', false);
        });

    }

    
});

$(document).on('change', '#SparePartTypeID', function (e) {

    var SparePartTypeID = $(this).val();
    var EquipmentID = $('#EquipmentID').val();

    $("#SparePartID").empty();

    $.ajax({
        url: "/Checllist/GetSparePart",
        async: false,
        data: { SparePartTypeID: SparePartTypeID, EquipmentID: EquipmentID },
        success: function (res) {

            var statesSelect = $('#SparePartID');
            statesSelect.empty();

            $("#SparePartID").append("<option value='0'> ----------  </option>");

            $.each(res, function (index, state) {
                statesSelect.append($('<option/>', {
                    value: state.value,
                    text: state.text
                }));
            });



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

$(document).on('click', '.btnAddproduct', function (e) {

    var rowsproduct = $('#tblproduct TBODY tr').length;
    

    var SparePartTypeIDValue = $("#SparePartTypeID option:selected").val();
    var SparePartIDValue = $("#SparePartID option:selected").val();
    var countidValue = $('#countid').val();
    var NoteValue = $('#Note').val();
        var countryfirst = '';
        var rowscount = $('#tblproduct TBODY tr').length;

        var ClassName = "";

    if ($("#SparePartTypeID").val().trim() == "") {
            var ClassName = "aa";
        }
        else {
        var ClassName = document.getElementById("SparePartTypeID").value;
        }

        var rows = $('#tblproduct tbody tr td.' + ClassName).length;
        //alertify.alert("rows"+rows);
        /*var txtCountry = $('#ContactType option:selected')*/
        //Get the reference of the Table's TBODY element.
        var tBody = $("#tblproduct > TBODY")[0];
        var rowCount = $('#tblproduct tr').length;


    if (SparePartTypeIDValue != 0 && SparePartIDValue != 0 && countidValue !='') {


            var row = tBody.insertRow(-1);

            //Add Name cell.
            var cell = $(row.insertCell(-1));


            var x = "<input type='hidden'  name='custId' value='" + SparePartTypeIDValue + "'>";
            var y = "<input type='hidden'  name='custId' value='" + SparePartIDValue + "'>";
            var z = "<input type='hidden'  name='custId' value='" + countidValue + "'>";
            var a = "<input type='hidden'  name='custId' value='" + NoteValue + "'>";
        cell.html($("#SparePartTypeID option:selected").text() + x);
        cell.attr("Class", SparePartTypeIDValue);
            //Add Country cell.
            cell = $(row.insertCell(-1));

        cell.html($("#SparePartID option:selected").text() + y);

            cell = $(row.insertCell(-1));

        cell.html($('#countid').val() + z);

        cell = $(row.insertCell(-1));

        cell.html($('#Note').val() + a);

        cell = $(row.insertCell(-1));

            var btnRemoveProduct = $(" <button class='btn delet-btn btnDelete btn waves-effect waves-light btn-grd-primary' ><i class='fa fa-trash-o'></i>حذف</button>");
            btnRemoveProduct.attr("type", "button");

            btnRemoveProduct.attr("onclick", "RemoveProduct(this);");
            btnRemoveProduct.val("RemoveProduct");
            cell.append(btnRemoveProduct);


        $("#SparePartTypeID").val(0);
        $("#SparePartID").val(0);
        $("#countid").val('');
        }
        else {

        if ($("#SparePartTypeID").val() == 0) {
            alert(lang == "ar" ? "نوع جزء الصيانة" : "نوع جزء الصيانة");

            }
        else if ($("#SparePartID").val() == 0) {
            alert(lang == "ar" ? "جزء الصيانة" : "جزء الصيانة");
            }
            else {
                alert(lang == "ar" ? "ادخل العدد" : "ادخل العدد");
            }

        }
    
});

function RemoveProduct(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    var name = "";
    if (confirm("Do you want to delete: " + name)) {
        //Get the reference of the Table.
        var table = $("#tblproduct")[0];
        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);

    }
}

$(document).on('click', '.btnsava', function (e) {
    e.preventDefault();

    // alert("dd");
    var CheckInputs = 1;
    var Check = 1;

    var LocationID = $('#LocationID').val();
    var EquipmentTypeID = $('#EquipmentTypeID').val();
    var EquipmentID = $('#EquipmentID').val();
    var PlanID = $('#PlanID').val();
    var rowsproduct = $('#tblproduct TBODY tr').length;
    var checklistid = $('#checklistid').val();
   

  
    if (Check == 1) {
        if (LocationID == 0 ||  EquipmentID == 0 || PlanID == 0||rowsproduct == 0 ) {

            if (LocationID == 0) {
                CheckInputs = 0;

                $('#LocationID').css("border", "red solid 1px");
            }
            else {
                $('#LocationID').css("border", "#C2C2C2 solid 1px");
            }

           
            if (EquipmentID == 0) {
                CheckInputs = 0;

                $('#EquipmentID').css("border", "red solid 1px");
            }
            else {
                $('#EquipmentID').css("border", "#C2C2C2 solid 1px");
            }
            if (PlanID == 0) {
                CheckInputs = 0;

                $('#PlanID').css("border", "red solid 1px");
            }
            else {
                $('#PlanID').css("border", "#C2C2C2 solid 1px");
            }

            if (CheckInputs == 0) {

                alert(lang == "ar" ? "يجب ادخال البيانات" : "Please Enter all data");
            }
            if (rowsproduct == 0) {
                alert(lang == "ar" ? " يجب أدخل بيانات الاجزاء الصيانة" : "يجب أدخل بيانات الاجزاء الصيانة");
            }

        }
        else {

            var products = new Array();
           
            ///productdata
            $("#tblproduct TBODY TR").each(function () {

                var row = $(this);
                var product = {};

                product.SparePartID = row.find("TD").eq(1).find('INPUT').val();//.html();
                product.Quantity = row.find("TD").eq(2).find('INPUT').val();//.html();
                product.CommentExecuted = row.find("TD").eq(3).find('INPUT').val();//.html();
                products.push(product);
            });


            $.ajax({
                type: "POST",
                url: "/Checllist/savadataDetails",
                data: { checklistid:checklistid,LocationID: LocationID, EquipmentID: EquipmentID, PlanID: PlanID, products: products },

                async: false,
                success: function (result) {






                    if (result > 0) {

                        var id= $('#checklistName').val();
                        var selectedRadio = $('input[name="' + id + '"]');

                        if (selectedRadio.length > 0) {
                            var value = selectedRadio.val();
                            var td = selectedRadio.closest('td');     // نحصل على قيمة الراديو
                            var label = selectedRadio.closest('label');           // أقرب عنصر label

                            label.remove(); /*replaceWith('<span>سليمة</span>');*/
                            td.append('<span>بها عطل</span>');
                        }



                        jQuery.gritter.add({
                            position: lang == "ar" ? 'top-left' : 'top-right',
                            text: lang == "ar" ? "تم الحفظ بنجاح" : "Saved Successfully",
                            class_name: 'growl-success',
                            sticky: false,
                            time: '1500'
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
        }

    }


});

$(document).ready(function () {
    $('#addUnivercityModal').modal({
        backdrop: 'static',   // يمنع الإغلاق عند الضغط خارج المودال
        keyboard: false       // يمنع الإغلاق بزر Esc
    });
});