using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class CertificadoConFirma : CertificadoDecorator
    {
        private readonly string _nombreInstructor;

        public CertificadoConFirma(ICertificado certificado, string nombreInstructor)
            : base(certificado)
        {
            _nombreInstructor = nombreInstructor;
        }

        public override string GenerarTexto()
        {
            string baseTexto = base.GenerarTexto();
            return baseTexto + $"\nFirma del instructor: {_nombreInstructor}\n";
        }
    }
}
