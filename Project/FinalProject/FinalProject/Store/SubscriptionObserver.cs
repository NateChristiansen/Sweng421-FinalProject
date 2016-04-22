using System.Collections.Generic;

namespace FinalProject
{
    public class SubscriptionObserver
    {
        private readonly Dictionary<string, List<IMember>> _subscribers = new Dictionary<string, List<IMember>>();
        public void NotifySubscribers(string title)
        {
            _subscribers[title].ForEach(s => s.Notify(title));
        }

        public void AddSubscription(string title, IMember member)
        {
            if (_subscribers.ContainsKey(title))
            {
                _subscribers[title].Add(member);
            }
            else
            {
                var list = new List<IMember> {member};
                _subscribers.Add(title, list);
            }
        }
    }
}