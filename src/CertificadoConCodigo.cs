using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class CertificadoConCodigo : CertificadoDecorator
    {
        private readonly string _codigo;

        public CertificadoConCodigo(ICertificado certificado, string codigo)
            : base(certificado)
        {
            _codigo = codigo;
        }

        public override string GenerarTexto()
        {
            string baseTexto = base.GenerarTexto();
            return baseTexto + $"Código interno: {_codigo}\n";
        }
    }
}
