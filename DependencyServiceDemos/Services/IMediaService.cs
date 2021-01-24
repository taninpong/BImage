using System.Threading.Tasks;

namespace DependencyServiceDemos.Droid
{
    public interface IMediaService
    {
        Task SaveImageFromByte();
    }
}