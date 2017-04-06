<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreHtml.aspx.cs" Inherits="Web.admin.preview.PreHtml" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="FlexPaper/css/flexpaper.css" rel="stylesheet" type="text/css" />
    <script src="FlexPaper/js/jquery.min.js" type="text/javascript"></script>
    <script src="FlexPaper/js/flexpaper.js" type="text/javascript"></script>
    <script src="FlexPaper/js/flexpaper_handlers.js" type="text/javascript"></script>
    <style type="text/css">
        *{margin:0;padding:0;}
        html,body{height:100%;width:100%;}
        #form1{width:100%;height:100%;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="lit" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="litR" Visible="false" runat="server"></asp:Literal>
                <div id="documentViewer" class="flexpaper_viewer" style="width:100%;height:99%; margin:0; padding:0; "></div>
          <script type="text/javascript">
              function getContentPath() {
                  var pathName = document.location.pathname;
                  var index = pathName.substr(1).indexOf("/");
                  var path = pathName.substr(0, index + 1);
                  return path;
              }
              $('#documentViewer').FlexPaperViewer({
                  config: {
                      SwfFile: '<%=lit.Text%>',
                      Scale: 0.6,
                      ZoomTransition: 'easeOut',
                      ZoomTime: 0.5,
                      ZoomInterval: 0.2,
                      FitPageOnLoad: true,
                      FitWidthOnLoad: true,
                      FullScreenAsMaxWindow: false,
                      ProgressiveLoading: false,
                      MinZoomSize: 0.2,
                      MaxZoomSize: 5,
                      SearchMatchAll: false,
                      InitViewMode: 'Portrait',
                      RenderingOrder: 'flash',

                      ViewModeToolsVisible: true,
                      ZoomToolsVisible: true,
                      NavToolsVisible: true,
                      CursorToolsVisible: true,
                      SearchToolsVisible: true,
                      localeChain: 'zh_CN',
                      jsDirectory: "FlexPaper/js/",
                      cssDirectory:"FlexPaper/css/"
                  }
              });
          </script>

       <%--<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%"
  height="100%" codebase="http://active.macromedia.com/flash5/cabs/swflash.cab#version=8,0,0,0">
         <param name="MOVIE" value='<%=lit.Text %>'>
         <param name="PLAY" value="true">
         <param name="LOOP" value="true">
         <param name="QUALITY" value="high">
         <param name="FLASHVARS" value="zoomtype=1">
        <embed src="<%=lit.Text %>" width="100%" height="99%" play="true" ALIGN="" loop="true" quality="high" type="application/x-shockwave-flash" flashvars="zoomtype=1"
  pluginspage="http://www.macromedia.com/go/getflashplayer">  
 </embed>
 </object>--%>
    </form></body>
</html>