function ConvertTo12Hour(time24) {
    var ts = "";
    if (time24 != "")
    {
        ts = time24;
        var H = +ts.substr(0, 2);
        var h = (H % 12) || 12;
        h = (h < 10) ? ("0" + h) : h;  // leading 0 at the left for 1 digit hours
        var ampm = H < 12 ? " AM" : " PM";
        ts = h + ts.substr(2, 3) + ampm;
    }
        return ts;
    }
function convertTo24Hour(time) {
    var t_time = "";
    if (time != "")
    {
        var hours = parseInt(time.substr(0, 2));
        if (time.indexOf('am') != -1 && hours == 12) {
            time = time.replace('12', '0');
        }
        if (time.indexOf('pm') != -1 && hours < 12) {
            time = time.replace(hours, (hours + 12));
        }
        if (time.indexOf('am') != -1 && hours < 12 && time.substr(0, 1) != "0") {
            time = "0" + time
        }
        t_time = time.replace(/(am|pm)/, '').replace(' ', '');
    }
    return t_time;
}
function FilterFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
    var input, filter, ul, li, a, i;
    input = document.getElementById("txtSearch");
    filter = input.value.toUpperCase();
    div = document.getElementById("myDropdown");
    a = div.getElementsByTagName("a");
    for (i = 0; i < a.length; i++) {
        txtValue = a[i].textContent || a[i].innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            a[i].style.display = "";
        } else {
            a[i].style.display = "none";
        }
    }
}
function DateTimeFormat(_Date,DateFormat, _Time) {
    if (DateFormat.toLowerCase() == 'dd/mm/yyyy') {
        arr = new Array();
        arr = _Date.split("/");
        _Date = arr[1] + "/" + arr[0] + "/" + arr[2];
        //var newdate = new Date(_Date);
    }  
    var fromDateTime = new Date(_Date + ' ' + _Time);
    return fromDateTime;
}
function DifferenceInMinutes(start, end) {
    var totalMinutes = function(value) {
        var match = (/(\d{1,2}):(\d{1,2})/g).exec(value);
        return (Number(match[1]) * 60) + Number(match[2]);
    }    
    return totalMinutes(end) - totalMinutes(start);
}
function AddMinutes(time, minsToAdd) {
    function z(n) { return (n < 10 ? '0' : '') + n; };
    var bits = time.split(':');
    var mins = bits[0] * 60 + +bits[1] + +minsToAdd;
    return z(mins % (24 * 60) / 60 | 0) + ':' + z(mins % 60);
}
function ValidatePrice(price) {
        var valid = true;
        var regx = /^[0-9]+(\.[0-9]{1,2})?$/;
        if (!price.match(regx)) {
            valid = false;
        }
        return valid;
    }
