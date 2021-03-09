function CheckIfUserIsBlocked(isAuthenticated) {
    //console.log(isAuthenticated);
    //console.log(location.pathname);
    if (isAuthenticated != "True" || location.pathname == "/Account/Block")
        return;

    const Http = new XMLHttpRequest();
    //const url = '/Talent/GetPrice';
    Http.open("GET", "/Account/IsBlocked");
    Http.send();
    Http.onreadystatechange = (e) => {
        if (Http.responseText != undefined && Http.responseText != "") {
            var responseJSON = JSON.parse(Http.responseText);
            console.log(responseJSON);
            if (responseJSON.isBlocked)
                location.replace("/Account/Block");
        }
    };
}