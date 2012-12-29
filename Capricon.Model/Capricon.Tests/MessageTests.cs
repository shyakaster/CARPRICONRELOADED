using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using Capricon.Model;
using Capricon.DataAccess;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Capricon.Tests
{
    [TestFixture]
    public class MessageTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }
        [Test]
        public void AddMessage()
        {
            using (CapriconContext context = new CapriconContext())
            {
                var newMessage = new Message()
                {
                    Body = "Testing...............",
                    Sent = DateTime.Now,
                    MessageStatus = MessageStatus.Sent
                };


                var messageRep = new MessageRepository(context);
                messageRep.Add(newMessage);

                try
                {
                    context.SaveChanges();
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
                var context1 = new CapriconContext();
                var repository = new MessageRepository(context1);
                var savedMessages = repository.GetAll().ToList();

                Assert.AreEqual(savedMessages.Count(), 2, "returns 20 records");
                var savedMessagesList = savedMessages;
                savedMessagesList.ForEach
                    (
                        m =>
                        {
                            Debug.WriteLine(m.MessageId + " - " + m.Body);
                        }
                    );
            };
        }

        [Test]
        public void UpdateMessage()
        {
            using (var uow = new CapriconContext())
            {
                var messageRep = new MessageRepository(uow);

                var existingMessages = messageRep.GetAll().ToList();

                var existingMessage = existingMessages.Find(m => m.MessageId == 3);

                Assert.IsNotNull(existingMessage);

                existingMessage.Body = "";
                existingMessage.Sent = DateTime.Now;
                existingMessage.MessageStatus = MessageStatus.Received;

                //check for validation rules
                //existingMessage.Body = "";
                //existingMessage.Sent = DateTime.Now;
                //existingMessage.MessageStatus = MessageStatus.Not_Specified;

                messageRep.Attach(existingMessage);
                uow.Entry(existingMessage).State = EntityState.Modified;

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
            };

            //retrieve saved object
            var uow1 = new CapriconContext();
            var repository = new MessageRepository(uow1);
            var savedMessages = repository.GetAll().ToList();

            Assert.AreEqual(savedMessages[0].MessageId, 1);
        }

        [Test]
        public void DeleteMessage()
        {
            using (var uow = new CapriconContext())
            {
                var messageRep = new MessageRepository(uow);
                var existingMessage = messageRep.Find(m => m.MessageId == 2).FirstOrDefault();

                Assert.IsNotNull(existingMessage);

                int messageId;
                if (existingMessage != null)
                {
                    messageId = existingMessage.MessageId;

                    //Delete message
                    messageRep.Delete(existingMessage);

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

                    Assert.IsNull(messageRep.Find(m => m.MessageId == messageId).FirstOrDefault());
                }
                else //no messages were selected
                    Assert.Fail("No message was selected");
            }
        }
    }
}
