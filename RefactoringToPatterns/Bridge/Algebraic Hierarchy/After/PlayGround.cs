namespace RefactoringToPatterns.Bridge.Algebraic_Hierarchy.After
{
    public class PlayGround
    {
        public static void Test()
        {
            var number = new Complex(2, 3);

            number.mul(new Complex(2,-3));

            Console.WriteLine(number);
        }
    }
}
