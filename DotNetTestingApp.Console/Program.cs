using DotNetTestingApp.Console.OptionMonad;

var result1 = new Result(1, "Description1");
var result2 = new Result(2, "Description2");

var option1 = new OptionMonad("Option1", Option<Result>.Some(result1));
var option2 = new OptionMonad("Option2", Option<Result>.None());