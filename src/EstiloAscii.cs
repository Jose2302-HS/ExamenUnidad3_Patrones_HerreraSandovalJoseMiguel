using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class EstiloAscii : IEstiloCertificado
    {
        public string ObtenerEncabezado()
        {
            return "+--------------------------------------------+\n" +
                   "|              CERTIFICADO TI                |\n" +
                   "+--------------------------------------------+";
        }

        public string ObtenerPie()
        {
            return "+--------------------------------------------+\n" +
                   "|        Plataforma de Cursos en TI          |\n" +
                   "+--------------------------------------------+";
        }
    }
}
