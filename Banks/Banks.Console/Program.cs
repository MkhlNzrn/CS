using Banks.Accounts;
using Spectre.Console;

namespace Banks;

public static class Program
{
    private static void Main(string[] args)
    {
        CentralBank centralBank = CentralBank.GetCentralBank();

        Bank bank = centralBank.AddNewBank("Bank 1", 8, 30000, 4, new NewDepositCalc());
        AnsiConsole.Markup(
            "[Bold blue]Приветствую тебя! Для взаимодействия с банками зарегестрируй первого пользователя[/]");
        AnsiConsole.WriteLine();
        Client client = new Client(1);

        ConsoleFunctions.ShowMenu(centralBank);
    }
}