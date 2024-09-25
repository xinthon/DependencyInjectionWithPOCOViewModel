using Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Windows;
using DevExpress.Mvvm.POCO;

namespace PrismDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(
                typeof(IDataStorage<Person>),
                typeof(PersonStorage));

            containerRegistry.RegisterManySingleton(
                ViewModelSource.GetPOCOType(typeof(DetailViewModel)),
                    typeof(DetailViewModel),
                    typeof(IDetailViewModel));

            containerRegistry.Register(
                typeof(CollectionViewModel),
                ViewModelSource.GetPOCOType(typeof(CollectionViewModel)));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                if (viewType == typeof(MainView))
                    return ViewModelSource.GetPOCOType(typeof(CollectionViewModel));

                if (viewType == typeof(DetailView))
                    return ViewModelSource.GetPOCOType(typeof(DetailViewModel));

                throw new NotSupportedException();
            });
        }

        protected override Window CreateShell() => Container.Resolve<PrismDI.MainView>();
    }
}
