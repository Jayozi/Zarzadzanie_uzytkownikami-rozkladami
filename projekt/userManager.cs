using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekt
{
    public static class userManager
    {
        public static List<User> ListaUzytkownikow = new List<User>();

        public static void AddUser(User user)
        {
            ListaUzytkownikow.Add(user);
        }
        public static User GetUserByEmail(string email)
        {
            return ListaUzytkownikow.FirstOrDefault(u => u.Email == email);
        }
    

        public static bool SprawdzPoprawnoscEmail(string email)
        {

                string wzorzec = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, wzorzec);
        }

        public static bool CzyIstniejEmail(string email)
        {
            if (ListaUzytkownikow.Any(u => u.SprawdzEmail(email)))
            {
                return true;
            }
            else return false;
        }

        public static bool SprawdzPoprawnoscHasla(string haslo)
        {
            if(string.IsNullOrWhiteSpace(haslo))
            {
                return false;
            }

            if (haslo.Length >= 5 || haslo.Length <= 14)
            {
                if (haslo.Any(char.IsUpper))
                {
                    if (haslo.Any(char.IsLower))
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;

        }
    }
}
