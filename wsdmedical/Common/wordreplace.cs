﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Web;

namespace Common
{
   public  class wordreplace
    {
            private object tempFile = null;
            private object saveFile = null;
            private static Word._Document wDoc = null; //word文档
            private static Word._Application wApp = null; //word进程
            private object missing = System.Reflection.Missing.Value;

            public wordreplace(string tempFile, string saveFile)
            {

                this.tempFile = Path.Combine(HttpContext.Current.Server.MapPath("../../Files/"), @tempFile);
                this.saveFile = Path.Combine(HttpContext.Current.Server.MapPath("../../Files/"), @saveFile);
            }

            /// <summary>
            /// 模版包含头部信息和表格，表格重复使用
            /// </summary>
            /// <param name="dt">重复表格的数据</param>
            /// <param name="expPairColumn">word中要替换的表达式和表格字段的对应关系</param>
            /// <param name="simpleExpPairValue">简单的非重复型数据</param>
            public bool GenerateWord(DataTable dt, Dictionary<string, string> expPairColumn, Dictionary<string, string> simpleExpPairValue)
            {
                if (!File.Exists(tempFile.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('" + string.Format("{0}模版文件不存在，请先设置模版文件。", tempFile.ToString()) + "');</script>");
                    return false;
                }
                try
                {
                    wApp = new Word.Application();

                    wApp.Visible = false;

                    wDoc = wApp.Documents.Add(ref tempFile, ref missing, ref missing, ref missing);

                    wDoc.Activate();// 当前文档置前

                    bool isGenerate = false;
                    //不重复替换
                    if (simpleExpPairValue != null && simpleExpPairValue.Count > 0)
                        isGenerate = ReplaceAllRang(simpleExpPairValue);

                    // 表格有重复
                    if (dt != null && dt.Rows.Count > 0 && expPairColumn != null && expPairColumn.Count > 0)
                        isGenerate = GenerateTable(dt, expPairColumn);

                    if (isGenerate)
                        wDoc.SaveAs(ref saveFile, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                    DisposeWord();

                    return true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("<script>alert('" + "生成失败" + ex.Message + "');</script>");
                    return false;
                }
            }

            /// <summary>
            /// 单个替换 模版没有重复使用的表格
            /// </summary>
            /// <param name="dc">要替换的</param>
            public bool GenerateWord(Dictionary<string, string> dc)
            {
                return GenerateWord(null, null, dc);
            }

            /// <summary>
            /// 替换文件
            /// </summary>
            /// <param name="dt">要更新的数据</param>
            /// <param name="expPairColumn">当前要替换的数据字典</param>
            /// <returns></returns>
            private bool GenerateTable(DataTable dt, Dictionary<string, string> expPairColumn)
            {
                try
                {
                    int tableNums = dt.Rows.Count;

                    Word.Table tb = wDoc.Tables[1];

                    tb.Range.Copy();

                    Dictionary<string, object> dc = new Dictionary<string, object>();
                    for (int i = 0; i < tableNums; i++)
                    {
                        dc.Clear();

                        if (i == 0)
                        {
                            foreach (string key in expPairColumn.Keys)
                            {
                                string column = expPairColumn[key];
                                object value = null;
                                value = dt.Rows[i][column];
                                dc.Add(key, value);
                            }

                            ReplaceTableRang(wDoc.Tables[1], dc);
                            continue;
                        }

                        wDoc.Paragraphs.Last.Range.Paste();

                        foreach (string key in expPairColumn.Keys)
                        {
                            string column = expPairColumn[key];
                            object value = null;
                            value = dt.Rows[i][column];
                            dc.Add(key, value);
                        }
                        ReplaceTableRang(wDoc.Tables[1], dc);
                    }


                    return true;
                }
                catch (Exception ex)
                {
                    DisposeWord();
                    HttpContext.Current.Response.Write("<script>alert('" + "生成模版里的表格失败。" + ex.Message + "');</script>");
                    return false;
                }
            }
            /// <summary>
            /// 替换文件
            /// </summary>
            /// <param name="table">当前Word中表格中要替换的Table</param>
            /// <param name="dc">要替换的数据字典</param>
            /// <returns></returns>
            private bool ReplaceTableRang(Word.Table table, Dictionary<string, object> dc)
            {
                try
                {

                    object replaceArea = Word.WdReplace.wdReplaceAll;
                    //替换Word中指定Table的字段信息
                    foreach (string item in dc.Keys)
                    {
                        object replaceKey = item;
                        object replaceValue = dc[item];
                        table.Range.Find.Execute(ref replaceKey, ref missing, ref missing, ref missing,
                          ref  missing, ref missing, ref missing, ref missing, ref missing,
                          ref  replaceValue, ref replaceArea, ref missing, ref missing, ref missing,
                          ref  missing);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    DisposeWord();
                    HttpContext.Current.Response.Write("<script>alert('" + string.Format("{0}模版中没有找到指定的要替换的表达式。{1}", tempFile, ex.Message) + "');</script>");
                    return false;
                }
            }
            /// <summary>
            /// 替换不重复数据
            /// 当前表格中的所有信息都替换
            /// </summary>
            /// <param name="dc">替换的数据字典</param>
            /// <returns></returns>
            private bool ReplaceAllRang(Dictionary<string, string> dc)
            {
                try
                {
                    object replaceArea = Word.WdReplace.wdReplaceAll;
                    //替换整个Word文档里面的字段信息
                    foreach (string item in dc.Keys)
                    {
                        object replaceKey = item;
                        object replaceValue = dc[item];
                        wApp.Selection.Find.Execute(ref replaceKey, ref missing, ref missing, ref missing,
                          ref  missing, ref missing, ref missing, ref missing, ref missing,
                          ref  replaceValue, ref replaceArea, ref missing, ref missing, ref missing,
                          ref  missing);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("<script>alert('" + string.Format("{0}模版中没有找到指定的要替换的表达式。{1}", tempFile, ex.Message) + "');</script>");
                    return false;
                }
            }
            /// <summary>
            /// 释放资源
            /// </summary>
            private void DisposeWord()
            {
                object saveOption = Word.WdSaveOptions.wdSaveChanges;
                //释放资源并且保持Word
                wDoc.Close(ref saveOption, ref missing, ref missing);

                saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;

                wApp.Quit(ref saveOption, ref missing, ref missing); //关闭Word进程
            }
    }
}
