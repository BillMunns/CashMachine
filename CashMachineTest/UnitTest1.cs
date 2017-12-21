using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashMachine;

namespace CashMachineTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWithdraw_GOOD()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.Withdraw("W $200");
                Assert.AreEqual("Success: Dispensed $200", sw.ToString().Substring(0,sw.ToString().IndexOf("\r\n")));
            }
        }
        [TestMethod]
        public void TestWithdraw_INSF()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.Withdraw("W $20000");
                Assert.AreEqual("Failure: Insufficient funds", sw.ToString().Substring(0, sw.ToString().IndexOf("\r\n")));
            }
        }
        [TestMethod]
        public void TestWithdraw_BAD()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.Withdraw("W 20");
                Assert.AreEqual("Invalid Command", sw.ToString().Substring(0, sw.ToString().IndexOf("\r\n")));
            }
        }
        [TestMethod]
        public void TestWithdraw_MULTIPLE()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.Withdraw("W $200");
                machine.Withdraw("W $37");
                Assert.AreEqual("Success: Dispensed $200\r\n\r\nMachine Balance:\r\n$100 - 8\r\n$50 - 10\r\n$20 - 10\r\n$10 - 10\r\n$5 - 10\r\n$1 - 10\r\nSuccess: Dispensed $37\r\n\r\nMachine Balance:\r\n$100 - 8\r\n$50 - 10\r\n$20 - 9\r\n$10 - 9\r\n$5 - 9\r\n$1 - 8\r\n", sw.ToString());
            }
        }
        [TestMethod]
        public void TestInquiry_GOODSINGLE()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.InquirySelected("I $50");
                Assert.AreEqual("Machine Balance:\r\n$50 - 10\r\n", sw.ToString());
            }
        }
        [TestMethod]
        public void TestInquiry_GOODMULTIPLE()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.InquirySelected("I $10 $5 $100");
                Assert.AreEqual("Machine Balance:\r\n$10 - 10\r\n$5 - 10\r\n$100 - 10\r\n", sw.ToString());
            }
        }
        [TestMethod]
        public void TestInquiry_POSTWITHDRAWAL()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.Withdraw("W $1015");
                machine.InquirySelected("I $10 $5 $100 $1");
                Assert.AreEqual("Success: Dispensed $1015\r\n\r\nMachine Balance:\r\n$100 - 0\r\n$50 - 10\r\n$20 - 10\r\n$10 - 9\r\n$5 - 9\r\n$1 - 10\r\nMachine Balance:\r\n$10 - 9\r\n$5 - 9\r\n$100 - 0\r\n$1 - 10\r\n", sw.ToString());
            }
        }
        [TestMethod]
        public void TestInquiry_BAD()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                ATM machine = new ATM();
                machine.InquirySelected("I $25");
                Assert.AreEqual("Invalid Command", sw.ToString().Substring(0, sw.ToString().IndexOf("\r\n")));
            }
        }
    }
}
