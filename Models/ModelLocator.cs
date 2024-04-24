/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Models"
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
using Models.FluentValidators;
using System;

namespace Models
{
    /// <summary>
    /// This class contains static references to all the models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ModelLocator class.
        /// </summary>
        public ModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Validator
            if (!SimpleIoc.Default.IsRegistered<IValidator<Users>>())
                SimpleIoc.Default.Register<IValidator<Users>, UsersValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<Ch_prs_code>>())
                SimpleIoc.Default.Register<IValidator<Ch_prs_code>, Ch_prs_codeValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<Pds_rec>>())
                SimpleIoc.Default.Register<IValidator<Pds_rec>, Pds_recValidator>();

            if (!SimpleIoc.Default.IsRegistered<IValidator<Pds_note>>())
                SimpleIoc.Default.Register<IValidator<Pds_note>, Pds_noteValidator>();
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

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}