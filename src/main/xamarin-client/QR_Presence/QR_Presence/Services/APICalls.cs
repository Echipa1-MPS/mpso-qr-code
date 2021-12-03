using Newtonsoft.Json;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QR_Presence.Services
{
    public static class APICalls
    {
        #region URLs
        public static string BaseUrlAuth = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/authentication/";
        public static string BaseUrlAdmin = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/admin/";

        public static string BaseUrlSubject = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/subject/";
        public static string BaseUrlSubjectAdmin = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/subject/admin/";
        public static string BaseUrlSchedAdmin = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/schedule/admin/";
        public static string BaseUrlQr = "http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/qr/student/";


        #endregion URLs

        #region Login/Register

        public async static Task<bool> RegisterUser(User user, string password)
        {
            using (var c = new HttpClient())
            {
                var client = new HttpClient();
                var jsonRequest = new
                {
                    name = user.name,
                    password = password,
                    surname = user.surname,
                    username = user.username,
                    email = user.email,
                    group = user.group,
                    role = user.Privilege,
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlAuth + "register"), content);

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

                var response = await client.PostAsync(new Uri(BaseUrlAuth + "login"), content);

                if (response.IsSuccessStatusCode)
                {
                    User user;

                    if (!await DatabaseConnection.ExistUser())
                    {
                        user = new User { email = email };
                        await DatabaseConnection.AddUser(user);
                    }
                    else
                        user = await DatabaseConnection.GetUser();

                    LoginResponse result = JsonConvert.DeserializeObject<LoginResponse>(response.Content.ReadAsStringAsync().Result);
                    user.Privilege = Int32.Parse(result.role);

                    try
                    {
                        await SecureStorage.SetAsync("password", $"{password}");
                        await SecureStorage.SetAsync("email", $"{email}");
                        await SecureStorage.SetAsync("oauth_token", $"{result.jwt_token}");
                        Preferences.Set("IsLogIn", $"true");
                        Preferences.Set("Role", $"{user.Privilege}");

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
        #endregion Login/Register

        #region Admin

        public async static Task<StudentsAdmin> GetStudentsAdminAsync()
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var response = await client.GetAsync(new Uri(BaseUrlAdmin + "get-students"));

                StudentsAdmin Items = new StudentsAdmin();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Items = JsonConvert.DeserializeObject<StudentsAdmin>(content);
                }

                return Items;
            }
        }

        public async static Task<TeachersAdmin> GetProfessorsAdminAsync()
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var response = await client.GetAsync(new Uri(BaseUrlAdmin + "get-teachers"));

                TeachersAdmin Items = new TeachersAdmin();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Items = JsonConvert.DeserializeObject<TeachersAdmin>(content);
                }

                return Items;
            }
        }


        public async static Task<bool> DeleteUserAdminAsync(string email)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    email = email,
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = content,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(BaseUrlAdmin + "delete-user")
                };
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<bool> UpdateUserAdminAsync(User user)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    email = user.email,
                    name = user.name,
                    surname = user.surname,
                    user_id = user.user_id,
                    group = user.group
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var method = new HttpMethod("PATCH");
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = content,
                    Method = method,
                    RequestUri = new Uri(BaseUrlAdmin + "update-user")
                };

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<bool> AddUserAdminAsync(User user, string password)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    email = user.email,
                    name = user.name,
                    surname = user.surname,
                    password = password,
                    group = user.group,
                    role = user.Privilege
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlAdmin + "add-user"), content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<GetCoursesModel> GetAllCoursesAdminAsync()
        {

            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var response = await client.GetAsync(new Uri(BaseUrlSubjectAdmin + "get-all-courses"));

                GetCoursesModel Items = new GetCoursesModel();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Items = JsonConvert.DeserializeObject<GetCoursesModel>(content);
                }

                return Items;
            }
        }

        public async static Task<int> CreateCourseAdminAsync(Cours course, int Id_Professor)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    nameC = course.Name_C,
                    idProfessor = Id_Professor,
                    desc = course.Desc,
                    grading = course.Grading
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlSubjectAdmin + "create-course"), content);

                if (response.IsSuccessStatusCode)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    int id = JsonConvert.DeserializeObject<int>(cont);
                    return id;
                }

                return -1;
            }
        }

        public async static Task<bool> UpdateCourseAdminAsync(Cours course, int Id_Professor)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    course_id = course.Id_Course,
                    nameC = course.Name_C,
                    idProfessor = Id_Professor,
                    desc = course.Desc,
                    grading = course.Grading
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlSubjectAdmin + "update-course"), content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<bool> DeleteCourseAdminAsync(int id_course)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(BaseUrlSubjectAdmin + "delete-course/" + id_course + "\n")
                };

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<bool> EnroleStudentsAdminAsync(EnrolleStudents student_to_enrolle)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var serializedJsonRequest = JsonConvert.SerializeObject(student_to_enrolle);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlSubjectAdmin + "enroll-students"), content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public async static Task<bool> AddIntervalsCoursAsync(IntervalPicker interval, int subject)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;
                
                var jsonRequest = new
                {
                    day = interval.Day,
                    duration = interval.Duration,
                    start_time = $"{interval.StartH}:00",
                    subject = subject
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlSchedAdmin + "add-schedule"), content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }


        #endregion Admin

        #region Student

        public async static Task<UserCourses> GetUserCoursesAsync()
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var response = await client.GetAsync(new Uri(BaseUrlSubject + "get-all-courses-for-current-user"));

                UserCourses Items = new UserCourses();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Items = JsonConvert.DeserializeObject<UserCourses>(content);
                }

                  return Items;
            }
        }
        public async static Task<ProfileModel> GetProfileAsync()
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var response = await client.GetAsync(new Uri(BaseUrlSubject + "get-next-courses-for-current-user"));

                ProfileModel Items = new ProfileModel();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Items = JsonConvert.DeserializeObject<ProfileModel>(content);
                }

                return Items;
            }
        }

        public async static Task<string> ScanQrAsync(int subject, int qr_id, int key)
        {
            using (var c = new HttpClient())
            {
                HttpClient client = new HttpClient();
                var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("oauth_token"));

                client.DefaultRequestHeaders.Authorization = authHeader;

                var jsonRequest = new
                {
                    subject = subject,
                    qr_id = qr_id,
                    key = key
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(BaseUrlQr + "scan-qr"), content);

                if (response.IsSuccessStatusCode)
                {

                }

                string cont = await response.Content.ReadAsStringAsync();
                //string message = JsonConvert.DeserializeObject<string>(cont);
                
                return $"{cont} - {response.StatusCode}";
            }
        }
        #endregion Student

    }
}
