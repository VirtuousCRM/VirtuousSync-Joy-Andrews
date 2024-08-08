using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Models
{
    public class ContactQueryRequest
    {
        public ContactQueryRequest()
        {
            Groups = new List<Group>();
            SortBy = "Id";
            Descending = false;
        }

        public List<Group> Groups { get; set; }

        public string SortBy { get; set; } = string.Empty;

        public bool Descending { get; set; }
    }

    public class Group
    {
        public List<Condition> Conditions { get; set; }

    }

    public class Condition
    {
        public string Parameter { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string SecondaryValue { get; set; } = string.Empty;
        public List<string> Values { get; set; }

    }
}
