//accept decimal point
function isNumberKeyDecimal(e) {
    let charCode = (e.which) ? e.which : e.keyCode;
    return !(charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57));
}

//dont't accept decimal
function isNumberKey(e) {
    let charCode = (e.which) ? e.which : e.keyCode
    if (String.fromCharCode(charCode).match(/\D/g))
        return false;
}

function validateEmail(email) {
    const mailformat = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
    return email.trim().match(mailformat);
}

function moneyDigits(num_str) {
    let right_number = num_str.split(".")[0];

    let right_number_with_digit = "";

    for (let i = right_number.length - 1, j = 1; i >= 0; i--, j++) {

        right_number_with_digit = right_number[i] + right_number_with_digit;
        if ((j % 3) == 0 && (i - 1) >= 0) {
            right_number_with_digit = "," + right_number_with_digit;
        }

    }
    if (num_str.includes(".")) {
        return right_number_with_digit + "." + num_str.split('.')[1];
    }
    else {
        return right_number_with_digit;
    }
}