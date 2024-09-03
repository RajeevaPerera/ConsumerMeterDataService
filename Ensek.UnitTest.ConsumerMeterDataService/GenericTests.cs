using System.Threading.Channels;
using FluentAssertions;
using NSubstitute;

namespace Ensek.UnitTest.ConsumerMeterDataService;

public static class GenericTests
{
    public static void TestsConstructor<TClass>() where TClass : class
    {
        var constructors = typeof(TClass).GetConstructors();

        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var constructorArgs = parameters.Select(x => Substitute.For(new[] { x.ParameterType }, Array.Empty<object>())).ToArray();
            var names = parameters.Select(x => x.Name).ToArray();

            for (int index= 0; index < constructorArgs.Length; index++)
            {
                var clone = constructorArgs.ToArray();
                
                clone[index] = null;
                
                Action action = () => constructor.Invoke(clone);

                try
                {
                    action.Should().Throw<Exception>()
                        .WithInnerException<ArgumentNullException>()
                        .Where(x=>x.ParamName== names[index]);
                }
                catch (Exception)
                {
                    throw new Exception("Did not throw ArgumentNullException for " + names[index]);
                }
            }
            
        }
    }
}