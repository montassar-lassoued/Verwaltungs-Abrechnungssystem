using System;

namespace FCC_Verwaltungssystem
{
    [Flags]
    public enum Zahlungsstatus
    {
        OFFEN = 1,
        BEZAHLT = 2,
        STORNIERT = 3
    }
}
