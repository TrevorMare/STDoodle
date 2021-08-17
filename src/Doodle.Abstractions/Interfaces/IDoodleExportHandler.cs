using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleExportHandler
    {
        Task ExportImageBase64(string base64ImageData);
    }

}