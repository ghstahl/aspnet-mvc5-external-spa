using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtCo.Providers.Stores
{
    public class InMemoryRazorLocationStore : IRazorLocationStore
    {
        private Dictionary<string, RazorLocation> _records;

        protected Dictionary<string, RazorLocation> Records
        {
            get => _records ?? (_records = new Dictionary<string, RazorLocation>());
            set => _records = value;
        }
        public async Task InsertAsync(RazorLocation document)
        {
            lock (Records)
            {
                Records[document.Location] = document;
            }
        }
        public void Insert(RazorLocation document)
        {
            lock (Records)
            {
                Records[document.Location] = document;
            }
        }
        public void Insert(List<RazorLocation> documents)
        {
            lock (Records)
            {
                foreach (var doc in documents)
                {
                    Records[doc.Location] = doc;
                }
            }
        }
        public async Task UpdateAsync(RazorLocation document)
        {
            lock (Records)
            {
                Records[document.Location] = document;
            }
        }

        public async Task DeleteAsync(RazorLocationQuery query)
        {
            lock (Records)
            {
                if (Records.ContainsKey(query.Location))
                {
                    Records.Remove(query.Location);
                }
            }
        }

        public async Task<RazorLocation> FetchAsync(RazorLocationQuery query)
        {
            lock (Records)
            {
                if (Records.ContainsKey(query.Location))
                {
                    return Records[query.Location];
                }
                return null;
            }
        }

        public async Task<IEnumerable<RazorLocation>> FetchAllAsync()
        {
            lock (Records)
            {
                var a = Records.ToList();
                var query = from item in a
                    let c = item.Value
                    select c;
                var result = query.ToList();
                return result;
            }
        }
    }
}