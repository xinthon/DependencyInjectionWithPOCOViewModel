using Autofac;
using Common;
using DevExpress.Mvvm.POCO;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AutofacDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = BuildUpContainer();
            DISource.Resolver = Resolve;
        }

        private object Resolve(Type type, object key, string name)
        {
            if (type == null)
                return null;

            if (key != null)
                return Container.ResolveKeyed(key, type);

            if (name != null)
                return Container.ResolveNamed(name, type);

            return Container.Resolve(type);
        }

        private static IContainer BuildUpContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(PersonStorage))
                .As(typeof(IDataStorage<Person>))
                .SingleInstance();

            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(DetailViewModel)))
                .As(typeof(IDetailViewModel), typeof(DetailViewModel))
                .SingleInstance();

            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(CollectionViewModel)))
                .As(typeof(CollectionViewModel));

            return builder.Build();
        }
    }
}
