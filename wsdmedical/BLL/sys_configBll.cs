using Common;
using DAL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BLL
{
    public class sys_configBll
    {
        private readonly sys_configDal dal = new sys_configDal();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public sys_config loadConfig()
        {
            sys_config model = new sys_config();
            model = dal.loadConfig(Utils.GetXmlMapPath("Configpath"));
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public sys_config saveConifg(sys_config model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath("Configpath"));
        }

        /// <summary>
        ///  读取配置国家信息
        /// </summary>
        public string ReadConfig()
        {
            string model = dal.ReadConfig();
            return model;
        }


        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public List<Model.sys_url> GetList(string channel)
        {
            List<Model.sys_url> ls = new List<Model.sys_url>();
            string filePath = Utils.GetXmlMapPath("Urlspath");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            foreach (XmlElement xe in xn.ChildNodes)
            {
                if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                {
                    Model.sys_url model = new Model.sys_url();
                    if (xe.Attributes["name"] != null)
                        model.name = xe.Attributes["name"].Value;
                    if (xe.Attributes["path"] != null)
                        model.path = xe.Attributes["path"].Value;
                    if (xe.Attributes["pattern"] != null)
                        model.pattern = xe.Attributes["pattern"].Value;
                    if (xe.Attributes["querystring"] != null)
                        model.querystring = xe.Attributes["querystring"].Value;
                    ls.Add(model);
                }
            }
            return ls;
        }
    }
}
