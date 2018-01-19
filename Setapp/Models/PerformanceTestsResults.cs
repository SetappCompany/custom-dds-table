using Setapp.Business;

namespace Setapp.Models
{
    public class PerformanceTestsResults
    {
        public readonly TimeComparision FillingData;
        public readonly TimeComparision RetrievingData;
        public readonly TimeComparision RetrievingDataWithoutFakeData;

        public PerformanceTestsResults(TimeComparision fillingData, TimeComparision retrievingData, TimeComparision retrievingDataWithoutFakeData)
        {
            FillingData = fillingData;
            RetrievingData = retrievingData;
            RetrievingDataWithoutFakeData = retrievingDataWithoutFakeData;
        }
    }
}