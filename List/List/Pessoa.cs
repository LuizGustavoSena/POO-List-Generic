using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class Pessoa
    {
        public string Nome { get; set; }

        public Telefone[] telefone;

        public override string ToString()
        {
            string fones ="";
            foreach (Telefone f in telefone)
                fones += f.ToString();
            return ">>>Contato " + Nome + "<<<" + "\n" + fones;
        }
        
    }
}
