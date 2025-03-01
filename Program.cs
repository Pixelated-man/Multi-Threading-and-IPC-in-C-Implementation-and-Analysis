using System;
using System.Threading;

class BankAccount
{
    private int balance;
    private readonly object lockObject = new object(); // Mutex for thread safety

    public BankAccount(int initialBalance)
    {
        balance = initialBalance;
    }

    public int Balance
    {
        get { return balance; }
    }

    public void Withdraw(int amount, string customerName)
    {
        lock (lockObject)
        {
            if (balance >= amount)
            {
                Console.WriteLine($"{customerName} is withdrawing {amount}");
                balance -= amount;
                Console.WriteLine($"{customerName} completed withdrawal. New balance: {balance}");
            }
            else
            {
                Console.WriteLine($"{customerName} cannot withdraw {amount}. Insufficient balance: {balance}");
            }
        }
    }

    public void Deposit(int amount, string customerName)
    {
        lock (lockObject)
        {
            Console.WriteLine($"{customerName} is depositing {amount}");
            balance += amount;
            Console.WriteLine($"{customerName} completed deposit. New balance: {balance}");
        }
    }

    public void Transfer(BankAccount targetAccount, int amount, string customerName)
    {
        Console.WriteLine($"{customerName} is attempting to transfer {amount}");

        // Phase 4: Deadlock Resolution - Use ordered locking to prevent deadlocks
        var firstLock = lockObject;
        var secondLock = targetAccount.lockObject;

        if (firstLock.GetHashCode() < secondLock.GetHashCode())
        {
            lock (firstLock)
            {
                lock (secondLock)
                {
                    PerformTransfer(targetAccount, amount, customerName);
                }
            }
        }
        else
        {
            lock (secondLock)
            {
                lock (firstLock)
                {
                    PerformTransfer(targetAccount, amount, customerName);
                }
            }
        }
    }

    private void PerformTransfer(BankAccount targetAccount, int amount, string customerName)
    {
        if (balance >= amount)
        {
            balance -= amount;
            targetAccount.balance += amount;
            Console.WriteLine($"{customerName} transferred {amount}. New balance: {balance}");
        }
        else
        {
            Console.WriteLine($"{customerName} cannot transfer {amount}. Insufficient balance: {balance}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount account1 = new BankAccount(1000);
        BankAccount account2 = new BankAccount(1000);

        // Phase 1: Basic Thread Operations - Create threads for concurrent transactions
        Thread customer1 = new Thread(() => PerformTransactions(account1, account2, "Customer1"));
        Thread customer2 = new Thread(() => PerformTransactions(account2, account1, "Customer2"));

        // Start the threads
        customer1.Start();
        customer2.Start();

        // Wait for threads to complete
        customer1.Join();
        customer2.Join();

        Console.WriteLine("All transactions completed.");
        Console.WriteLine($"Final balance of Account 1: {account1.Balance}");
        Console.WriteLine($"Final balance of Account 2: {account2.Balance}");
    }

    static void PerformTransactions(BankAccount sourceAccount, BankAccount targetAccount, string customerName)
    {
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int amount = random.Next(50, 200);

            // Phase 2: Resource Protection - Use locks for shared resource access
            sourceAccount.Withdraw(amount, customerName);

            // Phase 3: Deadlock Creation - Simulate deadlock by transferring between accounts
            sourceAccount.Transfer(targetAccount, amount, customerName);

            Thread.Sleep(random.Next(100, 300)); // Simulate some delay
        }
    }
}