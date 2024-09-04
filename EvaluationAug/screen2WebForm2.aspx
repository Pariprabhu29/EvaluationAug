<%@ Page Title="" Language="C#" MasterPageFile="~/screen2.Master" AutoEventWireup="true" CodeBehind="screen2WebForm2.aspx.cs" Inherits="EvaluationAug.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table td tr {
            border: 1px solid white;
            border-collapse: collapse;
        }

        td {
            text-align: center;
        }

        .item td tr {
            color: black;
            width: 70px;
           border:2px solid black;
        }

        .conditions {
            color: white;
        }

        #filtersAndLV {
            display: flex;
            flex-direction: column;
            margin-left: 1px;
            width: 100%;
        }


        .datacol1 {
            width: 100px;
        }

        .datacol2 {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="filtersAndLV">
        <table style="background-color: #202648">
            <tr class="conditions">
                <td cssclass="datacol2">Machine</td>
                <td>
                    <asp:DropDownList ID="machine" CssClass="datacol1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="machine_SelectedIndexChanged"></asp:DropDownList></td>
                <td cssclass="datacol2">Component</td>
                <td>
                    <asp:DropDownList ID="component" CssClass="datacol1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="component_SelectedIndexChanged"></asp:DropDownList></td>
                <td cssclass="datacol2">Operation</td>
                <td>
                    <asp:DropDownList ID="operation" CssClass="datacol1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="operation_SelectedIndexChanged"></asp:DropDownList></td>
                <td cssclass="datacol2">Charecteristic</td>
                <td>
                    <asp:ListBox ID="characteristic" SelectionMode="Multiple" Height="50px" Width="200px" runat="server"></asp:ListBox>
                </td>
            </tr>
            <tr class="conditions">
                <td cssclass="datacol2">From Date</td>
                <td>
                    <input cssclass="datacol1" id="fromDate" type="date" runat="server" />
                </td>

                <td cssclass="datacol2">To Date</td>
                <td>
                    <input cssclass="datacol1" id="toDate" type="date" runat="server" />
                </td>

                <td cssclass="datacol2">Serial No.</td>
                <td>
                    <asp:TextBox ID="serialno" CssClass="datacol1" runat="server"></asp:TextBox></td>
                <td cssclass="datacol2">Status</td>

                <td>
                    <asp:ListBox ID="status" SelectionMode="Multiple" Height="50px" Width="200px" runat="server">
                        <asp:ListItem>Ok</asp:ListItem>
                        <asp:ListItem>Rejected</asp:ListItem>
                        <asp:ListItem>Empty</asp:ListItem>
                        <asp:ListItem>Rework</asp:ListItem>
                    </asp:ListBox>
                <td>
                    <asp:Button ID="btnview" Text="View" runat="server" OnClick="btnview_Click"></asp:Button>
                    <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="btnExport_Click" />
            </tr>
        </table>
        <asp:ListView ID="LV1" runat="server" ItemPlaceholderID="itemPlaceholderRow">
            <LayoutTemplate>
                <table style=" border-collapse: collapse;">
                    <tr style="border:2px solid black;" runat="server" id="itemPlaceholderRow">
                    </tr>
                </table>
                <asp:DataPager runat="server" PageSize="4">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Button" />
                    </Fields>
                </asp:DataPager>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="item">
                    <td>
                        <asp:Label ID="lblDate" Text='<%# Eval("Date")%>' runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblShift" Text='<%# Eval("Shift")%>' runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblComponent" Text='<%# Eval("Component")%>' runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSerialNo" Text='<%# Eval("SerialNo")%>' runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:ListView DataSource='<%# Eval("DynamicColHeaders") %>' ID="LV2" runat="server" ItemPlaceholderID="itemPlaceholderCol">
                            <LayoutTemplate>
                                <td runat="server" id="itemPlaceholderCol"></td>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <td>
                                    <asp:Label ID="lblDynamicColSubHeader" Text='<%# Eval("ColSubHeaders") %>' runat="server"></asp:Label>
                                </td>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSpindleLoad" Text='<%# Eval("SpindleLoad")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblResult" Text='<%# Eval("Result")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRemarks" Text='<%# Eval("Remarks")%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                sorry no data LV1
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</asp:Content>
