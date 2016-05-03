using System.Collections.Generic;

namespace FinalProject
{
    public class SubscriptionObserver
    {
        private readonly Dictionary<IBook, List<Member>> _subscribers = new Dictionary<IBook, List<Member>>();
        public void NotifySubscribers(IBook book)
        {
            if (!_subscribers.ContainsKey(book)) return;
            _subscribers[book].ForEach(s =>
            {
                s.Notify(book);
            });
        }

        public void AddSubscription(IBook book, Member member)
        {
            if (_subscribers.ContainsKey(book))
            {
                _subscribers[book].Add(member);
            }
            else
            {
                var list = new List<Member> {member};
                _subscribers.Add(book, list);
            }
        }
    }
}