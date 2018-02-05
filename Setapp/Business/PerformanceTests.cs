using System;
using System.Diagnostics;
using System.Linq;
using EPiServer.Data.Dynamic;
using Setapp.DataStore;
using Setapp.Models;

namespace Setapp.Business
{
    public class PerformanceTests
    {
        private const int RowsInTableCount = 50000;

        private readonly Type _defaultTableStoreType = typeof(DefaultTablePageViewsData);
        private readonly Type _customTableStoreType = typeof(CustomTablePageViewsData);
        private readonly Type _fakeDataStoreType = typeof(FakeData);

        public PerformanceTestsResults RunTests()
        {
            TimeComparision fillDataTimeComparision = FillTablesWithData();
            TimeComparision retrievingDataTimeComparision = TestRetrievingData();

            _fakeDataStoreType.GetOrCreateStore().DeleteAll();

            TimeComparision retrievingDataTimeComparisionWithoutFakeData = TestRetrievingData();

            return new PerformanceTestsResults(fillDataTimeComparision, retrievingDataTimeComparision, retrievingDataTimeComparisionWithoutFakeData);
        }

        private TimeComparision FillTablesWithData()
        {
            const int iterationsCount = 10;
            double defaultTableTime = 0,
                customTableTime = 0;

            _fakeDataStoreType.GetOrCreateStore().DeleteAll();
            FillFakeDataStore();

            for (var i = 0; i < iterationsCount; i++)
            {
                _defaultTableStoreType.GetOrCreateStore().DeleteAll();
                _customTableStoreType.GetOrCreateStore().DeleteAll();

                var defaultTableWatch = Stopwatch.StartNew();
                FillDefaultTablePageViewStoreForType(_defaultTableStoreType);
                defaultTableWatch.Stop();
                defaultTableTime += defaultTableWatch.Elapsed.TotalMilliseconds;

                var customTableWatch = Stopwatch.StartNew();
                FillCustomTablePageViewStoreForType(_customTableStoreType);
                customTableWatch.Stop();
                customTableTime += customTableWatch.Elapsed.TotalMilliseconds;
            }

            double defaultTableAvg = defaultTableTime / iterationsCount;
            double customTableAvg = customTableTime / iterationsCount;

            return new TimeComparision(defaultTableAvg, customTableAvg);
        }

        private TimeComparision TestRetrievingData()
        {
            const int iterationsCount = 1000;
            double defaultTableTime = 0,
                customTableTime = 0;

            for (var i = 0; i < iterationsCount; i++)
            {
                var defaultTableWatch = Stopwatch.StartNew();
                _defaultTableStoreType.GetOrCreateStore()
                    .Items<DefaultTablePageViewsData>()
                    .FirstOrDefault(item => item.ViewsAmount == 25000);
                defaultTableWatch.Stop();
                defaultTableTime += defaultTableWatch.Elapsed.TotalMilliseconds;

                var customTableWatch = Stopwatch.StartNew();
                _customTableStoreType.GetOrCreateStore()
                    .Items<CustomTablePageViewsData>()
                    .FirstOrDefault(item => item.ViewsAmount == 25000);
                customTableWatch.Stop();
                customTableTime += customTableWatch.Elapsed.TotalMilliseconds;
            }

            double defaultTableAvg = defaultTableTime / iterationsCount;
            double customTableAvg = customTableTime / iterationsCount;

            return new TimeComparision(defaultTableAvg, customTableAvg);
        }

        private void FillFakeDataStore()
        {
            DynamicDataStore store = _fakeDataStoreType.GetOrCreateStore();

            for (int i = 0; i < RowsInTableCount; i++)
            {
                store.Save(new FakeData
                {
                    FakeInt = i,
                    FakeString = i.ToString()
                });
            }
        }

        private void FillDefaultTablePageViewStoreForType(Type storeType)
        {
            DynamicDataStore store = storeType.GetOrCreateStore();

            for (int i = 0; i < RowsInTableCount; i++)
            {
                store.Save(new DefaultTablePageViewsData
                {
                    PageId = i,
                    ViewsAmount = i
                });
            }
        }

        private void FillCustomTablePageViewStoreForType(Type storeType)
        {
            DynamicDataStore store = storeType.GetOrCreateStore();

            for (int i = 0; i < RowsInTableCount; i++)
            {
                store.Save(new CustomTablePageViewsData
                {
                    PageId = i,
                    ViewsAmount = i
                });
            }
        }
    }
}