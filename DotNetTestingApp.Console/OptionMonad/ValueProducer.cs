
using System.Runtime;

namespace DotNetTestingApp.Console.OptionMonad
{
    public class ValueProducer
    { }

    public class OptionMonad(string name, Option<Result> result)
    {
        public string Name { get; } = name;
        public Option<Result> Result { get; } = result;
    }

    public class Result(int idx, string description)
    {
        public int index { get; set; } = idx;
        public string Description { get; set; } = description;
    }

    public class Option<T> where T : class
    {
        private readonly T? _value;

        private Option(T? value) => _value = value;

        public bool IsSome => _value is not null;

        public static Option<T> Some(T value) => new(value);

        public static Option<T> None() => new(default);

        public Option<TOut> Map<TOut>(Func<T, TOut> map) where TOut : class => _value is not null ? Option<TOut>.Some(map(_value)) : Option<TOut>.None();
        
        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bind) where TOut : class => _value is not null ? bind(_value) : Option<TOut>.None();

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) => _value is not null ? some(_value) : none();

        public Option<T> Filter(Func<T, bool> predicate) => _value is not null && predicate(_value) ? Some(_value) : None();

        public T ValueOrThrow() => _value ?? throw new InvalidOperationException("The value is not present");
    }
}