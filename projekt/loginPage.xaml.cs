using Microsoft.Maui.Controls;

namespace projekt
{
    public partial class loginPage : ContentPage
    {
        public static User? LoggedInUser;
        public loginPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            var user = userManager.ListaUzytkownikow.FirstOrDefault(u => u.Email == email && u.Haslo == password);
            LoggedInUser = user;

            if (user != null)
            {
                if (user.Uprawnienia == "adm123")
                {
                    await Navigation.PushAsync(new AdminPanelPage());
                }
                else
                {
                    await Navigation.PushAsync(new UserPanelPage());
                }
            }
            else
            {
                ErrorLabel.Text = "Niepoprawny e-mail lub hasło!";
            }
        }
    }
}





