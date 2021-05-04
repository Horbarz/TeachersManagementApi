using SchAppAPI.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public interface IChatRepository:IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetActiveChats(string userId);
    }
}
