using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Application.Dtos.Assuntos
{

    public class AssuntoDto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }

    public class AssuntoCreateDto
    {
        public string Descricao { get; set; } = string.Empty;
    }

    public class AssuntoUpdateDto : AssuntoCreateDto
    {
        public int CodAs { get; set; }
    }
}
