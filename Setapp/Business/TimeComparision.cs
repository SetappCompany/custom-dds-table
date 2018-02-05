namespace Setapp.Business
{
    public class TimeComparision
    {
        public readonly double DefaultTableExecutionTime;
        public readonly double CustomTableExecutionTime;

        public TimeComparision(double defaultTableExecutionTime, double customTableExecutionTime)
        {
            DefaultTableExecutionTime = defaultTableExecutionTime;
            CustomTableExecutionTime = customTableExecutionTime;
        }
    }
}