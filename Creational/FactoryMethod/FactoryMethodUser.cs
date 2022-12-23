using DesignPatternExamples.Creational.FactoryMethod.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.Creational.FactoryMethod;
public class FactoryMethodUser
{
    public void Use()
    {
        var useCase = new GeneralUserUseCase();
        useCase.SubscribeNewPlan(1000, new BasicPlanFactory());
    }
}
