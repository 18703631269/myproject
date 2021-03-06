﻿using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.plugins
{
    public partial class link_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        int id = 0;
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                quanxian = DTRequest.GetQueryString("qx");
                action = DTRequest.GetQueryString("action");
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    id = DTRequest.GetQueryInt("id");
                    if (id == 0)
                    {
                        JscriptMsg("传输参数不正确！", "back");
                    }
                    if (!new linkBll().Exists(id))
                    {
                        base.JscriptMsg("内容不存在或已被删除！", "back");
                        return;
                    }
                }
                if (!Page.IsPostBack)
                {
                    if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                    {
                        ChkAdminLevel(id, "Edit"); //检查权限
                        ShowInfo(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("link_edit.aspx", "Page_Load", ex.Message);
            }
        }

        private void ShowInfo(int _id)
        {
            try
            {
                link model = new linkBll().GetModel(_id);

                txtTitle.Text = model.title;

                rblIsLock.SelectedValue = model.is_lock.ToString();
                txtSortId.Text = model.sort_id.ToString();
                txtSiteUrl.Text = model.link_url;
                txtImgUrl.Text = model.img_url;
            }
            catch (Exception ex)
            {
                throw new Exception("--link_edit-->ShowInfo" + ex.Message, ex);
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
                        JscriptMsg("保存过程中发生错误！", string.Empty);
                    }
                    else
                    {
                        JscriptMsg("修改幻灯片成功！", "link_index.aspx");
                    }
                }
                else
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", string.Empty);
                    }
                    else
                    {
                        JscriptMsg("添加幻灯片成功！", "link_index.aspx");
                    }

                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("link_edit.aspx", "btnSubmit_Click", ex.Message);
            }
        }

        private bool DoAdd()
        {
            try
            {
                Model.manager usermodel = Session["userinfo"] as Model.manager;
                bool flag = false;
                link model = new link();
                linkBll lkBll = new linkBll();
                model.title = this.txtTitle.Text.Trim();
                model.is_lock = Utils.StrToInt(this.rblIsLock.SelectedValue, 0);

                model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 0x63);
                model.link_url = this.txtSiteUrl.Text.Trim();
                model.img_url = this.txtImgUrl.Text.Trim();
                model.add_time = Utils.StrToDateTime(DateTime.Now.ToString());

                model.add_user = usermodel.id;
                if (lkBll.Add(model) > 0)
                {
                    AddAdminLog("新增", "添加幻灯片：" + model.title);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception("--link_edit-->DoAdd" + ex.Message, ex);
            }
        }


        private bool DoEdit(int _id)
        {
            try
            {
                Model.manager usermodel = Session["userinfo"] as Model.manager;
                bool flag = false;
                linkBll lkBll = new linkBll();
                link model = lkBll.GetModel(_id);
                model.title = this.txtTitle.Text.Trim();
                model.is_lock = Utils.StrToInt(this.rblIsLock.SelectedValue, 0);
                model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 0x63);
                model.link_url = this.txtSiteUrl.Text.Trim();
                model.img_url = this.txtImgUrl.Text.Trim();
                model.add_user = usermodel.id;
                model.add_time = Utils.StrToDateTime(DateTime.Now.ToString());
                if (lkBll.Update(model))
                {
                    AddAdminLog("编辑", "修改幻灯片：" + model.title);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception("--link_edit-->DoEdit" + ex.Message, ex);
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