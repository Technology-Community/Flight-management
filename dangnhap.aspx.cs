using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class dangnhap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string password = TextBox2.Text;
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        AccessDataSource vd3 = new AccessDataSource();
        vd3.DataFile = Server.MapPath(".") + "//App_Data/db1.mdb";
        vd3.SelectCommandType = SqlDataSourceCommandType.Text;
        vd3.SelectCommand = "Select Matkhau from Table1 where matkhau=@mk and tendn=@tdn";
        vd3.SelectParameters.Add("mk", TypeCode.String, password);
        vd3.SelectParameters.Add("tdn", TypeCode.String, TextBox1.Text);
        GridView1.DataSource = vd3;
        GridView1.DataBind();
        GridView1.Visible = false;
        if (GridView1.Rows.Count == 0)
        {
            Label4.Text = "Dang nhap khong dung";
        }
        else
        {
            AccessDataSource vd2 = new AccessDataSource();
            vd2.DataFile = Server.MapPath(".") + "//App_Data/db1.mdb";
            vd2.SelectCommandType = SqlDataSourceCommandType.Text;
            vd2.SelectCommand = "Select * from Table1 where tendn=@tdn and loainguoidung<>'0'";
            vd2.SelectParameters.Add("tdn", TypeCode.String, TextBox1.Text);
            GridView2.DataSource = vd2;
            GridView2.DataBind();

            if (GridView2.Rows.Count == 1)
            {
                Session["tendn"] = "admin";
                Response.Redirect("trangadmin.aspx");
            }
            else
            {
                Session["tendn"] = TextBox1.Text;
                Response.Redirect("trangthanhvien.aspx");
            }

        }
    }
}