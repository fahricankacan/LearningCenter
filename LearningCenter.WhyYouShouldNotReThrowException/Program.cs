// See https://aka.ms/new-console-template for more information
using LearningCenter.WhyYouShouldNotReThrowException;

try
{
    Human human = new Human();
    human.CheckNameLenght(string.Empty);
}
catch (Exception ex)
{
    Console.WriteLine(ex.StackTrace.ToString());
}