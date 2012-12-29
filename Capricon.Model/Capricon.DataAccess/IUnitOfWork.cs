using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capricon.Model;

namespace Capricon.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get;  }
        IRepository<Message> Messages { get;  }
        IRepository<Agent> Agents { get; }
        IRepository<Payment> Payments { get; }
        IRepository<UserMessage> UserMessages { get; }
        IRepository<AgentMessage> AgentMessages { get; }
        void Commit();
    }
}
