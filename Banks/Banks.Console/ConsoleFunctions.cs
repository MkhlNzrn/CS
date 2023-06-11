using System.Diagnostics;
using Spectre.Console;

namespace Banks;

public class ConsoleFunctions
{
    internal static void ShowMenu(CentralBank centralBank)
    {
        Bank bank = null;
        Client client = null;
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var task2 = ctx.AddTask("[bold blue]Загрузка банковской системы[/]");

                while (!ctx.IsFinished)
                {
                    task2.Increment(0.000009);
                }
            });
        var table = new Table();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddColumn("[Purple] Banks [/]");
                ctx.Refresh();
                Thread.Sleep(1000);
            });
        AnsiConsole.WriteLine();

        AnsiConsole.Status()
            .Start("[yellow]Загружается...[/]", ctx =>
            {
                AnsiConsole.MarkupLine("[yellow]Уже почти[/]");
                Thread.Sleep(2500);

                ctx.Status("[green]Еще чуть-чуть[/]");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));

                AnsiConsole.MarkupLine("[green]Пара секунд[/]");
                Thread.Sleep(4000);
            });
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup("[bold red]Меню:[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[bold blue]1. Выберите банк[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[bold blue]2. Клиенты[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[bold blue]3. Счет[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[bold blue]4. Транзакция[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[bold blue]5. Выход[/]");
            AnsiConsole.WriteLine();
            if (bank != null)
                Console.WriteLine("Выбрали банк " + bank.Name);

            if (client != null)
            {
                Console.WriteLine(string.Format("Выбрали клиента Id={0}. ФИО: {1}", client.Id, client.FullName));

                Debug.Assert(bank != null, nameof(bank) + " != null");
                if (bank.IsAccount(client))
                    Console.WriteLine(string.Format("Баланс {0}.", bank.GetClientBalanceInfo(client)));
            }

            string input = Console.ReadLine();
            if (int.TryParse(input, out int menu))
            {
                if (menu == 5)
                    break;

                switch (menu)
                {
                    case 1:
                        bank = centralBank.FindBank(GetBanks(centralBank));
                        break;
                    case 2:
                        client = GetClient(bank);
                        break;
                    case 3:
                        ShowAccountInfo(bank, client);
                        break;
                    case 4:
                        SetTransaction(bank, client, centralBank);
                        break;
                    default:
                        continue;
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static string GetBanks(CentralBank centralBank)
    {
        Console.Clear();

        Console.WriteLine("Банки:");
        List<string> banks = centralBank.GetBanks();
        for (int i = 0; i < banks.Count; i++)
        {
            Console.WriteLine(string.Format("{0}. {1}", i + 1, banks[i]));
        }

        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int menu) && menu >= 0 && menu <= banks.Count)
            {
                return banks[menu - 1];
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static Client GetClient(Bank bank)
    {
        if (bank == null)
        {
            Console.WriteLine("Необходимо выбрать банк. Нажмите для продолжен.");
            Console.ReadLine();
            return null;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Клиенты:");
            Console.WriteLine("0. Новый клиент:");
            List<Client> clients = bank.GetClients();
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. ID={1} ФИО {2}", i + 1, clients[i].Id, clients[i].FullName));
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int menu) && menu >= 0 && menu <= clients.Count)
                {
                    if (menu == 0)
                    {
                        return bank.AddNewClient();
                    }
                    else
                    {
                        return clients[menu - 1];
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат ввода");
                }
            }
        }
    }

    private static void ShowAccountInfo(Bank bank, Client client)
    {
        if (bank == null || client == null)
        {
            Console.WriteLine("Необходимо выбрать банк и клиента. Нажмите для продолжен.");
            Console.ReadLine();
        }
        else
        {
            Console.Clear();
            Console.WriteLine(string.Format("Клиент ID={0} ФИО {1}", client.Id, client.FullName));

            if (!bank.IsAccount(client))
            {
                OpenAccount(bank, client);
            }
            else
            {
                Console.WriteLine("Счет создан. Нажмите для продолжен.");
                Console.ReadLine();
            }
        }
    }

    private static void OpenAccount(Bank bank, Client client)
    {
        Console.WriteLine("1. Дебетовый счет:");
        Console.WriteLine("2. Депозитный счет :");
        Console.WriteLine("3. Кредитный счет :");

        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int menu) && menu >= 1 && menu <= 3)
            {
                if (menu == 1)
                {
                    bank.OpenNewDebitAccount(client, InputMoney(), InputDate());
                }
                else if (menu == 2)
                {
                    bank.OpenNewDepositAccount(client, InputMoney(), InputDate());
                }
                else
                {
                    bank.OpenNewCreditAccount(client, InputMoney(), InputDate());
                }

                break;
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static double InputMoney()
    {
        while (true)
        {
            Console.WriteLine("Введите сумму");
            if (double.TryParse(Console.ReadLine(), out double money))
            {
                return money;
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static DateTime InputDate()
    {
        while (true)
        {
            Console.WriteLine("Введите дату открыт.");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static string InputBank(Bank current, CentralBank centralBank)
    {
        while (true)
        {
            Console.WriteLine("Текущий  банк " + current.Name);
            Console.WriteLine("Выберите банк:");
            List<string> banks = centralBank.GetBanks();
            for (int i = 0; i < banks.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, banks[i]));
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int menu) && menu >= 0 && menu <= banks.Count)
                {
                    return banks[menu - 1];
                }
                else
                {
                    Console.WriteLine("Неверный формат ввода");
                }
            }
        }
    }

    private static Client InputClient(string toBank, CentralBank centralBank)
    {
        Console.WriteLine("Клиенты:");
        List<Client> clients = centralBank.FindBank(toBank).GetClients();
        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine(string.Format("{0}. ID={1} ФИО {2}", i + 1, clients[i].Id, clients[i].FullName));
        }

        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int menu) && menu >= 0 && menu <= clients.Count)
            {
                return clients[menu - 1];
            }
            else
            {
                Console.WriteLine("Неверный формат ввода");
            }
        }
    }

    private static void SetTransaction(Bank bank, Client client, CentralBank centralBank)
    {
        Console.Clear();

        if (bank == null || (client == null && !bank.IsAccount(client)))
        {
            Console.WriteLine("Необходимо выбрать банк и клиента. Нажмите для продолжен.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("1. Снять:");
            Console.WriteLine("2. Пополнить:");
            Console.WriteLine("3. Перевод :");

            Console.WriteLine("Список транзакции:");
            foreach (var transaction in bank.GetTransactions(client))
            {
                Console.WriteLine("№{0}. Сумма: {1}", transaction.Number, transaction.Amount);
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int menu) && menu >= 1 && menu <= 3)
                {
                    if (menu == 1)
                    {
                        bank.Withdraw(client, InputMoney(), InputDate());
                    }
                    else if (menu == 2)
                    {
                        bank.Deposit(client, InputMoney(), InputDate());
                    }
                    else
                    {
                        string toBank = InputBank(bank, centralBank);
                        bank.Transfer(client, InputClient(toBank, centralBank), InputMoney(), toBank, InputDate());
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат ввода");
                }
            }
        }
    }
}