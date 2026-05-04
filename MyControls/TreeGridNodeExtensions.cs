using System.Linq;

namespace MyControls
{
    public static class TreeGridNodeExtensions
    {
        public static bool IsLastVisibleChild(this TreeGridNode node)
        {
            if (node.Parent == null)
                return true;

            var siblings = node.Children.Where(n => n.Visible).ToList();
            return siblings.LastOrDefault() == node;
        }

        public static int getLastChildIndex(this TreeGridNode node)
        {
            if (node.Parent == null)
                return 0;

            var siblings = node.Parent.Children.Where(n => n.Visible).ToList();
            return siblings.LastOrDefault().Index;
        }

        public static TreeGridNode GetAncestor(this TreeGridNode node, int level)
        {
            TreeGridNode current = node;
            while (current != null && current.Level > level)
            {
                current = current.Parent;
            }
            return current;
        }
    }
}
