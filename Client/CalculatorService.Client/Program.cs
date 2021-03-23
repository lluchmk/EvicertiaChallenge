using CalculatorService.Server;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorService.Client
{
    public class Program
    {
        private const string TRACKID_HEADER = "X-Evi-Tracking-Id";
        private static string trackId;

        private static HttpClient HttpClient;
        private static CalculatorServiceClient CalculatorServiceClient;

        static async Task Main(string[] args)
        {
            HttpClient = new HttpClient();
            CalculatorServiceClient = new CalculatorServiceClient("https://localhost:5001", HttpClient);

            string selection;
            do
            {
                selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What you want to do?")
                        .AddChoices(Choices.ALL)
                    );

                try
                {
                    switch (selection)
                    {
                        case Choices.ADD:
                            await Add();
                            break;
                        case Choices.SUB:
                            await Sub();
                            break;
                        case Choices.MULT:
                            await Mult();
                            break;
                        case Choices.DIV:
                            await Div();
                            break;
                        case Choices.SQRT:
                            await Sqrt();
                            break;
                        case Choices.JOURNAL_SEE:
                            await JournalSee();
                            break;
                        case Choices.JOURNAL_START:
                            JournalStart();
                            break;
                        case Choices.JOURNAL_END:
                            JournalEnd();
                            break;
                        default:
                            break;
                    }
                }
                catch (ApiException)
                {
                    AnsiConsole.MarkupLine("[red]Seems that something went wrong[/]");
                }
            }
            while (selection != Choices.EXIT);
        }

        private static async Task Add()
        {
            Console.Clear();

            var addends = new List<int>();
            int? addend;
            do
            {
                addend = PromptSelection("Enter an integer, or 'X' to end:");
                if (addend.HasValue)
                {
                    addends.Add(addend.Value);
                }
            }
            while (addend.HasValue);

            if (!addends.Any())
            {
                AnsiConsole.MarkupLine($"There are no addends, no operation performed\n");
                return;
            }

            var addRequest = new AddRequest { Addends = addends };
            var response = await CalculatorServiceClient.AddAsync(addRequest);

            AnsiConsole.MarkupLine($"The sum of the addends is: {response.Sum}\n");
        }

        private static async Task Sub()
        {
            Console.Clear();

            var minuend = PromptSelection("Enter the minuend, or 'X' to end:");
            if (minuend is null)
            {
                AnsiConsole.MarkupLine($"There is no minuend, no operation performed\n");
                return;
            }

            var substrahend = PromptSelection("Enter the substrahend, or 'X' to end:");
            if (minuend is null)
            {
                AnsiConsole.MarkupLine($"There is no substrahend, no operation performed\n");
                return;
            }

            var subRequest = new SubRequest { Minuend = minuend.Value, Subtrahend = substrahend.Value };
            var response = await CalculatorServiceClient.SubAsync(subRequest);

            AnsiConsole.MarkupLine($"The difference is: {response.Difference}\n");
        }

        private static async Task Mult()
        {
            Console.Clear();

            var factors = new List<int>();
            int? factor;
            do
            {
                factor = PromptSelection("Enter an integer, or 'X' to end:");
                if (factor.HasValue)
                {
                    factors.Add(factor.Value);
                }
            }
            while (factor.HasValue);

            if (!factors.Any())
            {
                AnsiConsole.MarkupLine($"There are no factors, no operation performed\n");
                return;
            }

            var multRequest = new MultRequest { Factors = factors };
            var response = await CalculatorServiceClient.MultAsync(multRequest);

            AnsiConsole.MarkupLine($"The product of the factors is: {response.Product}\n");
        }

        private static async Task Div()
        {
            Console.Clear();

            var dividend = PromptSelection("Enter the dividend, or 'X' to end:");
            if (dividend is null)
            {
                AnsiConsole.MarkupLine($"There is no dividend, no operation performed\n");
                return;
            }

            var divisor = PromptSelection("Enter the divisor, or 'X' to end:");
            if (dividend is null)
            {
                AnsiConsole.MarkupLine($"There is no divisor, no operation performed\n");
                return;
            }

            var divRequest = new DivRequest { Dividend = dividend.Value, Divisor = divisor.Value };
            var response = await CalculatorServiceClient.DivAsync(divRequest);

            AnsiConsole.MarkupLine($"The quotient is: {response.Quotient} and the remainder is {response.Remainder}\n");
        }

        private static async Task Sqrt()
        {
            Console.Clear();

            var number = PromptSelection("Enter the number, or 'X' to cancel:");
            if (number is null)
            {
                AnsiConsole.MarkupLine($"There is no number, no operation performed\n");
                return;
            }

            var sqrtRequest = new SqrtRequest { Number = number.Value };
            var response = await CalculatorServiceClient.SqrtAsync(sqrtRequest);

            AnsiConsole.MarkupLine($"The sqrt is: {response.Square}\n");
        }

        private static async Task JournalSee()
        {
            Console.Clear();

            if (string.IsNullOrWhiteSpace(trackId))
            {
                AnsiConsole.MarkupLine("You are not tracking your journal");
                if (AnsiConsole.Confirm("Would you like to start tracking it?"))
                {
                    JournalStart();
                }
                else
                {
                    return;
                }
            }

            var journalRequest = new JournalRequest { Id = trackId };
            var response = await CalculatorServiceClient.QueryAsync(journalRequest);

            AnsiConsole.MarkupLine($"Operations for the tracking id '{trackId}':");

            var table = new Table();
            table.AddColumn("Operation");
            table.AddColumn("Calculation");
            table.AddColumn("Date");

            foreach (var operation in response.Operations)
            {
                table.AddRow(operation.Operation, operation.Calculation, operation.Date.ToString());
            }

            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("Press any key to exit");
            Console.ReadKey();
            Console.Clear();
        }

        private static void JournalStart()
        {
            if (!string.IsNullOrWhiteSpace(trackId))
            {
                AnsiConsole.MarkupLine($"You are alredy tracking your journal with the ID '{trackId}'");
                if (!AnsiConsole.Confirm("Do you want to overwrite it?"))
                {
                    return;
                }
            }

            trackId = AnsiConsole.Ask<string>("Please enter your journal id: ");

            HttpClient.DefaultRequestHeaders.Remove(TRACKID_HEADER);
            HttpClient.DefaultRequestHeaders.Add(TRACKID_HEADER, trackId);
        }

        private static void JournalEnd()
        {
            Console.Clear();

            if (string.IsNullOrWhiteSpace(trackId))
            {
                AnsiConsole.MarkupLine("You are not tracking your journal");
                return;
            }

            trackId = null;
            HttpClient.DefaultRequestHeaders.Remove(TRACKID_HEADER);
            AnsiConsole.MarkupLine("Your journal is no longer being stored\n");
        }

        private static int? PromptSelection(string promptText)
        {
            int number = 0;
            var selection = AnsiConsole.Prompt(
                new TextPrompt<string>(promptText)
                    .Validate(sel =>
                    {
                        return sel.ToUpper() switch
                        {
                            "X" => ValidationResult.Success(),
                            _ when int.TryParse(sel, out number) => ValidationResult.Success(),
                            _ => ValidationResult.Error("Enter a valid integer or 'X' to end")
                        };
                    }));

            if (selection.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return number;
        }
    }
}
