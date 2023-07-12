$.ajax({
    url: 'https://accounts.zoho.com/oauth/v2/token',
    type: 'GET',
    dataType: 'json',
    success: function (response) {
        var accessToken = response.accessToken;
    },
    error: function (error) {
        console.error('Failed to retrieve access token:', error);
    }
});
