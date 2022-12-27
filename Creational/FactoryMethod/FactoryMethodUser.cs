using GoFDesignPatternExamples.Creational.FactoryMethod.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFDesignPatternExamples.Creational.FactoryMethod;
public class FactoryMethodUser : IUser
{
    public void Use()
    {
        var useCase = new GeneralUserUseCase();
        useCase.SubscribeNewPlan(1000, new BasicPlanFactory());
    }
}
