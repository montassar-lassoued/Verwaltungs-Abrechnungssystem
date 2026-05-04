using MyControls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class KostenEintragHelper
    {
        private List<MyFieldDate> zeitraeume_von = new List<MyFieldDate>();
        private List<MyFieldDate> zeitraeume_bis = new List<MyFieldDate>();

        private List<MyFieldFactory> betraege = new List<MyFieldFactory>();
        private List<MyPushButton> pbloeschen = new List<MyPushButton>();

        public List<MyFieldDate> Zeitraeume_von { get => zeitraeume_von; set => zeitraeume_von = value; }
        public List<MyFieldDate> Zeitraeume_bis { get => zeitraeume_bis; set => zeitraeume_bis = value; }
        public List<MyFieldFactory> Betraege { get => betraege; set => betraege = value; }

        public void add(MyFieldDate dateTextBoxVon, MyFieldDate dateTextBoxBis, MyFieldFactory betrag, MyPushButton MyPushButton)
        {
            Zeitraeume_von.Add(dateTextBoxVon);
            Zeitraeume_bis.Add(dateTextBoxBis);
            Betraege.Add(betrag);
            pbloeschen.Add(MyPushButton);
        }
        public void ReassignmentElementsCoord()
        {
            int space = 30;
            int textBoxVon_Y;
            int textBoxBis_Y;
            int textBoxBet_Y;
            int textBoxButt_Y;
            for (int i = 0; i < Zeitraeume_von.Count; i++)
            {
                if (i == 0)
                {
                    // hiert sind die Feste Felder (erste Reihe)
                    // sie dürfen nicht verändert werden
                    continue;
                }
                else
                {
                    textBoxVon_Y = Zeitraeume_von[i - 1].Location.Y;
                    textBoxBis_Y = Zeitraeume_bis[i - 1].Location.Y;
                    textBoxBet_Y = Betraege[i - 1].Location.Y;
                    textBoxButt_Y = pbloeschen[i - 1].Location.Y;
                }
                /*else
                {
                    textBoxVon_Y = startField_DateVon.Location.Y;
                    textBoxBis_Y = startField_DateBis.Location.Y;
                    textBoxBet_Y = startField_Betrag.Location.Y;
                    textBoxButt_Y = startMyPushButton_Loeschen.Location.Y;
                }*/

                Zeitraeume_von[i].Location = new Point(Zeitraeume_von[0].Location.X, textBoxVon_Y + space);
                Zeitraeume_bis[i].Location = new Point(Zeitraeume_bis[0].Location.X, textBoxBis_Y + space);
                Betraege[i].Location = new Point(Betraege[0].Location.X, textBoxBet_Y + space);
                pbloeschen[i].Location = new Point(pbloeschen[0].Location.X, textBoxButt_Y + space);
            }
        }
        public void getLastElements(out MyFieldDate dateTextBoxVon, out MyFieldDate dateTextBoxBis, out MyFieldFactory textBoxBetrag, out MyPushButton MyPushButton)
        {
            dateTextBoxVon = Zeitraeume_von.LastOrDefault();
            dateTextBoxBis = Zeitraeume_bis.LastOrDefault();
            textBoxBetrag = Betraege.LastOrDefault();
            MyPushButton = pbloeschen.LastOrDefault();
        }
        public int Count()
        {
            return Zeitraeume_von.Count;
        }

        internal Control getElementByIndex(int index, string listenname)
        {
            index = pbloeschen.FindIndex(p => p.TabIndex == index);
            if (listenname.Equals("ZEITRAUM_VON"))
            {
                return Zeitraeume_von[index];
            }
            else if (listenname.Equals("ZEITRAUM_BIS"))
            {
                return Zeitraeume_bis[index];
            }
            else if (listenname.Equals("FELD_BETRAG"))
            {
                return Betraege[index];
            }
            else if (listenname.Equals("MyPushButton"))
            {
                return pbloeschen[index];
            }
            return null;
        }

        internal void DeleteByIndex(int index)
        {
            int listIndex = pbloeschen.FindIndex(p => p.TabIndex == index);
            Zeitraeume_von.RemoveAt(listIndex);
            Zeitraeume_bis.RemoveAt(listIndex);
            Betraege.RemoveAt(listIndex);
            pbloeschen.RemoveAt(listIndex);
        }
        internal List<Control> getAllControls()
        {
            List<Control> controls = new List<Control>();
            controls.AddRange(zeitraeume_von.Where((v, i) => i > 0));
            controls.AddRange(zeitraeume_bis.Where((v, i) => i > 0));
            controls.AddRange(betraege.Where((v, i) => i > 0));
            controls.AddRange(pbloeschen.Where((v, i) => i > 0));

            return controls;
        }
        internal void DeleteAllElements()
        {
            zeitraeume_von.RemoveRange(1, zeitraeume_von.Count - 1);
            zeitraeume_bis.RemoveRange(1, zeitraeume_bis.Count - 1);
            betraege.RemoveRange(1, betraege.Count - 1);
            pbloeschen.RemoveRange(1, pbloeschen.Count - 1);
        }
    }
}