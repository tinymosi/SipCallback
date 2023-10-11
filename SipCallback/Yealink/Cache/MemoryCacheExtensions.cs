using System.Diagnostics.CodeAnalysis;
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

	public static bool TryGetValue(this IMemoryCache cache, CallCacheEntry searchEntry, [NotNullWhen(true)] out CallCacheEntry? value)
	{
		if (cache.TryGetValue(searchEntry.Key, out object? result))
		{
			switch (result)
			{
				case null:
					value = null;
					return false;
				case CallCacheEntry entry:
					value = entry;
					return true;
			}
		}

		value = null;
		return false;
	}

	public static void RemoveCallEntry(this IMemoryCache cache, CallCacheEntry entry)
	{
		cache.Remove(entry.Key);
	}
}