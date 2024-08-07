using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync
{
    public class AbbreviatedContact
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ContactType { get; set; } = string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
