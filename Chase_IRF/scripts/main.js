/* User Sign In */
function userSignIn() {
    var userid, pwd;
    try{
        userid = $('#userid').val();
        pwd = $('#password').val();

        if (userid !== '' && password !== '') {
            var jqxhr = $.post("Login.aspx/UserLogin", { 'userId': userid, 'password': pwd })
              .done(function( data ) {
              alert( "Data Loaded: " + data );
            })
            .fail(function () {
                alert("error");
            })
            .always(function () {
                alert("finished");
            });
        }
    } catch (e) {
        console.log("Error: User Sign In...");
    }
}