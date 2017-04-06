<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreCad.aspx.cs" Inherits="Web.admin.preview.PreCad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style type="text/css">
        *{margin:0;padding:0;}
        html,body{height:100%;width:100%;}
        #form1{width:100%;height:100%;}

    </style>
    <SCRIPT language="JavaScript">
        document.oncontextmenu = new Function('event.returnValue=false;'); //禁用右键
</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
        <object classid="clsid:74A777F8-7A8F-4e7c-AF47-7074828086E2" id="MxDrawXCtrl"  width="100%" height="100%"> 
           <param name="ToolBarFiles" value="~/MX/MxDraw-ToolBar-ET.mxt">
  <param name="_Version" value="65536">
  <param name="_ExtentX" value="24262">
  <param name="_ExtentY" value="16219">
  <param name="_StockProps" value="0">
  <param name="DwgFilePath" value="<%=Literal1.Text%>">
  <param name="IsRuningAtIE" value="1">
  <param name="EnablePrintCmd" value="0">
</object>
    </form>
</body>
</html>
