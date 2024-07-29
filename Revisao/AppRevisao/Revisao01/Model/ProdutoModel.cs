using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRevisao.Revisao01.Model
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public string Marca { get; set; }
        public int Quantidade { get; set; }
        public string CodigoBarra { get; set;}
    }
}
