using System.Collections.Generic;

namespace LibraryLocal.Modules
{
    public class DirectoryViewModel : IPageTitleContainer
    {
        public string Title { get; init; }
        
        public string BackLink { get; init; }
        
        public IReadOnlyList<ItemViewModel> Items { get; init; }
    }
}