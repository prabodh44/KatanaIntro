using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaIntro
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";
            using (WebApp.Start<StartUp>(uri))
            {
                Console.WriteLine("Started!!");
                Console.ReadKey();
                Console.WriteLine("Stopping");
            }


        }
    }

    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
            //this method takes an argument which is the appFunc for next
            //component in the pipeline


        }
    }

    //class responsible to write Hello World to each request
    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }
        public Task Invoke (IDictionary<string, object> environment)
        {
            //a null cannot be returned in this method
            //it has to either return a task or an Exception

            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("hello !!");
            }
        }
    }
}
