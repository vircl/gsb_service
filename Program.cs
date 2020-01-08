using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliGestionCloture
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string chaineConnexion = "server=localhost;port=3308;user id=root;database=gsb_frais;SslMode=none";
            testSQL(chaineConnexion);
        }
        static void testSQL(string chaineConnexion)
        {
            try
            {
                ConnexionSql sql = new ConnexionSql(chaineConnexion);
                Console.Write("Init OK");
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            Console.ReadLine();
        }
    }
}
