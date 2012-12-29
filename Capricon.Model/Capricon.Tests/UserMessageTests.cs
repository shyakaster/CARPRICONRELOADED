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
    public class UserMessageTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }

        [Test]
        public void AddUserMessage()
        {
            using (var uow = new CapriconContext())
            {
                //retreive an existing user
                var userRepository = new UserRepository(uow);
                var existingUser = userRepository.GetAll().FirstOrDefault();

                Assert.IsNotNull(existingUser);

                //retreive an existing message
                var messageRepository = new MessageRepository(uow);
                var existingMessage = messageRepository.GetAll().FirstOrDefault();

                Assert.IsNotNull(existingMessage);

                //create new user messsage
                var newUserMessage = new UserMessage()
                {
                    User = existingUser,
                    Message = existingMessage
                };

                //add the new user message to the repository
                var userMessageRepository = new UserMessageRepository(uow);
                userMessageRepository.Add(newUserMessage);

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
                var repository = new UserMessageRepository(uow1);
                var savedUserMessages = repository.GetAll().ToList();

                Assert.AreEqual(savedUserMessages[0].User.FirstName, existingUser.FirstName = "james");
                Assert.AreEqual(savedUserMessages[0].Message.MessageId, existingMessage.MessageId = 1);
            };
        }

        //Attach/modify an existing user message
        [Test]
        public void UpdateUserMessage()
        {
            using (var uow = new CapriconContext())
            {
                var userMessageRepository = new UserMessageRepository(uow);

                var existingUserMessage = userMessageRepository.Find(um => um.Id == 1).FirstOrDefault();


                Assert.IsNotNull(existingUserMessage);

                //retreive an existing user
                var userRepository = new UserRepository(uow);
                var existingUser = userRepository.Find(u => u.UserId == 1).FirstOrDefault();

                Assert.IsNotNull(existingUser);

                //retreive an existing message
                var messageRepository = new MessageRepository(uow);
                var existingMessage = messageRepository.Find(m => m.MessageId == 1).FirstOrDefault();

                Assert.IsNotNull(existingMessage);

                //edit an existing user message
                existingUserMessage.User = existingUser;
                existingUserMessage.Message = existingMessage;

                userMessageRepository.Attach(existingUserMessage);
                uow.Entry(existingUserMessage).State = EntityState.Modified;

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
                var repository = new UserMessageRepository(uow1);
                var savedUserMessages = repository.GetAll().ToList();

                Assert.AreEqual(savedUserMessages[0].User, existingUser);
                Assert.AreEqual(savedUserMessages[0].Message, existingMessage);
            };
        }

        //delete an existing user message
        [Test]
        public void DeleteUserMessage()
        {
            using (var uow = new CapriconContext())
            {
                var userMessageRepository = new UserMessageRepository(uow);
                var existingUserMessage = userMessageRepository.Find(um => um.Id == 2).FirstOrDefault();

                Assert.IsNotNull(existingUserMessage);

                int id;
                if (existingUserMessage != null)
                {
                    id = existingUserMessage.Id;

                    //Delete selected user message
                    userMessageRepository.Delete(existingUserMessage);

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

                    Assert.IsNull(userMessageRepository.Find(um => um.Id == id).FirstOrDefault());
                }
                else //no user messages were selected
                    Assert.Fail("No user message was selected");
            }
        }
    }
}


