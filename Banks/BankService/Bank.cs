using System;
using System.Collections.Generic;
using System.Linq;
using Banks.AccountType;
using Banks.BankInformationBuilder;
using Banks.TransactionsService;
using Banks.UserService;
using Banks.Utilities;

namespace Banks.BankService
{
    public class Bank
    {
        public Bank(int id, BankInformation bankinfo)
        {
            Id = id;
            Accounts = new List<AccountReference>();
            Transactions = new List<BankTransactions>();
            BankInfo = bankinfo ?? throw new ArgumentNullException(nameof(bankinfo));
        }

        public int Id { get; set; }
        public List<AccountReference> Accounts { get; set; }
        public List<BankTransactions> Transactions { get; set; }
        public BankInformation BankInfo { get; set; }

        public void NewDebitAccount(string clientid, List<User> users, double money, DateProvider today)
        {
            int cl = FindClient(clientid, users);
            Accounts.Add(new DebitAccount(clientid, money, today, users[cl].Istrustful(), BankInfo));
            BankInfo.AvailibleAccountId += 1;
        }

        public void NewDepositAccount(string clientid, List<User> users, double money, DateProvider today, DateProvider limitdue)
        {
            int cl = FindClient(clientid, users);
            Accounts.Add(new DepositAccount(clientid, money, today, users[cl].Istrustful(), BankInfo, limitdue));
            BankInfo.AvailibleAccountId += 1;
        }

        public void NewCreditAccount(string clientid, List<User> users, double money, DateProvider today)
        {
            int cl = FindClient(clientid, users);
            Accounts.Add(new CreditAccount(clientid, money, today, users[cl].Istrustful(), BankInfo));
            BankInfo.AvailibleAccountId += 1;
        }

        public void DailyAccountReNewal(DateProvider today)
        {
            foreach (AccountReference account in Accounts)
                account.ReNew(today);
        }

        public void Cancel(string id)
        {
            int i = FindTransaction(id);
            if (Transactions[i].IsCanceled)
            {
                throw new BanksExeption("Transaction was already canceled");
            }
            else
            {
                Transactions[i].Type.Cancel(Transactions[i]);
            }
        }

        public void Replenish(double amount, int toId)
        {
            ITransaction replenish = new Replenishment();
            replenish.ProcessTransaction(new BankTransactions(Accounts[toId], amount, BankInfo));
        }

        public void Withdraw(double amount, int fromId, DateProvider today)
        {
            ITransaction withdraw = new Withdrawal();
            withdraw.ProcessTransaction(new BankTransactions(Accounts[fromId], amount, BankInfo));
            BankInfo.AvailibleTransactionId += 1;
        }

        public void Transfer(double amount, int fromId, int toId, DateProvider today)
        {
            ITransaction transfer = new Transfer();
            transfer.ProcessTransaction(new BankTransactions(Accounts[fromId], Accounts[toId], amount, BankInfo));
            BankInfo.AvailibleTransactionId += 1;
        }

        public int FindAccount(int id)
        {
            int t = 0;
            int ans = -1;
            while (t < Accounts.Count())
            {
                if (Accounts[t].Id == id)
                {
                    ans = t;
                    break;
                }

                t++;
            }

            if (ans == -1)
                throw new BanksExeption("Requested ID does not exists");
            return ans;
        }

        public int FindClient(string id, List<User> users)
        {
            int t = 0;
            int ans = -1;
            while (t < users.Count())
            {
                if (users[t].UId.ToString() == id)
                {
                    ans = t;
                    break;
                }

                t++;
            }

            if (ans == -1)
                throw new BanksExeption("Requested ID does not exists");
            return ans;
        }

        public int FindTransaction(string id)
        {
            int t = 0;
            int ans = -1;
            while (t < Transactions.Count())
            {
                if (Transactions[t].Id == id)
                {
                    ans = t;
                    break;
                }

                t++;
            }

            if (ans == -1)
                throw new BanksExeption("Requested ID does not exists");
            return ans;
        }
    }
}
