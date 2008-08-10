﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Payroll;

namespace PayrollTest
{
    /// <summary>
    /// Summary description for PayrollTest
    /// </summary>
    [TestClass]
    public class PayrollTest
    {
        public PayrollTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddSalariedEmployeeTest()
        {
            int id = 1;
            AddSalariedEmployee transaction = new AddSalariedEmployee(id, "Bob", "Home", 1000.00);
            transaction.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);

            Assert.AreEqual("Bob", e.Name);

            Assert.IsTrue(e.Classification is SalariedClassification);

            SalariedClassification sc = e.Classification as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, 0.01);

            Assert.IsTrue(e.Method is HoldMethod);

            Assert.IsTrue(e.Schedule is MonthlySchedule);
        }

        [TestMethod]
        public void AddHourlyEmployeeTest()
        {
            int id = 2;
            AddHourlyEmployee transaction = new AddHourlyEmployee(id, "Warren", "ms", 22.00);
            transaction.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);

            Assert.AreEqual("Warren", e.Name);
            Assert.AreEqual("ms", e.Address);

            Assert.IsTrue(e.Classification is HourlyClassification);
            HourlyClassification hc = e.Classification as HourlyClassification;
            Assert.AreEqual(22.00, hc.HourlyRate, 0.01);

            Assert.IsTrue(e.Schedule is WeeklySchedule);

            Assert.IsTrue(e.Method is HoldMethod);
        }

        [TestMethod]
        public void AddCommissionedEmployeeTest()
        {
            int id = 3;
            AddCommissionedEmployee transaction = new AddCommissionedEmployee(id,"Selena", "shanghai", 2400.00, 0.20);
            transaction.Execute();


            Employee e = PayrollDatabase.GetEmployee(id);
            
            Assert.AreEqual("Selena", e.Name);
            Assert.AreNotEqual("selena", e.Name);

            Assert.AreEqual("shanghai", e.Address);
            
            Assert.IsTrue(e.Classification is CommissionedClassification);
            CommissionedClassification c = e.Classification as CommissionedClassification;
            Assert.AreEqual(2400.00, c.Salary, 0.01);
            Assert.AreEqual(0.20, c.CommissionRate, 0.01);

            Assert.IsTrue(e.Schedule is BiweeklySchedule);
            Assert.IsTrue (e.Method is HoldMethod);
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            int id = 4;
            AddSalariedEmployee trans = new AddSalariedEmployee(id, "ToBeDeleted", "somewhere", 1000.00);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);
            Assert.AreEqual("ToBeDeleted", e.Name);

            DeleteEmployee del = new DeleteEmployee(id);
            del.Execute();

            e = PayrollDatabase.GetEmployee(id);
            Assert.IsNull(e);
        }

        [TestMethod]
        public void TimeCardTransactionTest()
        {
            int id = 5;
            double hourlyRate = 10.0;
            double delta = 0.01;
            DateTime date = new DateTime(2008, 8, 8);  
          
            TimeSpan time = new TimeSpan(2, 30, 0);

            AddEmployee add = new AddHourlyEmployee(id, "HourlyEmployee", "swhere", hourlyRate);
            add.Execute();

            TimeCardTransaction trans = new TimeCardTransaction(id, date, time);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);
            Assert.IsNotNull(e);

            HourlyClassification c = e.Classification as HourlyClassification;
            Assert.AreEqual(hourlyRate, c.HourlyRate, delta);

            TimeCard card = c.GetTimeCard(date);
            Assert.IsNotNull(card);

            TimeSpan actualTime = card.Time;
            Assert.AreEqual(time.TotalMinutes,actualTime.TotalMinutes);
        }

        [TestMethod]
        public void ServiceReceiptTransactionTest()
        {
            int id = 6;
            DateTime date = new DateTime(2008,8, 8);
            double amount = 2000.0;

            AddEmployee add = new AddCommissionedEmployee(id, "Warren", "somewhere", 5000.00, 0.3);
            add.Execute();

            SalesReceiptTransaction trans = new SalesReceiptTransaction(id, date, amount);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);
            Assert.IsNotNull(e);

            Assert.IsTrue(e.Classification is CommissionedClassification);
            CommissionedClassification c = e.Classification as CommissionedClassification;

            SalesReceipt receipt = c.GetSalesReceipt(date);
            Assert.AreEqual(amount, receipt.Amount, 0.01);
        }
    }
}