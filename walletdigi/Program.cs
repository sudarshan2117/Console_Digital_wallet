using System;
using System.Collections.Generic;

namespace DigitalWalletApp
{
 
   //1.Fundamental & interface building class 
    class Wallet
    {
        //set and get the username
        public string UserName { get; private set; }

        //set and get amount
        public decimal Balance { get; private set; }
        private List<string> transactions;     //this convert the transaction amt into list as form of string

        //in class create wallet using username input with balance entry
        public Wallet(string userName, decimal initialAmount)
        {
            UserName = userName;
            Balance = initialAmount;
            transactions = new List<string>();
        }

        //here is the boolean method check where account has balance or not for the entry true or false
        public bool Transfer(decimal amount, Wallet receiver)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance!");
                return false;
            }
            Balance -= amount;
            transactions.Add($"Transferred {amount} to {receiver.UserName}");
            //user can put the transfer amt along with registar name
            receiver.Receive(amount);
            return true;
        }

        // here sucess message show the Receive amt
        public void Receive(decimal amount)
        {
            Balance += amount;
            transactions.Add($"Received {amount}");
        }


        //the method use for trasaction history
        public (decimal balance, List<string> transactions) GetStatement()
        {
            return (Balance, transactions);
        }
    }

    //2. Functionality in the digital wallet
    class DigitalWalletApp
    {
        private Dictionary<string, Wallet> wallets;//

        public DigitalWalletApp()
        {
            wallets = new Dictionary<string, Wallet>();
        }

        //3. this part is creating the wallet 
        public void CreateWallet()
        {
            Console.Write("Enter your name: \n "  );
            string userName = Console.ReadLine();
            Console.Write("Enter the initial amount for the wallet: \n");
            decimal initialAmount = decimal.Parse(Console.ReadLine());
                
                //if the same name wallet is exist then statement goes to false
            if (wallets.ContainsKey(userName))
            {
                Console.WriteLine("Wallet already exists for this user.\n");
                return;
            }

            wallets[userName] = new Wallet(userName, initialAmount);
            Console.WriteLine($"Wallet created for '{userName}' with balance '{initialAmount}'.");
        }

        //4. transfer Amount confrigutation
        public void TransferAmount()
        {
            Console.Write("Enter sender's name: \n ");
            string senderName = Console.ReadLine();

            Console.Write("Enter receiver's name: \n ");
            string receiverName = Console.ReadLine();

            Console.Write("Enter amount to transfer: \n");
            decimal amount = decimal.Parse(Console.ReadLine());

            //here use boolean expressio he also compair operator
            if (!wallets.ContainsKey(senderName) || !wallets.ContainsKey(receiverName))
            {
                Console.WriteLine("One or both users do not exist.\n");
                return;
            }

            //this is checkpoint of wallet validation of both user
            var senderWallet = wallets[senderName];
            var receiverWallet = wallets[receiverName];

            if (senderWallet.Transfer(amount, receiverWallet))
                Console.WriteLine($"Transferred '{amount}' from '{senderName}' is Sucessfully Receive = '{receiverName}'.");
        }

       //5. account statement part where is store record on stement between 2 account
        public void AccountStatement()
        {
            Console.Write("Enter account holder's name:\n ");
            string userName = Console.ReadLine();

            if (!wallets.ContainsKey(userName))
            {
                Console.WriteLine("\nUser does not exist.");
                return;
            }

            //This part is use get statement of account
            var wallet = wallets[userName];
            var statement = wallet.GetStatement();

            Console.WriteLine($"\nAccount Statement for {userName}:");
            Console.WriteLine($"Current Balance: {statement.balance}");
            Console.WriteLine("Transactions:");

            foreach (var transaction in statement.transactions)
                Console.WriteLine(transaction);
        }

        public void Overview()
        {
            Console.WriteLine("Overview of all wallets:");

            foreach (var wallet in wallets)
                Console.WriteLine($"{wallet.Key}: Balance - {wallet.Value.Balance}");
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nDigital Wallet Console Application");
                Console.WriteLine("1. Create Wallet\n");
                Console.WriteLine("2. Transfer Amount\n");
                Console.WriteLine("3. Account Statement\n");
                Console.WriteLine("4. Overview\n");
                Console.WriteLine("5. Exit\n");

                string choice = Console.ReadLine();

                switch (choice)//using switch & case choie where made and wallet process start
                {
                    case "1":
                        CreateWallet();
                        break;
                    case "2":
                        TransferAmount();
                        break;
                    case "3":
                        AccountStatement();
                        break;
                    case "4":
                        Overview();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the application.\n");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            DigitalWalletApp app = new DigitalWalletApp();
            app.Run();
        }
    }
}