﻿using SSELexTranslatorCore.DelegateManagement;
using SSELexTranslatorCore.TranslateCore;
using SSELexTranslatorCore.TranslateManage;
using System.Text.Json;
using System.Web;

namespace SSELexTranslatorCore.PlatformManagement
{
    public class GoogleTransApi
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static string ToLanguageCode(Languages language)
        {
            return language switch
            {
                Languages.English => "en",
                Languages.SimplifiedChinese => "zh-CN",
                Languages.TraditionalChinese => "zh-TW",
                Languages.Japanese => "ja",
                Languages.German => "de",
                Languages.Korean => "ko",
                Languages.Turkish => "tr",
                Languages.Brazilian => "pt-BR",
                Languages.Russian => "ru",
                _ => throw new ArgumentOutOfRangeException(nameof(language), "Unsupported language")
            };
        }

        public string Translate(string text, Languages targetLanguage, Languages? sourceLanguage = null)
        {
            try {
            string targetCode = ToLanguageCode(targetLanguage);
            string sourceCode = sourceLanguage.HasValue ? ToLanguageCode(sourceLanguage.Value) : "auto";

            string url = $"https://translation.googleapis.com/language/translate/v2" +
                         $"?key={Translator.CurrentSetting.GoogleApiKey}" +
                         $"&q={HttpUtility.UrlEncode(text)}" +
                         $"&target={targetCode}" +
                         $"&source={sourceCode}";

            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;

            DelegateHelper.SetLog("GoogleApi:" + json);

            using JsonDocument doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("data", out JsonElement dataElem) &&
                dataElem.TryGetProperty("translations", out JsonElement translationsElem) &&
                translationsElem.GetArrayLength() > 0 &&
                translationsElem[0].TryGetProperty("translatedText", out JsonElement textElem))
            {
                return textElem.GetString() ?? string.Empty;
            }

            return string.Empty;
            }
            catch { return string.Empty; }
        }
    }
}
