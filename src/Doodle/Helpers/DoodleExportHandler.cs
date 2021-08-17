using System.Threading.Tasks;

namespace Doodle.Helpers
{

    public delegate void OnExportImageBase64Handler(object sender, string base64Image);

    public class DoodleExportHandler : Abstractions.Interfaces.IDoodleExportHandler
    {

        public event OnExportImageBase64Handler OnExportImageBase64;

        public Task ExportImageBase64(string base64ImageData)
        {
            OnExportImageBase64?.Invoke(this, base64ImageData);
            return Task.CompletedTask;
        }
    }

}