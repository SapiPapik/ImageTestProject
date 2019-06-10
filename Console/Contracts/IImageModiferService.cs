using TestProject.Models;

namespace TestProject.Contracts
{
    public interface IImageModiferService
    {
        void DrawStoryboard(ImageRow row, int width);
        void DrawStoryboard(ImageColumn column, int width);
    }
}