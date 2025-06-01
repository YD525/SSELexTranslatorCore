using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSELexTranslatorCore.TranslateCore;

namespace SSELexTranslatorCore.PlatformManagement
{
    public class SettingsConfiger
    {
        public bool UsingContext { get; set; } = true;
        public bool PhraseEngineUsing { get; set; } = false;
        public bool CodeParsingEngineUsing { get; set; } = true;
        public bool ConjunctionEngineUsing { get; set; } = false;
        public bool BaiDuYunApiUsing { get; set; } = false;
        public bool ChatGptApiUsing { get; set; } = false;
        public bool GeminiApiUsing { get; set; } = false;
        public bool DeepSeekApiUsing { get; set; } = false;
        public bool GoogleYunApiUsing { get; set; } = false;
        public bool DivCacheEngineUsing { get; set; } = false;
        public bool DeepLApiUsing { get; set; } = false;
        public Languages TargetLanguage { get; set; } = Languages.English;
        public Languages CurrentUILanguage { get; set; } = Languages.English;
        public string GoogleApiKey { get; set; } = "";
        public string BaiDuAppID { get; set; } = "";
        public string BaiDuSecretKey { get; set; } = "";
        public string ChatGptKey { get; set; } = "";
        public string ChatGptModel { get; set; } = "gpt-4o-mini";
        public string GeminiKey { get; set; } = "";
        public string GeminiModel { get; set; } = "gemini-2.0-flash";
        public string DeepSeekKey { get; set; } = "";
        public string DeepSeekModel { get; set; } = "deepseek-chat";
        public string DeepLKey { get; set; } = "";
        public bool IsFreeDeepL { get; set; } = true;
        public string ProxyIP { get; set; } = ""; 
        public string UserCustomAIPrompt { get; set; } = "";

        public string ViewMode { get; set; } = "Normal";
    }
}
