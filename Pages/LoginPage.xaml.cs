using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TaskApp.Helpers;
using TaskApp.Model;

namespace TaskApp.Pages;

public partial class LoginPage : ContentPage
{

	private readonly AuthHelper _authHelper;
	public LoginPage(AuthHelper apiHelper)
	{
		_authHelper = apiHelper;
		InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
     
        btnLogin.IsEnabled = false;


	  var  result=	await _authHelper.Login(username.Text, passwrd.Text);

		if (result)
		{

            Application.Current.MainPage = new AppShell();

        }
        btnLogin.IsEnabled = true;
    }
}