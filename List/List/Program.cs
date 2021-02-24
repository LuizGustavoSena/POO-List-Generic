using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lista = new List<string>();
            string nome;

            Console.WriteLine("Capacidade: " + lista.Capacity);

            do
            {
                Console.Write("Digite um nome para inserir na lista: ");
                nome = Console.ReadLine();
                lista.Add(nome);
            } while (nome !="");

            Console.WriteLine("Quantidade: " + lista.Count);

            lista.ForEach(i => Console.WriteLine(i)); // IMPRESSÃO LISTA

            Console.WriteLine("Ordenar");
            lista.Sort(); // ORDENAR LISTA
            lista.ForEach(i => Console.WriteLine(i)); // IMPRESSÃO LISTA

            lista[3] = "JOHN JOHN";

            lista.Insert(1, "Tiririca"); // POSIÇÃO , NOME

            Console.WriteLine("Lista modificada");
            lista.ForEach(i => Console.WriteLine(i)); // IMPRESSÃO LISTA
            Console.WriteLine("Capacidade: " + lista.Capacity);

            Console.ReadKey();

        }
    }
}
