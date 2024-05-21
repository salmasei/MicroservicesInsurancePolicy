using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace InsuranceMicroserviceTests.Mocks
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;

    public class MockMemoryCache : IMemoryCache
    {
        private readonly Dictionary<object, object> _cache = new Dictionary<object, object>();

        public ICacheEntry CreateEntry(object key)
        {
            var entry = new MockCacheEntry(key, _cache);
            _cache[key] = entry;
            return entry;
        }

        public void Dispose() { }

        public void Remove(object key)
        {
            _cache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                value = ((MockCacheEntry)entry).Value;
                return true;
            }

            value = null;
            return false;
        }

        private class MockCacheEntry : ICacheEntry
        {
            private readonly Dictionary<object, object> _cache;

            public MockCacheEntry(object key, Dictionary<object, object> cache)
            {
                Key = key;
                _cache = cache;
            }

            public void Dispose() { }

            public object Key { get; }

            private object _value;
            public object Value
            {
                get => _value;
                set => _value = value;
            }

            public DateTimeOffset? AbsoluteExpiration { get; set; }

            public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

            public TimeSpan? SlidingExpiration { get; set; }

            public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();

            public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; } = new List<PostEvictionCallbackRegistration>();

            public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

            public long? Size { get; set; }
        }
    }

}
