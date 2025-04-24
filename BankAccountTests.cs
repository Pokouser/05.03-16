using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;
using System;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() =>
            account.Debit(debitAmount));
        }
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }
        [TestMethod]
        public void Credit_witchZeroAmount_ShouldNotChangeBalace()
        {
            double beginningBalance = 100.0;
            double creditAmount = 0.0;
            BankAccount account = new BankAccount("Test user", beginningBalance);

            account.Credit(creditAmount);

            Assert.AreEqual(beginningBalance, account.Balance, 0.001, "balance should not change when credit zero amount");
        }

        [TestMethod]
        public void Debit_WhenAmountIsNegative_ShouldThrowCorrectExceptionMessage()
        {
            double debitAmount = -50.0;
            BankAccount account = new BankAccount("Test User", 100.0);
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        public void Debit_WithFullBalanceAmount_ShouldSetBalanceToZero()
        {

            double beginningBalance = 100.0;
            BankAccount account = new BankAccount("Test User", beginningBalance);


            account.Debit(beginningBalance);

            Assert.AreEqual(0.0, account.Balance, 10000,
                "Balance should be zero after debiting full amount");
        }
    }
}