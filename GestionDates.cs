using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliGestionCloture
{
    public abstract class GestionDates
    {
        /// <summary>
        /// Retourne le n° du jour de la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">DateTime : Date à tester</param>
        /// <returns>String : n° du jour sur deux chiffres </returns>
        public static int getNumeroJour(DateTime uneDate)
        {
            return uneDate.Day;
        }

        /// <summary>
        /// Converti le nombre passé en paramètres en chaîne de caractère affichée sur deux chiffres
        /// </summary>
        /// <param name="nombre">int : le nombre à convertir</param>
        /// <returns>String Le nombre sur 2 chiffres</returns>
        public static string convertirNombre(int nombre)
        {
            return nombre.ToString("00");
        }

        /// <summary>
        /// Retourne le n° du mois précédant la date courante
        /// </summary>
        /// <returns>String n° du mois sur 2 chiffres</returns>
        public static string getMoisPrecedent()
        {
            DateTime uneDate = DateTime.Now;          
            int mois = uneDate.Month;
            return convertirNombre(uneDate.AddMonths(-1).Month);
        }
        /// <summary>
        /// Retourne le n° du mois précédant la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">Date à tester</param>
        /// <returns>String n° du mois sur 2 chiffres</returns>
        public static string getMoisPrecedent(DateTime uneDate)
        {
            int mois = uneDate.Month;
            return convertirNombre(uneDate.AddMonths(-1).Month);
        }
        /// <summary>
        /// Retourne le n° du mois suivant la date courante 
        /// </summary>
        /// <returns>String : le n° du mois sur deux chiffres</returns>
        public static string getMoisSuivant()
        {
            DateTime uneDate = DateTime.Now;
            int mois = uneDate.Month;
            return convertirNombre(uneDate.AddMonths(+1).Month);
        }
        /// <summary>
        /// Retourne le n° du mois suivant la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">Date à tester</param>
        /// <returns>String : n° du mois sur 2 chiffres</returns>
        public static string getMoisSuivant(DateTime uneDate)
        {
            int mois = uneDate.Month;
            return convertirNombre(uneDate.AddMonths(+1).Month);
        }
        /// <summary>
        /// Teste si le n° de jour de la date actuelle se trouve entre les bornes passées en paramètre
        /// Si les bornes sont définies en dehors du nombre de jours que comporte le mois 
        /// la fonction retourne false
        /// </summary>
        /// <param name="min">int : Borne min</param>
        /// <param name="max">int : Borne max</param>
        /// <returns>Boolean : True si la date est située entre les bornes</returns>
        public static bool entre(int min, int max)
        {
            DateTime uneDate = DateTime.Now;
            int daysInMonth = DateTime.DaysInMonth(uneDate.Year, uneDate.Month);
            int numeroJour = getNumeroJour(uneDate);
            return (min >= 1) && (max <= daysInMonth) && (numeroJour >= min && numeroJour <= max);
        }
        /// <summary>
        /// Teste si le n° de jour de la date passée en paramètres se trouve entre les bornes passées en paramètres
        /// Si les bornes sont définies en dehors du nombre de jours que comporte le mois
        /// la fonction retourne false
        /// </summary>
        /// <param name="min">int : Borne min</param>
        /// <param name="max">int : Borne max</param>
        /// <param name="uneDate">DateTime : La date à tester</param>
        /// <returns>Boolean : True si la date est située entre les bornes</returns>
        public static bool entre(int min, int max, DateTime uneDate)
        {
            int daysInMonth = DateTime.DaysInMonth(uneDate.Year, uneDate.Month);
            int numeroJour = getNumeroJour(uneDate);
            return (min >= 1) && (max <= daysInMonth) && (numeroJour >= min && numeroJour <= max);
        }
    }
}
