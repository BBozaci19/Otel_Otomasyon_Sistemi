using Otel_Star;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STAR_OTEL
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // İlk açılacak formu belirtin
            Application.Run(new FrmGiris());  // FrmGiris, ilk açılacak form adı
        }
    }
}
