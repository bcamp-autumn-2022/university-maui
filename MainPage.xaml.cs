using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace university_maui;

public partial class MainPage : ContentPage
{
    int count = 0;

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

            //dataGrid.ItemsSource = users;//writes the data to DataGrid

            string user_data = "";
            foreach (var user in users)
            {
                user_data += user.firstname + " | " + user.lastname + " | " + user.username + "\n";
            }
            labelResult.Text = user_data;

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
