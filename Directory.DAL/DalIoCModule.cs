namespace Directory.DAL
{
    using Autofac;
    using Directory.DAL.Abstract.IRepositories;
    using Directory.DAL.Implement.Repositories;

    public sealed class DalIoCModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
        }

    }
}
