using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace Setapp.DataStore
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class FakeData
    {
        public Identity Id { get; set; }

        public int FakeInt { get; set; }

        public string FakeString { get; set; }
    }
}