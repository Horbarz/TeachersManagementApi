
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

        public async Task<IEnumerable<Message>> GetActiveChats(string userId)
        {
            var source = this.dbContext.Messages
                .Include(s => s.Sender)
                .Include(r => r.Receipient)
                .Where(msg => msg.ReceipientId == userId || msg.SenderId == userId)
                .Select(
                    m => new
                    {
                        Key = new
                        {
                            m.ReceipientId, m.SenderId
                        },
                        Message = m
                    }
                );
              var subquery =  source.Select(e => e.Key).Distinct()
                .SelectMany(
                    key => source.Where( a => a.Key.ReceipientId == key.ReceipientId && a.Key.SenderId == key.SenderId)
                        .Select( e => e.Message)
                        .OrderByDescending(m => m.CreatedOn)
                        .Take(1)
                 );
            var sql = subquery.ToQueryString();
            var result = subquery.AsEnumerable<Message>();
            return result;
        }
    }
}
