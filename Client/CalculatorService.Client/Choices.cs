namespace CalculatorService.Client
{
    public static class Choices
    {
        public const string ADD = "Add";
        public const string SUB = "Sub";
        public const string MULT = "Mult";
        public const string DIV = "Div";
        public const string SQRT = "Sqrt";
        public const string JOURNAL_SEE = "See your journal";
        public const string JOURNAL_START = "Set you journal id";
        public const string JOURNAL_END = "Remove you journal id";
        public const string EXIT = "Exit";

        public static readonly string[] ALL = new[]
        {
            ADD,
            SUB,
            MULT,
            DIV,
            SQRT,
            JOURNAL_SEE,
            JOURNAL_START,
            JOURNAL_END,
            EXIT
        };
    }
}
