using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for DataCalendar
/// </summary>

namespace DataControls
{
    public class DataCalendarItem : Control , INamingContainer
    {

        private DataRow _dataItem;

        public DataCalendarItem(DataRow dr)
        {
            _dataItem = dr;
        }

        public DataRow DataItem {
            get { return _dataItem; }
            set {_dataItem= value;}
        }
    }

    public class DataCalendar : Calendar, INamingContainer
    {
        private object _dataSource;
        private string _dataMember;
        private String _dayField;
        private ITemplate _itemTemplate;
        private ITemplate _noEventsTemplate;
        private TableItemStyle _dayWithEventsStyle;
        private DataTable _dtSource;

        public object DataSource {

            get { return _dataSource; }

            set {
                if (value is DataTable || value is DataSet)
                {
                    _dataSource = value;
                }
                else
                    throw new Exception("The DataSource property of the DataCalendar control" +
                                        " must be a DataTable or DataSet object");
            }

        }

        public string DataMember
        {
            get { return _dataMember; }
            set { _dataMember = value; }
        }

        public string DayField
        {
            get { return _dayField; }
            set{_dayField= value;}
        }

        public TableItemStyle DayWithEventsStyle
        {
            get { return _dayWithEventsStyle; }
            set { _dayWithEventsStyle = value; }
        }

        [TemplateContainer(typeof(DataCalendarItem))]
        public ITemplate ItemTemplate
        {
            get { return _itemTemplate; }
            set { _itemTemplate = value; }
        }


        [TemplateContainer(typeof(DataCalendarItem))]
        public ITemplate NoEventsTemplate
        {
            get { return _noEventsTemplate; }
            set { _noEventsTemplate = value; }
        }

        //Constructor
        public DataCalendar(): base()
        {
            this.SelectionMode= CalendarSelectionMode.None;
            this.ShowGridLines= true;
        
        }


        private void SetupCalendarItem(TableCell cell, DataRow r, ITemplate t)
        {
            DataCalendarItem dti = new DataCalendarItem(r);
            t.InstantiateIn(dti);
            dti.DataBind();
            cell.Controls.Add(dti);
        
        }

        protected override void OnDayRender(TableCell cell, CalendarDay day)
        {
            if (_dtSource != null)
            {
                DataView dv = new DataView(_dtSource);
                dv.RowFilter = string.Format(
                   "{0} >= #{1}# and {0} < #{2}#",
                   this.DayField,
                   day.Date.ToString("MM/dd/yyyy"),
                   day.Date.AddDays(1).ToString("MM/dd/yyyy")
                );


                if (dv.Count > 0)
                {
                    if (this.DayWithEventsStyle != null)
                        cell.ApplyStyle(this.DayWithEventsStyle);

                    if (this.ItemTemplate != null)
                        for (int i = 0; i < dv.Count; i++)
                        {
                        SetupCalendarItem(cell, dv[i].Row, this.ItemTemplate);
                        }
                }
                else
                if (this.NoEventsTemplate != null)
                {
                    SetupCalendarItem(cell, null, this.NoEventsTemplate);
                }


            // call the base render method too
            base.OnDayRender(cell, day);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            _dtSource = null;

            if (this.DataSource != null && this.DayField != null)
            {
                // determine if the datasource is a DataSet or DataTable
                if (this.DataSource is DataTable)
                    _dtSource = (DataTable)this.DataSource;
                if (this.DataSource is DataSet)
                {
                    DataSet ds = (DataSet)this.DataSource;
                    if (this.DataMember == null || this.DataMember == "")
                        // if data member isn't supplied, default to the first table
                        _dtSource = ds.Tables[0];
                    else
                        // if data member is supplied, use it
                        _dtSource = ds.Tables[this.DataMember];
                }
                // throw an exception if there is a problem with the data source
                if (_dtSource == null)
                    throw new Exception("Error finding the DataSource.  Please check " +
                                        " the DataSource and DataMember properties.");
            

            }

            // call the base Calendar's Render method
            // allowing OnDayRender() to be executed
            base.Render(writer);
   
}


        }


}
