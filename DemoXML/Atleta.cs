using System;

namespace DemoXML
{
    public class Atleta
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Presenze { get; set; }
        public DateTime DataNascita { get; set; }
        public override string ToString()
        {
            return $"{Nome} {Cognome}";
        }
    }
}
