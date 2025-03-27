using Microsoft.Maui.Controls;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace projekt
{
    public partial class AdminPanelPage : ContentPage
    {
        public AdminPanelPage()
        {
            InitializeComponent();
            loadUsers();

        }

        private void loadUsers()
        {
            Database.Connection();
            UserList.ItemsSource = null;
            UserList.ItemsSource = userManager.ListaUzytkownikow;
        }
        private async void OnChangePassword(object sender, EventArgs e)
        {
            string newPassword = await DisplayPromptAsync("Edycja uzytkownika", $"Podaj nowe haslo");

            if(loginPage.LoggedInUser.zmianaHasla(newPassword))
            {
                await DisplayAlert("Udane!", "Pomyslnie zmienione haslo", "Ok");
            }
            else
            {
                await DisplayAlert("Blad!", "Haslo nie spelnia wymagan", "Ok");
            }
        }

        private void OnManageUserClicked(object sender, EventArgs e)
        {
            loadUsers();
            userManagementPanel.IsVisible = true;
        }

        private async void OnDeleteUser(object sender, EventArgs e)
        {
            if (loginPage.LoggedInUser == null)
            {
                await DisplayAlert("Blad!", "Nie jestes zalogowany!", "Ok");
                return;
            }

            var button = sender as Button;
            var userToDelete = button?.CommandParameter as User;
            
            if (userToDelete == null) return;

            bool confirm = await DisplayAlert("Usun uzytkownika", $"Czy na pewno chcesz usunac {userToDelete.Email}?", "Tak", "Nie");
            if (!confirm) return;

            if (userToDelete.Email != loginPage.LoggedInUser.Email)
            {
                Database.DeleteUserConnetion(userToDelete);
                OnPropertyChanged(nameof(userManager.ListaUzytkownikow));
                loadUsers();
            }
            else
                await DisplayAlert("Blad!", "Nie mozesz usunac uzytkownika na ktorego jestes zalogowany!", "Ok");
        }

        private async void OnEditUser(object sender, EventArgs e)
        {
            var button = sender as Button;
            var userToEdit = button?.CommandParameter as User;

            string newName = await DisplayPromptAsync("Edycja uzytkownika", $"Podaj nowe imie dla {userToEdit.Imie}", initialValue: userToEdit.Imie);

            string newSurname = await DisplayPromptAsync("Edycja uzytkownika", $"Podaj nowe nazwisko dla {userToEdit.Nazwisko}", initialValue: userToEdit.Nazwisko);

            string newPassword = await DisplayPromptAsync("Edycja uzytkownika", $"Podaj nowye haslo dla {userToEdit.Haslo}", initialValue: userToEdit.Haslo);

            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newSurname) || string.IsNullOrWhiteSpace(newPassword)) return;

            userToEdit.edycjaDanych(newName, newSurname, newPassword);
            loadUsers();
        }

        private void OnAddUser(object sender, EventArgs e)
        {
            userAddForm.IsVisible = true;
        }
        private async void OnAddUserToDatabase(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string surname = SurnameEntry.Text;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string admPass = AdmPassEntry.Text;  

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Błąd", "Wszystkie pola są wymagane!", "OK");
                return;
            }

            User newUser = new User(name, surname, email, password, admPass);

            Database.InsertUserConnection(newUser);
            userManagementPanel.IsVisible = false;

            loadUsers();

            await DisplayAlert("Sukces", "Użytkownik został dodany", "OK");
            userAddForm.IsVisible = false;

            NameEntry.Text = string.Empty;
            SurnameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            AdmPassEntry.Text = string.Empty;
        }

        private void OnViewSchedule(object sender, EventArgs e)
        {
            // ========================
            // === WORK IN PROGRESS ===
            // ========================
            DisplayAlert("Rozkład jazdy", "Tutaj będzie rozkład jazdy.", "OK");
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }


    }
}
