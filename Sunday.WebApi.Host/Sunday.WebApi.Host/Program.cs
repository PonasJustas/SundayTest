using Sunday.WebApi.Host.Configuration;
using Topshelf;

namespace Sunday.WebApi.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {  
                c.RunAsLocalSystem();

                c.Service<WebApiManager>(s =>
                {
                    s.ConstructUsing(name => new WebApiManager());
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });

            });
        }
    }
}
