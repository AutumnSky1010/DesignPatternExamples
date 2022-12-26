using DesignPatternExamples.Creational.SingletonPattern;
using DesignPatternExamples.Creational.FactoryMethod;
using DesignPatternExamples.Behavioural.TemplateMethod;
using DesignPatternExamples;
using DesignPatternExamples.Creational.BuilderPattern;
using DesignPatternExamples.Creational.AbstractFactory;

namespace DesignPatternExamples;
class Program
{
	private static void Main()
	{
		var user = new AbstractFactoryUser();
		user.Use();
	}
}