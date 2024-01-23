namespace RefactoringToPatterns.Builder.Pizaa.Before
{
    public class Pizza
    {
        private int size;
        private bool cheese;
        private bool pepperoni;
        private bool bacon;

        public Pizza(int size)
        {
            this.size = size;
        }

        public Pizza(int size, bool cheese) : this(size)
        {
            this.cheese = cheese;
        }

        public Pizza(int size, bool cheese, bool pepperoni) : this(size, cheese)
        {
            this.pepperoni = pepperoni;
        }

        public Pizza(int size, bool cheese, bool pepperoni, bool bacon) : this(size, cheese, pepperoni)
        {
            this.bacon = bacon;
        }

        public override string ToString()
        {
            return $"size:{size} cheese:{cheese} pepperoni:{pepperoni} bacon:{bacon}";
        }
    }
}
