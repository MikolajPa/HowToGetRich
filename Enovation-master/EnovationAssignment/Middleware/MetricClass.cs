namespace EnovationAssignment.Middleware
{
    public static class MetricClass
    {
        private static Dictionary<DateOnly, int> Success = new Dictionary<DateOnly, int>();
        private static Dictionary<DateOnly, int> Fail = new Dictionary<DateOnly, int>();
        public static void AddSuccess()
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            if(Success.TryGetValue(date, out _))
            {
                Success[date] += 1;
            }
            else
            {
                Success.Add(date, 1);
            }

        }
        public static void AddFail()
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            if (Fail.TryGetValue(date, out _))
            {
                Fail[date] += 1;
            }
            else
            {
                Fail.Add(date, 1);
            }

        }
    }
}
