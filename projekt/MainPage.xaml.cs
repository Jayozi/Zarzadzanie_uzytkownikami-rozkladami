using Microsoft.Maui.Controls;

namespace projekt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Database.Connection();
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Przechodzimy do strony logowania
            await Navigation.PushAsync(new loginPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Przechodzimy do strony rejestracji
            await Navigation.PushAsync(new registerPage());
        }


    }
}







