using System;
using System.Collections.Generic;
using Banks.AccountType;
using Banks.BankInformationBuilder;
using Banks.BankService;
using Banks.TransactionsService;
using Banks.UserService;

namespace Banks
{
    internal static class Program
    {
        public static void ShowClients(List<User> users)
        {
            Console.WriteLine("-----Clientlist:-------");
            Console.WriteLine("ID\tName\tSurname\tTrusted\tPassprt\tAddress");
            foreach (User user in users)
                Console.WriteLine(user.UId + "\t" + user.Name + "\t" + user.Surname + "\t" + user.Istrustful() + "\t" + user.Passport + "\t" + user.Address);
            Console.WriteLine("-----------------------");
        }

        public static void ShowAccounts(List<AccountReference> accounts)
        {
            Console.WriteLine("-----Accountlist:------");
            Console.WriteLine("ID\tClID\tType\tMoney");
            foreach (AccountReference account in accounts)
                Console.WriteLine(account.Id + "\t" + account.ClientId + "\t" + account.AccType + "\t" + account.Money);
            Console.WriteLine("-----------------------");
        }

        public static void ShowTransactions(List<BankTransactions> transactions)
        {
            Console.WriteLine("-----Transactions:-----");
            Console.WriteLine("ID\tType\tID1\tID2\tCncld\tMoney");
            foreach (BankTransactions transaction in transactions)
                Console.WriteLine(transaction.Id + "\t" + transaction.Type + "\t" + transaction.FirstAcc + "\t" + transaction.SecAcc + "\t" + transaction.IsCanceled + "\t" + transaction.Amount);
            Console.WriteLine("-----------------------");
        }

        private static void Main()
        {
            var users = new List<User>();
            var date = new DateProvider(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            var depositspercents = new List<DepositYearPercent>();
            depositspercents.Add(new DepositYearPercent(0, 5));
            depositspercents.Add(new DepositYearPercent(50000, 5.5));
            depositspercents.Add(new DepositYearPercent(100000, 6));
            IUserBuilder userBuilder = new UserBuilder();
            IBankInfoBuilder bankInfoBuilder = new BankInfoBuilder();
            bankInfoBuilder.SetTrustLimit(100000);
            bankInfoBuilder.SetDebitYearlyPercent(3.65);
            bankInfoBuilder.SetCreditLimit(-100000);
            bankInfoBuilder.SetCreditMonthlyComission(500);
            bankInfoBuilder.SetDepositYearlyPercent(depositspercents);
            var bank = new Bank(0, bankInfoBuilder.GetBankInfo());

            string ans = "1";
            while (ans == "1")
            {
                Console.WriteLine("Choose one option");
                Console.WriteLine("1. Create users bank account");
                Console.WriteLine("2. Choose account type");
                Console.WriteLine("3. Withdraw some cash");
                Console.WriteLine("4. Replenish some cash");
                Console.WriteLine("5. Transfer money");
                Console.WriteLine("6. Show clients");
                Console.WriteLine("7. Show accounts");
                Console.WriteLine("8. Show transactions");
                Console.WriteLine("9. Drink some beer");
                Console.WriteLine("0. Close");
                string key = Console.ReadLine();

                if (key == "1")
                {
                    Console.WriteLine("Write client name");
                    userBuilder.SetName(Console.ReadLine());

                    Console.WriteLine("Write client surname");
                    userBuilder.SetSurname(Console.ReadLine());

                    Console.WriteLine("Do you want add address? (type y or yes)");
                    if (Console.ReadLine() is "y" or "yes")
                    {
                        userBuilder.SetAdress(Console.ReadLine());
                    }
                    else if (Console.ReadLine() is "n" or "no")
                    {
                        Console.WriteLine("Ok, next step, but we can't trust you");
                    }

                    Console.WriteLine("Do you want add passport? (type y or yes)");
                    if (Console.ReadLine() is "y" or "yes")
                    {
                        userBuilder.SetPassport(Console.ReadLine());
                    }
                    else if (Console.ReadLine() is "n" or "no")
                    {
                        Console.WriteLine("Wow, we can't trust you!");
                    }

                    users.Add(userBuilder.GetUser());
                }

                if (key == "2")
                {
                    Console.WriteLine("Now choose a type of account");
                    Console.WriteLine("1. Debit account");
                    Console.WriteLine("2. Credit account");
                    Console.WriteLine("3. Deposit account");
                    string accType = Console.ReadLine();

                    if (accType == "1")
                    {
                        Console.WriteLine("Okay, choose our hero");
                        ShowClients(users);
                        Console.WriteLine("Type an UId");
                        string uid = Console.ReadLine();
                        Console.WriteLine("Type a count of money");
                        int money = Convert.ToInt32(Console.ReadLine());
                        bank.NewDebitAccount(uid, users, money, date);
                    }

                    if (accType == "2")
                    {
                        Console.WriteLine("Okay, choose our hero");
                        ShowClients(users);
                        Console.WriteLine("Type an UId");
                        string uid = Console.ReadLine();
                        Console.WriteLine("Type a count of money");
                        int money = Convert.ToInt32(Console.ReadLine());
                        bank.NewCreditAccount(uid, users, money, date);
                    }

                    if (accType == "3")
                    {
                        Console.WriteLine("Okay, choose our hero");
                        ShowClients(users);
                        Console.WriteLine("Type an UId");
                        string uid = Console.ReadLine();
                        Console.WriteLine("Type a count of money");
                        int money = Convert.ToInt32(Console.ReadLine());
                        bank.NewDepositAccount(uid, users, money, date, new DateProvider(21, 01, 2022));
                    }
                }

                if (key == "3")
                {
                    Console.WriteLine("Okay, choose our hero");
                    ShowClients(users);
                    string uid = Console.ReadLine();
                    Console.WriteLine("Type an UId");
                    Console.WriteLine("Type amount sum");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    bank.Withdraw(amount, Convert.ToInt32(uid), date);
                    Console.WriteLine("Withdrawal of " + amount + "from " + uid);
                }

                if (key == "4")
                {
                    Console.WriteLine("Okay, choose our hero");
                    ShowClients(users);
                    string uid = Console.ReadLine();
                    Console.WriteLine("Type an UId");
                    Console.WriteLine("Type amount sum");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    bank.Replenish(amount, Convert.ToInt32(uid));
                    Console.WriteLine("Replenishment of" + amount + "to " + uid);
                }

                if (key == "5")
                {
                    Console.WriteLine("Okay, choose our heroes");
                    ShowClients(users);
                    Console.WriteLine("Type an UIds of 1st user");
                    int fromId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Type an UIds of 2nd user");
                    int toId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Type amount sum");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    bank.Transfer(amount, fromId, toId, date);
                    Console.WriteLine("Transfer of " + amount + "from " + fromId + "to " + toId);
                }

                if (key == "6")
                {
                    ShowClients(users);
                }

                if (key == "7")
                {
                    ShowAccounts(bank.Accounts);
                }

                if (key == "8")
                {
                    ShowTransactions(bank.Transactions);
                }

                if (key == "9")
                {
                    Console.WriteLine("Slurp... Slurp... Slurp... Nice!");
                }

                Console.WriteLine("Type any cifru");
                ans = Console.ReadLine();
            }

            for (int i = 0; i < 6; i++)
            {
                date.AddOneDay();
                Console.Write("Iterating: ");
                date.Show();
                bank.DailyAccountReNewal(date);
            }

            for (int i = 0; i < 366; i++)
            {
                date.AddOneDay();
                Console.Write("Iterating: ");
                date.Show();
                bank.DailyAccountReNewal(date);
            }
        }
    }
}
