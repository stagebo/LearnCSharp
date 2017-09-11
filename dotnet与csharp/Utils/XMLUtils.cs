using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
namespace BaseCSharp.Utils
{
    class XMLUtils
    {
        #region 构造和析构

        /// <summary>
        /// 构造类
        /// </summary>
        public XMLUtils()
        {
            m_systemPath = Path.Combine(Application.StartupPath, "", "SystemConfig.xml");
            m_systemDefaultPath = Path.Combine(Application.StartupPath, "", "SystemConfigBak.xml");
            doc = new XmlDocument();
            doc.Load(m_systemPath);
            Defalut_doc = new XmlDocument();
            Defalut_doc.Load(m_systemDefaultPath);
            doc.NodeChanged += new XmlNodeChangedEventHandler(doc_NodeChanged);

            //初始化默认的config
            defaultConfig = new XMLUtils(1);
        }

        /// <summary>
        /// 构造默认值用
        /// </summary>
        /// <param name="sign">类标识</param>
        public XMLUtils(int sign)
        {
            m_systemPath = Path.Combine(Application.StartupPath, "", "SystemConfigBak.xml");
            doc = new XmlDocument();
            doc.Load(m_systemPath);
        }
        
        /// <summary>
        /// 默认值
        /// </summary>
        public static XMLUtils defaultConfig = null;
        
        #endregion // 构造和析构

        #region 私有方法

        /// <summary>
        /// 文档改变重新加载
        /// 更新捕捉的设置
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件数据</param>
        private void doc_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            //XmlElement Elem = (e.Node.ParentNode as XmlAttribute).OwnerElement;
            //var config = XSystem.GetConfig<SnapContext>();
            //if (Elem.Name == "capturer")
            //{
            //    int radius = 10;
            //    bool capturer = true;
            //    if (!int.TryParse(Elem.Attributes["radius"].Value, out radius)
            //        || !bool.TryParse(Elem.Attributes["checked"].Value, out capturer))
            //    {
            //        return;
            //    };
            //    config.SetProperty("SnapRadius", radius);
            //    config.SetProperty("Enable", capturer);
            //}
        }
        #endregion // 私有方法

        #region 公有方法

        /// <summary>
        ///  获取配置
        /// </summary>
        /// <param name="node">配置名</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string组</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.GetCofig("Node", "")
         * SystemConfigXML.GetCofig("Node", "Attribute")
         ************************************************/
        public string[] GetCofig(string name, string attribute)
        {
            string[] values = new string[] { "" };
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(name);
                for (int i = 0; i < xns.Count; i++)
                {
                    XmlNode xn = xns[i];
                    values[i] = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
                }
            }
            catch (Exception ex)
            {
            }
            return values;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="node">标签名称</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Read("Node", "")
         * SystemConfigXML.Read("Node", "Attribute")
         ************************************************/
        public string Read(string node, string attribute)
        {
            string value = "";
            try
            {
                XmlNodeList xn = doc.GetElementsByTagName(node);
                value = (attribute.Equals("") ? xn[0].InnerText : xn[0].Attributes[attribute].Value);
            }
            catch (Exception ex)
            {
            }
            return value;
        }

