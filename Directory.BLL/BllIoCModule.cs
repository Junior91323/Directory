namespace Directory.BLL
{
    using Autofac;
    using Directory.BLL.Abstract.IServices;
    using Directory.BLL.Implement.Services;

    public sealed class BllIoCModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
        }

    }
}
