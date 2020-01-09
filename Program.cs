using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliGestionCloture
{
   
    class Program
    {
        /// <summary>
        /// Chaine de connexion à la base de données
        /// </summary>
        private static string chaineConnexion = "server=localhost;port=3308;user id=root;database=gsb_frais;SslMode=none";
        
        /// <summary>
        /// Affiche sur la console le message passé en paramètres si la variable debug est à true
        /// </summary>
        /// <param name="message">Le message à afficher, laisser vide pour un saut de ligne, G.SEPARATEUR pour une ligne de ----</param>
        public static void console(string message = "")
        {
            if (G.DEBUG) Console.WriteLine(message);
        }

        /// <summary>
        /// Retourne le mois précédent la date actuelle sous la forme aaaamm
        /// </summary>
        /// <returns>String : mois sous la forme aaaamm</returns>
        public static string getMoisPrecedent()
        {
            string mois = GestionDates.getMoisPrecedent();
            string annee = GestionDates.getAnneeMoisPrecedent();
            return annee + mois;
        }

        /// <summary>
        /// Retourne le nombre de fiches dont le statut et le mois sont passés en paramètres
        /// </summary>
        /// <param name="etat">String : Etat des fiches</param>
        /// <param name="mois">String : Mois concerné</param>
        /// <returns>Int : nombre de fiches correspondant aux critères </returns>
        public static int getNombreFiches(string etat, string mois)
        {
            int resultat = -1;
            try
            {
                ConnexionSql sql = new ConnexionSql(chaineConnexion);
                string requete = "SELECT COUNT(*) as resultat FROM fichefrais WHERE idEtat = '" + etat + "' and mois = '" + mois + "'";

                sql.reqSelect(requete);

                resultat =  Convert.ToInt32(sql.champ("resultat"));
                sql.close();
      
                
            } catch (Exception e)
            {
                console("Erreur getNombreFiches : " + e.Message);
            }
            return resultat;

        }

        /// <summary>
        /// Change l'état des fiches de frais dont le mois et l'état initial son passés en paramètres
        /// </summary>
        /// <param name="ancienEtat"></param>
        /// <param name="nouvelEtat"></param>
        /// <param name="mois"></param>
        public static void majEtatFichesFrais(string ancienEtat, string nouvelEtat, string mois )
        {
            try
            {
                ConnexionSql sql = new ConnexionSql(chaineConnexion);
                string requete = "UPDATE fichefrais SET idEtat = '" + nouvelEtat + "', datemodif = DATE(NOW()) WHERE idEtat = '" + ancienEtat + "' AND mois = '" + mois + "'";
                sql.reqUpdate(requete);
            } catch (Exception e)
            {
                console("Erreur majFicheFrais : " + e.Message);
            }  
        }

        /// <summary>
        /// Clôture toutes les fiches du mois précédent si la campagne de validation est en cours
        /// </summary>
        public static void cloturerFichesDeFrais()
        {

            // La campagne de validation doit s'effectuer entre le premier et le 10 du mois 

            bool dateOK = GestionDates.entre(G.DEBUT_VALIDATION, G.FIN_VALIDATION);
            if(dateOK)
            {
                string mois = getMoisPrecedent();

                console("Campagne de validation en cours");
                console(G.SEPARATEUR);
                console();
                console("Lecture de la base de données...");
                console();
                console("Chaine de connexion = " + chaineConnexion);
                console(G.SEPARATEUR);
                console();
                console("Nombre de fiches à clôturer : " + getNombreFiches("CR", mois).ToString());
                majEtatFichesFrais("CR", "CL", mois);
            } else
            {
                if (G.DEBUG) Console.WriteLine("Campagne du validation terminée");
            }
        }


        public static void rembourserFichesDeFrais()
        {

        }
        static void Main(string[] args)
        {
            //string chaineConnexion = "server=localhost;port=3308;user id=root;database=gsb_frais;SslMode=none";
            cloturerFichesDeFrais();
            Console.ReadLine();
        }

    }
}
