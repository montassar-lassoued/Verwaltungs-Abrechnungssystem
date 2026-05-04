using System;

namespace FCC_Verwaltungssystem
{
    public static class Globals
    {
        public const Int32 RESRVE = 0;
        public const Int32 VERTRAG_ERFASSEN = 1;
        public const Int32 VERTRAG_SUCHEN = 2;
        public const Int32 KUNDE = 3;
        public const Int32 RECHNUNG = 4;
        public const Int32 JOURNAL = 5;
        public const Int32 KALENDAR = 6;
        public const Int32 UMSATZ = 7;
        public const Int32 KOSTEN = 8;
        public const Int32 EINNAHMENUEBERSCHUSS = 9;
        //***Aktion in WinForm
        public const Int32 NO_ACTION = 0;
        public const Int32 INSERT = 1;
        public const Int32 UPDATE = 2;
        public const Int32 DELETE = 3;
        //****rechnungskreisnummer
        public const string KREIS_FORMAT_0 = "00000";
        public const int KREIS_FORMAT_int = 5;
        //** Formulare
        public const string FORMULAR_RECHNUNG = "RECHNUNG";
        public const string FORMULAR_STORNO_RECHNUNG = "STORNO_RECHNUNG";
        public const string FORMULAR_ANGEBOT = "ANGEBOT";
        //** Stornorechnung
        public const string STORNO_TEIL_KORREKTUR = "TEIL";
        public const string STORNO_GESAMT_KORREKTUR = "GESAMT";
        //*****Kalendar
        public const int HEADER = -1;
        public const int FREEAPPOINTEMENT = -2;
        public const int ITEM_OR_SCALE = -3;
        public const int NONE = -4;
        //
        public const int header = 0;
        public const int freeAppointement = 1;
        public const int dayScaleTime = 2;
        //
        public const int Scroll_Up = 1;
        public const int Scroll_Initialize = 0;
        public const int Scroll_Down = -1;
        //
        public const string VARIABLE_KOSTEN = "VARIABLE";
        public const string FIX_KOSTEN = "FIX";
        //
        public const string KOSTEN_INTERVALL_EINMALIG = "EINMALIG";
        public const string KOSTEN_INTERVALL_MONATLICH = "MONATLICH";
        public const string KOSTEN_INTERVALL_JAEHRLICH = "JÄHRLICH";
        internal static int uMSG = 2025;
    }
}