function ConfirmationDialog(_title, _text, action) {
        var confhtml = "";
        var _action = action.name;
        confhtml = '<div class="row">';
        confhtml += '<div class="col-md-12">';
        confhtml += '<p id="ConfirmText">' + _text + '</p>';
        confhtml += '<div class="gfooter">';
        confhtml += '<button type="button" class="gbtn" id="yes" onclick = "' + _action + '()" data-dismiss="modal">Continue</button>';
        confhtml += '<button type="button" class="gbtn" id="no" data-dismiss="modal">Cancel</button>';
        confhtml += '</div>';
        confhtml += '</div>';
        confhtml += '</div>';
        $('#cbody').html('');
        $('#cbody').html(confhtml);
        var confirmDialog = $('#confirm');
        confirmDialog.modal('show');
}
function ValidatePassword(PasswordtoValidate) {
    var ValidPasswordRegEx = '^(?=.*\\d)(?=.*[a-z])[\\w~@@#$%^&*+=`|{}:;!.?\\"()\\[\\]-]{8,}$';
    ValidPasswordRegEx = ValidPasswordRegEx.replace(/\L/g, '@@');
    var re = new RegExp(ValidPasswordRegEx);
    var valid = re.exec(PasswordtoValidate);
    if (!valid) {
        return false;
    } else {
        return true;
    }
}
function ValidateEmailFormat(emailToValidate) {
    var validEmailFormat = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (!validEmailFormat.test(emailToValidate))
        return false;
    else return true;
}
function SavePasswordChange() {
    $.blockUI({ message: $('#imgBusyBox') });
    var oldPass = $("#txtOldPassword").val();
    var newPass = $("#txtNewPassword").val();

    if (oldPass != "" && newPass != "") {
        if (!ValidatePassword(newPass)) {
            $.unblockUI();
            $("#MessageText").html("Password required of 8 characters with at least 1 letter, 1 number.");
            $('#MessageDialogue').modal('show');
            return;
        }
        $.get('/User/ChangePassword_NBS', { OldPassword: oldPass, NewPassword: newPass }, function (result) {
            $.unblockUI();
            if (result == "True") {
                localStorage.setItem('IsValidPassword', '1');
                $("#MessageText").html("Password Changed Successfully");
                $('#MessageDialogue').modal('show');
                $("#txtOldPassword").val("");
                $("#txtNewPassword").val("");
                setTimeout(function () { location.reload(); }, 2000)
            }
            else {
                $("#MessageText").html("Old Password is not correct");
                $('#MessageDialogue').modal('show');
            }
        });
    }
    else {
        $.unblockUI();
        $("#MessageText").html("Password Fields can not be Empty");
        $('#MessageDialogue').modal('show');
    }
}
function DeleteAccount() {
    $.blockUI({ message: $('#imgBusyBox') });
    $.get("/User/SendRemoveAccountEmail_NBS", function (data) {
        $.unblockUI();
        if (data) {
            $("#MessageText").html("Please check your email to remove your account. Please refresh the screen once you have confirmed email");
            $('#MessageDialogue').modal('show');
        } else {
            $("#MessageText").html("Error occurred while remove your account.");
            $('#MessageDialogue').modal('show');
        }
    });
}
function GetPlayerAllLessonCredits(_PLayerId) {
    $("#divPaymentMethod").html('');
    //var currenySymbol = localStorage.getItem("CurrencySymbol");
    var packageHTML = '';
    packageHTML += '<tr>';
    packageHTML += '<th>Credit Name</td> ';
    packageHTML += '<th>Lesson Type</td> ';
    packageHTML += '<th>Credit Code</td>';
    packageHTML += '<th>Total Credits</td>';
    packageHTML += '<th>Booked Credits</td>';
    packageHTML += '<th>Total Amount</td>';
    packageHTML += '<th>Used Amount</td>';
    packageHTML += '</tr>';

    $.getJSON('/PaymentType/GetPlayerAllLessonCredits', { PlayerId: _PLayerId }, function (LessonCredits) {

        $.each(LessonCredits, function (i, opt) {
            packageHTML += '<tr>';

            packageHTML += '<td>' + opt.VoucherName + '</td> ';

            packageHTML += '<td>' + opt.ProgramTypeName + '</td>';

            packageHTML += '<td>' + opt.VoucherPin + '</td>';

            packageHTML += '<td>' + opt.TotalCredits + '</td>';

            packageHTML += '<td>' + opt.BookedCredits + '</td>';

            packageHTML += '<td>' + opt.TotalAmount + '</td>';
            packageHTML += '<td>' + opt.CreditUsedAmount + '</td>';

            packageHTML += '</tr>';
        });
        var ulPackage = $('#ulPackageslist');
        ulPackage.empty;
        ulPackage.html(packageHTML);
    });
}
function GetUserAvailableGifts(_UserId)
{
    $.getJSON('/User/GetUserAvailableGifts', { UserId: _UserId }, function (AvailableGifts) {
        var giftHTML = '';
        giftHTML += '<tr>';

        giftHTML += '<th>Gift Card Coupon Code</th> ';

        giftHTML += '<th>Total Amount</th> ';

        giftHTML += '<th>Used Amount</th>';

        giftHTML += '</tr>';
        $.each(AvailableGifts, function (i, opt) {

            giftHTML += '<tr>';

            giftHTML += '<td>' + opt.GiftCardCoupon + '</td> ';

            giftHTML += '<td>' + opt.TotalAmount + '</td>';

            giftHTML += '<td>' + opt.UsedAmount + '</td>';

            giftHTML += '</tr>';
        });
        var ulGifts = $('#ulGiftslist');
        ulGifts.empty;
        ulGifts.html(giftHTML);
    });
}
function SaveUserGiftCard(UserId) {
    var txtGiftCardCode = document.getElementById("txtGiftCardCodeH").value;
    if (txtGiftCardCode != "") {
        $.blockUI({ message: $('#imgBusyBox') });
        $.get('/User/SaveGiftCardCode', { GiftCardCode: txtGiftCardCode }, function (result) {
            if (result) {
                    GetUserAvailableGifts(UserId);
                    $.unblockUI();
                    $("#MessageText").html("Gift card code saved successfully.");
                    $('#MessageDialogue').modal('show');
                    document.getElementById("txtGiftCardCode").value = "";
            }
            else {
                    $.unblockUI();
                    $("#MessageText").html("Gift card code is not valid.");
                    $('#MessageDialogue').modal('show');
                    document.getElementById("txtGiftCardCode").value = "";
            }
        });
    }
    else {
        $("#MessageText").html("Gift Card Code Can not be Empty");
        $('#MessageDialogue').modal('show');
    }
}
function OpenBillingAddModal() {
    GetBillingAddress();
    $('#updateAddressModal').modal('show');
}
function GetBillingAddress() {
    $.get('/User/GetBillingAddress_NBS', function (response) {
        if (response !== "") {
            $("#txtBillingAddLine1").val(response.BillingAddressLine1);
            $("#txtBillingAddLine2").val(response.BillingAddressLine2);
            $("#txtCity").val(response.BillingCity);
            $("#txtZipCode").val(response.BillingZipCode);
            $('#slctCountry').val(response.CountryValue);
        }
    })
}
function SaveBillingAddress() {

    //var txtBillCounty = $('#txtCountyState').val();
    var txtBillAdd = $('#txtBillAdd').val();
    var txtBillAddLine1 = $('#txtBillingAddLine1').val();
    var txtBillAddLine2 = $('#txtBillingAddLine2').val();
    var txtBillCity = $('#txtCity').val();
    var txtBillCountry = $('#slctCountry').val();
    var CountryName = $('#slctCountry').children("option:selected").text();
    var txtBillZipCode = $('#txtZipCode').val();
    var ddlVal = "0";
    var CountyName = "";

    if (txtBillAdd == '') {
        $("#MessageText").html("Please enter billing address");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (txtBillAddLine1 == '') {
        $("#MessageText").html("Please enter address line 1");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (txtBillAddLine2 == '') {
        $("#MessageText").html("Please enter address line 2");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (txtBillCity == '') {
        $("#MessageText").html("Please enter city");
        $('#MessageDialogue').modal('show');
        return false;
    }

    //if (txtBillCounty == '') {
    //    $("#MessageText").html("Please enter billing county");
    //    $('#MessageDialogue').modal('show');
    //    return false;

    //}

    if (txtBillCountry == '0') {
        $("#MessageText").html("Please enter billing country");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (txtBillZipCode == '') {
        $("#MessageText").html("Please enter zip code");
        $('#MessageDialogue').modal('show');
        return false;
    }

    var userRoleId = $("#userRoleIdHdn").val();
    if (+userRoleId == 1) {
        $.get('/User/ChangeBillAddressByAcademy_NBS', { AddressLine1: txtBillAddLine1, AddressLine2: txtBillAddLine2, City: txtBillCity, County: ddlVal, Country: txtBillCountry, CountryName: CountryName, ZipCode: txtBillZipCode }, function (result) {

            if (result == 1) {
                $("#MessageText").html("Billing address updated");
                $('#tdPlayerBillingAddress').html('');
                $('#tdPlayerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdCoachBillingAddress').html('');
                $('#tdCoachBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdManagerBillingAddress').html('');
                $('#tdManagerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdOwnerBillingAddress').html('');
                $('#tdOwnerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);

                $('#MessageDialogue').modal('show');
                $('#updateAddressModal').modal('hide');
            }
            else {
                $("#MessageText").html("Billing address not updated");
                $('#MessageDialogue').modal('show');

            }
        });
    }
    else {
        $.get('/User/ChangeBillAddress_NBS', { AddressLine1: txtBillAddLine1, AddressLine2: txtBillAddLine2, City: txtBillCity, County: ddlVal, Country: txtBillCountry, CountryName: CountryName, ZipCode: txtBillZipCode, countyState: "" }, function (result) {

            if (result == 1) {
                $("#MessageText").html("Billing address updated");
                $('#tdPlayerBillingAddress').html('');
                $('#tdPlayerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdCoachBillingAddress').html('');
                $('#tdCoachBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdManagerBillingAddress').html('');
                $('#tdManagerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);
                $('#tdOwnerBillingAddress').html('');
                $('#tdOwnerBillingAddress').html(txtBillAddLine1 + '<br/>' + txtBillAddLine2 + '<br/>' + txtBillCity + ',' + CountryName + '.<br/>' + CountyName + '<br/>' + txtBillZipCode);

                $('#MessageDialogue').modal('show');
                $('#updateAddressModal').modal('hide');
            }
            else {
                $("#MessageText").html("Billing address not updated");
                $('#MessageDialogue').modal('show');
            }

        });
    }

}
function EditFbUser()
{
    if (document.getElementById('txtFbID') != null)
        document.getElementById('txtFbID').value = $("#tdfbName").html();

    if (document.getElementById('txtFbToken') != null)
        document.getElementById('txtFbToken').value = $("#tdfbToken").html();
    $('#divFbAdd').modal('show');
    return false;
}
function AddUserFb() {
    var mtxtFbID = $('#txtFbID').val();
    var mtxtFbToken = $('#txtFbToken').val();

    if (mtxtFbID == '') {
        $("#MessageText").html("Please enter Facebook Login");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (mtxtFbToken == '') {
        $("#MessageText").html("Please enter Facebook Password");
        $('#MessageDialogue').modal('show');
        return false;
    }
    $.post('/User/RegisterFBUser',
{
    fbUserId: mtxtFbID,
    fbUserToken: mtxtFbToken
}, function (ActionResponse) {
        if (ActionResponse)
        {
            $("#tdfbName").html(mtxtFbID);
            $("#tdfbToken").html(mtxtFbToken);
            $("#MessageText").html("FB User Added!");
            $('#MessageDialogue').modal('show');
        }
        else
        {
            $("#MessageText").html("Unable to add fb user.");
            $('#MessageDialogue').modal('show');
        }
    });
}
function EditTwitterUser() {

    if (document.getElementById('txtAccToke') != null)
        document.getElementById('txtAccToke').value = $('#lTwitterID').html();

    if (document.getElementById('txtAccessTokenSec') != null)
        document.getElementById('txtAccessTokenSec').value = $('#lAccesTokenSecret').html();

    $('#divTwitter').modal('show');
    return false;
}
function AddUserTiwitter() {
    var mtxtAccToke = $('#txtAccToke').val();
    var mtxtAccessTokenSec = $('#txtAccessTokenSec').val();

    if (mtxtAccToke == '') {
        $("#MessageText").html("Please enter Twitter Login");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (mtxtAccessTokenSec == '') {
        $("#MessageText").html("Please enter Twitter Password");
        $('#MessageDialogue').modal('show');
        return false;
    }

    $.post('/User/RegisterTwUser',
{
    AccessToken: mtxtAccToke,
    TokenSecret: mtxtAccessTokenSec
}, function (ActionResponse) {
    if (ActionResponse) {
        $("#lTwitterID").html(mtxtAccToke);
        $("#lAccesTokenSecret").html(mtxtAccessTokenSec);
        $("#MessageText").html("Twitter Credential Saved!");
        $('#MessageDialogue').modal('show');
    }
    else {
        $("#MessageText").html("Unable to add twitter user.");
        $('#MessageDialogue').modal('show');
    }
});
}
function UpdateUserGDPR()
{

    $.getJSON('/User/UpdateUserGDPR', { RecEmail: $('#chkRecEmail').is( ":checked" ) }, function (Response) {
        return false;
    });
}
function LoadGlobalImages(typeOfImage) {
    var DivGImage = "";
    $.get('/User/GetGlobalImagesByRepositoryType_NBS', { RepositoryId: 1 }, function (gImages) {
        for (var i = 0; i < gImages.length; i++) {
            DivGImage += "<a onclick='return SelectImage(" + typeOfImage + ", " + gImages[i].MediaId + ",\"" + gImages[i].ThumbURL + "\",\"" + gImages[i].ImageURL + "\");' href='#'><img style='padding:2px;' src='" + gImages[i].ThumbURL + "' alt='' width='120' height='100' /></a>";
        }
        DivGImage += "";
        var GlobalElement = $('#globalImagesContent');
        GlobalElement.html(DivGImage);
    });

    $('#globalImagesModal').modal('show');
    return false;
}
function SelectImage(typeOfImage, MediaFileId, Thumbnail, FilePath) {

    if (typeOfImage == 1) {
        $('#imgBackgroundPik').attr('src', Thumbnail);
    }
    else if (typeOfImage == 2) {
        $('#imgPublicBackgroundPik').attr('src', Thumbnail);
    }
    $('#globalImagesModal').modal('hide');

    $.ajax({
        type: "GET",
        url: '/User/SetPictureFromGallery_NBS',
        data: { mediaID: MediaFileId, mediaType: typeOfImage, filePath: FilePath }
    }).done(function (response) {
        $("#MessageText").html("Photo uploaded successfully");
        $('#MessageDialogue').modal('show');
        location.reload();
    });
    return false;
}
function UpdateAcademySetting(_SettingName)
{
    var _setting = "";
    if(_SettingName == "EnableAddress")
    {
        if ($('#cbAddressEnable').prop('checked'))
            _setting = "True";
        else
            _setting = "False";
    }
    else if (_SettingName == "AcademyWeatherSettings")
    {
        if ($('#rdoAcademyCelsius').prop('checked'))
            _setting = "C";
         else
            _setting = "F";
    }
    else if (_SettingName == "12HoursTimeFormat")
    {
        if ($('#cbTimeFormat').prop('checked'))
            _setting = "True";
        else
            _setting = "False";
    }
    else if (_SettingName == "Manager.IsMembershipShow")
    {
        if ($('#cbMembershipfields').prop('checked'))
            _setting = "True";
        else
            _setting = "False";
    }

    $.getJSON('/Academy/UpdateAcademySetting', { Setting: _setting, SettingName: _SettingName }, function (response) {
        return false;
    });
}
function UpdateRescheduleHourProperty() {

    var hours = $("#Duration").val();

    if (hours == "") {
        $("#MessageText").html("Hours should not be empty.");
        $('#MessageDialogue').modal('show');
        return;
    }
    $.getJSON('/Academy/UpdateRescheduleHourLimitProperty', { RescheduleHourLimit: hours }, function (response) {
        $("#MessageText").html("Reschedule Hours updated successfully.");
        $('#MessageDialogue').modal('show');
    });
}
function GetReminder() {
    $('#divAddReminder').modal('show');
    $.blockUI({ message: $('#imgBusyBox') });
    $.getJSON("/Academy/GetReminder", function (result) {
        if(result.ReminderId !=0)
        {
            $("#ddlReminderType").val(result.ReminderType);
            $("#ddlReminderDuration").val(result.ReminderDuration);
            $("#cbReminderActive").attr("checked",result.IsActive);
            $("#hfReminderId").val(result.ReminderId);
        }
        else
        {
            $("#hfReminderId").val(0);
        }
        $.unblockUI();
    });
}
function SaveReminder()
{
    var reminderType = $("#ddlReminderType").val();
    var reminderDuration = $("#ddlReminderDuration").val();
    var isActive= $("#cbReminderActive").prop('checked');
    if(isActive)
    {
        if(reminderType == "0")
        {
            $("#MessageText").html("Please select reminder type");
            $('#MessageDialogue').modal('show');
            return;
        }
        if(reminderDuration =="0")
        {
            $("#MessageText").html("Please select reminder duration");
            $('#MessageDialogue').modal('show');
            return;
        }
    }
    $.getJSON("/Academy/SaveReminder",{ ReminderId:  $("#hfReminderId").val(),ReminderType: reminderType,ReminderDuration:reminderDuration,IsActive:isActive }, function (result) {

    if(!result)
    {
        $("#MessageText").html("Reminder not updated successfully");
        $('#MessageDialogue').modal('show');
    }
    else
    {
        $("#MessageText").html("Reminder saved successfully");
        $('#MessageDialogue').modal('show');
    }
});

}
function OpenEditMgrProfileModal() {
    $("#updatePersonalDetailsMgr").modal('show');
}
function SavePersonalDetails() {
    var txtEditPersonalDetailsFirstName = $('#txtEditPersonalDetailsFirstName').val();
    var txtEditPersonalDetailsLastName = $('#txtEditPersonalDetailsLastName').val();
    var DatatxtEditPersonalDetailsDOB = document.getElementById('txtEditPersonalDetailsDOB').value;
    var DatatxtEditPersonalDetailsAddressLine1 = $('#txtEditPersonalDetailsAddressLine1').val();
    var DatatxtEditPersonalDetailsAddressLine2 = $('#txtEditPersonalDetailsAddressLine2').val();
    var DatatxtEditPersonalDetailsCity = $('#txtEditPersonalDetailsCity').val();
    var DatatxtEditPersonalCountry = $('#EditPersonalDetailsDropDownCountry').val();
    var DatatxtEditPersonalEmail = $("#txtEditPersonalEmail").val();
    var DatatxtEditPersonalMobile = $("#txtEditPersonalMobile").val();
    var DatatxtEditPersonalTelephone = $("#txtEditPersonalTelephone").val();
    var DatatxtEditPersonalMedicalHistroy = $('#txtEditPersonalMedicalHistroy').val();
    var userRoleId = $("#userRoleIdHdn").val();

    if (txtEditPersonalDetailsFirstName == '') {
        $("#MessageText").html("Please enter your first name");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (txtEditPersonalDetailsLastName == '') {
        $("#MessageText").html("Please enter your last name");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (DatatxtEditPersonalDetailsDOB == '') {
        $("#MessageText").html("Please enter Date of Birth");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (DatatxtEditPersonalDetailsAddressLine1 == '') {
        $("#MessageText").html("Please enter your address line 1");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (DatatxtEditPersonalDetailsCity == '') {
        $("#MessageText").html("Please enter your city");
        $('#MessageDialogue').modal('show');
        return false;
    }
    if (DatatxtEditPersonalCountry == '0') {
        $("#MessageText").html("Please enter choose your country");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (DatatxtEditPersonalEmail == '') {
        $("#MessageText").html("Please choose your Email");
        $('#MessageDialogue').modal('show');
        return false;
    }
    else if (ValidateEmailFormat(DatatxtEditPersonalEmail) == false) {
        $("#MessageText").html("Please choose Valid Email");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (DatatxtEditPersonalMobile == '') {
        $("#MessageText").html("Please choose your Mobile");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (DatatxtEditPersonalTelephone == '') {
        $("#MessageText").html("Please enter your telephone");
        $('#MessageDialogue').modal('show');
        return false;
    }

    if (userRoleId == "3" || userRoleId == "4") {
        if (DatatxtEditPersonalMedicalHistroy == '') {
            $("#MessageText").html("Please enter medical history");
            $('#MessageDialogue').modal('show');
            return false;
        }
    }

    $.post('/User/SaveEditedPlayerPersonalDetails_NBS',
        {
            FirstName: txtEditPersonalDetailsFirstName,
            LastName: txtEditPersonalDetailsLastName,
            AddressLine1: DatatxtEditPersonalDetailsAddressLine1,
            AddressLine2: DatatxtEditPersonalDetailsAddressLine2,
            City: DatatxtEditPersonalDetailsCity,
            CountryId: DatatxtEditPersonalCountry,
            Email: DatatxtEditPersonalEmail,
            Mobile: DatatxtEditPersonalMobile,
            Telephone: DatatxtEditPersonalTelephone,
            SkypeId: '',
            FbID: '',
            TwitterID: '',
            MedicalHistroy: DatatxtEditPersonalMedicalHistroy,
            DateOfBirth: DatatxtEditPersonalDetailsDOB
        }, function (ActionResponse) {
            $("#MessageText").html("Profile Details Updated Successfully.");
            $('#MessageDialogue').modal('show');
            location.reload();
        });
}
function GetMyAccounts() {
    $('#divLoggedInAccounts').modal("show");
    var accounts = "<table>";
    $.get("/Common/GetLoggedInUsers", function (data) {
        $.each(data, function (i, Accounts) {

            accounts += "<tr><td style='width:150px'>" + Accounts.FirstName + " " + Accounts.LastName + " </td><td>" + Accounts.PersonalEmail + " </td>";

            if (Accounts.CurrentUser == 0) {
                accounts += "<td style='width:100px;text-align:right;'><input type='button' class='btnAccount' onclick='LoginAccount(" + Accounts.UserId + ")' value='Switch'></input></td>";
            }
            else {
                accounts += "<td style='width:100px;text-align:right;'><input type='button' style='background: #cccccc;color: #646564;border: 1px solid black;border - radius: 0px;text - transform: uppercase;padding: 4px 10px;cursor: pointer;' value='Active'></input></td>";
            }
            accounts += "</tr>";

        });
        accounts += "</table>";
        $("#divAccountsData").html(accounts);
    });
}