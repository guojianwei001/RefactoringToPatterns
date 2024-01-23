Code example is from Aaron https://stackoverflow.com/a/1953567/10131074
post url: https://stackoverflow.com/questions/328496/when-would-you-use-the-builder-pattern

As Joshua Bloch states in Effective Java, 2nd Edition:
```
The builder pattern is a good choice when designing classes whose constructors or static factories would have more than a handful of parameters.
```

We've all at some point encountered a class with a list of constructors where each addition adds a new option parameter:
```
Pizza(int size) { ... }
Pizza(int size, boolean cheese) { ... }
Pizza(int size, boolean cheese, boolean pepperoni) { ... }
Pizza(int size, boolean cheese, boolean pepperoni, boolean bacon) { ... }
```

The problem with this pattern is that once constructors are 4 or 5 parameters long it becomes 
difficult to remember the required order of the parameters as well as what particular constructor 
you might want in a given situation.

One alternative you have to the Telescoping Constructor Pattern is the JavaBean Pattern where 
you call a constructor with the mandatory parameters and then call any optional setters after:
```
Pizza pizza = new Pizza(12);
pizza.setCheese(true);
pizza.setPepperoni(true);
pizza.setBacon(true);
```

The problem here is that because the object is created over several calls it may be in an inconsistent 
state partway through its construction. The created Pizza is mutable.