using System.IO;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Windows.Forms;
namespace simpleNotify
{
    public static class Notify
    {
        public static void Activate(ToastNotification toastNotification)
        {
            ToastNotificationManager.CreateToastNotifier(Application.ProductName).Show(toastNotification);
        }


        static public ToastNotification Create(string title, string text, string image)
        {
            XmlDocument toastXml;
            if (image == "")
            {
                toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            }
            else
            {
                toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
                XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                imageElements[0].Attributes.GetNamedItem("src").NodeValue = "file:///" + Path.GetFullPath(image);
            }
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");

            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));
            return new ToastNotification(toastXml);
        }
        static public ToastNotification Create(string title, string text)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));
            return new ToastNotification(toastXml);
        }
        static public ToastNotification Create(string titleOrText)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(titleOrText));
            return new ToastNotification(toastXml);
        }
        static public void Show(string title, string text, string image)
        {
            Activate(Create(title, text, image));
        }
        static public void Show(string title, string text)
        {
            Activate(Create(title, text));
        }
        static public void Show(string titleOrText)
        {
            Activate(Create(titleOrText));
        }

    }
}
