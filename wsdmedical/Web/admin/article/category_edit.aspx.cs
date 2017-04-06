using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Web.UI;
using BLL;
using System.Configuration;

namespace Web.admin.article
{
    public partial class category_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected string quanxian = string.Empty;//权限
        protected int channel_id;
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string _action = DTRequest.GetQueryString("action");
                channel_id = DTRequest.GetQueryInt("channel_id");
                quanxian = DTRequest.GetQueryString("qx") + "?channel_id=" + channel_id;
                id = DTRequest.GetQueryInt("id");

                if (channel_id == 0)
                {
                    JscriptMsg("频道参数不正确！", "back");
                    return;
                }
                //channel_id = 14;
                channel_name = new channelBll().GetChannelName(channel_id); //取得频道名称
                if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
                {
                    this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                    if (this.id == 0)
                    {
                        JscriptMsg("传输参数不正确！", "back");
                        return;
                    }
                    if (!new article_categoryBll().Exists(id))
                    {
                        JscriptMsg("类别不存在或已被删除！", "back");
                        return;
                    }
                }
                if (!Page.IsPostBack)
                {
                    TreeBind(channel_id); //绑定类别
                    if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                    {
                        ChkAdminLevel(id, "Edit"); //检查权限
                        ShowInfo(id);
                    }
                    else
                    {
                        if (id > 0)
                        {
                            ddlParentId.SelectedValue = id.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit.aspx", "Page_Load", ex.Message);
            }
        }

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            article_categoryBll bll = new BLL.article_categoryBll();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级分类", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            article_categoryBll bll = new article_categoryBll();
            Model.article_category model = bll.GetModel(_id);
            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtCallIndex.Text = model.call_index;//别名
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    if (!DoEdit(this.id))
                    {

                        JscriptMsg("保存过程中发生错误！", "");
                        return;
                    }
                    JscriptMsg("修改类别成功！", "category_list.aspx?channel_id=" + channel_id);
                }
                else //添加
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", "");
                        return;
                    }
                    JscriptMsg("添加类别成功！", "category_list.aspx?channel_id=" + channel_id);
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit.aspx", "btnSubmit_Click", ex.Message);
            }
        }

        private bool DoAdd()
        {
            try
            {
                Model.article_category model = new Model.article_category();
                article_categoryBll bll = new article_categoryBll();
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.parent_id = int.Parse(ddlParentId.SelectedValue);
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                if (bll.Add(model) > 0)
                {
                    AddAdminLog("新增", "添加" + channel_name + "分类:" + model.title); //记录日志
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit.aspx", "btnSubmit_Click", ex.Message);
                return false;
            }
        }

        private bool DoEdit(int _id)
        {
            try
            {
                article_categoryBll bll = new article_categoryBll();
                Model.article_category model = bll.GetModel(_id);
                int parentId = int.Parse(ddlParentId.SelectedValue);
                model.channel_id = channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                //如果选择的父ID不是自己,则更改
                if (parentId != model.id)
                {
                    model.parent_id = parentId;
                }
                if (bll.Update(model))
                {
                    AddAdminLog("编辑", "修改" + channel_name + "分类:" + model.title); //记录日志
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit.aspx", "btnSubmit_Click", ex.Message);
                return false;
            }
        }

        public int GetPage()
        {
            int tid = 0;
            navigationBll bll = new navigationBll();
            manager_roleBll bllRole = new manager_roleBll();
            tid = bll.GetIdByPage(quanxian);//表示页面对应的id
            return tid;
        }
    }
}