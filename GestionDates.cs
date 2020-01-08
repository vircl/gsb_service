using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliGestionCloture
{
    public class GestionDates
    {
        /// <summary>
        /// Retourne le n° du jour de la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">DateTime : Date à tester</param>
        /// <returns>String : n° du jour sur deux chiffres </returns>
        public string getNumeroJour(DateTime uneDate)
        {

        }
        /// <summary>
        /// Retourne le n° du mois précédant la date courante
        /// </summary>
        /// <returns>String n° du mois sur 2 chiffres</returns>
        public string getMoisPrecedent()
        {

        }
        /// <summary>
        /// Retourne le n° du mois précédant la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">Date à tester</param>
        /// <returns>String n° du mois sur 2 chiffres</returns>
        public string getMoisPrecedent(DateTime uneDate)
        {

        }
        /// <summary>
        /// Retourne le n° du mois suivant la date courante 
        /// </summary>
        /// <returns>String : le n° du mois sur deux chiffres</returns>
        public string getMoisSuivant()
        {

        }
        /// <summary>
        /// Retourne le n° du mois suivant la date passée en paramètres
        /// </summary>
        /// <param name="uneDate">Date à tester</param>
        /// <returns>String : n° du mois sur 2 chiffres</returns>
        public string getMoisSuivant(DateTime uneDate)
        {

        }
        /// <summary>
        /// Teste si le n° de jour de la date actuelle se trouve entre les bornes passées en paramètre
        /// </summary>
        /// <param name="min">string : Borne min</param>
        /// <param name="max">string : Borne max</param>
        /// <returns>Boolean : True si la date est située entre les bornes</returns>
        public bool entre(string min, string max)
        {

        }
        /// <summary>
        /// Teste si le n° de jour de la date passée en paramètres se trouve entre les bornes min et max
        /// </summary>
        /// <param name="min">string : Borne min</param>
        /// <param name="max">string : Borne max</param>
        /// <param name="uneDate">DateTime : La date à tester</param>
        /// <returns>Boolean : True si la date est située entre les bornes</returns>
        public bool entre(string min, string max, DateTime uneDate)
        {

        }
    }
}