        /// <summary>
        /// 用节点名称获取第一个节点
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>获取的节点</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.getNode("Node", "")
         ************************************************/
        public XmlNode getNode(string node)
        {
            XmlNode value = null;
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(node);
                value = xns[0];
            }
            catch (Exception ex)
            {
            }
            return value;
        }

        /// <summary>
        /// 用节点名称获取指定的属性值
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="pName">属性名称</param>
        /// <returns>属性值</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.getNode("Node", "")
         * SystemConfigXML.getNode("Node", "Attribute")
         ************************************************/
        public string getNode(string nodeName, string pName)
        {
            XmlNode value = null;
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(nodeName);
                value = xns[0];
                if (value != null)
                {
                    return value.Attributes[pName].Value;
                }
                return "";
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        /// <summary>
        /// 用默认值还原节点的内容
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>获取的节点</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.RecoverNode("Node")
         ************************************************/
        public XmlNode RecoverNode(string node)
        {
            XmlNode value = null;
            try
            {
                XmlNodeList xns = Defalut_doc.GetElementsByTagName(node);
                value = xns[0];
                XmlNodeList xns1 = doc.GetElementsByTagName(node);
                xns1[0].InnerXml = (value.InnerXml);
            }
            catch (Exception ex)
            {
            }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Insert("/Node", "Element", "", "Value")
         * SystemConfigXML.Insert("/Node", "Element", "Attribute", "Value")
         * SystemConfigXML.Insert("/Node", "", "Attribute", "Value")
         ************************************************/
        public void Insert(string node, string element, string attribute, string value)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        ///  插入文本格式的项 默认为选中
        /// </summary>
        /// <param name="node">父节点</param>
        /// <param name="nodetxt">子节点text</param>
        /// <param name="value">子节点值</param>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Insert("/Node", "Text", "Value")
         ************************************************/
        public void Insert(string node, string nodetxt, string value)
        {
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(node);
                foreach (XmlNode xn in xns)
                {
                    XmlElement xe = doc.CreateElement("item");
                    xe.SetAttribute("text", nodetxt);
                    xe.SetAttribute("value", value);
                    xe.SetAttribute("checked", true.ToString());
                    xn.AppendChild(xe);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Update("/Node", "", "Value")
         * SystemConfigXML.Update("/Node", "Attribute", "Value")
         ************************************************/
        public void Update(string node, string attribute, string value)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                {
                    xe.InnerText = value;
                }
                else
                {
                    xe.SetAttribute(attribute, value);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.UpdateSingle("Node", "", "Value")
         * SystemConfigXML.UpdateSingle("Node", "Attribute", "Value")
         ************************************************/
        public bool UpdateSingle(string node, string attribute, string value)
        {
            bool result = false;
            try
            {
                XmlNodeList xn = doc.GetElementsByTagName(node);
                XmlElement xe = (XmlElement)xn[0];
                if (attribute.Equals(""))
                {
                    xe.InnerText = value;
                }
                else
                {
                    xe.SetAttribute(attribute, value);
                }
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 根据项的属性值查找项
        /// </summary>
        /// <param name="node">父节点</param>
        /// <param name="nodetxt">子节点text</param>
        /// <param name="attribute">子节点属性名称</param>
        /// <param name="value">子节点属性值</param>
        /// <returns>是否更新成功</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Insert("/Node", "NodeText", "Attribute", "Value")
         ************************************************/
        public bool UpdateItem(string node, string nodetxt, string attribute, string value)
        {
            bool result = false;
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(node);
                foreach (XmlNode xn in xns)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.GetAttribute("text") == nodetxt)
                    {
                        xe.SetAttribute(attribute, value);
                        break;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="path">节点text</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Insert("/Node", "NodeText", "Attribute", "Value")
         ************************************************/
        public bool UpdateSingle(string node, string nodetxt, string text, string value)
        {
            bool result = false;
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(node);
                foreach (XmlNode xn in xns)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.GetAttribute("text") == nodetxt)
                    {
                        xe.SetAttribute("text", text);
                        xe.SetAttribute("value", value);
                        break;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Delete("/Node", "")
         * SystemConfigXML.Delete("/Node", "Attribute")
         ************************************************/
        public void Delete(string node, string attribute)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <param name="nodetxt">节点文本</param>
        /**************************************************
         * 使用示列:
         * SystemConfigXML.Delete("/Node", "NodeTxt")
         ************************************************/
        public void DeleteSingle(string node, string nodetxt)
        {
            try
            {
                XmlNodeList xns = doc.GetElementsByTagName(node);
                foreach (XmlNode xn in xns)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.GetAttribute("text") == nodetxt)
                    {
                        xn.ParentNode.RemoveChild(xn);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 还原默认值
        /// </summary>
        public void OverWrite()
        {
            try
            {
                Defalut_doc.Save(m_systemPath);
                doc.Load(m_systemPath);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            try
            {
                if (!string.IsNullOrEmpty(doc.InnerXml))
                {
                    doc.Save(m_systemPath);
                }
                else
                {
                    throw new Exception("配置文件被清空，请联系管理员！");
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion // 公有方法
        /// <summary>
        /// 参数路径 
        /// </summary>
        private string m_systemPath;

        /// <summary>
        /// 默认参数路径
        /// </summary>
        private string m_systemDefaultPath;

        /// <summary>
        /// 当前应用的XmlDocument
        /// </summary>
        private XmlDocument doc;

        /// <summary>
        /// 用于恢复默认值的XmlDocument
        /// </summary>
        private XmlDocument Defalut_doc;
    }
}
