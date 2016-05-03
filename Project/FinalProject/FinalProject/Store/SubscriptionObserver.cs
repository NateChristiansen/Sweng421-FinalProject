using System.Collections.Generic;

namespace FinalProject
{
    public class SubscriptionObserver
    {
        private readonly Dictionary<string, List<Member>> _subscribers = new Dictionary<string, List<Member>>();
        public void NotifySubscribers(string title)
        {
            if (!_subscribers.ContainsKey(title)) return;
            _subscribers[title].ForEach(s =>
            {
                if (_subscribers.ContainsKey(title))
                    s.Notify(title);
            });
        }

        public void AddSubscription(string title, Member member)
        {
            if (_subscribers.ContainsKey(title))
            {
                _subscribers[title].Add(member);
            }
            else
            {
                var list = new List<Member> {member};
                _subscribers.Add(title, list);
            }
        }
    }
}