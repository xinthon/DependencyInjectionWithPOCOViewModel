using Common;
using DevExpress.Mvvm.POCO;
using Ninject.Modules;
using Ninject;
using System.Windows;
using System;

namespace NinjectDI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IKernel Kernel { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        Kernel = new StandardKernel(new MyModule());

        DISource.Resolver = Resolve;
    }

    private object Resolve(Type type, object key, string name)
    {
        if(type == null)
            return null;

        if(name != null)
            return Kernel.Get(type, name);

        return Kernel.Get(type);
    }
}

public class MyModule : NinjectModule
{
    public override void Load()
    {
        Bind(typeof(IDataStorage<Person>))
            .To(typeof(PersonStorage))
            .InSingletonScope();

        Bind(typeof(IDetailViewModel), typeof(DetailViewModel))
            .To(ViewModelSource.GetPOCOType(typeof(DetailViewModel)))
            .InSingletonScope();

        Bind(typeof(CollectionViewModel))
            .To(ViewModelSource
            .GetPOCOType(typeof(CollectionViewModel)));
    }
}
