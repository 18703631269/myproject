using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Web.UI;
using BLL;
using System.Configuration;

namespace Web.admin.article
{
    public partial class article_list : ManagePage
    {
        protected int channel_id = 0;
        protected int totalCount;
        protected int page;
        protected static int pageSize;

        protected int category_id = 0;
        protected string channel_name = string.Empty;

        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = "Txt";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                channel_id = DTRequest.GetQueryInt("channel_id");
                category_id = DTRequest.GetQueryInt("category_id");
                keywords = DTRequest.GetQueryString("keywords");
                property = DTRequest.GetQueryString("property");

                a_add.HRef ="article_edit.aspx?action=Add&channel_id="+channel_id;
                //channel_id = 14;
                if (channel_id == 0)
                {
                    JscriptMsg("频道参数不正确！", "back");
                    return;
                }
                channelBll clBll = new channelBll();
                channel_name = clBll.GetChannelName(channel_id);
                pageSize = GetPageSize(15); //每页数量
                prolistview = Session["showview"] == "Img" ? "Img" : "Txt"; //设置显示方式
                if (!Page.IsPostBack)
                {
                    Bindquanxian();//绑定权限
                    TreeBind(channel_id); //绑定类别
                    RptBind(channel_id, category_id, "id>0" + CombSqlTxt(keywords, category_id, property), "sort_id asc,add_time desc,id desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "Page_Load", ex.Message);
            }
        }
        public void Bindquanxian()
        {
            try
            {
                Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
                if (adminModel == null)
                {
                    return;
                }
                string pagename = Request.Params["qx"];
                DataTable _dt = new DataTable();
                if (adminModel.role_type == 1)//表示是超级管理员
                {
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 1);//新增
                    AuthorityPage.QuanXianVis(btnSave, "Save", _dt, 1);//保存
                    AuthorityPage.QuanXianVis(btnAudit, "Audit", _dt, 1);//审核
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 0);//新增
                    AuthorityPage.QuanXianVis(btnSave, "Save", _dt, 0);//保存
                    AuthorityPage.QuanXianVis(btnAudit, "Audit", _dt, 0);//审核
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            article_categoryBll bll = new article_categoryBll();
            DataTable dt = bll.GetList(0,_channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("无父级分类", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer-1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, int _category, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }

            if (_category > 0)
            {
                strTemp.Append(" and category_id =" + _category);
            }

            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock"://锁定
                        strTemp.Append(" and status=0");
                        break;
                    case "unIsLock":
                        strTemp.Append(" and status=1");
                        break;
                    case "unIsShow"://状态2表示不显示
                        strTemp.Append(" and status=2");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(int _channel_id, int _category_id, string _strWhere, string _orderby)
        {
            page = DTRequest.GetQueryInt("page", 1);
            if (category_id > 0)
            {
                ddlCategoryId.SelectedValue = _category_id.ToString();
            }
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            //图表或列表显示
            articleBll bll = new articleBll();
            switch (prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetList(_channel_id, _category_id, pageSize, page, _strWhere, _orderby, out this.totalCount);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetList(_channel_id, _category_id, pageSize, page, _strWhere, _orderby, out this.totalCount);
                    this.rptList2.DataBind();
                    break;
            }
            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                _channel_id.ToString(), _category_id.ToString(), keywords, property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                    channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "btnSearch_Click", ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                articleBll bll = new articleBll();
                Repeater rptList = new Repeater();
                switch (this.prolistview)
                {
                    case "Txt":
                        rptList = this.rptList1;
                        break;
                    default:
                        rptList = this.rptList2;
                        break;
                }
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    int sortId;
                    if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                    {
                        sortId = 99;
                    }
                    bll.UpdateField(id, "sort_id=" + sortId.ToString());
                }
                AddAdminLog("编辑", "修改" + channel_name + "内容排序"); //记录日志
                JscriptMsg("保存排序成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                           channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "btnSave_Click", ex.Message);
            }
        }
        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize = pageSize;//获取设置的新的分页个数
            if (_pagesize > 0)
            {
                return _pagesize;
            }
            return _default_size;
        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int sucCount = 0; //成功数量
                int errorCount = 0; //失败数量
                articleBll bll = new articleBll();
                Repeater rptList = new Repeater();
                switch (this.prolistview)
                {
                    case "Txt":
                        rptList = this.rptList1;
                        break;
                    default:
                        rptList = this.rptList2;
                        break;
                }
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                    if (cb.Checked)
                    {
                        if (bll.Delete(id))
                        {
                            sucCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                }
                AddAdminLog("编辑", "删除" + channel_name + "内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
                JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                  channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "btnDelete_Click", ex.Message);
            }
        }

        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //channel_id = 14;
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                   channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "ddlCategoryId_SelectedIndexChanged", ex.Message);
            }
        }

        /// <summary>
        /// 设置置顶等效果
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
                articleBll bll = new articleBll();
                Model.article model = bll.GetModel(id);
                RptBind(this.channel_id, this.category_id, "id>0" + CombSqlTxt(keywords, category_id, property), "sort_id asc,add_time desc,id desc");
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "rptList_ItemCommand", ex.Message);
            }
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                    channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "ddlProperty_SelectedIndexChanged", ex.Message);
            }
        }

        protected void lbtnViewImg_Click(object sender, EventArgs e)
        {
            try
            {
                Session["showview"] = "Img";
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                    this.channel_id.ToString(), category_id.ToString(), keywords, property, page.ToString()));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "lbtnViewImg_Click", ex.Message);
            }
        }

        protected void lbtnViewTxt_Click(object sender, EventArgs e)
        {
            try
            {
                Session["showview"] = "Txt";
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                    this.channel_id.ToString(), category_id.ToString(), keywords, property, page.ToString()), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "lbtnViewTxt_Click", ex.Message);
            }
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int _pagesize = 0;
                if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
                {
                    pageSize = _pagesize;
                }
                Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                     channel_id.ToString(), ddlCategoryId.SelectedValue, txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.articleBll bll = new BLL.articleBll();
                Repeater rptList = new Repeater();
                switch (this.prolistview)
                {
                    case "Txt":
                        rptList = this.rptList1;
                        break;
                    default:
                        rptList = this.rptList2;
                        break;
                }
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                    if (cb.Checked)
                    {
                        bll.UpdateField(id, "status=1");
                    }
                }
                AddAdminLog("编辑", "审核" + channel_name + "内容信息");
                JscriptMsg("批量审核成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                        channel_id.ToString(), ddlCategoryId.SelectedValue, keywords, property));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_list.aspx", "btnAudit_Click", ex.Message);
            }
        }

        /// <summary>
        /// 获取当前页面对应的关联id--可设置点击时,弹出框,不允许访问,类似编辑时的提示
        /// </summary>
        /// <returns></returns>
        public int GetPage()
        {
            int tid = 0;
            string pagename = Request.RawUrl.ToString();
            if (pagename.IndexOf('/') >= 0)
            {
                pagename = pagename.Substring(pagename.LastIndexOf('/') + 1);
            }
            navigationBll bll = new navigationBll();
            manager_roleBll bllRole = new manager_roleBll();
            tid = bll.GetIdByPage(pagename);//表示页面对应的id
            return tid;
        }

    }
}