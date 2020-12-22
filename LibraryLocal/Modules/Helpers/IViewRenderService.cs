using System.Threading.Tasks;

namespace LibraryLocal.Modules.Helpers
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}