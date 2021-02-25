using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pessoa> contatos = new List<Pessoa>();
            Pessoa pessoa;
            byte op;
            string remover, buscar;

            returnFile("contatos.txt", contatos);
            do
            {
                op = menu();
                Console.Clear();

                switch (op)
                {
                    case 1: // INSERIR CONTATO

                        pessoa = lerPessoa();
                        contatos.Add(pessoa);
                        contatos = contatos.OrderBy(x => x.Nome).ToList();
                        addFile(contatos, "contatos.txt");

                        break;
                    case 2: // IMPRIMIR CONTATOS

                        contatos.ForEach(i => Console.WriteLine(i.ToString()));

                        break;
                    case 3: //DELETAR CONTATOS

                        Console.Write("Qual contato deseja remover: ");
                        remover = Console.ReadLine();
                        pessoa = contatos.Find(x => x.Nome.Equals(remover));
                        contatos.Remove(pessoa);
                        addFile(contatos, "contatos.txt");

                        break;
                    case 4: // BUSCAR CONTATO
                        Console.Write("Qual contato deseja encontrar: ");
                        buscar = Console.ReadLine();
                        pessoa = contatos.Find(x => x.Nome.Equals(buscar));
                        Console.WriteLine(pessoa.ToString());
                        break;
                    case 5: // QUANTIDADE CONTATOS
                        Console.Write("Quantidade de contatos: " + contatos.Count);
                        break;
                    case 6: // IMPRESSAO BREAK
                        break;
                }
            } while (op != 0);

            Console.ReadKey();

        }

        public static void addFile(List<Pessoa> contatos, string text)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\LuizSena\source\repos\LuizGustavoSena\POO-List-Generic\List\" + text))
            {
                for (int i = 0; i < contatos.Count; i++)
                {
                    file.Write(contatos[i].Nome + ";" + contatos[i].telefone.Length + ",");
                    foreach (Telefone t in contatos[i].telefone)
                        file.Write(t.Consultar());
                    file.WriteLine(";");
                }
            }
        }

        public static void returnFile(string text, List<Pessoa> lista)
        {
            try
            {
                using (StreamReader file = new StreamReader(@"C:\Users\LuizSena\source\repos\LuizGustavoSena\POO-List-Generic\List\" + text, Encoding.UTF8))
                {
                    string line;
                    string[] campos;
                    string[] tels;
                    List<Telefone> telefones;
                    Pessoa p;
                    while (!file.EndOfStream)
                    {
                        line = file.ReadLine();
                        campos = line.Split(';');
                        tels = campos[1].Split(',');
                        telefones = new List<Telefone>();
                        
                        for (int i = 1, y = 0; y < int.Parse(tels[0]); i += 3, y++)
                        {
                            telefones.Add(new Telefone() { Tipo = tels[i], DDD = int.Parse(tels[i + 1]), Numero = int.Parse(tels[i + 2]) });
                        }
                        p = new Pessoa()
                        {
                            Nome = campos[0],
                            telefone = telefones.ToArray(),
                        };
                        lista.Add(p);
                    }
                }
            }
            catch (Exception)
            {

            }
            
        }
        static byte menu()
        {
            try
            {
                Console.WriteLine("------------------------------\n" +
                                "1 - Inserir novo Contato\n" +
                                "2 - Imprimir Contatos\n" +
                                "3 - Deletar Contato\n" +
                                "4 - Buscar Contato\n" +
                                "5 = Quantidade de Contatos\n" +
                                "0 = Sair\n" +
                                "------------------------------");
                return byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                return menu();
            }
        }

        static Pessoa lerPessoa()
        {
            try
            {
                string nome, tipo;
                int ddd, numero;
                List<Telefone> telefones = new List<Telefone>();

                do
                {
                    Console.Write("Informe o nome do contato:");
                    nome = Console.ReadLine();
                } while (nome == "");

                do
                {
                    Console.Write("Informe o DDD: ");
                    ddd = int.Parse(Console.ReadLine());

                    if (ddd != 0)
                    {
                        Console.Write("Informe o Número: ");
                        numero = int.Parse(Console.ReadLine());

                        Console.Write("Informe o Tipo: ");
                        tipo = Console.ReadLine();
                        telefones.Add(new Telefone { DDD = ddd, Numero = numero, Tipo = tipo });
                    }
                } while (ddd != 0);

                return new Pessoa
                {
                    Nome = nome,
                    telefone = telefones.ToArray(),
                };
            }
            catch (Exception)
            {
                return lerPessoa();
            }

        }
    }
}
