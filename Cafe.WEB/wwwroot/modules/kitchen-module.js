let lang = "ar";


function finishOrder(orderId, button) {

    if (!confirm("هل أنت متأكد من إنهاء هذا الطلب؟")) return;

    $.ajax({
        url: '/Kitchen/MarkOrderAsDone',
        type: 'POST',
        data: { orderId: orderId },

        success: function (res) {
            // نحذف الكارت من الصفحة.
            if (res == true) {
                const card = $(button).closest('.order-card');
                card.fadeOut(300, function () { $(this).remove(); });
            }
            else {

                alert("حدث خطأ أثناء إنهاء الطلب");
            }

        },
        error: function () {
            alert("حدث خطأ أثناء إنهاء الطلب");
        }
    });
}