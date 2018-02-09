

namespace BDCompany
{
    using Autofac;

    using BDCompany.ViewModels;
    using BDCompany.Views;

    using TinyLittleMvvm;

    /// <summary>
    /// The Shell interface.
    /// </summary>
    public interface IShell
    {
    }

    /// <inheritdoc />
    /// <summary>
    /// The app bootstrapper.
    /// </summary>
    internal class AppBootstrapper : BootstrapperBase<MainViewModel>
    {
        /// <summary>
        /// The configure container.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainView>();

            builder.RegisterType<BeerCalculatorViewModel>();
            builder.RegisterType<BeerCalculatorView>();

            builder.RegisterType<OfficialNewsView>();
            builder.RegisterType<BDOPlannerView>();
            builder.RegisterType<FammesMapView>();

            builder.RegisterType<SampleDialogView>().InstancePerDependency().AsSelf();
            builder.RegisterType<SampleDialogViewModel>().InstancePerDependency().AsSelf();

            builder.RegisterType<SettingsFlyoutView>().InstancePerDependency().AsSelf();
            builder.RegisterType<SettingsFlyoutViewModel>().InstancePerDependency().AsSelf();
        }
    }
}