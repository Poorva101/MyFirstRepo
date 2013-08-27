<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" MasterPageFile="~/DashBoard.master"%>
<%@ Register TagPrefix="aspUserCalendar" Namespace="DataControls" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="Content1" Runat="Server">
      
        <h3>Events From Access DB through OLEDB</h3>
               
        
        <aspUserCalendar:DataCalendar id="cal1" runat="server" Width="100%" VisibleDate="1/1/2013">      
            <DayStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Size="8" Font-Names="Arial" />
            <OtherMonthDayStyle BackColor="Gray" ForeColor="DarkGray" />
              
              
            <ItemTemplate>
                <br />
                <a href='Default4.aspx?id=<%# Container.DataItem["EventID"] %>'>
                   <img src='images/<%# Container.DataItem["Event_Image"]%>' height="12" width="12" align="absmiddle" border="0" />
                   <font color='<%# Container.DataItem["Event_Color"]%>'>
                        <%# Container.DataItem["Event_Title"]%>
                   </font>
                
                
                </a>
            
            </ItemTemplate>
            
                <NoEventsTemplate>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </NoEventsTemplate>
                
        </aspUserCalendar:DataCalendar>
        
         
    </asp:Content>


