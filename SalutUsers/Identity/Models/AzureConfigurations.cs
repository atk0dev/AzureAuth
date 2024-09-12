using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AzureConfiguration
    {
        public string Instance { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GraphScope { get; set; }
        public string ObjectId { get; set; }
        public string InvitationRedirectUrl { get; set; }
        public string Issuer { get; set; }
    }
}
