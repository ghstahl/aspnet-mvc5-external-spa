using System.Collections.Generic;
using System.Linq;

namespace VirtCo.Providers.Stores
{
    public class InMemoryExternalSpaStore : IExternalSpaStore
    {
        private Dictionary<string, ExternalSPARecord> _records;

        private Dictionary<string, ExternalSPARecord> Records
        {
            get => _records ?? (_records = new Dictionary<string, ExternalSPARecord>());
            set => _records = value;
        }

        public ExternalSPARecord GetRecord(string key)
        {
            var sKey = key.ToLower();
            if (Records.ContainsKey(sKey))
            {
                return Records[sKey];
            }
            return null;
        }

        public void AddRecord(ExternalSPARecord record)
        {
            var sKey = record.Key.ToLower();

            Records[sKey] = record;
        }

        public void RemoveRecord(string key)
        {
            var sKey = key.ToLower();
            if (Records.ContainsKey(sKey))
            {
                Records.Remove(sKey);
            }
        }

        public IEnumerable<ExternalSPARecord> GetRecords()
        {
            var query = from item in Records
                let c = item.Value
                select c;
            return query;
        }
    }
}