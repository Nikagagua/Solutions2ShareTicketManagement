using Microsoft.AspNetCore.Mvc;
using Solutions2ShareTickets;
using System.Data.SqlClient;
using System;

public class ZohoDeskController : Controller
{
    private readonly ZohoAuthenticationHelper authenticationHelper;

    public ZohoDeskController()
    {
        var clientId = Environment.GetEnvironmentVariable("ZOHO_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("ZOHO_CLIENT_SECRET"); 
        authenticationHelper = new ZohoAuthenticationHelper(clientId!, clientSecret!);
    }

    public async Task<ActionResult> GetAccessToken()
    {
        var scope = Environment.GetEnvironmentVariable("ZOHO_DESK_SCOPE");
        var accessToken = await authenticationHelper.GetAccessToken(scope!);
        using (var connection = new SqlConnection("Server=localhost. 8080;Database=S2STicketsDb;Trusted_Connection=True;Encrypt=false;user id=sa;password=Str0ngPa$$w0rd"))
        {
            connection.Open();

            var query = $"SELECT * FROM S2STicketsDb WHERE AccessToken = '{accessToken}'";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccessToken", accessToken);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            
            command.ExecuteNonQuery();
        }

        return Json(new { accessToken }, "application/json");   
    }

    public async Task<ActionResult> GenerateAccessTokenAsync(string clientId, string clientSecret,string scope)
    {
        try
        {
            var authenticationHelper = new ZohoAuthenticationHelper(clientId, clientSecret);
            var accessToken = await authenticationHelper.GetAccessToken(scope);
            return Json(new { accessToken });
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message });
        }
    }
}
