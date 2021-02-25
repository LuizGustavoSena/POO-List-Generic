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
            // VARIAVEIS
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

                        pessoa = lerPessoa(); // LE PESSOA E TELEFONES
                        contatos.Add(pessoa); // ADICIONA PESSOA A LISTA
                        contatos = contatos.OrderBy(x => x.Nome).ToList(); // ORDENA
                        addFile(contatos, "contatos.txt"); // ADICIONA LISTA NOS ARQUIVOS

                        break;
                    case 2: // IMPRIMIR CONTATOS

                        contatos.ForEach(i => Console.WriteLine(i.ToString())); // IMPRIME LISTA

                        break;
                    case 3: //DELETAR CONTATOS

                        Console.Write("Qual contato deseja remover: ");
                        remover = Console.ReadLine();
                        pessoa = contatos.Find(x => x.Nome.Equals(remover)); // BUSCA PESSOA DA LISTA
                        contatos.Remove(pessoa); // REMOVE PESSOA ENCONTRADA
                        addFile(contatos, "contatos.txt");

                        break;
                    case 4: // BUSCAR CONTATO
                        Console.Write("Qual contato deseja encontrar: ");
                        buscar = Console.ReadLine();
                        pessoa = contatos.Find(x => x.Nome.Equals(buscar)); // BUSCA PESSOA DA LISTA
                        Console.WriteLine(pessoa.ToString()); // IMPRIME PESSOA ENCONTRADA
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

        public static void addFile(List<Pessoa> contatos, string text) // FUNÇÃO ADICIONA LISTA NO ARQUIVO
        {
            // ADICIOINA ARQUIVO NO DIRETORIO INDICADO
            using (StreamWriter file = new StreamWriter(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\POO-List-Generic\List\" + text))
            {
                for (int i = 0; i < contatos.Count; i++) // LAÇO INCREMENTA A LISTA NO ARQUIVO
                {
                    file.Write(contatos[i].Nome + ";" + contatos[i].telefone.Length + ",");
                    foreach (Telefone t in contatos[i].telefone)
                        file.Write(t.Consultar());
                    file.WriteLine(";");
                }
            }
        }

        public static void returnFile(string text, List<Pessoa> lista) //FUNÇÃO RETORNA ARQUIVO JA CRIADO
        {
            // SE ARQUIVO EXISTIR 
            if (File.Exists(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\POO-List-Generic\List\" + text)) 
            {
                // PEGA ARQUIVO DO DIRETORIO INDICADO
                using (StreamReader file = new StreamReader(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\POO-List-Generic\List\" + text, Encoding.UTF8))
                {
                    // VARIAVEIS
                    string line;
                    string[] campos;
                    string[] tels;
                    List<Telefone> telefones;
                    Pessoa p;

                    while (!file.EndOfStream) // ENQUANTO ARQUIVO EXISTIR
                    {
                        // INICIA AS VARIAVEIS
                        line = file.ReadLine();
                        campos = line.Split(';'); // DIVIDE A LINHA EM CAMPO (NOME / NUMEROS)
                        tels = campos[1].Split(','); // DIVIDE O CAMPO NUMEROS (QUANTIDADE / TIPO / DDD / NUMERO)
                        telefones = new List<Telefone>();
                        
                        // LAÇO PARA ATRIBUIR NUMEROS DE TELEFONE AO MESMO CONTATO
                        for (int i = 1, y = 0; y < int.Parse(tels[0]); i += 3, y++)
                        {
                            telefones.Add(new Telefone() { Tipo = tels[i], DDD = int.Parse(tels[i + 1]), Numero = int.Parse(tels[i + 2]) });
                        }

                        // ADICIONA OS NÚMEROS A PESSOA
                        p = new Pessoa()
                        {
                            Nome = campos[0],
                            telefone = telefones.ToArray(),
                        };
                        lista.Add(p); // ADICIONA PESSOA A LISTA DE CONTATOS
                    }
                }
            }
        }
        static byte menu()
        {
            try
            {          // MENU
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
                // VARIAVEIS
                string nome, tipo;
                int ddd, numero;
                List<Telefone> telefones = new List<Telefone>();

                do
                { // LAÇO PARA NÃO DEIXAR CAMPO VAZIO
                    Console.Write("Informe o nome do contato:");
                    nome = Console.ReadLine();
                } while (nome == "");

                do
                { // LAÇO PARA INFORMAR NUMEROS
                    // PARADA INFORMAR ZERO AO DDD
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

                return new Pessoa // RETURNA PESSOA COM SEUS TELEFONES
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
