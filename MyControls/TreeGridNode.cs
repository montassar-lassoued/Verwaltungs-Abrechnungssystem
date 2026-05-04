using System.Collections.Generic;
using System.Windows.Forms;

namespace MyControls
{
    public class TreeGridNode : DataGridViewRow
    {

        public List<TreeGridNode> Children { get; set; } = new List<TreeGridNode>();
        public TreeGridNode Parent { get; set; }
        public bool IsExpanded { get; set; } = false;

        public int Level => Parent == null ? 0 : Parent.Level + 1;

        public TreeGridNode()
        {
            // Leerer Konstruktor für DataGridView intern
        }

        public TreeGridNode(params object[] values)
        {

            foreach (var val in values)
            {
                var cell = new TreeGridCell
                {
                    Value = val,
                };
                Cells.Add(cell);

            }
        }

    }
}
