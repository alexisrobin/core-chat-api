using ChatAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private readonly ChatContext _context;

        public ChatHub(ChatContext context)
        {
            _context = context;
        }

        public override Task OnConnected()
        {
            /**
            //Get the UserId
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                }
            }
            */

            return base.OnConnected();
        }

        public void ConnectionToChats(int userId)
        {
            var chats = _context.ChatUsers.Include(e => e.Chat)
                   .Where(e => e.UserId == userId)
                   .Select(e => e.Chat);

            foreach (var chat in chats)
            {
                Groups.Add(Context.ConnectionId, chat.Id.ToString());
            }
        }
    }
}
