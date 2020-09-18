using System.IO;

namespace FitReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var fileName = args[0];
                if (File.Exists(fileName))
                {
                    EndianBinaryReader binaryReader = new EndianBinaryReader(File.Open(fileName, FileMode.Open));
                    Fit fit = new Fit(binaryReader);
                }
            }
        }
    }
}
