using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliGestionCloture
{
    /// <summary>
    /// Variables globales à l'application
    /// </summary>
    abstract class G
    {
        public static bool DEBUG = true;

        public static string SEPARATEUR = "------------------------------------------";
        public static int DEBUT_VALIDATION = 1;
        public static int FIN_VALIDATION = 10;
        public static int DEBUT_REMBOURSEMENT = 20;
    }
}
