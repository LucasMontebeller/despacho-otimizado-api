using System.Collections.Concurrent;

namespace Despacho.Otimizado.Infra.Data.Cache
{
    public class RecordCache<T>
    {
        private readonly int Size;
        private readonly ConcurrentQueue<T> Cache;

        public RecordCache(int maxSize)
        {
            Size = maxSize;
            Cache = new ConcurrentQueue<T>();
        }

        public void Add(T record)
        {
            if (Cache.Count >= Size)
                Cache.TryDequeue(out _);

            Cache.Enqueue(record);
        }

        public List<T> GetRecords()
        {
            return Cache.ToList();
        }
        
        public bool ContainsRecord(T record)
        {
            return Cache.Contains(record);
        }
    }
}