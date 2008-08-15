using System;
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
        #region Fields & properties
        private static int _nextId = 1;
        private static int NextId { get { return _nextId++; } }

        private const double delta = 0.01;

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
        #endregion

        #region Operations
        private static void Reset()
        {
            PayrollDatabase.Reset();
            _nextId = 0;
        }
        #endregion

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

        #region Basic perations tests
        [TestMethod]
        public void AddSalariedEmployeeTest()
        {
            int id = NextId;
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
            int id = NextId;
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
            int id = NextId;
            AddCommissionedEmployee transaction = new AddCommissionedEmployee(id, "Selena", "shanghai", 2400.00, 0.20);
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
            Assert.IsTrue(e.Method is HoldMethod);
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            int id = NextId;
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
            int id = NextId;
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
            Assert.AreEqual(time.TotalMinutes, actualTime.TotalMinutes);
        }

        [TestMethod]
        public void SalesReceiptTransactionTest()
        {
            int id = NextId;
            DateTime date = new DateTime(2008, 8, 8);
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

        [TestMethod]
        public void ServiceChargeTest()
        {
            int id = NextId;
            string name = "Dummy7";

            int memberId = 1;
            double amount = 200.0;
            DateTime date = new DateTime(2008, 8, 11);

            AddSalariedEmployee add = new AddSalariedEmployee(id, name, "home office", 1000.00);
            add.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);
            Assert.IsNotNull(e);

            UnionAffiliation affiliation = new UnionAffiliation(NextId, 22.1);
            e.Affiliation = affiliation;

            PayrollDatabase.AddUnionMember(memberId, e);

            ServiceChargeTransaction trans = new ServiceChargeTransaction(memberId,
                date, amount);
            trans.Execute();

            ServiceCharge charge = affiliation.GetServiceCharge(date);
            Assert.IsNotNull(charge);
            Assert.AreEqual(amount, charge.Amount, 0.01);
        }

        #endregion

        #region Change tests
        [TestMethod]
        public void ChangeNameTransaction()
        {
            int empid = NextId;
            string name = "somebody";
            string newName = "somebody else";

            AddSalariedEmployee add = new AddSalariedEmployee(empid, name, "somewhere", 11.11);
            add.Execute();

            ChangeNameTransaction changeName = new ChangeNameTransaction(empid, newName);
            changeName.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(newName, e.Name);
        }

        [TestMethod]
        public void ChangeAddressTransaction()
        {
            int empid = NextId;
            string address = "somebody";
            string newAddress = "somebody else";

            AddSalariedEmployee add = new AddSalariedEmployee(empid, "someone", address, 11.11);
            add.Execute();

            Transaction changeAddresss = new ChangeAddressTransaction(empid, newAddress);
            changeAddresss.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.AreEqual(newAddress, e.Address);
        }

        [TestMethod]
        public void ChangeSalariedTransactionTest()
        {
            int empid = NextId;

            Transaction trans = new AddCommissionedEmployee(empid, "somebody", "somewhere", 111.11, 0.03);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNotNull(e);
            Assert.IsTrue(e.Classification is CommissionedClassification);

            trans = new ChangeSalariedTransaction(empid, 222.22);
            trans.Execute();

            SalariedClassification sc = e.Classification as SalariedClassification;
            Assert.IsNotNull(sc);
            Assert.AreEqual(222.22, sc.Salary);

            Assert.IsTrue(e.Schedule is MonthlySchedule);
        }

        [TestMethod]
        public void ChangeHourlyTransactionTest()
        {
            int empid = NextId;
            double hourlyRate = 44.44;

            Transaction trans = new AddSalariedEmployee(empid, "warren", "office", 10000.00);
            trans.Execute();

            trans = new ChangeHourlyTransaction(empid, hourlyRate);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNotNull(e);

            HourlyClassification hc = e.Classification as HourlyClassification;
            Assert.IsNotNull(hc);
            Assert.AreEqual(hourlyRate, hc.HourlyRate);

            Assert.IsTrue(e.Schedule is WeeklySchedule);
        }

        [TestMethod]
        public void ChangeCommissionedTransactionTest()
        {
            int empid = NextId;
            double salary = 2222.22;
            double commissionRate = .3;

            Transaction trans = new AddHourlyEmployee(empid, "someone", "somewhere", 33.0);
            trans.Execute();

            trans = new ChangeCommissionedTransaction(empid, salary, commissionRate);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsNotNull(e);

            CommissionedClassification cc = e.Classification as CommissionedClassification;
            Assert.IsNotNull(cc);

            Assert.AreEqual(salary, cc.Salary, delta);
            Assert.AreEqual(commissionRate, cc.CommissionRate, delta);

            Assert.IsTrue(e.Schedule is BiweeklySchedule);
        }

        [TestMethod]
        public void ChangeMethodTransactonTest()
        {
            int empid = NextId;
            string bank = "ICBC";
            string account = "12345";

            string address = "wall street.";

            Transaction trans = new AddHourlyEmployee(empid, "nobody", "nowhere", 33);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            Assert.IsTrue(e.Method is HoldMethod);

            //Change to direct method.
            trans = new ChangeDirectTransaction(empid, bank, account);
            trans.Execute();

            DirectMethod dm = e.Method as DirectMethod;
            Assert.IsNotNull(dm);
            Assert.AreEqual(bank, dm.Bank);
            Assert.AreEqual(account, dm.Account);

            //Changes to MailMethod
            trans = new ChangeMailTransaction(empid, address);
            trans.Execute();

            MailMethod mm = e.Method as MailMethod;
            Assert.IsNotNull(mm);
            Assert.AreEqual(address, mm.Address);

            //Changes back to HoldMethod
            trans = new ChangeHoldTransaction(empid);
            trans.Execute();

            Assert.IsTrue(e.Method is HoldMethod);
        }

        [TestMethod]
        public void ChangeAffiliationTransactionTest()
        {
            int empid = NextId;
            int memberId = NextId;
            double dues = 33.33;

            Transaction trans = new AddSalariedEmployee(empid, "someone", "some place", 3223);
            trans.Execute();

            //Change to unionAffiliation
            trans = new ChangeMemberTransaction(empid, memberId, dues);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(empid);
            UnionAffiliation ua = e.Affiliation as UnionAffiliation;
            Assert.IsNotNull(ua);
            Assert.AreEqual(dues, ua.Dues, delta);

            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreSame(e, member);

            //Change back to NoAffiliation
            trans = new ChangeUnaffiliatedTranaction(empid);
            trans.Execute();

            Assert.IsTrue(e.Affiliation is NoAffiliation);

            Assert.IsNull(PayrollDatabase.GetUnionMember(memberId));
        }
        #endregion

        #region Paying tests
        [TestMethod]
        public void PaySingleSalariedTest()
        {
            Reset();

            int id = NextId;
            DateTime date = new DateTime(2008, 8, 31);
            double salary = 2222.22;

            Transaction trans = new AddSalariedEmployee(id, "warren", "ms", salary);
            trans.Execute();

            Employee e = PayrollDatabase.GetEmployee(id);
            Assert.IsTrue(e.IsPayDay(date));

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNotNull(pc);
            Assert.AreEqual(salary, pc.Gross, delta);
            Assert.AreEqual(0.0, pc.Deductions, delta);
            Assert.AreEqual(salary, pc.Net, delta);
        }

        [TestMethod]
        public void PaySingleSalariedOnWrongDate()
        {
            Reset();

            int id = NextId;
            DateTime date = new DateTime(2008, 8, 8);

            Transaction trans = new AddSalariedEmployee(id, "someone", "ms", 1111.11);
            trans.Execute();

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PaySingleHourlyTest()
        {
            Reset();

            int id = NextId;
            double hourlyRate = 33.33;
            DateTime date = new DateTime(2008, 8, 15);

            Transaction trans = new AddHourlyEmployee(id, "somebody", "somewhere", hourlyRate);
            trans.Execute();

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNotNull(pc);
            Assert.AreEqual(0, pc.Gross, delta);
            Assert.AreEqual(0, pc.Deductions, delta);
            Assert.AreEqual(0, pc.Net, delta);
        }

        [TestMethod]
        public void PaySingleHourlyOnWrongDate()
        {
            Reset();

            int id = NextId;
            double hourlyRate = 33.33;
            DateTime date = new DateTime(2008, 8, 14);

            Transaction trans = new AddHourlyEmployee(id, "somebody", "somewhere", hourlyRate);
            trans.Execute();

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PaySingleHourlyOneTimeCard()
        {
            Reset();

            int id = NextId;
            double hourlyRate = 33.33;
            DateTime date = new DateTime(2008, 8, 15); //Friday

            Transaction trans = new AddHourlyEmployee(id, "somebody", "somewhere", hourlyRate);
            trans.Execute();

            trans = new TimeCardTransaction(id, date, new TimeSpan(2, 0, 0));
            trans.Execute();

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNotNull(pc);

            Assert.AreEqual(2 * hourlyRate, pc.Gross, delta);
            Assert.AreEqual(0, pc.Deductions, delta);
            Assert.AreEqual(2 * hourlyRate, pc.Net, delta);
        }

        [TestMethod]
        public void SalariedUnionMemberDuesTest()
        {
            Reset();
            int id = NextId;
            int memberId = NextId;
            double salary = 1200.0;
            double dues = 33.33;
            DateTime date = new DateTime(2008, 8, 31);

            Transaction trans = new AddSalariedEmployee(id, "someone", "someplace", salary);
            trans.Execute();

            trans = new ChangeMemberTransaction(id, memberId, dues);
            trans.Execute();

            PaydayTransaction pt = new PaydayTransaction(date);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(id);
            Assert.IsNotNull(pc);

            Assert.AreEqual(salary, pc.Gross, delta);
            Assert.AreEqual(5 * dues, pc.Deductions, delta);
            Assert.AreEqual(salary - 5 * dues, pc.Net, delta);
        }
        #endregion
    }
}
