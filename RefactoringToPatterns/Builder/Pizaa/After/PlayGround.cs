namespace RefactoringToPatterns.Builder.Pizaa.After
{
    public class PlayGround
    {
        public static void Test()
        {
            var pizza = new Pizza.Builder(12)
                .cheese(true)
                .pepperoni(true)
                .bacon(true)
                .build();

            Console.WriteLine(pizza);
        }
    }
}
