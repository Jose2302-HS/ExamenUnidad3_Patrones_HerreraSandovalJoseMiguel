using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class EstiloMinimalista : IEstiloCertificado
    {
        public string ObtenerEncabezado()
        {
            return "================ CERTIFICADO =================";
        }

        public string ObtenerPie()
        {
            return "==============================================";
        }
    }
}
