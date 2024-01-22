namespace RefactoringToPatterns.Bridge.Algebraic_Hierarchy.After
{
    // decouple an abstraction from its implementation
    // The original code snippet is from https://tinf2.vub.ac.be/~dvermeir/c++/EuroPLoP98.html#AlgebraicHierarchy
    // I am not sure if the author's intention was fulfilled here. The original cpp code is abbreviated. 
    public class Number
    {
        public void add(Number n) => this.rep.add(n.rep);
        public void mul(Number n) => this.rep.mul(n.rep);

        public override string ToString() => this.rep?.ToString();

        public NumberRep rep;
    }

    class Complex : Number
    {
        public Complex(double r, double i)
        {
            this.rep = new ComplexRep(r, i);
        }
    }

    //------------------representation-------------------------
    public abstract class NumberRep
    {
        public abstract void add(NumberRep number);
        public abstract void mul(NumberRep n);
    }

    public class ComplexRep : NumberRep
    {
        private double rpart;
        private double ipart;

        public ComplexRep(double r, double i)
        {
            this.rpart = r;
            this.ipart = i;
        }

        public override void add(NumberRep n)
        {
            var complexRep = n as ComplexRep;
            this.rpart += complexRep.rpart;
            this.ipart += complexRep.ipart;
        }

        public override void mul(NumberRep n)
        {
            var complexRep = n as ComplexRep;
            var a = this.rpart * complexRep.rpart - this.ipart * complexRep.ipart;
            this.ipart = this.rpart * complexRep.ipart + this.ipart * complexRep.rpart;
            this.rpart = a;
        }

        public override string ToString()
        {
            return $"{this.rpart}+({this.ipart})i";
        }
    }
}
