using DesignPatternExamples.Creational.SingletonPattern;
using DesignPatternExamples.Creational.FactoryMethod;
using DesignPatternExamples.Behavioural.TemplateMethod;
using DesignPatternExamples;
namespace DesignPatternExamples;
class Program
{
	private static void Main()
	{
		var user = new FactoryMethodUser();
		user.Use();
	}
}