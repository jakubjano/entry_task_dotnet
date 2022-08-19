using entry_task.Models;

namespace entry_task.Repository;

public class EventResult
{
    public string Name { get; set; }
    public int Count { get; set; }
}

public class EventRepo
{
    private readonly Dictionary<string, List<Event>> _events = new();

    public Event Create(string name)
    {
        var e = new Event
        {
            Name = name,
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
        };
        if (_events.ContainsKey(name))
        {
            _events[name].Add(e);
        }
        else
        {
            _events.Add(name, new List<Event>());
            _events[name].Add(e);
        }

        return e;
    }
    public  List<EventResult> GetAggregatedEvents(string name, int from, int to, int interval)
    {
        var eventsInInterval = new List<EventResult>();
        if (!_events.ContainsKey(name))
        {
            return new List<EventResult>();
        }
        var eventsFromTo = _events[name].Where(e => e.CreatedAt >= from & e.CreatedAt < to).ToList();
        for (var i = from; i < to; i = i + interval)
        {
            if (i + interval > to)
            {
                interval = to - i;
            }
            var counter = eventsFromTo.Count(e => e.CreatedAt >= i & e.CreatedAt < i + interval);
            eventsInInterval.Add(new EventResult
            {
                Name = name,
                Count = counter
            });
        }
        return eventsInInterval;
    }
}