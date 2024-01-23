namespace RefactoringToPatterns.Builder.Pizaa.Before
{
    public class PlayGround
    {
        public static void Test()
        {
            var pizza = new Pizza(12, true, true, true);

            Console.WriteLine(pizza);
        }
    }
}
