using RefactoringToPatterns.Builder.TreeNode;

namespace RefactoringToPatterns.Builder.TreeNode;

public class PlayGround
{
    public static void Test()
    {
        // 1. 我们的源 Markdown 文本
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

        // 2. 调用解析器
        TreeNode<string> companyTree = MarkdownTreeParser.Parse(markdownInput);

        Console.WriteLine("Parsing complete!");
        Console.WriteLine("");

        // 3. 打印构建好的树对象进行验证
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