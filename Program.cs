using DesignPatternExamples.Creational.SingletonPattern;
using DesignPatternExamples.Creational.FactoryMethod;
using DesignPatternExamples.Behavioural.TemplateMethod;
using DesignPatternExamples;
using DesignPatternExamples.Creational.BuilderPattern;

namespace DesignPatternExamples;
class Program
{
	private static void Main()
	{
		var user = new BuilderUser();
		user.Use();
	}
}