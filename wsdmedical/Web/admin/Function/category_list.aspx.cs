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

namespace Web.admin.Function
{
    public partial class category_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected string channel_name = string.Empty; //频道名称

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Bindquanxian();
                    RptBind();
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_list.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        //数据绑定
        private void RptBind()
        {
            BLL.navigationBll bll = new BLL.navigationBll();
            DataTable dt = bll.GetList(0);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 39, LitImg2, LitImg1);
                }
            }
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.navigationBll bll = new BLL.navigationBll();
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
                AddAdminLog("编辑", "保存" + channel_name + "类别"); //记录日志
                JscriptMsg("保存排序成功！", Utils.CombUrlTxt("category_list.aspx", "", this.channel_id.ToString()), "parent.loadMenuTree");
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_list.aspx", "btnSave_Click", ex.Message);
            }
        }

        //删除类别
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.navigationBll bll = new BLL.navigationBll();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                    if (cb.Checked)
                    {
                        bll.Delete(id);
                    }
                }
                AddAdminLog("删除", "删除" + channel_name + "类别"); //记录日志
                JscriptMsg("删除数据成功！", Utils.CombUrlTxt("category_list.aspx", "channel_id={0}", this.channel_id.ToString()), "parent.loadMenuTree");
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_list.aspx", "btnSave_Click", ex.Message);
            }
        }

    }
}