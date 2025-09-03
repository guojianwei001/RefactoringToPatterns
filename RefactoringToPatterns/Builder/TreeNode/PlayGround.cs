namespace RefactoringToPatterns.Builder.TreeNode;

public class PlayGround
{
    public static void Test()
    {
        // 1. source Markdown text
        string markdownInput = @"
- CEO (name: Alice, office: A-101)
  - CTO (name: Bob)
    - Engineering Lead (name: Charlie)
      - Frontend Developer
      - Backend Developer
    - DevOps Lead (name: David)
  - CFO (name: Eve)
    - Accountant
- COO (name: Frank)
  - Operations Manager
";

        Console.WriteLine("--- Parsing Markdown Tree ---");

        // 2. parser
        var companyTree = MarkdownTreeParser.Parse(markdownInput);

        Console.WriteLine("Parsing complete!");
        Console.WriteLine("");

        // 3. print for verify
        if (companyTree != null)
        {
            Console.WriteLine("--- Resulting Tree Object ---");
            Console.WriteLine(companyTree.PrintTree());
        }
        else
        {
            Console.WriteLine("Failed to parse the tree.");
        }
    }
}