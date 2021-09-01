using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleSaveHandler
    {
        
        Task<string> SaveDoodleDraw(IDoodleDrawInteraction doodleDrawInteraction);

    }

}