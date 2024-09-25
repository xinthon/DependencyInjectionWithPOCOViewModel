using Common;
using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MicrosoftDI
{
    public partial class App : Application
    {
        private ServiceProvider? ServiceProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            ServiceProvider = new ServiceCollection()
                .AddSingleton(typeof(IDataStorage<Person>), typeof(PersonStorage))
                .AddSingleton(typeof(DetailViewModel), ViewModelSource.GetPOCOType(typeof(DetailViewModel)))
                .AddSingleton(typeof(IDetailViewModel), (sp) => sp.GetRequiredService<DetailViewModel>())
                .AddSingleton(typeof(CollectionViewModel), ViewModelSource.GetPOCOType(typeof(CollectionViewModel)))
                .BuildServiceProvider();

            DISource.Resolver = (Type type, object key, string name) =>
            {
                return ServiceProvider!.GetRequiredService(type);
            };
        }
    }
}
