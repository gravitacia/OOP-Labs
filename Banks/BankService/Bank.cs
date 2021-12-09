using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Banks.AccountType;
using Banks.UserService;

namespace Banks.BankService
{
    internal class Bank
    {
        public Bank(int id, BankInformation bankinfo)
        {
            ID = id;
            Accounts = new List<AccountReference>();
            Transactions = new List<BankTransactions>();
            BankInfo = bankinfo ?? throw new ArgumentNullException(nameof(bankinfo));
        }

        public int ID { get; set; }
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
                throw new Exception("Transaction was already canceled");

            if (Transactions[i].Type == "WWL")
            {
                int frm = FindAccount(Transactions[i].FirstAcc);
                Accounts[frm].Replenish(Transactions[i].Amount);
                Transactions[i].IsCanceled = true;
            }

            if (Transactions[i].Type == "RNT")
            {
                int to = FindAccount(Transactions[i].FirstAcc);
                bool t = Accounts[to].Withdraw(Transactions[i].Amount);
                if (t)
                    Transactions[i].IsCanceled = true;
                else
                    throw new Exception("Transaction cannot be canceled");
            }

            if (Transactions[i].Type == "TFR")
            {
                int frm = FindAccount(Transactions[i].FirstAcc);
                int to = FindAccount(Transactions[i].SecAcc);
                bool t = Accounts[to].Withdraw(Transactions[i].Amount);
                if (t)
                {
                    Accounts[frm].Replenish(Transactions[i].Amount);
                    Transactions[i].IsCanceled = true;
                }
                else
                {
                    throw new Exception("Transaction cannot be canceled");
                }
            }
        }

        public void Replenish(double amount, int toId)
        {
            int to = FindAccount(toId);
            Accounts[to].Replenish(amount);
            Transactions.Add(new BankTransactions("RNT", toId, amount, BankInfo));
            BankInfo.AvailibleTransactionId += 1;
        }

        public void Withdraw(double amount, int fromId, DateProvider today)
        {
            int frm = FindAccount(fromId);
            bool t = Accounts[frm].Withdraw(amount, today);
            if (!t)
                throw new Exception("Withdrawal cannot be completed");
            Transactions.Add(new BankTransactions("WWL", fromId, amount, BankInfo));
            BankInfo.AvailibleTransactionId += 1;
        }

        public void Transfer(double amount, int fromId, int toId, DateProvider today)
        {
            int frm = FindAccount(fromId);
            int to = FindAccount(toId);
            bool t = Accounts[frm].Withdraw(amount, today);
            if (t)
            {
                Accounts[to].Replenish(amount);
                Transactions.Add(new BankTransactions("TFR", fromId, toId, amount, BankInfo));
            }
            else
            {
                throw new Exception("Transfer cannot be completed");
            }

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
                throw new Exception("Requested ID does not exists");
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
                throw new Exception("Requested ID does not exists");
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
                throw new Exception("Requested ID does not exists");
            return ans;
        }

        public void ShowClients(List<User> users)
        {
            Console.WriteLine("-----Clientlist:-------");
            Console.WriteLine("ID\tName\tSurname\tTrusted\tPassprt\tAddress");
            foreach (User user in users)
                Console.WriteLine(user.UId + "\t" + user.Name + "\t" + user.Surname + "\t" + user.Istrustful() + "\t" + user.Passport + "\t" + user.Address);
            Console.WriteLine("-----------------------");
        }

        public void ShowAccounts()
        {
            Console.WriteLine("-----Accountlist:------");
            Console.WriteLine("ID\tClID\tType\tMoney");
            foreach (AccountReference account in Accounts)
                Console.WriteLine(account.Id + "\t" + account.ClientId + "\t" + account.AccType + "\t" + account.Money);
            Console.WriteLine("-----------------------");
        }

        public void ShowTransactions()
        {
            Console.WriteLine("-----Transactions:-----");
            Console.WriteLine("ID\tType\tID1\tID2\tCncld\tMoney");
            foreach (BankTransactions transaction in Transactions)
                Console.WriteLine(transaction.Id + "\t" + transaction.Type + "\t" + transaction.FirstAcc + "\t" + transaction.SecAcc + "\t" + transaction.IsCanceled + "\t" + transaction.Amount);
            Console.WriteLine("-----------------------");
        }
    }
}
