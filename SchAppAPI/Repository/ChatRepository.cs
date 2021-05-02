
using Microsoft.EntityFrameworkCore;
using SchAppAPI.Contexts;
using SchAppAPI.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class ChatRepository: BaseRepository<Message>, IChatRepository
    {
        public readonly SchoolDbContext dbContext;
        public ChatRepository(SchoolDbContext context) : base(context)
        {
            this.dbContext = context;

        }

        public async Task<IEnumerable<Message>> GetActiveChats(Guid userId)
        {
             return this.dbContext.Messages.Where(msg => msg.ReceipientId == userId || msg.SenderId == userId)
                .GroupBy(msg => new Message { ReceipientId = msg.ReceipientId, SenderId = msg.SenderId } )
                .Select(msg => msg.Key).AsEnumerable<Message>();
        }
    }
}
