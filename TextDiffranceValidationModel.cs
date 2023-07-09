using iText.IO.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IText7Extractor.Models
{
    public class TextDiffranceValidationModel
    {
        public const string EmptyFont = "_";
        public string? Text  { get; set; }
        public float FontSize { get; set; }
        public float SingleSpaceWidth { get; set; }
        public int MCID { get; set; }
        public string FontName { get; set; }
        public TextDiffranceValidationModel()
        {
            FontSize = -1;
            AddFontNameValidation(EmptyFont);
            MCID = -999999;
        }

        public void AddFontValidation(float fontSize)
        {
            //reste font
            if (fontSize==-1)
            {
                
                FontSize = -1;
            }
            if (FontSize==-1)
            {
                FontSize = fontSize;
            }
        }
        /// <summary>
        /// make sure that the previous sequence is stored
        /// </summary>
        /// <param name="fontName"></param>
        public void AddFontNameValidation(string fontName)
        {
            //rest font type 
            if (fontName== EmptyFont)
            {
                FontName = EmptyFont;
            }
            if (FontName== EmptyFont)
            {
                FontName = fontName;
            }
        }
        public bool IsTextChangedAttribute(TextDiffranceValidationModel refence)
        {
            return /*refence.FontName != this.FontName ||*/ refence.MCID != this.MCID /*|| refence.FontSize != this.FontSize*/;
        }
        public bool IsTextChangedAttributeFontStrategy(TextDiffranceValidationModel refence)
        {
            return refence.FontName != this.FontName || refence.MCID != this.MCID || refence.FontSize != this.FontSize;
        }
        public bool IsEmptyFont()
        {
            return FontName==EmptyFont;
        }
        public void Rest()
        {
            FontSize = -1;
            AddFontNameValidation(EmptyFont);
            MCID = -999999;
        }

    }
}
