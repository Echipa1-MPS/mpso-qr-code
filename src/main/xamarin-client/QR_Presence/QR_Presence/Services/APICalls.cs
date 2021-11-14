using Newtonsoft.Json;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QR_Presence.Services
{
    public static class APICalls
    {

        public static string BaseUrl = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/";
        public async static Task<bool> RegisterUser(UserModel user, string password)
        {
            using (var c = new HttpClient())
            {
                var client = new HttpClient();
                var jsonRequest = new
                {
                    name = user.Name,
                    password = password,
                    surname = user.SecondName,
                    ldap = user.LDAP,
                    email = user.Email,
                    group = user.Group,
                    role = user.Privilege,
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrl + "register"), content);

                if (response.IsSuccessStatusCode)
                {
                    if (Preferences.ContainsKey("Role"))
                    {
                        Preferences.Remove("Role");
                    }
                        await DatabaseConnection.DeleteAllUsers();

                    Preferences.Set("Role", $"{user.Privilege}");
                    await DatabaseConnection.AddUser(user);

                    return true;
                }
                return false;
            }
        }

        public async static Task<bool> LoginUser(string email, string password)
        {
            using (var c = new HttpClient())
            {
                var client = new HttpClient();
                var jsonRequest = new
                {
                    email = email,
                    password = password,
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrl + "login"), content);

                if (response.IsSuccessStatusCode)
                {
                    UserModel user;

                    if (!await DatabaseConnection.ExistUser())
                    {
                        user = new UserModel { Email = email };
                        await DatabaseConnection.AddUser(user);
                    }
                    else
                        user = await DatabaseConnection.GetUser();

                    LoginResponse result = JsonConvert.DeserializeObject<LoginResponse>(response.Content.ReadAsStringAsync().Result);
                    user.Id_User = Int32.Parse(result.user_id);

                    try
                    {
                        await SecureStorage.SetAsync("password", $"{password}");
                        await SecureStorage.SetAsync("email", $"{email}");
                        await SecureStorage.SetAsync("oauth_token", $"{result.jwt_token}");
                        Preferences.Set("IsLogIn", $"true");
                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                    }

                    await DatabaseConnection.UpdateUser(user);
                    return true;
                }
                return false;
            }
        }
    }
}
