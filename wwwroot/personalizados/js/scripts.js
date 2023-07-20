
//get navigator current location 

window.getCurrentLocation = function () {
    return new Promise(function (resolve, reject) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    resolve({
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude
                    });
                },
                function (error) {
                    reject(error.message);
                }
            );
        } else {
            reject('Geolocation is not supported by this browser.');
        }
    });
}
