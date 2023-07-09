using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IText7Extractor.Models
{
    public class ReportGeneratorInfoModel
    {
        public int HeadersCount { get;private set; }
        public int TablesHeadersCount { get;private set; }
        public string HeadersTexts { get; private set; }
        public string TablesHeadersTexts { get; private set; }
        public int CheckBoxesCount { get;private set; }
        public string CheckBoxesText { get; private set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string ReportDate { get; set; }
        public int ParagraphsCount { get; set; }
        public string ParagraphsTexts { get;private set; }
        public string TablesCount { get; set; }
        public string TablesParagraphsCount { get;private set; }
        public string TablesRowCount { get;private set; }
        public string TablesBluePrintCount { get;private set; }

        public string TablesParagraphsTexts { get;private set; }
        public string TablesRowTexts { get;private set; }
        public string TablesBluePrintTexts { get;private set; }

        public string TextInputsTexts { get; private set; }
        public int TextInputsCount { get; set; }
        public void SetHeadersText(List<string> headers)
        {
            HeadersTexts = string.Join("<br/><hr/>", headers);
            HeadersCount = headers.Count;
        }
        public void SetTablesHeadersText(List<string> tablesHeaders)
        {
            TablesHeadersTexts = string.Join("<br/><hr/>", tablesHeaders);
            TablesHeadersCount = tablesHeaders.Count;
        }
        public void SetCheckBoxesCount(List<InteractionField> InteractionFields)
        {
            var checkboxes = (InteractionFields != null) ? InteractionFields.Where(ifl => ifl.FieldType == "/Btn"|| ifl.FieldType =="/Chk").Select(obj => obj.Value).ToList() : new List<string?>();

            CheckBoxesCount = checkboxes.Count;
            CheckBoxesText = string.Join("<br/><hr/>", checkboxes);
        }
        public void SetParagraphsTexts(List<string>? paragraphs)
        {
            ParagraphsTexts = string.Join("<br/><hr/>", paragraphs);
            ParagraphsCount = paragraphs.Count;
        }
        public void SetTablesParagraphsTexts(List<string>? paragraphs)
        {

            TablesParagraphsTexts = string.Join("<br/><hr/>", paragraphs??new List<string>());
            TablesParagraphsCount = paragraphs?.Count.ToString()??"0";
            
        }
        public void SetTablesRowTexts(List<string>? rowsTextList)
        {
            TablesRowTexts = string.Join("<br/><hr/>", rowsTextList?? new List<string>());
            TablesRowCount = rowsTextList?.Count.ToString() ?? "0";
        }
        public void SetTablesBluePrintTexts(List<string>? BluePrintTextList)
        {
            TablesBluePrintTexts = string.Join("<br/><hr/>", BluePrintTextList);
            TablesBluePrintCount = BluePrintTextList?.Count.ToString() ?? "0";
        }
        public void SetTextInputsTexts(List<InteractionField> InteractionFields)
        {
            var textInputs = (InteractionFields != null) ? InteractionFields.Where(ifl => ifl.FieldType == "/Tx" ).Select(obj => obj.Value).ToList() : new List<string?>();

            TextInputsCount = textInputs.Count;
            TextInputsTexts = string.Join("<br/><hr/>", textInputs);
        }
        public void RestCheckBoxesCount()
        {
            CheckBoxesCount = 0;
        }
        public void RestParagraphsCount()
        {
            ParagraphsCount = 0;
        }

    }
}
