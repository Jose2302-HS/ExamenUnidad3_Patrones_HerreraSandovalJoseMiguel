using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class CertificadoBase : ICertificado
    {
        private readonly Usuario _usuario;
        private readonly Curso _curso;
        private readonly DateTime _fechaFinalizacion;

        public CertificadoBase(Usuario usuario, Curso curso, DateTime fechaFinalizacion)
        {
            _usuario = usuario;
            _curso = curso;
            _fechaFinalizacion = fechaFinalizacion;
        }

        public virtual string GenerarTexto()
        {
            return
                $"CERTIFICADO DE FINALIZACIÓN\n" +
                $"Alumno: {_usuario.NombreUsuario}\n" +
                $"Curso: {_curso.Nombre}\n" +
                $"Área: Tecnologías de la Información\n" +
                $"Fecha de finalización: {_fechaFinalizacion:dd/MM/yyyy}\n";
        }
    }
}
