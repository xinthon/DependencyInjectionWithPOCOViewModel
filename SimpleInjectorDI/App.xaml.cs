using Common;
using DevExpress.Mvvm.POCO;
using SimpleInjector;
using System;
using System.Windows;

namespace SimpleInjectorDI;

public partial class App : Application
{
    private Container _container = new Container();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _container.RegisterSingleton(
            typeof(IDataStorage<Person>),
            typeof(PersonStorage));

        _container.RegisterSingleton(
            typeof(DetailViewModel),
            ViewModelSource.GetPOCOType(typeof(DetailViewModel)));

        _container.RegisterSingleton(
            typeof(IDetailViewModel),
            ViewModelSource.GetPOCOType(typeof(DetailViewModel)));

        _container.Register(
            typeof(CollectionViewModel),
            ViewModelSource.GetPOCOType(typeof(CollectionViewModel)));

        DISource.Resolver = Resolve;
    }

    private object Resolve(Type type, object key, string name)
    {
        if (type == null)
            return null;

        return _container.GetInstance(type);
    }
}
