using System;
using System.Drawing;

namespace MyControls
{
    public class MyCalendarElementsLayout
    {
        #region declaratin
        private Color calendarColor;
        private Color scaleTimeColor;
        private Color lineDarkColor;
        private Color lineLightColor;
        private Color dayStandardColor;
        private Color dayBorderColor;
        private Color dayfreeAppointmentColor;
        private Color headerColor;
        public Color HeaderA = FromHex("#E4ECF6");
        public Color HeaderB = FromHex("#D6E2F1");
        public Color HeaderC = FromHex("#C2D4EB");
        public Color HeaderD = FromHex("#D0DEEF");
        private Color backgroudSelectedColor;
        private Color backgroudHighlightColor;
        private Color muniteSandardColor;
        private Color todayBorderColor;
        #endregion

        #region public methode
        public MyCalendarElementsLayout()
        {
        }
        public void StandardLayout()
        {
            CalendarColor = Color.FromArgb(227, 239, 255);
            ScaleTimeColor = Color.DeepSkyBlue;
            ScaleTimeLineDarkColor = Color.Gray; //FromHex("#C0C0C0");//;
            ScaleTimeLineLightColor = Color.FromArgb(213, 225, 241);
            DayStandardColor = FromHex("#B2CCFF"); // Color.FromArgb(229, 228, 226);
            DayBorderColor = Color.FromArgb(30, 144, 255);
            DayFreeAppointmentColor = FromHex("#7393B3");
            HeaderColor = Combine(SystemColors.ControlDark, SystemColors.Control);
            BackgroudSelectedColor = Color.Empty; //FromHex("#4682B4");// Color.DarkBlue;#4682B4
            BackgroudHighlightColor = FromHex("#F0F0F0");//Combine(SystemColors.Control, SystemColors.ControlLightLight); //(248, 248, 255); //Combine(Color.White, Color.WhiteSmoke);
            MuniteSandardColor = FromHex("#B2CCFF");
            TodayBorderColor = Color.Orange;
        }
        #endregion
        #region private methode
        public static Color Combine(Color c1, Color c2)
        {
            return Color.FromArgb(
                (c1.R + c2.R) / 2,
                (c1.G + c2.G) / 2,
                (c1.B + c2.B) / 2
                );
        }
        public static Color FromHex(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length != 6) throw new Exception("Color not valid");

            return Color.FromArgb(
                int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber));
        }
        #endregion
        #region getter/setter
        public Color CalendarColor
        {
            get => calendarColor;
            set => calendarColor = value;
        }
        public Color ScaleTimeColor
        {
            get => scaleTimeColor;
            set => scaleTimeColor = value;
        }
        public Color ScaleTimeLineDarkColor
        {
            get => lineDarkColor;
            set => lineDarkColor = value;
        }
        public Color ScaleTimeLineLightColor
        {
            get => lineLightColor;
            set => lineLightColor = value;
        }
        public Color DayStandardColor
        {
            get => dayStandardColor;
            set => dayStandardColor = value;
        }
        public Color DayBorderColor
        {
            get => dayBorderColor;
            set => dayBorderColor = value;
        }
        public Color DayFreeAppointmentColor
        {
            get => dayfreeAppointmentColor;
            set => dayfreeAppointmentColor = value;
        }
        public Color HeaderColor
        {
            get => headerColor;
            set => headerColor = value;
        }
        public Color BackgroudSelectedColor
        {
            get => backgroudSelectedColor;
            set => backgroudSelectedColor = value;
        }
        public Color BackgroudHighlightColor
        {
            get => backgroudHighlightColor;
            set => backgroudHighlightColor = value;
        }

        public Color MuniteSandardColor { get => muniteSandardColor; set => muniteSandardColor = value; }
        public Color TodayBorderColor { get => todayBorderColor; set => todayBorderColor = value; }
        #endregion
    }
}
