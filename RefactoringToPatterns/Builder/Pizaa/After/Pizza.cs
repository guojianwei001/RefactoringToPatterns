namespace RefactoringToPatterns.Builder.Pizaa.After
{
    public class Pizza
    {
        private int size;
        private bool cheese;
        private bool pepperoni;
        private bool bacon;

        internal class Builder
        {
            //required
            protected internal readonly int _size;

            //optional
            protected internal bool _cheese = false;
            protected internal bool _pepperoni = false;
            protected internal bool _bacon = false;

            public Builder(int size)
            {
                _size = size;
            }

            public Builder cheese(bool value)
            {
                _cheese = value;
                return this;
            }

            public Builder pepperoni(bool value)
            {
                _pepperoni = value;
                return this;
            }

            public Builder bacon(bool value)
            {
                _bacon = value;
                return this;
            }

            public Pizza build()
            {
                return new Pizza(this);
            }
        }

        private Pizza(Builder builder)
        {
            size = builder._size;
            cheese = builder._cheese;
            pepperoni = builder._pepperoni;
            bacon = builder._bacon;
        }

        public override string ToString()
        {
            return $"size:{size} cheese:{cheese} pepperoni:{pepperoni} bacon:{bacon}";
        }
    }
}
