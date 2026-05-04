using System.Collections.Generic;

namespace FCC_Verwaltungssystem
{
    public class Kosten
    {
        private int id;
        private string bezeichnung;
        private string kostenart;
        private string intervall;
        private List<Kostenzeitraum> kostenzeitraum;

        public string Bezeichnung { get => bezeichnung; set => bezeichnung = value; }
        public string Kostenart { get => kostenart; set => kostenart = value; }
        public string Intervall { get => intervall; set => intervall = value; }
        public List<Kostenzeitraum> Kostenzeitraum { get => kostenzeitraum; set => kostenzeitraum = value; }
        public int Id { get => id; set => id = value; }
    }
}