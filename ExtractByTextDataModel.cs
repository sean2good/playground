using iText.Forms.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static InteractiveField;

namespace IText7Extractor.Models
{
    public class ExtractByTextDataModel
    {
        public ExtractByTextDataModel()
        {
            Data = new List<ExtractedTextInfos>();
            InteractiveFieldsData = new List<InteractiveField>();
        }
        public List<ExtractedTextInfos>? Data { get;  set; }
        public List<InteractiveField> InteractiveFieldsData { get;  set; }
        public void SetNewFontType(float fontSize, List<string>? associatedTexts)
        {
            var validatedData = associatedTexts?.Where(l => ChuckValidation(l) == true).ToList();


            if (fontSize != -1 && Data?.Where( t=>t.FontSize== fontSize).Count()>0)
            {
                Data?.Where(t => t.FontSize == fontSize)?.FirstOrDefault()?.AssociatedText?.AddRange(validatedData ?? new List<string>());
            }
            else
            {
                if (fontSize!=-1)
                {
                    Data.Add(new ExtractedTextInfos(fontSize, validatedData));
                }

            }
        }
        public void SetNewFontTypeRange(Dictionary<float, List<string>> fontsRang)
        {
            if (fontsRang != null)
            {
                foreach (var font in fontsRang)
                {
                    SetNewFontType(font.Key, font.Value);
                }

            }
        }
        public List<string>? GetTextTypeList(TextTypeBySize tType)
        {
            switch (tType)
            {
                case TextTypeBySize.Paragraph:
                  var headers=  GetHeaders();
                    if (headers!=null && headers.Count>0)
                    {
                        var result = new List<string>();
                        foreach (var h in headers)
                        {
                            result.AddRange(h.AssociatedText);
                        }
                        return result;
                    }
                    return new List<string>();
                case TextTypeBySize.Header:
                  var paragraphs=  GetParagraphs();
                    return paragraphs.AssociatedText;
                   


                default:
                    return new List<string>();
            }
        }

        private List<ExtractedTextInfos> GetHeaders()
        {
            if (Data == null || Data.Count == 0)
                throw new ArgumentException("Data list is empty or null.");

            float maxFontSize = Data.Max(info => info.FontSize);
            List<ExtractedTextInfos> exceptMaxFontSize = Data.Where(info => info.FontSize != maxFontSize).ToList();
            return exceptMaxFontSize;
        }

        private ExtractedTextInfos GetParagraphs()
        {
            if (Data == null || Data.Count == 0)
               return new ExtractedTextInfos();

            ExtractedTextInfos maxFontSizeInfo = Data.OrderByDescending(info => info.FontSize).First();
            return maxFontSizeInfo;
        }

        public int GetFontOccurrenceCount(float fontSize)
        {

            if (Data != null && fontSize != -1)
            {
                return Data.Where(t=>t.FontSize== fontSize).Count();
            }
            else
            {
                return 0;
            }

        }

        public void SetInteractiveField(InteractiveField field)
        {
            if (field!=null)
            {
                InteractiveFieldsData.Add(field);
            }
        }
        public List<InteractiveField> GetInteractiveFields(string type)
        {
            switch (type)
            {
                case InteractiveFieldsTypes.CheckBox:
                    return InteractiveFieldsData.Where(itf => itf.FieldType == InteractiveFieldsTypes.CheckBox ).ToList() ;
                case InteractiveFieldsTypes.TextInput:
                    return InteractiveFieldsData.Where(itf => itf.FieldType == InteractiveFieldsTypes.TextInput ).ToList();
                case InteractiveFieldsTypes.Button:
                    return InteractiveFieldsData.Where(itf => itf.FieldType == InteractiveFieldsTypes.Button).ToList();
                case InteractiveFieldsTypes.clickable:
                    var intr= InteractiveFieldsData.Where(itf => itf.FieldType == InteractiveFieldsTypes.CheckBox
                    || itf.FieldType == InteractiveFieldsTypes.Button).ToList();
                    return InteractiveFieldsData.Where(itf => itf.FieldType == InteractiveFieldsTypes.CheckBox
                    || itf.FieldType == InteractiveFieldsTypes.Button).ToList();
                default:
                    return new List<InteractiveField>();
            }
        }

        private bool ChuckValidation(string text)
        {
            return text != null && text.Trim().Length > 0;



        }
    }

}

public enum TextTypeBySize
{
    Header,
    Paragraph
}
public class ExtractedTextInfos
{
    public float FontSize { get; set; }
    public List<string>? AssociatedText { get; set; }
    public ExtractedTextInfos()
    {
        
    }

    public ExtractedTextInfos(float fontSize, List<string>? associatedText)
    {
        FontSize = fontSize;
        AssociatedText = associatedText;
    }
}

public class InteractiveField
{
    public string FieldType { get; set; }
    public string? Value { get; set; }
    public string? lable { get; set; }
    public InteractiveField()
    {
        
    }
   public void SetInteractiveFieldType(string typ)
    {
        switch (typ)
        {
            case "/Btn":
                FieldType = InteractiveFieldsTypes.Button;
                break;
            case "/Tx":
                FieldType = InteractiveFieldsTypes.TextInput;
                break;
            case "/Chk":
                FieldType = InteractiveFieldsTypes.CheckBox;
                break;
            default:
                break;
        }
    }
   public void SetInteractiveFieldValeu(string vl)
    {
        if (vl!= null && vl!="")
        {
            Value = vl;
        }
    }
   public void SetInteractiveFieldLable(string lbl)
    {
        if (lbl != null && lbl != "")
        {
            lable = lbl;
        }
    }
    public InteractiveField( string? value, string? lable)
    {
        
        Value = value;
        this.lable = lable;
    }
}

public class InteractiveFieldsTypes
{
    public const string CheckBox = "CheckBox",
       TextInput = "TextInput",
       Button = "Button",
       clickable = "clickable",
       None = "None";
}

