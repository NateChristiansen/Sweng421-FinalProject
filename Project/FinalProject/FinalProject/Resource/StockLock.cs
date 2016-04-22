using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FinalProject
{
    public class StockLock
    {
        private int _waiting;
        private int _outstanding;
        private readonly List<Thread> _waitingList = new List<Thread>();
        private Thread _writeThread;
        private object _lockObject = new object();

        public void ReadLock()
        {
            if (_writeThread == null)
            {
                _waiting++;
                while (_writeThread == null)
                    Monitor.Wait(this);
            }
            _outstanding++;
        }

        public void WriteLock()
        {
            var thread = Thread.CurrentThread;
            lock (this)
            {
                if (_writeThread == null)
                {
                    _writeThread = thread;
                    return;
                }
                _waitingList.Add(thread);
            }
            lock (_lockObject)
            {
                while (thread != _writeThread)
                    Monitor.Wait(thread);
            }
            lock (this)
            {
                _waitingList.Remove(thread);
            }
        }

        public void Done()
        {
            if (_outstanding > 0)
            {
                _outstanding--;
                if (!_waitingList.Any()) return;
                _writeThread = _waitingList[0];
                Monitor.PulseAll(_lockObject);
            }
            else if (Thread.CurrentThread == _writeThread)
            {
                if (_waitingList.Any())
                {
                    _writeThread = _waitingList[0];
                    Monitor.PulseAll(_lockObject);
                }
                else
                {
                    _writeThread = null;
                    if (_waiting > 0)
                        Monitor.PulseAll(this);
                }
            }
            else
            {
                const string msg = "Thread has no lock.";
                throw new Exception(msg);
            }
        }
    }
}