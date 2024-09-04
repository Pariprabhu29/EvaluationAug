<%@ Page Title="" Language="C#" MasterPageFile="~/screen1Master.Master" AutoEventWireup="true" CodeBehind="Screen1WebForm.aspx.cs" Inherits="EvaluationAug.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            border-radius: 100px;
            margin: 0 20px;
        }

        tr.values {
            background-color: #3051A0;
            height: 80px;
            width:auto;
        }

        td.values {
            font-size: 70px;
            color: #FFF10F;
            border-radius: 10px;
            text-align: center;
            width: 300px;
        }

        tr.headers {
            font-size: 30px;
            color: #014468;
            text-align: center;
            background-color: #B3C5EC;
            height: 60px;
            width:500px;
        }

        td.headers {
            font-size: 30px;
            border-bottom-right-radius: 10px;
            border-bottom-left-radius: 10px;
             width: 300px;
        }

        .row {
            display: flex;
            margin-left: 100px;
            margin-right: 50px;
            justify-content: center; /* Center the rows horizontally */
        }

        .column {
            padding: 5px;
            display: flex;
            flex-direction: row;
            padding-block: 60px;
            padding-right: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>

    <asp:Timer ID="Timer1" Enabled="True" Interval="10000" OnTick="Event_Tick" runat="server"></asp:Timer>

    <div runat="server" style="background-color: #F4F4F4">
        <div style="text-align: center; background-color: #114985; color: white; width: auto; font-size: 30px">KTA SPINDLE TOOLING </div>
        <div class="row">
            <div class="column" runat="server">
                <table>
                    <tr class="values">
                        <td class="values">
                            <asp:Label ID="target" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="headers">
                        <td class="headers">Cumulative Target Qty.</td>
                    </tr>
                </table>

                <table>
                    <tr class="values">
                        <td class="values">
                            <asp:Label ID="production" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="headers">
                        <td class="headers">Cumulative Production Qty.</td>
                    </tr>
                </table>
                <table>
                    <tr class="values">
                        <td class="values">
                            <asp:Label ID="shortfall" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="headers">
                        <td class="headers">Shortfall Qty.</td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="column">
                <table>
                    <tr class="values">
                        <td class="values">
                            <asp:Label ID="rejection" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="headers">
                        <td class="headers">Rejection Qty.</td>
                    </tr>
                </table>
                <table>
                    <tr class="values">
                        <td class="values">
                            <asp:Label ID="rework" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="headers">
                        <td class="headers">Rework Qty.
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
