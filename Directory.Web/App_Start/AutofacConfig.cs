namespace Directory.Web.App_Start
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using System.Web.Mvc;

    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new BLL.BllIoCModule());
            builder.RegisterModule(new DAL.DalIoCModule());

            // Register controllers all at once
           // builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterControllers(Assembly.GetExecutingAssembly()); // MVC

            // Set the dependency resolver to be Autofac
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); // MVC
        }
    }
}