namespace List
{
    public class Telefone
    {
        public int DDD { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }

        public override string ToString()
        {
            return Tipo + " (" + DDD + ") " + Numero + ";";
        }

        public string Consultar()
        {
            return Tipo + "," + DDD + "," + Numero + ";";
        }
    }
}