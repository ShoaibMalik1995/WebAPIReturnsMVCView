
function LoadMenuHeaderHtml(_AcademyId, _UserId) {
    try {
        //alert(_AcademyId + "_"+ _UserId);
        //let _AcademyId = 1176;
        //let _UserId = 39058;

        //var data = { AcademyId: _AcademyId, UserId: _UserId };
        $.ajax({
            //url: 'http://localhost:8083/api/htmlcontent/GetMenuHeaderHtml?AcademyId=' + _AcademyId + '&UserId=' + _UserId,
            url: 'http://app.glfbeta.com/MenuHtml/api/htmlcontent/GetMenuHeaderHtml?AcademyId=' + _AcademyId + '&UserId=' + _UserId,
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                //alert(result);
                $("#divHeader").html(result);
            },
            error: function (xhr, status, error) {
                //debugger;
                console.log("error" + error);
            }
        });
    }
    catch (er) {
        console.log("LoadMenuHeaderHtml:- " + er);
    }
}


function LoadFooterHtml(AcademyId) {
    try {
        //let _AcademyId = 1176;

        //var data = { AcademyId: _AcademyId };
        $.ajax({
            //url: 'http://localhost:8083/api/htmlcontent/GetFooterHtml?AcademyId=' + AcademyId,
            url: 'http://app.glfbeta.com/MenuHtml/api/htmlcontent/GetFooterHtml?AcademyId=' + AcademyId,
            type: "POST",
            contentType: "application/json",
            success: function (result) {
                //alert(result);
                $("#divFooter").html(result);
            },
            error: function (xhr, status, error) {
                //debugger;
                console.log("error" + error);
            }
        });
    }
    catch (er) {
        console.log("LoadFooterHtml:- " + er);
    }
}