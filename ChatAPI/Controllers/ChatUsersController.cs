using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/chatusers")]
    public class ChatUsersController : Controller
    {
        private readonly ChatContext _context;

        public ChatUsersController(ChatContext context)
        {
            _context = context;
        }

        // GET: api/ChatUsers
        [HttpGet]
        public IEnumerable<ChatUser> GetChatUsers()
        {
            return _context.ChatUsers;
        }

        // GET: api/ChatUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chatUser = await _context.ChatUsers.SingleOrDefaultAsync(m => m.ChatId == id);

            if (chatUser == null)
            {
                return NotFound();
            }

            return Ok(chatUser);
        }

        // POST: api/ChatUsers
        [HttpPost]
        public async Task<IActionResult> PostChatUser([FromBody] ChatUser chatUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ChatUsers.Add(chatUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatUserExists(chatUser.ChatId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChatUser", new { id = chatUser.ChatId }, chatUser);
        }

        // DELETE: api/ChatUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chatUser = await _context.ChatUsers.SingleOrDefaultAsync(m => m.ChatId == id);
            if (chatUser == null)
            {
                return NotFound();
            }

            _context.ChatUsers.Remove(chatUser);
            await _context.SaveChangesAsync();

            return Ok(chatUser);
        }

        private bool ChatUserExists(int id)
        {
            return _context.ChatUsers.Any(e => e.ChatId == id);
        }
    }
}