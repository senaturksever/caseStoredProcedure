using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SenaTest
{
    public partial class _Default : Page
    {
        private bool IsTableCreated = false;

        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        private void GetStockStatus()
        {
            try
            {
                //veritabanı stored procedure'e verdiğimiz ismi tanımladık
                string procedurName = "StockStatus";

                //web textboxlarından malkodunu ve tarihleri aldık.
                string order = txtOrderID.Value;
                DateTime firstDate = Convert.ToDateTime(txtfirstDate.Value);
                DateTime lastDate = Convert.ToDateTime(txtlastDate.Value);

                //tarihleri oaDate cinsine çevirdik
                int oaFirstDate = Convert.ToInt32(firstDate.ToOADate());
                int oaLastDate = Convert.ToInt32(lastDate.ToOADate());
                if (oaFirstDate > 0 && oaLastDate > 0 && !string.IsNullOrEmpty(order))
                {
                    //değişkenleri parametre olarak atadık. 
                    var parameter = new SqlParameter[3];
                    parameter[0] = Database.SetParameter("@Malkodu", System.Data.SqlDbType.NVarChar, 50, "Input", order);
                    parameter[1] = Database.SetParameter("@BaslangicTarihi", System.Data.SqlDbType.Int, 50, "Input", oaFirstDate);
                    parameter[2] = Database.SetParameter("@BitisTarihi", System.Data.SqlDbType.Int, 50, "Input", oaLastDate);

                    dt = Database.ExecuteStoredProcedure2DataTable(procedurName, parameter);
                    if (dt.Rows.Count > 0)
                    {
                        //veritabanından gelen değişkenleri for ile döndük
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var siraNo = dt.Rows[i]["SiraNo"].ToString();
                            var islemTur = dt.Rows[i]["IslemTur"].ToString();
                            var evrakNo = dt.Rows[i]["EvrakNo"].ToString();
                            var tarih = dt.Rows[i]["Tarih"].ToString();
                            var girisMiktar = dt.Rows[i]["GirisMiktar"].ToString();
                            var cikisMiktar = dt.Rows[i]["CikisMiktar"].ToString();
                            var stok = dt.Rows[i]["Stok"].ToString();


                            //asp.net'e özgü kontrollerden birisi olan Literal nesnesini tanımlayarak Text formatında HTML içeriğini temsil ettik ve pnlStock_list id'li tablomuza ekledik
                            var lt = new Literal
                            {
                                Text = "<tr><td>" + siraNo + "</td>" +
                                "<td>" + islemTur + "</td>" +
                                "<td>" + evrakNo + "</td>" +
                                "<td>" + tarih + "</td>" +
                                "<td>" + girisMiktar + "</td>" +
                                "<td>" + cikisMiktar + "</td>" +
                                "<td>" + stok + "</td></tr>"
                            };
                            pnlStock_List.Controls.Add(lt);
                        }



                    }
                    //dt değişkenini tutmak icin viewState'e kaydettik
                    ViewState["dt"] = dt;

                    IsTableCreated = true;

                    //eğer istenilirse butona tıklandığında excel formatına aktardık. 
                    BtnExcel.ServerClick += new EventHandler(BtnExcel_ServerClick);

                }
                else
                {
                    //log kaydı
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void GetExcelStockStatus()
        {
            try
            {
                //veritabanı stored procedure'e verdiğimiz ismi tanımladık
                string procedurName = "StockStatus";

                //web textboxlarından malkodunu ve tarihleri aldık.
                string order = txtOrderID.Value;
                DateTime firstDate = Convert.ToDateTime(txtfirstDate.Value);
                DateTime lastDate = Convert.ToDateTime(txtlastDate.Value);

                //tarihleri oaDate cinsine çevirdik
                int oaFirstDate = Convert.ToInt32(firstDate.ToOADate());
                int oaLastDate = Convert.ToInt32(lastDate.ToOADate());
                if (oaFirstDate > 0 && oaLastDate > 0 && !string.IsNullOrEmpty(order))
                {
                    //değişkenleri parametre olarak atadık. 
                    var parameter = new SqlParameter[3];
                    parameter[0] = Database.SetParameter("@Malkodu", System.Data.SqlDbType.NVarChar, 50, "Input", order);
                    parameter[1] = Database.SetParameter("@BaslangicTarihi", System.Data.SqlDbType.Int, 50, "Input", oaFirstDate);
                    parameter[2] = Database.SetParameter("@BitisTarihi", System.Data.SqlDbType.Int, 50, "Input", oaLastDate);

                    dt = Database.ExecuteStoredProcedure2DataTable(procedurName, parameter);
                    if (dt.Rows.Count > 0)
                    {
                        GetExcelExport(dt);
                    }

                }
                else
                {
                    //log kaydı
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void BtnStockStatus_ServerClick(object sender, EventArgs e)
        {
            GetStockStatus();
        }

        protected void BtnExcel_ServerClick(object sender, EventArgs e)
        {
            if (IsTableCreated)
            {
                GetExcelExport(dt);
            }
            else
            {
                GetExcelStockStatus();
            }
        }
        private void GetExcelExport(DataTable dt)
        {
            try
            {
                dt = ViewState["dt"] as DataTable;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Stok Bilgisi");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Stok_Bilgisi.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}