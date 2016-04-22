using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    public class SubscriptionObserver
    {
        private Dictionary<string, List<IMember>> subscribers;
        public void NotifySubscribers(string title)
        {
            subscribers[title].ForEach(s => s.Notify(title));
        }

        public void AddSubscription(string title, IMember member)
        {
            if (subscribers.ContainsKey(title))
            {
                subscribers[title].Add(member);
            }
            else
            {
                var list = new List<IMember>();
                list.Add(member);
                subscribers.Add(title, list);
            }
        }
    }
}