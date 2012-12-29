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
    public class UserTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }
        [Test]
        public void AddUser()
        {
            using (CapriconContext context = new CapriconContext())
            {
                
                var newUser = new User()
                {
                    FirstName = "james",
                    LastName = "kamau",
                    OtherName = "",
                    Gender = Gender.Female,
                    MobilePhone = "0756 123 456",
                    Email = "Hadija@gmail.com",
                    Town = "Nairobi",
                    District = "Nairobi",
                    DateOfBirth = DateTime.Now.AddYears(-40),
                };

                var userRep = new UserRepository(context);
                userRep.Add(newUser);

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
                var repository = new UserRepository(context1);
                var savedUsers = repository.GetAll().ToList();

               Assert.AreEqual(savedUsers.Count(), 1, "returns 20 records");
               var savedUsersList = savedUsers;
               savedUsersList.ForEach
                   (
                       s =>
                       {
                           Debug.WriteLine(s.UserId +" - "+ s.FirstName +" "+ s.LastName);
                       }
                   );
            };
        }

        [Test]
        public void UpdateUser()
        {
            using (var uow = new CapriconContext())
            {
                var userRep = new UserRepository(uow);

                var existingUsers = userRep.GetAll().ToList();

                var existingUser = existingUsers.Find(a => a.UserId == 3);

                Assert.IsNotNull(existingUser);

                existingUser.FirstName = "Aaron";
                existingUser.LastName = "Mukasa";
                existingUser.OtherName = "Gad";
                existingUser.Gender = Gender.Male;
                existingUser.DateOfBirth = DateTime.Now.AddYears(-60);
                existingUser.MobilePhone = "0777 700 700";
                existingUser.Email = "mukasa@hotmail.com";
                existingUser.Town = "Bugembe";
                existingUser.District = "Jinja";

                //check for validation rules
                //existingUser.FirstName = "";
                //existingUser.LastName = "";
                //existingUser.Gender = Gender.Not_Specified;
                //existingUser.Email = "";
                //existingUser.Town = "";
                //existingUser.District = "";

                userRep.Attach(existingUser);
                uow.Entry(existingUser).State = EntityState.Modified;

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
            var repository = new UserRepository(uow1);
            var savedUsers = repository.GetAll().ToList();

            Assert.AreEqual(savedUsers[0].LastName, "Mawa");
        }

        [Test]
        public void DeleteUser()
        {
            using (var uow = new CapriconContext())
            {
                var userRep = new UserRepository(uow);
                var existingUser = userRep.Find(u => u.UserId == 2).FirstOrDefault();

                Assert.IsNotNull(existingUser);

                int userId;
                if (existingUser != null)
                {
                    userId = existingUser.UserId;

                    //Delete employee
                    userRep.Delete(existingUser);

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

                    Assert.IsNull(userRep.Find(u => u.UserId == userId).FirstOrDefault());
                }
                else //no userss were selected
                    Assert.Fail("No user was selected");
            }
        }
    }
}
