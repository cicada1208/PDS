/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ViewModels"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using FluentValidation;
using GalaSoft.MvvmLight.Ioc;
using System;
using ViewModels.FluentValidators;

namespace ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register ViewModel
            //SimpleIoc.Default.Register<MainViewModel>();
            //SimpleIoc.Default.Register<MvvmViewModel>();

            // Register Validator
            if (!SimpleIoc.Default.IsRegistered<IValidator<MvvmViewModel>>())
                SimpleIoc.Default.Register<IValidator<MvvmViewModel>, MvvmViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<ValidationViewModel>>())
                SimpleIoc.Default.Register<IValidator<ValidationViewModel>, ValidationViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<LoginViewModel>>())
                SimpleIoc.Default.Register<IValidator<LoginViewModel>, LoginViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<PdsPrsCodeViewModel>>())
                SimpleIoc.Default.Register<IValidator<PdsPrsCodeViewModel>, PdsPrsCodeViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<PdsRecCancelViewModel>>())
                SimpleIoc.Default.Register<IValidator<PdsRecCancelViewModel>, PdsRecCancelViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<PdsRecEditViewModel>>())
                SimpleIoc.Default.Register<IValidator<PdsRecEditViewModel>, PdsRecEditViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<PdsUdrecChkClinicalViewModel>>())
                SimpleIoc.Default.Register<IValidator<PdsUdrecChkClinicalViewModel>, PdsUdrecChkClinicalViewModelValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<PdsNoteEditViewModel>>())
                SimpleIoc.Default.Register<IValidator<PdsNoteEditViewModel>, PdsNoteEditViewModelValidator>();

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
        }

        public static IValidator Validator(Type validatorType)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(validatorType) as IValidator;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public MainViewModel MainViewModel =>
        //    ServiceLocator.Current.GetInstance<MainViewModel>();

        //public MvvmViewModel MvvmViewModel =>
        //    ServiceLocator.Current.GetInstance<MvvmViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}