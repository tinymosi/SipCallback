using Microsoft.Extensions.Caching.Memory;

namespace SipCallback.Yealink.Cache;

public static class MemoryCacheExtensions
{
	public static CallCacheEntry Set(this IMemoryCache cache, CallCacheEntry entry, TimeSpan absoluteExpirationRelativeToNow)
	{
		ArgumentNullException.ThrowIfNull(cache);
		ArgumentNullException.ThrowIfNull(entry);

		return cache.Set(entry.Key, entry, absoluteExpirationRelativeToNow);
	}

	public static bool TryGetValue(this IMemoryCache cache, CallCacheEntry searchEntry, out CallCacheEntry? value)
	{
		if (cache.TryGetValue(searchEntry.Key, out object? result))
			switch (result)
			{
				case null:
					value = default;
					return true;
				case CallCacheEntry entry:
					value = entry;
					return true;
			}

		value = default;
		return false;
	}

	public static void Remove(this IMemoryCache cache, CallCacheEntry entry)
	{
		cache.Remove(entry.Key);
	}
}