namespace RefactoringToPatterns.Builder.OpenFile
{
    // https://isocpp.org/wiki/faq/ctors#named-ctor-idiom
    /*
     What is the “Named Parameter Idiom”?  
It’s a fairly useful way to exploit method chaining.

The fundamental problem solved by the Named Parameter Idiom is that C++ only supports positional parameters. 
For example, a caller of a function isn’t allowed to say, “Here’s the value for formal parameter xyz, 
and this other thing is the value for formal parameter pqr.” All you can do in C++ (and C and Java) is say, 
“Here’s the first parameter, here’s the second parameter, etc.” The alternative, called named parameters and
implemented in the language Ada, is especially useful if a function takes a large number of mostly default-able parameters.

Over the years people have cooked up lots of workarounds for the lack of named parameters in C and C++. One of these 
involves burying the parameter values in a string parameter then parsing this string at run-time. This is what’s 
done in the second parameter of fopen(), for example. Another workaround is to combine all the boolean parameters 
in a bit-map, then the caller or’s a bunch of bit-shifted constants together to produce the actual parameter. 
This is what’s done in the second parameter of open(), for example. These approaches work, but the following 
technique produces caller-code that’s more obvious, easier to write, easier to read, and is generally more elegant.

The idea, called the Named Parameter Idiom, is to change the function’s parameters to methods of a newly created class, 
where all these methods return *this by reference. Then you simply rename the main function into a parameterless 
“do-it” method on that class.

We’ll work an example to make the previous paragraph easier to understand.

The example will be for the “open a file” concept. Let’s say that concept logically requires a parameter for the file’s name, 
and optionally allows parameters for whether the file should be opened read-only vs. read-write vs. write-only, whether 
or not the file should be created if it doesn’t already exist, whether the writing location should be at the end (“append”) 
or the beginning (“overwrite”), the block-size if the file is to be created, whether the I/O is buffered or non-buffered, 
the buffer-size, whether it is to be shared vs. exclusive access, and probably a few others. If we implemented this concept 
using a normal function with positional parameters, the caller code would be very difficult to read: there’d be as many as 8 
positional parameters, and the caller would probably make a lot of mistakes. So instead we use the Named Parameter Idiom.
     */
    public class File
    {
        private readonly string _parameters;

        public File(OpenFile openFile)
        {
            _parameters = $"name:{openFile._filename} readonly={openFile._readonly} createIfNotExist={openFile._createIfNotExist} " +
                          $"blockSize:{openFile._blockSize} append={openFile._append}, buffered={openFile._buffered}, " +
                          $"exclusiveAccess={openFile._exclusiveAccess}";
        }

        public override string ToString()
        {
            return _parameters;
        }

        public class OpenFile
        {
            protected internal string _filename;
            protected internal bool _readonly; // defaults to false [for example]
            protected internal bool _createIfNotExist; // defaults to false [for example]
            protected internal uint _blockSize = 4096; // defaults to 4096 [for example]
            protected internal bool _append;
            protected internal bool _buffered = true;
            protected internal bool _exclusiveAccess;

            public OpenFile(string filename)
            {
                this._filename = filename;
            }

            public OpenFile readOnly()
            {
                _readonly = true;
                return this;
            }

            public OpenFile readwrite()
            {
                _readonly = false;
                return this;
            }

            public OpenFile createIfNotExist()
            {
                _createIfNotExist = true;
                return this;
            }

            public OpenFile blockSize(uint nbytes)
            {
                _blockSize = nbytes;
                return this;
            }

            public OpenFile appendWhenWriting()
            {
                _append = true;
                return this;
            }

            public OpenFile unbuffered()
            {
                _buffered = false;
                return this;
            }

            public OpenFile exclusiveAccess()
            {
                _exclusiveAccess = false;
                return this;
            }

            public File build()
            {
                return new File(this);
            }
        }
    }
}
