using System.Threading;
using System.Threading.Tasks;

namespace Hilma.Domain.Configuration
{
    public interface ITranslationProvider
    {
        Task<dynamic> GetDynamicObject(CancellationToken token);
    }
}
