using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cal1.DataSource = GetEventData();
        cal1.DayField = "EventDate";
    }

    DataTable GetEventData()
    {

        // We'll see if the events have already been loaded into a session
        // variable.  
        if (Session["EventData"] == null || Request["refresh"] == "1")
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from [ALRIMI].[dbo].[Event_Details]";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            Session["EventData"] = ds.Tables[0];

        }

        return (DataTable)Session["EventData"];



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    private string GetConnectionString()
    {
 	    return System.Configuration.ConfigurationManager.ConnectionStrings["ALRIMIConnectionString"].ConnectionString;
    }

}
