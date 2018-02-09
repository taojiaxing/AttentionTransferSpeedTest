using AttentionTransferSpeedTest.DAL.DBO;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;

namespace AttentionTransferSpeedTest.BLL
{
    internal class exportWord
    {
        //public string CreateWordFile(Result result, User user, string path)
        //{
        //    string message = "";
        //    try
        //    {
        //        Object Nothing = System.Reflection.Missing.Value;
        //        Directory.CreateDirectory("/");  //创建文件所在目录
        //        string name = user.Name + ".doc";
        //        object filename = "/" + name;  //文件保存路径
        //        //创建Word文档
        //        Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
        //        Microsoft.Office.Interop.Word.Document WordDoc = WordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

        //        //添加页眉
        //        WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
        //        WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
        //        WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[test]");
        //        WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//设置居中对齐
        //        WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;//跳出页眉设置

        //        WordApp.Selection.ParagraphFormat.LineSpacing = 15f;//设置文档的行间距

        //        //移动焦点并换行
        //        object count = 14;
        //        object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdLine;//换一行;
        //        WordApp.Selection.MoveDown(ref WdLine, ref count, ref Nothing);//移动焦点
        //        WordApp.Selection.TypeParagraph();//插入段落

        //        //文档中创建表格
        //        Microsoft.Office.Interop.Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 12, 3, ref Nothing, ref Nothing);
        //        //设置表格样式
        //        newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
        //        newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //        newTable.Columns[1].Width = 100f;
        //        newTable.Columns[2].Width = 220f;
        //        newTable.Columns[3].Width = 105f;
        //        newTable.Columns[4].Width = 220f;
        //        newTable.Columns[5].Width = 240f;
        //        newTable.Columns[6].Width = 220f;
        //        newTable.Columns[7].Width = 220f;

        //        //填充表格内容
        //        newTable.Cell(1, 1).Range.Text = "测试时间：" + user.Time;
        //        newTable.Cell(1, 1).Range.Bold = 2;//设置单元格中字体为粗体
        //                                           //合并单元格
        //        newTable.Cell(1, 1).Merge(newTable.Cell(1, 7));
        //        WordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //        WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;//

        //        //填充表格内容
        //        newTable.Cell(2, 1).Range.Text = "产品基本信息";
        //        newTable.Cell(2, 1).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkBlue;//设置单元格内字体颜色
        //                                                                                                     //合并单元格
        //        newTable.Cell(2, 1).Merge(newTable.Cell(2, 3));
        //        WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

        //        //填充表格内容
        //        newTable.Cell(3, 1).Range.Text = "品牌名称：";
        //        newTable.Cell(3, 2).Range.Text = BrandName;
        //        //纵向合并单元格
        //        newTable.Cell(3, 3).Select();//选中一行
        //        object moveUnit = Word.WdUnits.wdLine;
        //        object moveCount = 5;
        //        object moveExtend = Word.WdMovementType.wdExtend;
        //        WordApp.Selection.MoveDown(ref moveUnit, ref moveCount, ref moveExtend);
        //        WordApp.Selection.Cells.Merge();
        //        //插入图片
        //        string FileName = Picture;//图片所在路径
        //        object LinkToFile = false;
        //        object SaveWithDocument = true;
        //        object Anchor = WordDoc.Application.Selection.Range;
        //        WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
        //        WordDoc.Application.ActiveDocument.InlineShapes[1].Width = 100f;//图片宽度
        //        WordDoc.Application.ActiveDocument.InlineShapes[1].Height = 100f;//图片高度
        //                                                                         //将图片设置为四周环绕型
        //        Word.Shape s = WordDoc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
        //        s.WrapFormat.Type = Word.WdWrapType.wdWrapSquare;

        //        newTable.Cell(12, 1).Range.Text = "产品特殊属性";
        //        newTable.Cell(12, 1).Merge(newTable.Cell(12, 3));
        //        //在表格中增加行
        //        WordDoc.Content.Tables[1].Rows.Add(ref Nothing);

        //        WordDoc.Paragraphs.Last.Range.Text = "文档创建时间：" + DateTime.Now.ToString();//“落款”
        //        WordDoc.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

        //        //文件保存
        //        WordDoc.SaveAs(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //        WordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
        //        WordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
        //        message = name + "文档生成成功，以保存到C:/CNSI/下";
        //    }
        //    catch
        //    {
        //        message = "文件导出异常！";
        //    }
        //    return message;
        //}
    }
}