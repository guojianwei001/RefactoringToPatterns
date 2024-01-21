namespace RefactoringToPatterns.Visitor.TextExtractor.Before
{
    public class PlayGround
    {
        public static void Test()
        {
            Console.WriteLine(new TextExtractorVisitor().extracText());
        }
    }
}
