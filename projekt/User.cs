
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt
{
    public class User
    {
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        public string Email { get; private set; }
        public string Haslo { get; private set; }
        public string Uprawnienia { get; private set; }

        public User(string imie, string nazwisko, string email, string haslo, string uprawnienia)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Email = email;
            Haslo = haslo;
            Uprawnienia = uprawnienia;
        }

       
        public bool SprawdzHaslo(string podaneHaslo)
        {
            return Haslo == podaneHaslo;
        }

        public bool SprawdzEmail(string podanyEmail)
        {
            return Email == podanyEmail;
        }

        public string sprawdzUprawnienia(string podaneUprawnienia)
        {
            return Uprawnienia;
        }
        public void edycjaDanych(string imie, string nazwisko, string haslo)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Haslo = haslo;

            Database.UpdateUserConnetion(this);
        }

        public bool zmianaHasla(string haslo)
        {
            if (userManager.SprawdzPoprawnoscHasla(haslo))
            { 
                Haslo = haslo;
                Database.UpdateUserConnetion(this);
                return true;
            }
            else
                return false;
        }
    }

}
