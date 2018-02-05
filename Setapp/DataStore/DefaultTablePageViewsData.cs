using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Setapp.DataStore
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class DefaultTablePageViewsData
    {
        public Identity Id { get; set; }

        [EPiServerDataIndex]
        public int PageId { get; set; }

        public int ViewsAmount { get; set; }
    }
}