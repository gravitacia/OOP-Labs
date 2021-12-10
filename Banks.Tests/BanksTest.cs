using System;
using System.Collections.Generic;
using Banks.BankInformationBuilder;
using Banks.BankService;
using Banks.TransactionsService;
using Banks.UserService;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        [Test]
        public void AddUsersMakeMoney()
        {
            List<User> users = new List<User>();
            IUserBuilder userBuilder = new UserBuilder();
            IBankInfoBuilder bankInfoBuilder = new BankInfoBuilder();
            
            userBuilder.SetName("Alex");
            userBuilder.SetSurname("kos");
            userBuilder.SetAdress("asd");
            userBuilder.SetPassport("123123");
            User user1 = userBuilder.GetUser();
            
            userBuilder.SetName("Egor");
            userBuilder.SetSurname("Lol");
            userBuilder.SetAdress("asd");
            userBuilder.SetPassport("321321");
            User user2 = userBuilder.GetUser();
            
            var depositspercents = new List<DepositYearPercent>();
            depositspercents.Add(new DepositYearPercent(0, 5));
            depositspercents.Add(new DepositYearPercent(50000, 5.5));
            depositspercents.Add(new DepositYearPercent(100000, 6));
            bankInfoBuilder.SetTrustLimit(100000);
            bankInfoBuilder.SetDebitYearlyPercent(3.65);
            bankInfoBuilder.SetCreditLimit(-100000);
            bankInfoBuilder.SetCreditMonthlyComission(500);
            bankInfoBuilder.SetDepositYearlyPercent(depositspercents);
            var bank = new Bank(0, bankInfoBuilder.GetBankInfo());
            
            bank.NewDebitAccount(user1.UId.ToString(), users, 10000, new DateProvider(21, 2, 2022));
            bank.NewDebitAccount(user2.UId.ToString(), users, 5000, new DateProvider(21, 2, 2022));

            bank.Withdraw(5000, user1.UId, new DateProvider(21, 2, 2022));
            bank.Replenish(5000, user2.UId);

            Assert.Equals(bank.Accounts[bank.FindAccount(user1.UId)].Money, 5000);
            Assert.Equals(bank.Accounts[bank.FindAccount(user2.UId)].Money, 10000);
        }
        
    }
}