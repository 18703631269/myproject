using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Common;
using Web.UI;
using BLL;
using System.Configuration;

namespace Web.admin.article
{
    public partial class article_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected string quanxian = string.Empty;
        protected int channel_id;
        private int id = 0;
        channelBll chBll = new channelBll();
        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            try
            {
                channel_id = DTRequest.GetQueryInt("channel_id");
                navigationBll bll = new navigationBll();
                string pagename = Request.Params["qx"];
                id = bll.GetIdByPage(pagename);//表示页面对应的id
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_edit.aspx", "Page_Init", ex.Message);
            }
        }

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string _action = DTRequest.GetQueryString("action");
                action = _action;
                id = DTRequest.GetQueryInt("id");
                if (!Page.IsPostBack)
                {
                    Model.channel chModel = chBll.GetModel(channel_id);
                    if (chModel == null)
                    {
                        JscriptMsg("该频道无设定！", "article_list.aspx", "parent.loadMenuTree");
                    }
                    else
                    {
                        TreeBind(channel_id); //绑定类别
                        ShowSysField(channel_id);//显示选中的项
                        if (_action == DTEnums.ActionEnum.Edit.ToString()) //修改
                        {
                            ChkAdminLevel(id, "Edit"); //检查权限
                            ShowInfo(id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_edit.aspx", "Page_Load", ex.Message);
            }
        }

        /// <summary>
        /// 设定存在的关键字段
        /// </summary>
        /// <param name="_channel_id"></param>
        private void ShowSysField(int _channel_id)
        {
            //查找该频道所选的默认字段
            Model.channel chModel = chBll.GetModel(_channel_id);
            if (chModel == null)
            {
                div_video_container.Visible = false;
                div_albums_container.Visible = false;
                div_attach_container.Visible = false;
                field_tab_item.Visible = false;

                dlFylx.Visible = false;
                dlCity.Visible = false;
                dlHospl.Visible = false;
                dlZyxlzt.Visible = false;
                dlPaymoney.Visible = false;
                dlZslx.Visible = false;
                dlMdcity.Visible = false;
                dlSzjc.Visible = false;
            }
            else
            {
                if (chModel.is_video == 1)
                {
                    div_video_container.Visible = true;
                }
                if (chModel.is_albums == 1)
                {
                    div_albums_container.Visible = true;
                }
                if (chModel.is_attach == 1)
                {
                    div_attach_container.Visible = true;
                }
                if (chModel.is_details == 1)
                {
                    field_tab_item.Visible = true;
                }

                if (chModel.is_fylx == 1)
                {
                    dlFylx.Visible = true;
                }
                if (chModel.is_city == 1)
                {
                    dlCity.Visible = true;
                }
                if (chModel.is_hospl == 1)
                {
                    dlHospl.Visible = true;
                }
                if (chModel.is_zyxlzt == 1)
                {
                    dlZyxlzt.Visible = true;
                }
                if (chModel.is_paymoney == 1)
                {
                    dlPaymoney.Visible = true;
                }
                if (chModel.is_zslx == 1)
                {
                    dlZslx.Visible = true;
                }
                if (chModel.is_mdcity == 1)
                {
                    dlMdcity.Visible = true;
                }
                if (chModel.is_szjc == 1)
                {
                    dlSzjc.Visible = true;
                }
                 if (chModel.is_yybzj == 1)
                {
                    ddYybzj.Visible = true;
                }
                CheckBind();
                Hsql.Value = " town='city'";
                BindCity();
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
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        typeBll tll = new typeBll();
        private void CheckBind() 
        {
           
            //翻译类别
            cbFylx.DataSource = tll.GetList(" town='fy'");
            cbFylx.DataValueField = "tid";
            cbFylx.DataTextField = "tname";
            cbFylx.DataBind();
          
            //住宿类型
            ddlZslx.DataSource = tll.GetList(" town='zslb'");
            ddlZslx.DataValueField = "tid";
            ddlZslx.DataTextField = "tname";
            ddlZslx.DataBind();
            ddlZslx.Items.Insert(0, new ListItem(""));
           
        }
        /// <summary>
        /// 绑定所在城市
        /// </summary>
        protected void BindCity() 
        {
            //所在城市
            cbCity.DataSource = tll.GetList(Hsql.Value);
            cbCity.DataValueField = "tid";
            cbCity.DataTextField = "tname";
            cbCity.DataBind();
            //目的城市
            ddlmdcity.DataSource = tll.GetList(Hsql.Value);
            ddlmdcity.DataValueField = "tid";
            ddlmdcity.DataTextField = "tname";
            ddlmdcity.DataBind();
            ddlmdcity.Items.Insert(0, new ListItem(""));
            //航空公司
            ddlSzjc.DataSource = tll.GetList(Hkql.Value);
            ddlSzjc.DataValueField = "tid";
            ddlSzjc.DataTextField = "tname";
            ddlSzjc.DataBind();
            ddlSzjc.Items.Insert(0, new ListItem(""));
        }
        #endregion


        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            articleBll bll = new articleBll();
            article_albumsBll albumsBll = new article_albumsBll();
            article_attachBll attachBll = new article_attachBll();
            Model.article model = bll.GetModel(_id);

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            ShowCity();
          
            txtTitle.Text = model.title;
            content.Value = model.content;
            string filename = model.img_url.Substring(model.img_url.LastIndexOf("/") + 1);
            txtSortId.Text = model.sort_id.ToString();
            txtClick.Text = model.click.ToString();
            rblStatus.SelectedValue = model.status.ToString();
            txtImgUrl.Text = model.img_url;
            imgbeginPic.ImageUrl = model.img_url;
            txtVedio.Text = model.video_url;
            if (model.is_hot == 1)
            {
                cbIsHot.Checked = true;
            }
            else
            {
                cbIsHot.Checked = false;
            }
            if (action == DTEnums.ActionEnum.Edit.ToString())
            {
                txtAddTime.Text = model.update_time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                txtAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            txt_gjz.Text = model.keyword;//关键字
            txtlist_one.Text = model.detail_title_one;
            txtlist_two.Text = model.detail_title_two;
            txtlist_three.Text = model.detail_title_three;
            txtlist_four.Text = model.detail_title_four;
            txtContent_one.Value = model.details_one;
            txtContent_two.Value = model.details_two;
            txtContent_three.Value = model.details_three;
            txtContent_four.Value = model.detail_title_four;
            if (model.fylx != null && model.fylx != "")
            {
                //翻译类型
                string[] strFylx = model.fylx.Split(',');
                for (int i = 0; i < strFylx.Length; i++) 
                {
                    for (int j = 0; j < cbFylx.Items.Count; j++) 
                    {
                        if (string.Equals(strFylx[i], cbFylx.Items[j].Value)) 
                        {
                            cbFylx.Items[j].Selected = true;
                        }
                    }
                }
            }
           // txtfylx.Text = model.fylx;
           // txtcity.Text = model.city;

            if (model.city != null && model.city != "")
            {
                //所在城市
                string[] strCity = model.city.Split(',');
                for (int i = 0; i < strCity.Length; i++)
                {
                    for (int j = 0; j < cbCity.Items.Count; j++)
                    {
                        if (string.Equals(strCity[i], cbCity.Items[j].Value))
                        {
                            cbCity.Items[j].Selected = true;
                        }
                    }
                }
            }
            txthospl.Text = model.hospl;
            txtzyxlzt.Text = model.zyxlzt;
            txtpaymoney.Text = model.paymoney;
            ddlZslx.SelectedValue = model.zslx;
            ddlmdcity.SelectedValue = model.mdcity;
          //  txtzslx.Text = model.zslx;
           // txtmdcity.Text = model.mdcity;
            ddlSzjc.SelectedValue = model.szjc;//所在机场
            //txtszjc.Text = model.szjc;

            txtYYbzj.Text =Convert.ToString(model.chjj);//预约保证金
            //绑定图片相册
            if (filename.StartsWith("thumb_"))
            {
                hidFocusPhoto.Value = model.img_url; //封面图片
            }

            rptAlbumList.DataSource = albumsBll.GetList(_id);
            rptAlbumList.DataBind();


            rptAttachList.DataSource = attachBll.GetList(_id);
            rptAttachList.DataBind();


        }
        #endregion
        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    if (!DoEdit(this.id))
                    {
                        JscriptMsg("保存过程中发生错误啦！", string.Empty);
                        return;
                    }
                    JscriptMsg("修改信息成功！", "article_list.aspx?channel_id=" + this.channel_id);
                }
                else //添加
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", string.Empty);
                        return;
                    }
                    JscriptMsg("添加信息成功！", "article_list.aspx?channel_id=" + this.channel_id);
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("article_edit.aspx", "btnSubmit_Click", ex.Message);
            }
        }

        private bool DoEdit(int _id)
        {
            bool result = false;
            Model.manager usermodel = Session["userinfo"] as Model.manager;
            articleBll bll = new articleBll();
            article_albumsBll albumsBll = new article_albumsBll();
            article_attachBll attachBll = new article_attachBll();
            Model.article model = bll.GetModel(_id);
            model.category_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.img_url = txtImgUrl.Text;
            model.title = txtTitle.Text.Trim();
            model.content = content.Value.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.status = Utils.StrToInt(rblStatus.SelectedValue, 0);
            model.user_id = usermodel.id;
            model.update_time = DateTime.Now;
            model.video_url = txtVedio.Text.Trim();
            model.keyword = txt_gjz.Text;//关键字
            model.details_one = txtContent_one.Value.Trim();
            model.details_two = txtContent_two.Value.Trim();
            model.details_three = txtContent_three.Value.Trim();
            model.details_four = txtContent_four.Value.Trim();
            model.detail_title_one =txtlist_one.Text.Trim();
            model.detail_title_two = txtlist_two.Text.Trim();
            model.detail_title_three = txtlist_three.Text.Trim();
            model.detail_title_four = txtlist_four.Text.Trim();
            if (txtYYbzj.Text.Trim().Length > 0)
            {
                model.chjj = Convert.ToDouble(txtYYbzj.Text.Trim());
            }
            else 
            {
                model.chjj = 0;
            }
           
            string strfylx = "";
            for (int i = 0; i < cbFylx.Items.Count; i++)
            {
                if (cbFylx.Items[i].Selected == true)
                {
                    strfylx += cbFylx.Items[i].Value+",";
                }
            }
            model.fylx = strfylx.TrimEnd(',');

            //所在城市
            string strcity = "";
            for (int i = 0; i < cbCity.Items.Count; i++)
            {
                if (cbCity.Items[i].Selected == true)
                {
                    strcity += cbCity.Items[i].Value + ",";
                }
            }
            model.city = strcity.TrimEnd(',');
            model.hospl = txthospl.Text.Trim();
            model.zyxlzt = txtzyxlzt.Text.Trim();
            model.paymoney = txtpaymoney.Text.Trim();
            model.zslx = ddlZslx.SelectedValue;
            model.mdcity =ddlmdcity.SelectedValue;
            model.szjc = ddlSzjc.SelectedValue; //txtszjc.Text.Trim();


            if (cbIsHot.Checked == true)
            {
                model.is_hot = 1;
            }
            else
            {
                model.is_hot = 0;
            }
            if (bll.Update(model))
            {
                #region 保存相册====================
                //检查是否有自定义图片
                if (txtImgUrl.Text.Trim() == "")
                {
                    model.img_url = hidFocusPhoto.Value;
                }
                string[] albumArr = Request.Form.GetValues("hid_photo_name");
                string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
                if (albumArr != null)
                {
                    List<Model.article_albums> ls = new List<Model.article_albums>();
                    for (int i = 0; i < albumArr.Length; i++)
                    {
                        string[] imgArr = albumArr[i].Split('|');
                        int img_id = Utils.StrToInt(imgArr[0], 0);
                        if (imgArr.Length == 3)
                        {
                            if (!string.IsNullOrEmpty(remarkArr[i]))
                            {
                                ls.Add(new Model.article_albums { id = img_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                            }
                            else
                            {
                                ls.Add(new Model.article_albums { id = img_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2] });
                            }
                        }
                    }
                    albumsBll.edit(ls, id);
                }
                #endregion

                #region 保存附件====================

                string[] attachIdArr = Request.Form.GetValues("hid_attach_id");
                string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
                string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
                string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
                string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
                if (attachIdArr != null && attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                    && attachIdArr.Length > 0 && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
                {
                    List<Model.article_attach> ls = new List<Model.article_attach>();
                    for (int i = 0; i < attachFileNameArr.Length; i++)
                    {
                        int attachId = Utils.StrToInt(attachIdArr[i], 0);
                        int fileSize = Utils.StrToInt(attachFileSizeArr[i], 0);
                        string fileExt = Utils.GetFileExt(attachFilePathArr[i]);
                        int _point = Utils.StrToInt(attachPointArr[i], 0);
                        ls.Add(new Model.article_attach { id = attachId, article_id = _id, file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point, });
                    }
                    attachBll.edit(ls, id);
                }
                #endregion
                AddAdminLog("编辑", "修改" + channel_name + "内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }

        private bool DoAdd()
        {
            bool result = false;
            Model.manager usermodel = Session["userinfo"] as Model.manager;
            Model.article model = new Model.article();
            articleBll bll = new articleBll();
            article_albumsBll albumsBll = new article_albumsBll();
            article_attachBll attachBll = new article_attachBll();
            model.id = id;
            model.channel_id = this.channel_id;
            model.category_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.img_url = txtImgUrl.Text;
            model.title = txtTitle.Text.Trim();
            model.content = content.Value.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.status = Utils.StrToInt(rblStatus.SelectedValue, 0);
            model.user_id = usermodel.id;
            model.add_time = Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.update_time = Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.video_url = txtVedio.Text.Trim();
            model.keyword = txt_gjz.Text;//关键字
            model.details_one = txtlist_one.Text.Trim();
            model.details_two = txtlist_two.Text.Trim();
            model.details_three = txtlist_three.Text.Trim();
            model.details_four = txtlist_four.Text.Trim();
            model.detail_title_one = txtContent_one.Value.Trim();
            model.detail_title_two = txtContent_two.Value.Trim();
            model.detail_title_three = txtContent_three.Value.Trim();
            model.detail_title_four = txtContent_four.Value.Trim();

            string strfylx = "";
            for (int i = 0; i < cbFylx.Items.Count; i++)
            {
                if (cbFylx.Items[i].Selected == true)
                {
                    strfylx += cbFylx.Items[i].Value + ",";
                }
            }
            model.fylx = strfylx.TrimEnd(',');

            //所在城市
            string strcity = "";
            for (int i = 0; i < cbCity.Items.Count; i++)
            {
                if (cbCity.Items[i].Selected == true)
                {
                    strcity += cbCity.Items[i].Value + ",";
                }
            }
            model.city = strcity.TrimEnd(',');
            model.hospl = txthospl.Text.Trim();
            model.zyxlzt = txtzyxlzt.Text.Trim();
            model.paymoney = txtpaymoney.Text.Trim();
            model.zslx = ddlZslx.SelectedValue;
            model.mdcity = ddlmdcity.SelectedValue;
            model.szjc = ddlSzjc.SelectedValue; //txtszjc.Text.Trim();
            if (txtYYbzj.Text.Trim().Length > 0)
            {
                model.chjj = Convert.ToDouble(txtYYbzj.Text.Trim());
            }
            else
            {
                model.chjj = 0;
            }

            if (cbIsHot.Checked == true)
            {
                model.is_hot = 1;
            }
            else
            {
                model.is_hot = 0;
            }
            int ends = bll.Add(model);//ends也就是当前id
            if (ends > 0)
            {
                #region 保存相册====================
                //检查是否有自定义图片
                if (txtImgUrl.Text.Trim() == "")
                {
                    model.img_url = hidFocusPhoto.Value;
                }
                string[] albumArr = Request.Form.GetValues("hid_photo_name");
                string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
                if (albumArr != null && albumArr.Length > 0)
                {
                    List<Model.article_albums> ls = new List<Model.article_albums>();
                    for (int i = 0; i < albumArr.Length; i++)
                    {
                        string[] imgArr = albumArr[i].Split('|');
                        if (imgArr.Length == 3)
                        {
                            if (!string.IsNullOrEmpty(remarkArr[i]))
                            {
                                ls.Add(new Model.article_albums { original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                            }
                            else
                            {
                                ls.Add(new Model.article_albums { original_path = imgArr[1], thumb_path = imgArr[2] });
                            }
                        }
                    }
                    albumsBll.add(ls, ends);
                }
                #endregion

                #region 保存附件====================
                //保存附件
                string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
                string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
                string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
                string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
                if (attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                    && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
                {
                    List<Model.article_attach> ls = new List<Model.article_attach>();
                    for (int i = 0; i < attachFileNameArr.Length; i++)
                    {
                        int fileSize = Utils.StrToInt(attachFileSizeArr[i], 0);
                        string fileExt = Utils.GetFileExt(attachFilePathArr[i]);
                        int _point = Utils.StrToInt(attachPointArr[i], 0);
                        ls.Add(new Model.article_attach { file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point });
                    }
                    attachBll.add(ls, ends);
                }
                #endregion
                AddAdminLog("新增", "添加" + channel_name + "内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }

        protected void txtImgUrl_TextChanged(object sender, EventArgs e)
        {
            imgbeginPic.ImageUrl = txtImgUrl.Text;
        }

        /// <summary>
        /// 所属类别筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCity();
        }


        protected void ShowCity() 
        {
            sys_configBll sysBll = new sys_configBll();
            string[] strCity = sysBll.ReadConfig().Split(';');
            for (int i = 0; i < strCity.Length; i++)
            {
                if (ddlCategoryId.SelectedItem.Text.Contains(strCity[i]))
                {
                    Hsql.Value = "  town='city' and ctype='" + strCity[i] + "'";
                    Hkql.Value = " town='hkgs' and ctype='"+strCity[i]+"'";
                }
            }
            BindCity();
        }
    }
}