using System;

namespace KwestKarz.Entities
{
    public class ApiClientClaim
    {
        public int Id { get; set; }
        public ApiClientRole Role { get; set; }

        public Guid ApiClientKeyId { get; set; }
        public ApiClientKey ApiClientKey { get; set; }
    }
}
