using FactoryDI.Library;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryDI
{
    public class Program
    {
        protected Program() { }
        static void Main(string[] args)
        {
            var _serviceManager = ServiceManager.CreateServiceProvider();

            var service = _serviceManager.GetService<LabelGenService>()!;
            var label = service.Generate();
            Console.WriteLine(label);
        }
    }
}
