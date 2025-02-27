using System.Collections.Concurrent;
using System;

public class BlockingService
{
    private readonly ConcurrentDictionary<string, DateTime> _temporaryBlocks = new();
    public bool TemporarilyBlockCountry(string countryCode, int durationMinutes)
    {
        return _temporaryBlocks.TryAdd(countryCode, DateTime.UtcNow.AddMinutes(durationMinutes));
    }
    public void CleanupExpiredBlocks()
    {
        var now = DateTime.UtcNow;
        foreach (var key in _temporaryBlocks.Keys)
        {
            if (_temporaryBlocks[key] <= now)
                _temporaryBlocks.TryRemove(key, out _);
        }
    }
}
