using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Comdata.LogSys {
    public class ParametroEvento {

        public ParametroEvento(int ordem, string nome, string valor) {
            this.Ordem = ordem;
            this.Nome = nome;
            this.Valor = valor;
        }

        public ParametroEvento(int ordem, string nome, int valor) {
            this.Ordem = ordem;
            this.Nome = nome;
            this.Valor = valor.ToString();
        }

        public int Ordem { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
    }
}
