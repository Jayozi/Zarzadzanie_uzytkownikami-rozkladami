using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projekt
{
    public partial class UserPanelPage : ContentPage
    {
        public UserPanelPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public List<KeyValuePair<string, string>> UserDetails { get; set; }

        private void OnViewSchedule(object sender, EventArgs e)
        {
            // ========================
            // === WORK IN PROGRESS ===
            // ========================
            DisplayAlert("Rozkład jazdy", "Tutaj będzie rozkład jazdy.", "OK");
        }

        private void OnSettings(object sender, EventArgs e)
        {
            UserDetailsPanel.IsVisible = true;
            BackButton.IsVisible = true;

            UserDetails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Imię", loginPage.LoggedInUser.Imie),
                new KeyValuePair<string, string>("Nazwisko", loginPage.LoggedInUser.Nazwisko),
                new KeyValuePair<string, string>("Email", loginPage.LoggedInUser.Email)
            };

            UserDetailsList.ItemsSource = UserDetails;

            (sender as Button).IsVisible = false;
        }

        private void OnHideUserButton(object sender, EventArgs e)
        {
            UserDetailsPanel.IsVisible = false;
            BackButton.IsVisible = false;
            settingsButton.IsVisible = true;

        }

        private async void OnChangePassword(object sender, EventArgs e)
        {
            string newPassword = await DisplayPromptAsync("Zmiana hasła", "Podaj nowe hasło:");

            if (string.IsNullOrWhiteSpace(newPassword)) await DisplayAlert("Błąd!", "Hasło nie spełnia wymagań.", "OK"); ;

            if (loginPage.LoggedInUser.zmianaHasla(newPassword))
            {
                await DisplayAlert("Sukces!", "Hasło zostało zmienione.", "OK");
            }
            else
            {
                await DisplayAlert("Błąd!", "Hasło nie spełnia wymagań.", "OK");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
