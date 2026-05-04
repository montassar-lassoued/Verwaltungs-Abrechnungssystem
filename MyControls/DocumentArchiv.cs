using System;

namespace MyControls
{
    public class DocumentArchiv : EventArgs
    {
        public string TableName { get; set; }
        public long IdColumn { get; set; }
    }
}