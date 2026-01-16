using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Application.Dtos.Autores
{
    public class AutorDto
    {
        public int CodAu { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class AutorCreateDto
    {
        public string Nome { get; set; } = string.Empty;
    }


    public class AutorUpdateDto : AutorCreateDto
    {
        public int CodAu { get; set; }
    }
}
