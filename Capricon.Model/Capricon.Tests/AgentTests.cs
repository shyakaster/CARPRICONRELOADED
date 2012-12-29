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
    public class AgentTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }
        [Test]
        public void AddAgent()
        {
            using (CapriconContext context = new CapriconContext())
            {

                var newAgent = new Agent()
                {
                    FirstName = "Blaise",
                    LastName = "Nyamwamba",
                    OtherName = "",
                    Gender = Gender.Female,
                    MobilePhone = "0756 123 456",
                    Email = "Hadija@gmail.com",
                    Town = "Nairobi",
                    District = "Nairobi",
                    DateOfBirth = DateTime.Now.AddYears(-40),
                };

                var agentRep = new AgentRepository(context);
                agentRep.Add(newAgent);

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
                var repository = new AgentRepository(context1);
                var savedAgents = repository.GetAll().ToList();

                Assert.AreEqual(savedAgents.Count(), 1, "returns 20 records");
                var savedAgentsList = savedAgents;
                savedAgentsList.ForEach
                    (
                        s =>
                        {
                            Debug.WriteLine(s.AgentId + " - " + s.FirstName + " " + s.LastName);
                        }
                    );
            };
        }

        [Test]
        public void UpdateAgent()
        {
            using (var uow = new CapriconContext())
            {
                var agentRep = new AgentRepository(uow);

                var existingAgents = agentRep.GetAll().ToList();

                var existingAgent = existingAgents.LastOrDefault();

                Assert.IsNotNull(existingAgent);

                existingAgent.FirstName = "Hilda";
                existingAgent.LastName = "Kunda";
                existingAgent.OtherName = "";
                existingAgent.Gender = Gender.Male;
                existingAgent.DateOfBirth = DateTime.Now.AddYears(-60);
                existingAgent.MobilePhone = "0777 700 700";
                existingAgent.Email = "mukasa@hotmail.com";
                existingAgent.Town = "Bugembe";
                existingAgent.District = "Jinja";

                //check for validation rules
                //existingAgent.FirstName = "";
                //existingAgent.LastName = "";
                //existingAgent.Gender = Gender.Not_Specified;
                //existingAgent.Email = "";
                //existingAgent.Town = "";
                //existingAgent.District = "";

                agentRep.Attach(existingAgent);
                uow.Entry(existingAgent).State = EntityState.Modified;

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
            var repository = new AgentRepository(uow1);
            var savedAgents = repository.GetAll().ToList();

            Assert.AreEqual(savedAgents[0].LastName, "Byanjeru");
        }

        [Test]
        public void DeleteAgent()
        {
            using (var uow = new CapriconContext())
            {
                var agentRep = new AgentRepository(uow);
                var existingAgent = agentRep.Find(a => a.AgentId == 2).FirstOrDefault();

                Assert.IsNotNull(existingAgent);

                int agentId;
                if (existingAgent != null)
                {
                    agentId = existingAgent.AgentId;

                    agentRep.Delete(existingAgent);

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

                    Assert.IsNull(agentRep.Find(a => a.AgentId == agentId).FirstOrDefault());
                }
                else //no agents were selected
                    Assert.Fail("No agent was selected");
            }
        }
    }
}
