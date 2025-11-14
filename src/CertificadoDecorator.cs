using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    abstract class CertificadoDecorator : ICertificado
    {
        protected readonly ICertificado _envoltura;

        protected CertificadoDecorator(ICertificado certificado)
        {
            _envoltura = certificado;
        }

        public virtual string GenerarTexto()
        {
            return _envoltura.GenerarTexto();
        }
    }
}
