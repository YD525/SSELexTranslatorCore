
namespace SSELexTranslatorCore.DelegateManagement
{
    public class DelegateHelper
    {
        public static LogCall SetLog = null;
        public delegate bool LogCall(string Log);

        public static CacheFunction AddCache = null;
        public delegate bool CacheFunction(string SourceStr, int From, int To, string Content);

        public static QueryCacheFunction FindCache = null;
        public delegate string QueryCacheFunction(string SourceStr, int From, int To);

    }
}
