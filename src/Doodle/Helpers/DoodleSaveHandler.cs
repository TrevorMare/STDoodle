using System.Threading.Tasks;
using Doodle.Abstractions.Interfaces;

namespace Doodle.Helpers
{

    public delegate void OnSaveDoodleDrawHandler(object sender, string jsonData);

    public class DoodleSaveHandler : Abstractions.Interfaces.IDoodleSaveHandler
    {
        public event OnSaveDoodleDrawHandler OnSaveDoodleDraw;

        public async Task<string> SaveDoodleDraw(IDoodleDrawInteraction doodleDrawInteraction)
        {
            string result = null;
            if (doodleDrawInteraction?.DoodleStateManager != null)
            {
                result = await doodleDrawInteraction.DoodleStateManager.SaveCurrentState();
                this.OnSaveDoodleDraw?.Invoke(this, result);
            }
            return result;
        }

    }

}