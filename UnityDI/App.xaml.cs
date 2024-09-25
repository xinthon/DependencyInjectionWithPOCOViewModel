using Common;
using System;
using System.Windows;
using Unity;
using DevExpress.Mvvm.POCO;

namespace UnityDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IUnityContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = new UnityContainer();

            Container.RegisterSingleton(
                typeof(IDataStorage<Person>), 
                typeof(PersonStorage));

            Container.RegisterSingleton(
                typeof(DetailViewModel), 
                ViewModelSource.GetPOCOType(typeof(DetailViewModel)));

            Container.RegisterSingleton(
                typeof(IDetailViewModel), 
                typeof(DetailViewModel));

            Container .RegisterType(
                typeof(CollectionViewModel),
                ViewModelSource.GetPOCOType(typeof(CollectionViewModel)));

            DISource.Resolver = Resolve;
        }

        object Resolve(Type type, object key, string name)
        {
            if(type == null)
                return null;

            if(name != null)
                return Container.Resolve(type, name);

            return Container.Resolve(type);
        }
    }
}
