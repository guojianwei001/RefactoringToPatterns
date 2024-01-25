namespace RefactoringToPatterns.Builder.OpenFile
{
    internal class PlayGround
    {
        public static void Test()
        {
            var f = new File.OpenFile("foo.txt")
              .readOnly()
              .createIfNotExist()
              .appendWhenWriting()
              .blockSize(1024)
              .unbuffered()
              .exclusiveAccess()
              .build();

            Console.WriteLine(f);
        }
    }
}
