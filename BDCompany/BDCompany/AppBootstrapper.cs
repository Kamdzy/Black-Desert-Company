﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BDCompany.ViewModels;
using BDCompany.Views;
using TinyLittleMvvm;

namespace BDCompany
{

    public interface IShell{}

    class AppBootstrapper : BootstrapperBase<MainViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainView>();

            builder.RegisterType<SampleDialogView>().InstancePerDependency().AsSelf();
            builder.RegisterType<SampleDialogViewModel>().InstancePerDependency().AsSelf();

            builder.RegisterType<SettingsFlyoutView>().InstancePerDependency().AsSelf();
            builder.RegisterType<SettingsFlyoutViewModel>().InstancePerDependency().AsSelf();
        }
        /*protected override string GetLogFolder()
        {
            var logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly().GetName().Name,
                "Logs");

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            return logFolder;
        }*/

    }
}