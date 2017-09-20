using System.Collections.Generic;
using System.Text;

namespace VirtCo.Providers.Stores
{
    public interface IExternalSpaStore
    {
        ExternalSPARecord GetRecord(string key);
        void AddRecord(ExternalSPARecord record);
        void RemoveRecord(string key);
        IEnumerable<ExternalSPARecord> GetRecords();
    }
}
