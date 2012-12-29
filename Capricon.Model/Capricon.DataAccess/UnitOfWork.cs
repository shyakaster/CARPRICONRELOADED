using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Capricon.Model;

namespace Capricon.DataAccess
{
    /// <summary>
    /// This uow class makes sure that when multiple repositories are used they share a single database context
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private CapriconContext context = new CapriconContext();
        private Repository<User> userRepository;
        private Repository<Message> messageRepository;
        private Repository<Agent> agentRepository;
        private Repository<Payment> paymentRepository;
        private Repository<UserMessage> userMessageRepository;
        private Repository<AgentMessage> agentMessageRepository;

        public Repository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(context);
                }
                return userRepository;
            }
        }

        public Repository<Message> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new Repository<Message>(context);
                }
                return messageRepository;
            }
        }

        public Repository<Payment> PaymentRepository
        {
            get
            {
                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new Repository<Payment>(context);
                }
                return paymentRepository;
            }
        }

        public Repository<Agent> AgentRepository
        {
            get
            {
                if (this.agentRepository == null)
                {
                    this.agentRepository = new Repository<Agent>(context);
                }
                return agentRepository;
            }
        }

        public Repository<AgentMessage> AgentMessageRepository
        {
            get
            {
                if (this.agentMessageRepository == null)
                {
                    this.agentMessageRepository = new Repository<AgentMessage>(context);
                }
                return agentMessageRepository;
            }
        }

        public Repository<UserMessage> UserMessageRepository
        {
            get
            {
                if (this.userMessageRepository == null)
                {
                    this.userMessageRepository = new Repository<UserMessage>(context);
                }
                return userMessageRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
