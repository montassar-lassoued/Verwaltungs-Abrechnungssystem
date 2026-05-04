using System;

namespace MyControls
{
    public class TreeGridViewCellEventArgs : EventArgs
    {

        public TreeGridViewCellEventArgs(int columnIndex, int rowIndex, TreeGridNode node)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            Node = node;
        }

        public int ColumnIndex { get; }
        public int RowIndex { get; }
        public TreeGridNode Node { get; }
    }
}