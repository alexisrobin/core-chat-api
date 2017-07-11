using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }

        [JsonIgnore]
        public List<ChatUser> ChatUsers { get; set; }
    }
}
