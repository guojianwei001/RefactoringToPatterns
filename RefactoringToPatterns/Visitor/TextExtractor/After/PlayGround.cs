namespace RefactoringToPatterns.Visitor.TextExtractor.After
{
    public class PlayGround
    {
        public static void Test()
        {
            Console.WriteLine(new TextExtractorVisitor().extracText());
        }
    }
}
