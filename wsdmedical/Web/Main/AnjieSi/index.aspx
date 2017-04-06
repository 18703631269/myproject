﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.Main.AnjieSi.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安捷思</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/master.css" rel="stylesheet" />
    <link href="css/index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- ##########页首########## -->
        <!-- 顶部 -->
        <div class="container top">
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-2">
                    <a href="#" class="mail ant">
                        <span class="glyphicon glyphicon-envelope"></span>发送邮件
                    </a>
                </div>
                <div class="col-md-3">
                    <a href="#" class="phone ant">
                        <span class="glyphicon glyphicon-phone"></span>联系电话：18703631269
                    </a>
                </div>
            </div>
        </div>

        <!-- 公司logo -->
        <div class="container title-logo">
            <div class="row">
                <div class="col-md-4">
                    <a href="index.html">
                        <img class="img-responsive" src="images/logo.jpg" alt="网站logo">
                    </a>
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>

        <!-- 导航条 -->
        <nav class="navbar navbar-default" role="navigation">
            <div class="container">

                <ul class="nav navbar-nav" style="width: 100%;">
                    <li class="active nav-top">
                        <a href="index.html">首页</a>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="companyPro.html" class="dropdown-toggle on" data-toggle="dropdown">关于新纶</a>
                        <ul class="dropdown-menu">
                            <li><a href="companyPro.html">公司概况</a></li>
                            <li><a href="companyPro.html">管理架构</a></li>
                            <li><a href="companyPro.html">发展历程</a></li>
                            <li><a href="companyPro.html">产业基地</a></li>
                            <li><a href="companyPro.html">荣誉资质</a></li>
                            <li><a href="companyPro.html">营销网络</a></li>
                            <li><a href="companyPro.html">企业文化</a></li>
                            <li><a href="companyPro.html">资料下载</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="newsCenter.html" class="dropdown-toggle on" data-toggle="dropdown">新闻中心</a>
                        <ul class="dropdown-menu">
                            <li><a href="newsCenter.html">公司要闻</a></li>
                            <li><a href="newsCenter.html">行业新闻</a></li>
                            <li><a href="newsCenter.html">展会与活动</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="produces.html" class="dropdown-toggle on" data-toggle="dropdown">产品与服务</a>
                        <ul class="dropdown-menu">
                            <li><a href="produces.html">功能材料</a></li>
                            <li><a href="produces.html">净化工程</a></li>
                            <li><a href="produces.html">超净产品</a></li>
                            <li><a href="produces.html">护理产品</a></li>
                            <li><a href="produces.html">精密模具</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="develop.html" class="dropdown-toggle on" data-toggle="dropdown">产业发展</a>
                        <ul class="dropdown-menu">
                            <li><a href="develop.html">产业概况</a></li>
                            <li><a href="develop.html">电子功能材料</a></li>
                            <li><a href="develop.html">新型复合材料</a></li>
                            <li><a href="develop.html">其他产业</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="#" class="dropdown-toggle on" data-toggle="dropdown">科创中心</a>
                        <ul class="dropdown-menu">
                            <li><a href="#">研究领域</a></li>
                            <li><a href="#">科研合作</a></li>
                            <li><a href="#">研发成果</a></li>
                            <li><a href="#">技术服务</a></li>
                            <li><a href="#">中心荣誉</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="#" class="dropdown-toggle on" data-toggle="dropdown">投资者关系</a>
                        <ul class="dropdown-menu">
                            <li><a href="#">基本信息</a></li>
                            <li><a href="#">股票动态</a></li>
                            <li><a href="#">公司治理</a></li>
                            <li><a href="#">定期报告</a></li>
                            <li><a href="#">临时公告</a></li>
                            <li><a href="#">研究报告</a></li>
                            <li><a href="#">投资者交流</a></li>
                            <li><a href="#">股息资料</a></li>

                        </ul>
                    </li>

                    <li class="dropdown nav-top">
                        <a href="#" class="dropdown-toggle on" data-toggle="dropdown">人力资源</a>
                        <ul class="dropdown-menu">
                            <li><a href="#">人才理念</a></li>
                            <li><a href="#">员工活动</a></li>
                            <li><a href="#">加入新纶</a></li>

                        </ul>
                    </li>

                </ul>

            </div>
        </nav>

        <div class="line theme"></div>

        <!-- 广告轮播,根据数据库绑定轮播图--已绑定 -->
        <div id="ad-carousel" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <%if (_dtlb != null && _dtlb.Rows.Count >= 1)//逻辑必须要写到if和else中
                  {%>
                <li data-target="#ad-carousel" data-slide-to="0" class="active"></li>
                <%} %>
                <%if (_dtlb != null && _dtlb.Rows.Count >= 2)
                  { %>
                <li data-target="#ad-carousel" data-slide-to="1"></li>
                <%} %>
                <%if (_dtlb != null && _dtlb.Rows.Count >= 3)
                  { %>
                <li data-target="#ad-carousel" data-slide-to="2"></li>
                <%} %>
                <%if (_dtlb != null && _dtlb.Rows.Count >= 4)
                  { %>
                <li data-target="#ad-carousel" data-slide-to="3"></li>
                <%} %>
            </ol>
            <div class="carousel-inner">
                <div class="item active">
                    <%if (_dtlb != null && _dtlb.Rows.Count >= 1)//逻辑必须要写到if和else中
                      {%>
                    <img src="<%=_dtlb.Rows[0]["img_url"] %>" alt="1 slide" />
                    <%} %>
                </div>
                <div class="item">
                    <%if (_dtlb != null && _dtlb.Rows.Count >= 2)
                      { %>
                    <img src="<%=_dtlb.Rows[1]["img_url"] %>" alt="2 slide">
                    <%} %>
                </div>
                <div class="item">
                    <%if (_dtlb != null && _dtlb.Rows.Count >= 3)
                      { %>
                    <img src="<%=_dtlb.Rows[2]["img_url"] %>" alt="3 slide">
                    <%} %>
                </div>
                <div class="item">
                    <%if (_dtlb != null && _dtlb.Rows.Count >= 4)
                      { %>
                    <img src="<%=_dtlb.Rows[3]["img_url"] %>" alt="4 slide">
                    <%} %>
                </div>
            </div>
            <a class="left carousel-control" href="#ad-carousel" data-slide="prev"><span
                class="glyphicon glyphicon-chevron-left"></span></a>
            <a class="right carousel-control" href="#ad-carousel" data-slide="next"><span
                class="glyphicon glyphicon-chevron-right"></span></a>
        </div>
        <!-- ##########页首（end）########## -->

        <!-- ##########主体########## -->
        <div class="container">
            <!-- 栏目 -->
            <div class="row">

                <!-- 公司简介 -->
                <div class="col-md-4">
                    <span class="part1">
                        <a href="#">公司简介</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ About Us
                    </span>
                    <div class="line1">
                        <div class="line2 theme"></div>
                    </div>
                    <div>
                        <img class="img-responsive" src="images/about-us.jpg" alt="公司近景">
                        <p class="text1">
                            新纶科技股份有限公司是以中国钢研科技集团有限公司
				（原国家级大型科研院所钢铁研究总院）为主要发起人，
				联合清华紫光(集团)总公司等单位发起成立的高科技股份有限公司。
                        </p>
                    </div>
                </div>

                <!-- 公司新闻 -->
                <div class="col-md-8">
                    <span class="part1">
                        <a href="#">公司新闻</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ News
                    </span>
                    <button type="button" class="btn btn-default btn-xs more-btn">+&nbsp;MORE</button>
                    <div class="line1">
                        <div class="line2 theme"></div>
                    </div>
                    <div class="col-md-6">
                        <div id="myCarousel" class="carousel slide" data-ride="carousel">
                            <!-- 轮播（Carousel）指标 -->
                            <ol class="carousel-indicators nav-point">
                                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                <li data-target="#myCarousel" data-slide-to="1"></li>
                                <li data-target="#myCarousel" data-slide-to="2"></li>
                            </ol>
                            <!-- 轮播（Carousel）项目 -->
                            <div class="carousel-inner">
                                <div class="item active">
                                    <img src="images/news1.jpg" alt="news1">
                                    <div class="carousel-caption nav-title">公司召开2016年度工会工作总结考核交流会</div>
                                </div>
                                <div class="item">
                                    <img src="images/news3.jpg" alt="news2">
                                    <div class="carousel-caption nav-title">法国核工业巨头阿海珐集团访问新纶科技</div>
                                </div>
                                <div class="item">
                                    <img src="images/news1.jpg" alt="news3">
                                    <div class="carousel-caption nav-title">公司召开2016年度工会工作总结考核交流会</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <ul class="list-unstyled list-new">
                            <li>
                                <a href="#">公司召开2016年度工会工作总结考核交...</a><span>【2016-12-21 】</span>
                            </li>
                            <li>
                                <a href="#">推动总法律顾问制度建设，加强法律与...</a><span>【2016-12-20 】</span>
                            </li>
                            <li>
                                <a href="#">启赋新纶新材料并购基金启动仪式在常...</a><span>【2016-12-16 】</span>
                            </li>
                            <li>
                                <a href="#">法国核工业巨头阿海珐集团访问新纶科...</a><span>【2016-12-09 】</span>
                            </li>
                            <li>
                                <a href="#">新纶科技圆满完成“一企一标准一技术...</a><span>【2016-12-08 】</span>
                            </li>
                            <li>
                                <a href="#">加快推进两化融合 促进产业转型升级...</a><span>【2016-12-05 】</span>
                            </li>
                            <li>
                                <a href="#">新纶科技党委中心组学习十八届六中全...</a><span>【2016-11-18 】</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row row-2">

                <!-- 公告信息 -->
                <div class="col-md-4">
                    <span class="part1">
                        <a href="#">公告信息</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ Public Release
                    </span>
                    <div class="line1">
                        <div class="line2 theme"></div>
                    </div>
                    <div>
                        <img class="img-responsive" src="images/ggxx.jpg" alt="公告信息">
                        <ul class="list-unstyled list-new">
                            <li>
                                <a href="#">新纶科技股份有限公司关于与法国阿海珐集团合...</a><a href="#" class="public-detail">查看详情>></a>
                            </li>
                            <li>
                                <a href="#">新纶科技股份有限公司关于与法国阿海珐集团合...</a><a href="#" class="public-detail">查看详情>></a>
                            </li>
                            <li>
                                <a href="#">新纶科技股份有限公司关于与法国阿海珐集团合...</a><a href="#" class="public-detail">查看详情>></a>
                            </li>
                            <li>
                                <a href="#">新纶科技股份有限公司关于与法国阿海珐集团合...</a><a href="#" class="public-detail">查看详情>></a>
                            </li>
                            </li>
                        </ul>
                    </div>
                </div>

                <!-- 技术中心 -->
                <div class="col-md-4">
                    <span class="part1">
                        <a href="#">技术中心</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ Technology Center
                    </span>
                    <div class="line1">
                        <div class="line2 theme"></div>
                    </div>
                    <div>
                        <img class="img-responsive" src="images/jszx.jpg" alt="技术中心">
                        <p class="text1">
                            <font color="#d30a1c">新纶科技股份有限公司技术中心</font>2006年被国家发改委等五部委联合认定为“国家认定企业技术中心”。中心的定位是“技术引领、重点支撑、服务保障”，在先进材料技术、绿色能源材料技术等领域开 展前瞻性的技术创新和跨领域的技术集成，为公司产业的可持续发展提供关键技术支撑，同时面向社会提供共性技术服务。
                        </p>
                    </div>
                </div>

                <!-- 采购需求 -->
                <div class="col-md-4">
                    <span class="part1">
                        <a href="#">采购需求</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ Procurement
                    </span>
                    <div class="line1">
                        <div class="line2 theme"></div>
                    </div>
                    <div>
                        <ul class="list-unstyled procurement-li">
                            <li>
                                <a href="#">物流运输集中采购预告</a>
                            </li>
                            <li>
                                <a href="#">工业气体集中询价预告</a>
                            </li>
                            <li>
                                <a href="#">新纶科技电子采购平台域名启用</a>
                            </li>
                            <li>
                                <a href="#">联系人：姜伟 电话：010-62180969-866</a>
                            </li>
                            <li>
                                <a href="#">邮箱：caigou@atmcn.com</a>
                            </li>
                        </ul>
                        <div class="platform"><a href="#">进入采购平台</a></div>
                    </div>
                </div>
            </div>
            <div class="row">

                <!-- 产品展示 -->
                <div class="col-md-12">
                    <span class="part1">
                        <a href="#">产品展示</a>
                    </span>
                    <span class="part1 en">&nbsp;&nbsp;/ Products
                    </span>
                    <button type="button" class="btn btn-default btn-xs more-btn">+&nbsp;MORE</button>
                    <div class="line1" style="margin-bottom: 5px;">
                        <div class="line2 theme"></div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-pro">
                    <div id="Carousel" class="carousel slide" data-ride="carousel" style="margin-bottom: 30px">
                        <ol class="carousel-indicators" style="display: none;">
                            <li data-target="#Carousel" data-slide-to="0" class="active"></li>
                            <li data-target="#Carousel" data-slide-to="1"></li>
                            <li data-target="#Carousel" data-slide-to="2"></li>
                        </ol>
                        <!-- Carousel items  data-ride="carousel"-->
                        <div class="carousel-inner">
                            <div class="item active">
                                <div class="row">
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                </div>
                                <!--.row-->
                            </div>
                            <!--.item-->
                            <div class="item">
                                <div class="row">
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                </div>
                                <!--.row-->
                            </div>
                            <!--.item-->
                            <div class="item">
                                <div class="row">
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news1.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                    <div class="col-md-3 pro-images">
                                        <a href="#" class="thumbnail">
                                            <img src="images/news3.jpg" alt="Image" style="max-width: 100%;"></a>
                                    </div>
                                </div>
                                <!--.row-->
                            </div>
                            <!--.item-->
                        </div>
                        <!--.carousel-inner-->
                    </div>
                    <!--.Carousel-->
                </div>
            </div>

            <div class="row">
                <!-- 友情链接 -->
                <div class="col-md-12">
                    <h6>友情链接：</h6>
                    <ul class="list-inline youq-li">
                        <%if (_dtLk != null && _dtLk.Rows.Count > 0)
                          {
                              int numbers = 7;
                              if (_dtLk.Rows.Count < 7)
                              {
                                  numbers = _dtLk.Rows.Count;
                              }
                              for (int i = 0; i < numbers; i++)
                              { 
                        %>
                        <li><a target="_blank" href="<%=_dtLk.Rows[i]["link_url"] %>">
                            <img src="<%=_dtLk.Rows[i]["img_url"] %>"></a>

                        </li>
                        <%     }
                          } %>
                    </ul>
                </div>
            </div>
        </div>
        <!-- ##########主体（end）########## -->

        <!-- ##########页脚########## -->
        <div class="container web-footer">
            <!-- 网站地图 -->
            <div class="row" id="map-footer">
                <div class="col-md-2">
                    <dl>
                        <dt>关于新纶</dt>
                        <dd><a href="#">公司概况</a></dd>
                        <dd><a href="#">管理架构</a></dd>
                        <dd><a href="#">发展历程</a></dd>
                        <dd><a href="#">荣誉资质</a></dd>
                        <dd><a href="#">产业基地</a></dd>
                        <dd><a href="#">营销网络</a></dd>
                        <dd><a href="#">企业文化</a></dd>
                        <dd><a href="#">资料下载</a></dd>
                    </dl>
                </div>
                <div class="col-md-2">
                    <dl>
                        <dt>科技创新</dt>
                        <dd><a href="#">科创中心介绍</a></dd>
                        <dd><a href="#">研究领域</a></dd>
                        <dd><a href="#">科研合作</a></dd>
                        <dd><a href="#">技术服务</a></dd>
                        <dd><a href="#">中心荣誉</a></dd>
                    </dl>
                </div>
                <div class="col-md-2">
                    <dl>
                        <dt>产业发展</dt>
                        <dd><a href="#">产业概况</a></dd>
                        <dd><a href="#">电子功能材料</a></dd>
                        <dd><a href="#">新型复合材料</a></dd>
                        <dd><a href="#">洁净室工程与超净产品</a></dd>
                        <dd><a href="#">其它产业</a></dd>
                    </dl>
                </div>
                <div class="col-md-2">
                    <dl>
                        <dt>投资者关系</dt>
                        <dd><a href="#">基本信息</a></dd>
                        <dd><a href="#">股票动态</a></dd>
                        <dd><a href="#">公司治理</a></dd>
                        <dd><a href="#">定期报告</a></dd>
                        <dd><a href="#">临时公告</a></dd>
                        <dd><a href="#">研究报告</a></dd>
                        <dd><a href="#">投资者交流</a></dd>
                        <dd><a href="#">股息资料</a></dd>
                    </dl>
                </div>
                <div class="col-md-4" id="wx">
                    <p>扫描二维码，关注我们</p>
                    <img class="" src="images/wx.jpg" alt="wx">
                    <p>客服热线：<b style="font-size: 20px">400 000 2222</b></p>
                </div>
            </div>

            <!-- 底部 -->
            <div class="row" id="patent-footer">
                <p>© 2016 武汉安捷思科技有限公司 版权所有 | 赣ICP备06051111号 来源:<a href="http://www.mycodes.net/" target="_blank"></a>  </p>
            </div>
        </div>

        <script src="js/jquery-1.11.1.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script>

            /*导航条标题点击事件*/
            $(".dropdown-toggle").click(function () {

                if ($(this).attr('href')) {
                    window.location = $(this).attr('href');
                }
            });

            /*广告轮播*/
            $(function () {
                $('#ad-carousel').carousel();
                $('#menu-nav .navbar-collapse a').click(function (e) {
                    var href = $(this).attr('href');
                    var tabId = $(this).attr('data-tab');
                    if ('#' !== href) {
                        e.preventDefault();
                        $(document).scrollTop($(href).offset().top - 70);
                        if (tabId) {
                            $('#feature-tab a[href=#' + tabId + ']').tab('show');
                        }
                    }
                });
            });

            /*导航条*/
            $(function () {
                $(".dropdown").mouseover(function () {

                    $(this).addClass("open");
                });

                $(".dropdown").mouseleave(function () {

                    $(this).removeClass("open");
                });
            });

        </script>
    </form>
</body>
</html>
