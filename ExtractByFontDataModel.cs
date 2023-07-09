using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IText7Extractor.Models
{
    /// <summary>
    /// 
    /// </summary>
    public  class ExtractByFontDataModel
    {
        public ExtractByFontDataModel()
        {
            Data = new Dictionary<string, List<string>>();
        }
        public Dictionary<string, List<string>>? Data { get;private set; }
        public void SetNewFontType(string? fontName,List<string>? associatedTexts)
        {
            var validatedData = associatedTexts?.Where(l => ChuckValidation(l) == true).ToList();
            fontName =fontName??"";
            if ( Data.ContainsKey(fontName))
            {
                Data[fontName].AddRange(validatedData ?? new List<string>());
            }
            else
            {
                if (fontName!=null && validatedData != null)
                {
                    //exclude the blank text
                    Data.Add(fontName, validatedData);
                }
                
            }
        }
        public void SetNewFontTypeRange(Dictionary<string, List<string>> fontsRang)
        {
            if (fontsRang!=null)
            {
                foreach (var font in fontsRang)
                {
                    SetNewFontType(font.Key, font.Value);
                }

            }
        }
        public string GetHtmlFontAssociatedDate(string fontName)
        {
            fontName = GetFontSimilaireTo(fontName);
            if (fontName!="" && Data.ContainsKey(fontName))
            {
                return string.Join("<hr/>", Data[fontName]);
            }
            else
            {
                return "<hr/> The requested Font Does not exist in the current PDF file";
            }
        }
        public int GetFontOccurrenceCount(string fontName)
        {
            fontName = GetFontSimilaireTo(fontName);
            if (Data!=null && fontName!="")
            {
                return  Data[fontName].Count;
            }
            else
            {
                return 0;
            }

        }

        private  bool ChuckValidation(string text)
        {
            return text != null && text.Trim().Length > 0;



        }
        /// <summary>
        /// get the approximated font by user input 
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        private string GetFontSimilaireTo(string fontName)
        {
           return Data?.Keys.Where(f => f.ToLower().Contains(fontName.ToLower())).FirstOrDefault()??"";
        }
    }
}
