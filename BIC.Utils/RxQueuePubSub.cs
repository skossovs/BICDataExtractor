using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://michaelscodingspot.com/c-job-queues-with-reactive-extensions-and-channels/
/// </summary>
namespace BIC.Utils
{
    public interface IRxQueueJob
    {
    }

    public class RxQueuePubSub : IDisposable
    {
        Subject<IRxQueueJob> _jobs = new Subject<IRxQueueJob>();
        private IConnectableObservable<IRxQueueJob> _connectableObservable;

        public RxQueuePubSub()
        {
            _connectableObservable = _jobs.ObserveOn(Scheduler.Default).Publish();
            _connectableObservable.Connect();
        }

        public void Dispose()
        {
            _jobs.Dispose();
        }

        public void Enqueue(IRxQueueJob job)
        {
            _jobs.OnNext(job);
        }

        public void RegisterHandler<T>(Action<T> handleAction) where T : IRxQueueJob
        {
            _connectableObservable.OfType<T>().Subscribe(handleAction);
        }

    }
}
