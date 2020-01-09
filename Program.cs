using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AppliGestionCloture
{
   
    class Program
    {
        /// <summary>
        /// Chaine de connexion à la base de données
        /// </summary>
        private static string chaineConnexion = "server=localhost;port=3308;user id=root;database=gsb_frais;SslMode=none";

        private static bool DEBUG = true;

        private static string SEPARATEUR = "---------------------------------------------";

        private static int DEBUT_VALIDATION = 1;
        private static int FIN_VALIDATION = 10;
        private static int DEBUT_REMBOURSEMENT = 20;

        // On démarre aujourd'hui
        private static DateTime prochaineCloture = DateTime.Today; 
        private static DateTime prochainRemboursement = DateTime.Today;

        /// <summary>
        /// Affiche sur la console le message passé en paramètres si la variable debug est à true
        /// </summary>
        /// <param name="message">Le message à afficher, laisser vide pour un saut de ligne, SEPARATEUR pour une ligne de ----</param>
        public static void console(string message = "")
        {
            if (DEBUG) Console.WriteLine(message);
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
        /// Retourne le nombre de fiches dont l'état est passé en paramètres
        /// </summary>
        /// <param name="etat">String : Etat des fiches</param>
        /// <returns>Int : nombre de fiches correspondant aux critères </returns>
        public static int getNombreFiches(string etat)
        {
            int resultat = -1;
            try
            {
                ConnexionSql sql = new ConnexionSql(chaineConnexion);
                string requete = "SELECT COUNT(*) as resultat FROM fichefrais WHERE idEtat = '" + etat + "'";

                sql.reqSelect(requete);

                resultat = Convert.ToInt32(sql.champ("resultat"));
                sql.close();


            }
            catch (Exception e)
            {
                console("Erreur getNombreFiches : " + e.Message);
            }
            return resultat;

        }
        /// <summary>
        /// Retourne le nombre de fiches dont l'état et le mois sont passés en paramètres
        /// Surcharge la méthode précédente
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
        /// Met à jour l'état des fiches de frais dont l'état précédent est passé en paramètres
        /// </summary>
        /// <param name="ancienEtat">String : état initial</param>
        /// <param name="nouvelEtat">String : état à modifier</param>
        public static void majEtatFichesFrais(string ancienEtat, string nouvelEtat)
        {
            try
            {
                ConnexionSql sql = new ConnexionSql(chaineConnexion);
                string requete = "UPDATE fichefrais SET idEtat = '" + nouvelEtat + "', datemodif = DATE(NOW()) WHERE idEtat = '" + ancienEtat + "'";
                sql.reqUpdate(requete);
                sql.close();
            }
            catch (Exception e)
            {
                console("Erreur majFicheFrais : " + e.Message);
            }
        }

        /// <summary>
        /// Change l'état des fiches de frais dont le mois et l'état initial son passés en paramètres
        /// Surcharge la méthode majEtatFichesFrais
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
                sql.close();
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
            DateTime today = DateTime.Today;
            DateTime moisSuivant = today.AddMonths(+1);
            string mois = getMoisPrecedent();
            //majEtatFichesFrais("CR", "CL", mois);
            // Mémorise que la clôture a déjà été faite pour éviter les traitements inutiles 

            prochaineCloture = new DateTime(moisSuivant.Year, moisSuivant.Month, DEBUT_VALIDATION);
            
        }

        /// <summary>
        /// Rembourse toutes les fiches validées
        /// Pas de test sur la date car les comptables peuvent avoir oublié de valider une fiche du mois m-2
        /// </summary>
        public static void rembourserFichesDeFrais()        
        {
            DateTime today = DateTime.Today;
            DateTime moisSuivant = today.AddMonths(+1);
            //majEtatFichesFrais("VA", "RB");
            // Les comptables doivent arrêter la validation des fiches le 20 
            // Le remboursement intervient après 
            // Passé cette date, une seule requête nécessaire, mémorise la date suivante pour éviter les traitements inutiles
            prochainRemboursement = new DateTime(moisSuivant.Year, moisSuivant.Month, DEBUT_REMBOURSEMENT);
        }


        /// <summary>
        /// Défini le traitement à appliquer lors du déclenchement du timer
        /// cloturer les fiches de frais si la date est comprise entre le 1er et le 10 du mois
        /// rembourser les fiches de frais si la date est comprise entre le 20 et le 30
        /// Attendre le prochain déclenchement du timer dans tous les autres cas
        /// </summary>
        public static void traitements()
        {
            DateTime today = DateTime.Today;
            int dernierJourMois = DateTime.DaysInMonth(today.Year, today.Month);

            if (GestionDates.entre(DEBUT_VALIDATION, FIN_VALIDATION))
            {
                if(DateTime.Compare(today, prochaineCloture) >= 0)
                {                   
                    console("Cloturer les fiches de frais " + prochaineCloture.ToString());
                    cloturerFichesDeFrais();
                } else
                {
                    console("La clôture a déjà été effectuée " + prochaineCloture.ToString());
                }
                
                
            }
            else if (GestionDates.entre(DEBUT_REMBOURSEMENT, dernierJourMois))
            {
                if(DateTime.Compare(today, prochainRemboursement) >= 0)
                {              
                    console("Rembourser les fiches de frais" + prochainRemboursement.ToString());
                    rembourserFichesDeFrais();
                } else
                {
                    console("Les remboursements ont déjà été effectués " + prochainRemboursement.ToString());
                }

            }
            else
            {
                console("Attendre une date plus propice ...");
            }
        }


        // Ecouteur d'événement timer 
        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            traitements();
        }

        static void Main(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Enabled = true;
            timer.Elapsed +=
            new ElapsedEventHandler(timer_Elapsed);

            if (DEBUG) Console.ReadLine();
        }

    }
}
