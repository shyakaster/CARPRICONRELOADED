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
    public class PaymentTests
    {
        [TestFixtureSetUp]
        public void InitializeTest()
        {
            System.Data.Entity.Database.SetInitializer<CapriconContext>(new CapriconDatabaseInitializer());
        }
        [Test]
        public void AddPayment()
        {
            using (CapriconContext context = new CapriconContext())
            {
                var messageRepository = new MessageRepository(context);
                var existingMessage = messageRepository.GetAll().LastOrDefault();

                Assert.IsNotNull(existingMessage);

                var newPayment = new Payment()
                {
                    PaymentDate = DateTime.Now,
                    Amount = 750,
                    Message = existingMessage
                };

                var paymentRep = new PaymentRepository(context);
                paymentRep.Add(newPayment);

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
                var repository = new PaymentRepository(context1);
                var savedPayments = repository.GetAll().ToList();

                Assert.AreEqual(savedPayments.Count(), 3, "returns 20 records");
                var savedPaymentsList = savedPayments;
                savedPaymentsList.ForEach
                    (
                        p =>
                        {
                            Debug.WriteLine(p.PaymentId + " - " + p.Amount + " " + p.Message.MessageId);
                        }
                    );
            };
        }

        [Test]
        public void UpdatePayment()
        {
            using (var uow = new CapriconContext())
            {
                var paymentRep = new PaymentRepository(uow);

                var existingPayments = paymentRep.GetAll().ToList();

                var existingPayment = existingPayments.Find(p => p.PaymentId == 3);

                Assert.IsNotNull(existingPayment);

                var messageRepository = new MessageRepository(uow);
                var existingMessage = messageRepository.GetAll().FirstOrDefault();

                Assert.IsNotNull(existingMessage);

                existingPayment.PaymentDate = DateTime.Now;
                existingPayment.Amount = 350;
                existingPayment.Message = existingMessage;

                //check for validation rules
                //existingPayment.PaymentDate = DateTime.Now;

                paymentRep.Attach(existingPayment);
                uow.Entry(existingPayment).State = EntityState.Modified;

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
            var repository = new PaymentRepository(uow1);
            var savedPayments = repository.GetAll().ToList();

            Assert.AreEqual(savedPayments[0].Amount, 350);
        }

        [Test]
        public void DeletePayment()
        {
            using (var uow = new CapriconContext())
            {
                var paymentRep = new PaymentRepository(uow);
                var existingPayment = paymentRep.Find(p => p.PaymentId == 2).FirstOrDefault();

                Assert.IsNotNull(existingPayment);

                int paymentId;
                if (existingPayment != null)
                {
                    paymentId = existingPayment.PaymentId;

                    //Delete payment
                    paymentRep.Delete(existingPayment);

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

                    Assert.IsNull(paymentRep.Find(p => p.PaymentId == paymentId).FirstOrDefault());
                }
                else //no payments were selected
                    Assert.Fail("No payment was selected");
            }
        }
    }
}
