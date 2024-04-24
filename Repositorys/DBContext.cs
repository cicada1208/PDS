using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Lib;
using Params;

namespace Repositorys
{
    public class DBContext
    {
        public DBContext()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Repository
            if (!SimpleIoc.Default.IsRegistered<SchemaRepository>())
                SimpleIoc.Default.Register<SchemaRepository>();

            if (!SimpleIoc.Default.IsRegistered<DBRepository>())
                SimpleIoc.Default.Register<DBRepository>();

            if (!SimpleIoc.Default.IsRegistered<UsersRepository>())
                SimpleIoc.Default.Register<UsersRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mg_mnidRepository>())
                SimpleIoc.Default.Register<Mg_mnidRepository>();

            if (!SimpleIoc.Default.IsRegistered<SysParameterRepository>())
                SimpleIoc.Default.Register<SysParameterRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_prs_codeRepository>())
                SimpleIoc.Default.Register<Ch_prs_codeRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_prsRepository>())
                SimpleIoc.Default.Register<Ch_prsRepository>();

            if (!SimpleIoc.Default.IsRegistered<RECSerialNoRepository>())
                SimpleIoc.Default.Register<RECSerialNoRepository>();

            if (!SimpleIoc.Default.IsRegistered<PdsPatInfoRepository>())
                SimpleIoc.Default.Register<PdsPatInfoRepository>();

            if (!SimpleIoc.Default.IsRegistered<PdsMedInfoRepository>())
                SimpleIoc.Default.Register<PdsMedInfoRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mi_micbcodeRepository>())
                SimpleIoc.Default.Register<Mi_micbcodeRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_udrecRepository>())
                SimpleIoc.Default.Register<Ch_udrecRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mh_mpatRepository>())
                SimpleIoc.Default.Register<Mh_mpatRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_torRepository>())
                SimpleIoc.Default.Register<Ch_torRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mi_mipdRepository>())
                SimpleIoc.Default.Register<Mi_mipdRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_ipdtRepository>())
                SimpleIoc.Default.Register<Ch_ipdtRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_rel2Repository>())
                SimpleIoc.Default.Register<Ch_rel2Repository>();

            if (!SimpleIoc.Default.IsRegistered<Mn_mnslRepository>())
                SimpleIoc.Default.Register<Mn_mnslRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_hdbRepository>())
                SimpleIoc.Default.Register<Ch_hdbRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_prsnRepository>())
                SimpleIoc.Default.Register<Ch_prsnRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mr_mexpRepository>())
                SimpleIoc.Default.Register<Mr_mexpRepository>();

            if (!SimpleIoc.Default.IsRegistered<Pds_recRepository>())
                SimpleIoc.Default.Register<Pds_recRepository>();

            if (!SimpleIoc.Default.IsRegistered<Pds_recdRepository>())
                SimpleIoc.Default.Register<Pds_recdRepository>();

            if (!SimpleIoc.Default.IsRegistered<Rec_codeRepository>())
                SimpleIoc.Default.Register<Rec_codeRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_udrec_chkRepository>())
                SimpleIoc.Default.Register<Ch_udrec_chkRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_bcodeRepository>())
                SimpleIoc.Default.Register<Ch_bcodeRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_cdrRepository>())
                SimpleIoc.Default.Register<Ch_cdrRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mch_msenRepository>())
                SimpleIoc.Default.Register<Mch_msenRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mr_lstudRepository>())
                SimpleIoc.Default.Register<Mr_lstudRepository>();

            if (!SimpleIoc.Default.IsRegistered<Pds_noteRepository>())
                SimpleIoc.Default.Register<Pds_noteRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_traRepository>())
                SimpleIoc.Default.Register<Ch_traRepository>();

            if (!SimpleIoc.Default.IsRegistered<Mch_mnidRepository>())
                SimpleIoc.Default.Register<Mch_mnidRepository>();

            if (!SimpleIoc.Default.IsRegistered<Ch_remakemarRepository>())
                SimpleIoc.Default.Register<Ch_remakemarRepository>();
        }

        private DBUtil _NIS;
        public DBUtil NIS =>
            _NIS ?? (_NIS = new DBUtil(DBParam.DBName.NIS, DBParam.DBType.SYBASE));

        private DBUtil _Syb1;
        public DBUtil Syb1 =>
            _Syb1 ?? (_Syb1 = new DBUtil(DBParam.DBName.SYB1, DBParam.DBType.SYBASE));

        private DBUtil _Syb2;
        public DBUtil Syb2 =>
            _Syb2 ?? (_Syb2 = new DBUtil(DBParam.DBName.SYB2, DBParam.DBType.SYBASE));

        private DBUtil _PeriExam;
        public DBUtil PeriExam =>
            _PeriExam ?? (_PeriExam = new DBUtil(DBParam.DBName.PeriExam, DBParam.DBType.SQLSERVER));

        private DBUtil _PeriPhery;
        public DBUtil PeriPhery =>
            _PeriPhery ?? (_PeriPhery = new DBUtil(DBParam.DBName.PeriPhery, DBParam.DBType.SQLSERVER));

        private DBUtil _Inf;
        public DBUtil Inf =>
            _Inf ?? (_Inf = new DBUtil(DBParam.DBName.Inf, DBParam.DBType.SQLSERVER));

        private DBUtil _UAAC;
        public DBUtil UAAC =>
            _UAAC ?? (_UAAC = new DBUtil(DBParam.DBName.UAAC, DBParam.DBType.SQLSERVER));

        private DBUtil _MISSYS;
        public DBUtil MISSYS =>
            _MISSYS ?? (_MISSYS = new DBUtil(DBParam.DBName.MISSYS, DBParam.DBType.SQLSERVER));

        public SchemaRepository SchemaRepository =>
            ServiceLocator.Current.GetInstance<SchemaRepository>();

        public DBRepository DBRepository =>
            ServiceLocator.Current.GetInstance<DBRepository>();

        //private UsersRepository _UsersRepository;
        //public UsersRepository UsersRepository =>
        //    _UsersRepository ?? (_UsersRepository = new UsersRepository());
        public UsersRepository UsersRepository =>
            ServiceLocator.Current.GetInstance<UsersRepository>();

        public Mg_mnidRepository Mg_mnidRepository =>
            ServiceLocator.Current.GetInstance<Mg_mnidRepository>();

        public SysParameterRepository SysParameterRepository =>
            ServiceLocator.Current.GetInstance<SysParameterRepository>();

        public Ch_prs_codeRepository Ch_prs_codeRepository =>
            ServiceLocator.Current.GetInstance<Ch_prs_codeRepository>();

        public Ch_prsRepository Ch_prsRepository =>
            ServiceLocator.Current.GetInstance<Ch_prsRepository>();

        public RECSerialNoRepository RECSerialNoRepository =>
            ServiceLocator.Current.GetInstance<RECSerialNoRepository>();

        public PdsPatInfoRepository PdsPatInfoRepository =>
            ServiceLocator.Current.GetInstance<PdsPatInfoRepository>();

        public PdsMedInfoRepository PdsMedInfoRepository =>
            ServiceLocator.Current.GetInstance<PdsMedInfoRepository>();

        public Mi_micbcodeRepository Mi_micbcodeRepository =>
            ServiceLocator.Current.GetInstance<Mi_micbcodeRepository>();

        public Ch_udrecRepository Ch_udrecRepository =>
            ServiceLocator.Current.GetInstance<Ch_udrecRepository>();

        public Mh_mpatRepository Mh_mpatRepository =>
            ServiceLocator.Current.GetInstance<Mh_mpatRepository>();

        public Ch_torRepository Ch_torRepository =>
            ServiceLocator.Current.GetInstance<Ch_torRepository>();

        public Mi_mipdRepository Mi_mipdRepository =>
            ServiceLocator.Current.GetInstance<Mi_mipdRepository>();

        public Ch_ipdtRepository Ch_ipdtRepository =>
            ServiceLocator.Current.GetInstance<Ch_ipdtRepository>();

        public Ch_rel2Repository Ch_rel2Repository =>
            ServiceLocator.Current.GetInstance<Ch_rel2Repository>();

        public Mn_mnslRepository Mn_mnslRepository =>
            ServiceLocator.Current.GetInstance<Mn_mnslRepository>();

        public Ch_hdbRepository Ch_hdbRepository =>
            ServiceLocator.Current.GetInstance<Ch_hdbRepository>();

        public Ch_prsnRepository Ch_prsnRepository =>
            ServiceLocator.Current.GetInstance<Ch_prsnRepository>();

        public Mr_mexpRepository Mr_mexpRepository =>
            ServiceLocator.Current.GetInstance<Mr_mexpRepository>();

        public Pds_recRepository Pds_recRepository =>
            ServiceLocator.Current.GetInstance<Pds_recRepository>();

        public Pds_recdRepository Pds_recdRepository =>
            ServiceLocator.Current.GetInstance<Pds_recdRepository>();

        public Rec_codeRepository Rec_codeRepository =>
            ServiceLocator.Current.GetInstance<Rec_codeRepository>();

        public Ch_udrec_chkRepository Ch_udrec_chkRepository =>
            ServiceLocator.Current.GetInstance<Ch_udrec_chkRepository>();

        public Ch_bcodeRepository Ch_bcodeRepository =>
            ServiceLocator.Current.GetInstance<Ch_bcodeRepository>();

        public Ch_cdrRepository Ch_cdrRepository =>
            ServiceLocator.Current.GetInstance<Ch_cdrRepository>();

        public Mch_msenRepository Mch_msenRepository =>
            ServiceLocator.Current.GetInstance<Mch_msenRepository>();

        public Mr_lstudRepository Mr_lstudRepository =>
            ServiceLocator.Current.GetInstance<Mr_lstudRepository>();

        public Pds_noteRepository Pds_noteRepository =>
            ServiceLocator.Current.GetInstance<Pds_noteRepository>();

        public Ch_traRepository Ch_traRepository =>
            ServiceLocator.Current.GetInstance<Ch_traRepository>();

        public Mch_mnidRepository Mch_mnidRepository =>
            ServiceLocator.Current.GetInstance<Mch_mnidRepository>();

        public Ch_remakemarRepository Ch_remakemarRepository =>
            ServiceLocator.Current.GetInstance<Ch_remakemarRepository>();

    }
}
