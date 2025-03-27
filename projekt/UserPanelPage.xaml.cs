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

        // Funkcja dla przycisku "Zobacz rozkład jazdy"
        private void OnViewSchedule(object sender, EventArgs e)
        {
            // ========================
            // === WORK IN PROGRESS ===
            // ========================
            DisplayAlert("Rozkład jazdy", "Tutaj będzie rozkład jazdy.", "OK");
        }

        // Funkcja dla przycisku "Ustawienia"
        private void OnSettings(object sender, EventArgs e)
        {
            // Ukrywamy przyciski i pokazujemy dane użytkownika
            UserDetailsPanel.IsVisible = true;
            BackButton.IsVisible = true;

            // Przypisujemy dane użytkownika do listy Key-Value
            UserDetails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Imię", loginPage.LoggedInUser.Imie),
                new KeyValuePair<string, string>("Nazwisko", loginPage.LoggedInUser.Nazwisko),
                new KeyValuePair<string, string>("Email", loginPage.LoggedInUser.Email)
            };

            // Odświeżamy CollectionView
            UserDetailsList.ItemsSource = UserDetails;

            // Ukrywamy przycisk "Ustawienia"
            (sender as Button).IsVisible = false;
        }

        // Funkcja dla przycisku "Cofnij"
        private void OnHideUserButton(object sender, EventArgs e)
        {
            // Ukrywamy szczegóły użytkownika i przywracamy przyciski
            UserDetailsPanel.IsVisible = false;
            BackButton.IsVisible = false;
            settingsButton.IsVisible = true;

        }

        // Funkcja dla zmiany hasła
        private async void OnChangePassword(object sender, EventArgs e)
        {
            string newPassword = await DisplayPromptAsync("Zmiana hasła", "Podaj nowe hasło:");

            // Sprawdzenie zmiany hasła
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

        // Funkcja wylogowująca
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
