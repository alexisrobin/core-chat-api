using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ChatId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public ChatUser ChatUser { get; set; }
    }
}
