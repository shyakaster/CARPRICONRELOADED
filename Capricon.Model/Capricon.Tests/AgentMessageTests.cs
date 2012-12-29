using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using Capricon.Model;
using SchoolAdmin.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Capricon.DataAccess;

namespace SchoolAdmin.Data.Tests
{
    [TestFixture]
    public class AgentMessageTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }

        [Test]
        public void AddAgentMessage()
        {
            using (var uow = new CapriconContext())
            {
                //retreive an existing agent
                var agentRepository = new AgentRepository(uow);
                var existingAgent = agentRepository.GetAll().FirstOrDefault();

                Assert.IsNotNull(existingAgent);

                //retreive an existing message
                var messageRepository = new MessageRepository(uow);
                var existingMessage = messageRepository.GetAll().LastOrDefault();

                Assert.IsNotNull(existingMessage);

                //create new agent messsage
                var newAgentMessage = new AgentMessage()
                {
                    Agent = existingAgent,
                    Message = existingMessage
                };

                //add the new agent message to the repository
                var agentMessageRepository = new AgentMessageRepository(uow);
                agentMessageRepository.Add(newAgentMessage);

                try
                {
                    uow.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    //Retrieve validation errors
                    ex.EntityValidationErrors.ToList().ForEach
                    (
                        v =>
                        {
                            v.ValidationErrors.ToList().ForEach
                                (
                                    e =>
                                    {
                                        System.Diagnostics.Debug.WriteLine(e.ErrorMessage);
                                    }
                                );
                        }
                    );

                    Assert.Fail("Test failed");
                }

                //retrieve saved object
                var uow1 = new CapriconContext();
                var repository = new AgentMessageRepository(uow1);
                var savedAgentMessages = repository.GetAll().ToList();

                Assert.AreEqual(savedAgentMessages[0].Agent.FirstName, existingAgent.FirstName = "Blaise");
                Assert.AreEqual(savedAgentMessages[0].Message.MessageId, existingMessage.MessageId = 1);
            };
        }

        //Attach/modify an existing agent message
        [Test]
        public void UpdateAgentMessage()
        {
            using (var uow = new CapriconContext())
            {
                var agentMessageRepository = new AgentMessageRepository(uow);

                var existingAgentMessage = agentMessageRepository.Find(am => am.Id == 1).FirstOrDefault();


                Assert.IsNotNull(existingAgentMessage);

                //retreive an existing agent
                var agentRepository = new AgentRepository(uow);
                var existingAgent = agentRepository.Find(a => a.AgentId == 1).FirstOrDefault();

                Assert.IsNotNull(existingAgent);

                //retreive an existing message
                var messageRepository = new MessageRepository(uow);
                var existingMessage = messageRepository.Find(m => m.MessageId == 1).FirstOrDefault();

                Assert.IsNotNull(existingMessage);

                //edit an existing agent message
                existingAgentMessage.Agent = existingAgent;
                existingAgentMessage.Message = existingMessage;

                agentMessageRepository.Attach(existingAgentMessage);
                uow.Entry(existingAgentMessage).State = EntityState.Modified;

                try
                {
                    uow.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    //Retrieve validation errors
                    ex.EntityValidationErrors.ToList().ForEach
                    (
                        v =>
                        {
                            v.ValidationErrors.ToList().ForEach
                                (
                                    e =>
                                    {
                                        System.Diagnostics.Debug.WriteLine(e.ErrorMessage);
                                    }
                                );
                        }
                    );
                    Assert.Fail("Test failed");
                }

                //retrieve saved object
                var uow1 = new CapriconContext();
                var repository = new AgentMessageRepository(uow1);
                var savedAgentMessages = repository.GetAll().ToList();

                Assert.AreEqual(savedAgentMessages[0].Agent, existingAgent);
                Assert.AreEqual(savedAgentMessages[0].Message, existingMessage);
            };
        }

        //delete an existing agent message
        [Test]
        public void DeleteAgentMessage()
        {
            using (var uow = new CapriconContext())
            {
                var agentMessageRepository = new AgentMessageRepository(uow);
                var existingAgentMessage = agentMessageRepository.Find(am => am.Id == 2).FirstOrDefault();

                Assert.IsNotNull(existingAgentMessage);

                int id;
                if (existingAgentMessage != null)
                {
                    id = existingAgentMessage.Id;

                    //Delete selected agent message
                    agentMessageRepository.Delete(existingAgentMessage);

                    try
                    {
                        uow.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        //Retrieve validation errors
                        ex.EntityValidationErrors.ToList().ForEach
                        (
                            v =>
                            {
                                v.ValidationErrors.ToList().ForEach
                                    (
                                        e =>
                                        {
                                            System.Diagnostics.Debug.WriteLine(e.ErrorMessage);
                                        }
                                    );
                            }
                        );
                        Assert.Fail("Test failed");
                    }

                    Assert.IsNull(agentMessageRepository.Find(am => am.Id == id).FirstOrDefault());
                }
                else //no agent messages were selected
                    Assert.Fail("No agent message was selected");
            }
        }
    }
}


