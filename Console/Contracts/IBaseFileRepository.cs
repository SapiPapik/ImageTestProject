using System.Drawing;

namespace TestProject.Contracts
{
    public interface IBaseFileRepository
    {
        void SaveFile(string directory, Bitmap file);
        Bitmap LoadFile(string directory);
    }
}