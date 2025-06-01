
using System.Drawing;
using System.Text.RegularExpressions;
using SSELexTranslatorCore.DelegateManagement;
using SSELexTranslatorCore.PlatformManagement;
using SSELexTranslatorCore.TranslateCore;
using static SSELexTranslatorCore.TranslateManage.TransCore;

namespace SSELexTranslatorCore.TranslateManage
{
    public class Translator
    {
        public static int ReqTimeOut = 10000;

        public static SettingsConfiger CurrentSetting = null;

        public delegate void TranslateMsg(string EngineName, string Text, string Result);

        public static TranslateMsg SendTranslateMsg;

        public static Dictionary<string, string> TransData = new Dictionary<string, string>();

        public static void ClearCache()
        {
            TransData.Clear();
        }

        public static void ClearAICache()
        {
            EngineSelect.AIMemory.Clear();
        }

        public static TransCore CurrentTransCore = new TransCore();

        public static string ReturnStr(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str.Replace("　", "")))
            {
                return string.Empty;
            }
            else
            {
                return Str;
            }
        }

        public static bool IsOnlySymbolsAndSpaces(string Input)
        {
            return Regex.IsMatch(Input, @"^[\p{P}\p{S}\s]+$");
        }

        public static string QuickTrans(string Content,Languages To,ref bool CanSleep, bool IsBook = false)
        {
            string GetSourceStr = Content;

            if (IsOnlySymbolsAndSpaces(GetSourceStr))
            {
                return GetSourceStr;
            }

            if (GetSourceStr.Trim().Length == 0)
            {
                return GetSourceStr;
            } 

            bool HasOuterQuotes = TranslationPreprocessor.HasOuterQuotes(GetSourceStr.Trim());

            TranslationPreprocessor.ConditionalSplitCamelCase(ref Content);
            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            Languages SourceLanguage = LanguageHelper.DetectLanguageByLine(Content);
            if (SourceLanguage == To)
            {
                return GetSourceStr;
            }

            bool CanAddCache = true;
            Content = CurrentTransCore.TransAny(SourceLanguage, To, Content,IsBook,ref CanAddCache,ref CanSleep);

            TranslationPreprocessor.NormalizePunctuation(ref Content);
            TranslationPreprocessor.ProcessEmptyEndLine(ref Content);
            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            TranslationPreprocessor.StripOuterQuotes(ref Content);

            Content = Content.Trim();

            if (HasOuterQuotes)
            {
                Content = "\"" + HasOuterQuotes + "\"";
            }

            Content = ReturnStr(Content);

            if (CanAddCache && Content.Trim().Length>0)
            {
                DelegateHelper.AddCache(GetSourceStr, (int)SourceLanguage, (int)To, Content);
            }

            return Content;
        }

        public class QueryTransItem
        {
            public string Key = "";
            public string TransText = "";
            public Color Color;
            public int State = 0;
        }

        public class SetTransItem
        {
            public string Key = "";
            public Color Color;
            public int State = 0;
        }

       
    }
}
