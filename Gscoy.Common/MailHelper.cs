using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 发送邮件通用类
    /// </summary>
    public class MailHelper
    {
        #region 基本属性

        /// <summary>
        /// 邮件优先级设置
        /// </summary>
        public enum PriorityEnum
        {
            /// <summary>
            /// 优先级高
            /// </summary>
            High,
            /// <summary>
            /// 优先级低
            /// </summary>
            Low,
            /// <summary>
            /// 优先级普通
            /// </summary>
            Normal,
        };


        private string _Host;
        /// <summary>
        /// 邮件主机地址
        /// </summary>
        public string Host
        {
            set { _Host = value; }
            get { return _Host; }
        }
        private int _Port;
        /// <summary>
        /// 邮件主机端口
        /// </summary>
        public int Port
        {
            set { _Port = value; }
            get { return _Port; }
        }
        private string _ReplyTo;
        /// <summary>
        /// 邮件回复地址
        /// </summary>
        public string ReplyTo
        {
            set { _ReplyTo = value; }
            get { return _ReplyTo; }
        }
        private string _MailAddress;
        /// <summary>
        /// 发送邮件的地址
        /// </summary>
        public string MailAddress
        {
            set { _MailAddress = value; }
            get { return _MailAddress; }
        }

        private string _MailName;
        /// <summary>
        /// 登录邮件主机的用户名
        /// </summary>
        public string MailName
        {
            set { _MailName = value; }
            get { return _MailName; }
        }
        private string _MailPassword;
        /// <summary>
        /// 登录邮件主机的用户密码
        /// </summary>
        public string MailPassword
        {
            set { _MailPassword = value; }
            get { return _MailPassword; }
        }
        private string _MailRealName;
        /// <summary>
        /// 发件人名称
        /// </summary>
        public string MailRealName
        {
            set { _MailRealName = value; }
            get { return _MailRealName; }
        }
        private PriorityEnum _Priority;

        /// <summary>
        /// 邮件的优先等级(High,重要性高;Normal,重要性普通;Low,重要性低)
        /// </summary>
        public PriorityEnum Priority
        {
            set { _Priority = value; }
            get { return _Priority; }
        }
        private string _MailTo;
        /// <summary>
        /// 邮件接收人(多人接收时,请使用;分割接收人)
        /// </summary>
        public string MailTo
        {
            set { _MailTo = value; }
            get { return _MailTo; }
        }

        private string _MailCc;
        /// <summary>
        /// 邮件抄送人(多人接收时,请使用;分割接收人)
        /// </summary>
        public string MailCc
        {
            set { _MailCc = value; }
            get { return _MailCc; }
        }

        private string _MailBcc;
        /// <summary>
        /// 邮件密送人(多人接收时,请使用;分割接收人)
        /// </summary>
        public string MailBcc
        {
            set { _MailBcc = value; }
            get { return _MailBcc; }
        }

        public string _subject;
        /// <summary>
        /// 邮件主旨
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        public string _body;
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }

        private string _attachUrl;
        /// <summary>
        /// 附件地址,多附件请使用;分割
        /// </summary>
        public string AttachUrl
        {
            set { _attachUrl = value; }
            get { return _attachUrl; }
        }

        #endregion

        /// <summary>
        /// 生产邮件发送端
        /// </summary>
        /// <returns></returns>
        private SmtpClient MarkSmtpClient()
        {
            SmtpClient myCilent = new SmtpClient();
            if (string.IsNullOrEmpty(Host))
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("Host", "smtp.126.com")))
                {
                    throw (new Exception("配置文件中没有配置Host的值,请在调用方法的时候传递Host值"));
                }
                myCilent.Host = ConfigHelper.GetConfig("Host", "smtp.126.com");
            }
            else
            {
                myCilent.Host = Host;
            }
            if (Port != 0)
            {
                myCilent.Port = Port;
            }
            else
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("Port","25")))
                {
                    throw (new Exception("配置文件中没有配置Port的值,请在调用方法的时候传递Port值"));
                }
                myCilent.Port = ConfigHelper.GetConfig<int>("Port",25);
            }
            if (string.IsNullOrEmpty(this.MailName))
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("MailName", "gaoshuang1021@126.com")))
                {
                    throw (new Exception("配置文件中没有配置MailName的值,请在调用方法的时候传递MailName值"));
                }
                MailName = ConfigHelper.GetConfig("MailName", "gaoshuang1021@126.com");
            }
            if (string.IsNullOrEmpty(this.MailPassword))
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("MailPassword","lengshou01-=")))
                {
                    throw (new Exception("配置文件中没有配置MailPassword的值,请在调用方法的时候传递MailPassword值"));
                }
                MailPassword = ConfigHelper.GetConfig("MailPassword", "lengshou01-=");
            }

            myCilent.UseDefaultCredentials = false;
            myCilent.EnableSsl = false;
            myCilent.DeliveryMethod = SmtpDeliveryMethod.Network;
            myCilent.Credentials = new System.Net.NetworkCredential(MailName, MailPassword);

            return myCilent;
        }
        /// <summary>
        /// 产生邮件地址
        /// </summary>
        /// <param name="MailPath"></param>
        /// <returns></returns>
        private MailAddress MarkMailAddress(string MailPath, string RealName)
        {
            MailAddress myAddress = new MailAddress(MailPath, RealName);
            return myAddress;
        }

        /// <summary>
        /// 产生消息体
        /// </summary>
        /// <returns></returns>
        private MailMessage MarkMailMessage()
        {
            MailMessage myMessage = new MailMessage();

            if (string.IsNullOrEmpty(MailAddress))
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("MailAddress", "gaoshuang1021@126.com")))
                {
                    throw (new Exception("配置文件中没有配置Host的值,请在调用方法的时候传递Host值"));
                }
                MailAddress = ConfigHelper.GetConfig("MailAddress", "gaoshuang1021@126.com");
            }

            if (string.IsNullOrEmpty(MailRealName))
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfig("MailRealName")))
                {
                    throw (new Exception("配置文件中没有配置Host的值,请在调用方法的时候传递Host值"));
                }
                MailRealName = ConfigHelper.GetConfig("MailRealName");
            }

            myMessage.From = MarkMailAddress(MailAddress, this.MailRealName);

            //邮件接收人
            if (string.IsNullOrEmpty(MailTo))
            {
                throw (new Exception("请设置邮件接收人!"));
            }
            if (this.MailTo.IndexOf(";") > 0)
            {
                foreach (string mailAddress in MailTo.Split(';'))
                {
                    if (!string.IsNullOrEmpty(mailAddress.Trim()))
                    {
                        myMessage.To.Add(mailAddress);
                    }
                }
            }
            else
            {
                myMessage.To.Add(MailTo);
            }

            //邮件抄送人
            if (!string.IsNullOrEmpty(MailCc))
            {
                if (this.MailCc.IndexOf(";") > 0)
                {
                    foreach (string mailAddress in MailCc.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(mailAddress.Trim()))
                        {
                            myMessage.CC.Add(mailAddress);
                        }
                    }
                }
                else
                {
                    myMessage.CC.Add(MailCc);
                }
            }

            //邮件密送人
            if (!string.IsNullOrEmpty(this.MailBcc))
            {
                if (this.MailBcc.IndexOf(";") > 0)
                {
                    foreach (string mailAddress in MailBcc.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(mailAddress.Trim()))
                        {
                            myMessage.Bcc.Add(mailAddress);
                        }
                    }
                }
                else
                {
                    myMessage.Bcc.Add(MailBcc);
                }
            }
            myMessage.Subject = Subject;
            if (!string.IsNullOrEmpty(this.Priority.ToString()))
            {
                switch (this.Priority)
                {
                    //case "High":
                    case PriorityEnum.High:
                        myMessage.Priority = MailPriority.High;
                        break;
                    //case "Low":
                    case PriorityEnum.Low:
                        myMessage.Priority = MailPriority.Low;
                        break;
                    default:
                        myMessage.Priority = MailPriority.Normal;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(ReplyTo))
            {
                myMessage.ReplyTo = MarkMailAddress(this.ReplyTo, this.MailRealName);
            }


            myMessage.Body = Body;
            myMessage.IsBodyHtml = true;
            return myMessage;
        }

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <returns></returns>
        private MailMessage MarkMailMessageAttch()
        {
            MailMessage myMessage = new MailMessage();
            myMessage = MarkMailMessage();
            if (string.IsNullOrEmpty(AttachUrl))
            {
                throw (new Exception("还没有设置附件,<br/>请设置附件!"));
            }
            else
            {
                if (this.AttachUrl.IndexOf(";") > 0)
                {
                    foreach (string Attach in AttachUrl.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(Attach))
                        {
                            //产生附件对象
                            Attachment MailAttachment = new Attachment(Attach, System.Net.Mime.MediaTypeNames.Application.Octet);
                            //附件对象的基本信息
                            ContentDisposition Disposition = MailAttachment.ContentDisposition;
                            Disposition.CreationDate = System.IO.File.GetCreationTime(Attach);
                            Disposition.ModificationDate = System.IO.File.GetLastWriteTime(Attach);
                            Disposition.ReadDate = System.IO.File.GetLastAccessTime(Attach);

                            //为MailMessage增加附件
                            myMessage.Attachments.Add(MailAttachment);
                        }
                    }
                }
                else
                {
                    //产生附件对象
                    Attachment ObjAttachment = new Attachment(AttachUrl, System.Net.Mime.MediaTypeNames.Application.Octet);
                    //附件对象的基本信息
                    ContentDisposition ObjDisposition = ObjAttachment.ContentDisposition;
                    ObjDisposition.CreationDate = System.IO.File.GetCreationTime(AttachUrl);
                    ObjDisposition.ModificationDate = System.IO.File.GetLastWriteTime(AttachUrl);
                    ObjDisposition.ReadDate = System.IO.File.GetLastAccessTime(AttachUrl);

                    //为MailMessage增加附件
                    myMessage.Attachments.Add(ObjAttachment);
                }
                return myMessage;
            }

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public bool SendMail()
        {
            bool bResult = false;
            try
            {
                SmtpClient Client = new SmtpClient();
                Client = MarkSmtpClient();
                MailMessage Message = new MailMessage();
                Message = MarkMailMessage();
                Client.Send(Message);
                bResult = true;
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }
            return bResult;
        }

        /// <summary>
        /// 发送邮件(包含附件)
        /// </summary>
        /// <returns></returns>
        public bool SendMailAttach()
        {
            Boolean bResult = false;
            try
            {
                SmtpClient Client = new SmtpClient();
                Client = MarkSmtpClient();
                MailMessage Message = new MailMessage();
                Message = MarkMailMessageAttch();
                Client.Send(Message);
                bResult = true;
            }
            catch (Exception Ex)
            {
                throw (new Exception(Ex.Message));
            }
            return bResult;
        }

    }
}
