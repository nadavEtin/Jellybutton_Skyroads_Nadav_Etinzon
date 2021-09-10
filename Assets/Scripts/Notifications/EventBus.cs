using System;
using System.Collections.Generic;

public class EventBus : NativeSingleton<EventBus>
{
    private Dictionary<GameplayEventType, List<Action<EventParams>>> _subscription = new Dictionary<GameplayEventType, List<Action<EventParams>>>();

    public void Subscribe(GameplayEventType eventType, Action<EventParams> handler)
    {
        if(!_subscription.ContainsKey(eventType))
        {
            _subscription.Add(eventType, new List<Action<EventParams>>());
        }

        _subscription[eventType].Add(handler);
    }

    public void Unsubscribe(GameplayEventType eventType, Action<EventParams> handler)
    {
        if (!_subscription.ContainsKey(eventType))
        {
            return;
        }

        var handlesList = _subscription[eventType];
        handlesList.Remove(handler);
    }

    public void Publish(GameplayEventType eventType, EventParams eventParams)
    {
        if (!_subscription.ContainsKey(eventType))
        {
            return;
        }

        var handlers = _subscription[eventType];
        for (int i = 0; i < handlers.Count; i++)
        {
            var handler = handlers[i];
            handler?.Invoke(eventParams);
        }
    }
}
