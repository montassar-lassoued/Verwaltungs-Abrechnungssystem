using System.Drawing;
using System.IO;
using System.Reflection;

namespace MyControls
{
    public static class ResourceLoader
    {
        public static Image LoadImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
