using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.Function
{
    public partial class category_edit1 : Web.UI.ManagePage
    {
        string action = DTRequest.GetQueryString("action");//具体操作内容
        int id = DTRequest.GetQueryInt("id");//id
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                quanxian = DTRequest.GetQueryString("qx");
                if (!Page.IsPostBack)
                {
                    TreeBind();
                    ActionTypeBindOnlyShow();
                    if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                    {
                        ChkAdminLevel(id, "Edit"); //检查权限
                        ShowInfo(id);
                    }
                    else
                    {
                        if (id > 0)
                        {
                            ChkAdminLevel(id, "Add"); //检查权限
                            ddlParentId.SelectedValue = id.ToString();
                            BLL.navigationBll bll = new BLL.navigationBll();
                            Model.navigation model = bll.GetModel(id);
                            if (bll.GetIds(id) == Convert.ToString(id))//如果相同则表示此id不存在子类
                            {
                                if (model.link_url.Trim() != "")
                                {
                                    JscriptMsg("页面下不能再进行添加内容！", "category_list.aspx", "parent.loadMenuTree");
                                }
                                else
                                {
                                    ActionTypeBindAll();
                                }
                            }
                            else
                            {
                                ActionTypeBindOnlyShow();
                            }
                        }
                        //验证别名会不会重复 
                        txtCallIndex.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit1.aspx", "Page_Load", ex.Message);
            }
        }

        /// <summary>
        /// 绑定权限类型--所有
        /// </summary>
        private void ActionTypeBindAll()
        {
            cblActionType.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
            {
                cblActionType.Items.Add(new ListItem(kvp.Value + "(" + kvp.Key + ")", kvp.Key));
            }
        }


        /// <summary>
        /// 绑定权限类型--单独的显示
        /// </summary>
        private void ActionTypeBindOnlyShow()
        {
            cblActionType.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionShowOnly())
            {
                cblActionType.Items.Add(new ListItem(kvp.Value + "(" + kvp.Key + ")", kvp.Key));
            }
        }
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
                    JscriptMsg("修改类别成功！", "category_list.aspx", "parent.loadMenuTree");
                }
                else //添加
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", "");
                        return;
                    }
                    JscriptMsg("添加类别成功！", "category_list.aspx", "parent.loadMenuTree");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit1.aspx", "btnSubmit_Click", ex.Message);
            }
        }

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                Model.navigation model = new Model.navigation();
                BLL.navigationBll bll = new BLL.navigationBll();
                model.parent_id = int.Parse(ddlParentId.SelectedValue);//parent_id
                //model.nav_type = "System";
                model.title = txtTitle.Text.Trim();
                if (model.parent_id == 0)
                {
                    model.sub_title = model.title;
                }
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.icon_url = txtImgUrl.Text;
                model.link_url = txtLinkUrl.Text;
                model.remark = txtTitleRemark.Text;
                model.is_lock = Utils.StrToInt(rblStatus.SelectedValue, 0);
                model.name = txtCallIndex.Text.Trim();//标题别名
                //添加操作权限类型
                string action_type_str = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        action_type_str += cblActionType.Items[i].Value + ",";
                    }
                }
                model.action_type = Utils.DelLastComma(action_type_str);
                if (bll.Add(model) > 0)
                {
                    AddAdminLog("新增","新增" + model.title); //记录日志
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit1.aspx", "DoAdd", ex.Message);
                return false;
            }
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.navigationBll bll = new BLL.navigationBll();
                Model.navigation model = bll.GetModel(_id);

                int parentId = int.Parse(ddlParentId.SelectedValue);
                model.name = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.link_url = txtLinkUrl.Text.Trim();
                //如果选择的父ID不是自己,则更改
                if (parentId != model.id)
                {
                    model.parent_id = parentId;
                }
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                string action_type_str = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        action_type_str += cblActionType.Items[i].Value + ",";
                    }
                }
                model.is_lock = Utils.StrToInt(rblStatus.SelectedValue, 0);
                model.action_type = Utils.DelLastComma(action_type_str);
                if (bll.Update(model))
                {
                    AddAdminLog("编辑", "修改" + model.title); //记录日志
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("category_edit1.aspx", "DoEdit", ex.Message);
                return false;
            }
        }
        #endregion


        #region 绑定类别=================================
        private void TreeBind()
        {
            BLL.navigationBll bll = new BLL.navigationBll();
            DataTable dt = bll.GetList(0);

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

        #region 编辑赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.navigationBll bll = new BLL.navigationBll();
            Model.navigation model = bll.GetModel(_id);
            ddlParentId.Enabled = false;
            if (model.is_sys == 1)
            {
                txtCallIndex.ReadOnly = true;
            }
            ddlParentId.SelectedValue = model.parent_id.ToString();
            if (ddlParentId.SelectedValue == "0")
            {
                ActionTypeBindOnlyShow();
            }
            else
            {
                if (bll.GetIds(_id) == Convert.ToString(_id))//如果相同则表示此id不存在子类
                {
                    ActionTypeBindAll();
                }
                else
                {
                    ActionTypeBindOnlyShow();
                }
            }
            txtTitle.Text = model.title;
            txtLinkUrl.Text = model.link_url;
            txtSortId.Text = model.sort_id.ToString();
            rblStatus.SelectedValue = model.is_lock.ToString();
            txtCallIndex.Text = model.name.ToString();
            txtCallIndex.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate&old_name=" + Utils.UrlEncode(model.name));
            txtCallIndex.Focus(); //设置焦点，防止JS无法提交
            //赋值操作权限类型
            string[] actionTypeArr = model.action_type.Split(',');
            for (int i = 0; i < cblActionType.Items.Count; i++)
            {
                for (int n = 0; n < actionTypeArr.Length; n++)
                {
                    if (actionTypeArr[n].ToLower() == cblActionType.Items[i].Value.ToLower())
                    {
                        cblActionType.Items[i].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

        protected void ddlParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seltname = ddlParentId.SelectedValue;
            BLL.navigationBll bll = new BLL.navigationBll();
            int tid = Common.Utils.StrToInt(seltname, 0);
            if (tid == 0)
            {
                ActionTypeBindOnlyShow();
            }
            else
            {
                if (bll.GetIds(tid) == seltname)//如果相同则表示此id不存在子类
                {
                    ActionTypeBindAll();
                }
                else
                {
                    ActionTypeBindOnlyShow();
                }
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