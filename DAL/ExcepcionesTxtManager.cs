using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DAL
{
    public class ExcepcionesTxtManager
    {
        public ExcepcionesTxtManager()
        {
            
        }

        public static void SaveExcepctionTxt(string message)
        {
            try
            {
                // Usar StreamWriter con append: true para agregar texto al final del archivo
                using (StreamWriter sw = new StreamWriter("Exceptions.txt", true))
                {
                    sw.WriteLine(message+"    "+DateTime.Now.ToString());
                }
                Console.WriteLine("Texto agregado correctamente al archivo.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error al escribir en el archivo: " + e.Message);
            }

        }
    }
}
