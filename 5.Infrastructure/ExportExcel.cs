using System;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;

namespace MyProject.Infrastructure
{
    public class ExportExcel
    {
        public static bool ExportEx(string BillID,string Employee,string Customer,String Date,ListView listView1,String getTongTien)
        {
            try
            {
                //Tạo các đối tượng Excel
                Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
                Workbooks oBooks;
                Sheets oSheets;
                Workbook oBook;
                Worksheet oSheet;
                //Tạo mới một Excel WorkBook 
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oExcel.Application.SheetsInNewWorkbook = 1;
                oBooks = oExcel.Workbooks;
                oBook = (Workbook)(oExcel.Workbooks.Add(Type.Missing));
                oSheets = oBook.Worksheets;
                oSheet = (Worksheet)oSheets.get_Item(1);
                oSheet.Name = BillID;
                // Tạo phần đầu nếu muốn
                Range head = oSheet.get_Range("A1", "D1");
                head.MergeCells = true;
                head.Value2 = "Cửa hàng văn phòng phẩm DEFTNT";
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //Tạo thông tin đầu
                Range clnv = oSheet.get_Range("A2", "A2");
                clnv.Value2 = Employee;
                clnv.ColumnWidth = 40.0;
                Range clkh = oSheet.get_Range("A3", "A3");
                clkh.Value2 = Customer;
                clkh.ColumnWidth = 40.0;
                Range clngay = oSheet.get_Range("C2", "C2");
                clngay.Value2 = Date;
                clngay.ColumnWidth = 40.0;
                Range clt = oSheet.get_Range("C3", "C3");
                clt.Value2 = "Quầy thu ngân: Số 1";
                clt.ColumnWidth = 40.0;
                // Tạo tiêu đề cột 
                Range cl1 = oSheet.get_Range("A4", "A4");
                cl1.Value2 = "Tên sản phẩm";
                cl1.ColumnWidth = 40.0;
                Range cl2 = oSheet.get_Range("B4", "B4");
                cl2.Value2 = "Đơn giá";
                cl2.ColumnWidth = 30.0;
                Range cl3 = oSheet.get_Range("C4", "C4");
                cl3.Value2 = "Số lượng";
                cl3.ColumnWidth = 20.0;
                Range cl4 = oSheet.get_Range("D4", "D4");
                cl4.Value2 = "Thành tiền";
                cl4.ColumnWidth = 30.0;
                Range rowHead = oSheet.get_Range("A4", "D4");
                rowHead.Font.Bold = true;
                // Kẻ viền
                rowHead.Borders.LineStyle = Constants.xlSolid;
                // Thiết lập màu nền
                rowHead.Interior.ColorIndex = 15;
                rowHead.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.

                object[,] arr = new object[listView1.Items.Count, listView1.Columns.Count];

                //Chuyển dữ liệu từ DataTable vào mảng đối tượng
                int rowIndex = 0;
                for (int row = 0; row < listView1.Items.Count; row++)
                {
                    if (rowIndex <= listView1.Items.Count)
                        rowIndex++;
                    for (int col = 0; col < listView1.Columns.Count; col++)
                    {
                        arr[row, col] = listView1.Items[row].SubItems[col].Text.ToString();
                    }
                }
                //Thiết lập vùng điền dữ liệu

                int rowStart = 5;

                int columnStart = 1;

                int rowEnd = rowStart + listView1.Items.Count - 1;

                int columnEnd = listView1.Columns.Count;

                // Ô bắt đầu điền dữ liệu

                Range c1 = (Range)oSheet.Cells[rowStart, columnStart];

                // Ô kết thúc điền dữ liệu

                Range c2 = (Range)oSheet.Cells[rowEnd, columnEnd];

                // Lấy về vùng điền dữ liệu

                Range range = oSheet.get_Range(c1, c2);

                //Điền dữ liệu vào vùng đã thiết lập

                range.Value2 = arr;

                // Kẻ viền

                range.Borders.LineStyle = Constants.xlSolid;

                // Căn giữa cột STT

                Range c3 = (Range)oSheet.Cells[rowEnd, columnStart];

                Range c4 = oSheet.get_Range(c1, c3);

                oSheet.get_Range(c3, c4).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                // Tạo phần đầu nếu muốn
                string t1 = "A" + rowEnd + 1;
                Range tail = oSheet.get_Range("A" + (rowEnd + 1), "D" + (columnEnd + 6));
                tail.MergeCells = true;
                tail.Font.Bold = true;
                tail.Font.Name = "Tahoma";
                tail.Font.Size = "11";
                tail.HorizontalAlignment = XlHAlign.xlHAlignRight;
                tail.Value2 = getTongTien;

                // Save file
                oBook.SaveAs(System.Windows.Forms.Application.StartupPath + @"\ExportExcel\" + BillID + ".xls", XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);
                oExcel.Quit();
                FileInfo fil = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\ExportExcel\" + BillID + ".xls");
                if (fil.Exists == true)
                {
                    MessageBox.Show("Xuất thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Error: Lỗi chưa xác định", "Lỗi");
                return false;
            }
        }
    }
}
