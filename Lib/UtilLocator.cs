using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Lib
{
    public class UtilLocator
    {
        public UtilLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!SimpleIoc.Default.IsRegistered<ApiUtil>())
                SimpleIoc.Default.Register<ApiUtil>();

            if (!SimpleIoc.Default.IsRegistered<CtrlUtil>())
                SimpleIoc.Default.Register<CtrlUtil>();

            if (!SimpleIoc.Default.IsRegistered<DataTableUtil>())
                SimpleIoc.Default.Register<DataTableUtil>();

            if (!SimpleIoc.Default.IsRegistered<DateTimeUtil>())
                SimpleIoc.Default.Register<DateTimeUtil>();

            if (!SimpleIoc.Default.IsRegistered<ExcelUtil>())
                SimpleIoc.Default.Register<ExcelUtil>();

            if (!SimpleIoc.Default.IsRegistered<FileUtil>())
                SimpleIoc.Default.Register<FileUtil>();

            if (!SimpleIoc.Default.IsRegistered<HostUtil>())
                SimpleIoc.Default.Register<HostUtil>();

            if (!SimpleIoc.Default.IsRegistered<LinqUtil>())
                SimpleIoc.Default.Register<LinqUtil>();

            if (!SimpleIoc.Default.IsRegistered<MediaUtil>())
                SimpleIoc.Default.Register<MediaUtil>();

            if (!SimpleIoc.Default.IsRegistered<MedicalUtil>())
                SimpleIoc.Default.Register<MedicalUtil>();

            if (!SimpleIoc.Default.IsRegistered<ReflectionUtil>())
                SimpleIoc.Default.Register<ReflectionUtil>();

            if (!SimpleIoc.Default.IsRegistered<RuleUtil>())
                SimpleIoc.Default.Register<RuleUtil>();

            if (!SimpleIoc.Default.IsRegistered<SqlBuildUtil>())
                SimpleIoc.Default.Register<SqlBuildUtil>();

            if (!SimpleIoc.Default.IsRegistered<StrUtil>())
                SimpleIoc.Default.Register<StrUtil>();

            if (!SimpleIoc.Default.IsRegistered<ColorUtil>())
                SimpleIoc.Default.Register<ColorUtil>();
        }

        public ApiUtil Api =>
            ServiceLocator.Current.GetInstance<ApiUtil>();

        public CtrlUtil Ctrl =>
            ServiceLocator.Current.GetInstance<CtrlUtil>();

        public DataTableUtil DataTable =>
            ServiceLocator.Current.GetInstance<DataTableUtil>();

        public DateTimeUtil DateTime =>
            ServiceLocator.Current.GetInstance<DateTimeUtil>();

        public ExcelUtil Excel =>
            ServiceLocator.Current.GetInstance<ExcelUtil>();

        public FileUtil File =>
            ServiceLocator.Current.GetInstance<FileUtil>();

        public HostUtil Host =>
            ServiceLocator.Current.GetInstance<HostUtil>();

        public LinqUtil Linq =>
            ServiceLocator.Current.GetInstance<LinqUtil>();

        public MediaUtil Media =>
            ServiceLocator.Current.GetInstance<MediaUtil>();

        public MedicalUtil Medical =>
            ServiceLocator.Current.GetInstance<MedicalUtil>();

        public ReflectionUtil Reflection =>
            ServiceLocator.Current.GetInstance<ReflectionUtil>();

        public RuleUtil Rule =>
            ServiceLocator.Current.GetInstance<RuleUtil>();

        public SqlBuildUtil SqlBuild =>
            ServiceLocator.Current.GetInstance<SqlBuildUtil>();

        public StrUtil Str =>
            ServiceLocator.Current.GetInstance<StrUtil>();

        public ColorUtil Color =>
        ServiceLocator.Current.GetInstance<ColorUtil>();

    }
}
