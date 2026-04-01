using System;

namespace EcommerceDomainDrivenDesign.WebApp.BackgroundServices
{
    public class MessageProcessorTaskOptions
    {
        public MessageProcessorTaskOptions(TimeSpan interval, int batchSize)
        {
            Interval = interval;
            BatchSize = batchSize;
        }

        public TimeSpan Interval { get; }
        public int BatchSize { get; }
    }
}
