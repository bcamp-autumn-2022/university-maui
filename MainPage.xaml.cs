//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
//using static Android.Graphics.Paint;

namespace university_maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void GetUsers(object sender, EventArgs e)
    {
        var data = Task.Run(() => GetUsersFromApi());
        data.Wait();
        Console.WriteLine(data.Result);
        if (data.Result.Length > 3) //Result is not []
        {
            dynamic users = JsonConvert.DeserializeObject(data.Result);
            List<User> usersList = new List<User>();

            foreach (var user in users)
            {
                string identity;
                if (user.identity == 1) identity = "student";
                else if (user.identity == 2) identity = "admin";
                else identity = "teacher";
                usersList.Add(new User { Name = user.firstname+' '+user.lastname, Username=user.username, Identity=identity });
            }
            collectionUser.ItemsSource = usersList.ToList();
        }
        else
        {
            labelResult.Text = "There is no users";
        }
    }
    static async Task<string> GetUsersFromApi(string id = null)
    {
        //var userName = "admin";
        //var passwd = "1234";
        //var authData = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
        var response = string.Empty;
        var url = "https://pekan-dotnet-university.herokuapp.com/api/user";
        var client = new HttpClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));
        HttpResponseMessage result = await client.GetAsync(url);
        response = await result.Content.ReadAsStringAsync();
        return response;
    }

}
