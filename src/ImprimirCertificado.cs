using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class ImprimirCertificado
    {
        private readonly ICertificado _certificado;
        private readonly IEstiloCertificado _estilo;

        public ImprimirCertificado(ICertificado certificado, IEstiloCertificado estilo)
        {
            _certificado = certificado;
            _estilo = estilo;
        }

        public void Imprimir()
        {
            Console.WriteLine();
            Console.WriteLine(_estilo.ObtenerEncabezado());
            Console.WriteLine();
            Console.WriteLine(_certificado.GenerarTexto());
            Console.WriteLine(_estilo.ObtenerPie());
            Console.WriteLine();
        }
    }
}
