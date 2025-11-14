using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class ProgresoCurso
    {
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
        public bool Completado { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
    }
}
