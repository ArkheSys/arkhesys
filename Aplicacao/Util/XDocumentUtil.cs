using System.Linq;
using System.Xml.Linq;

namespace Aplicacao.Util
{
    public static class XDocumentUtil
    {
        public static XElement GetElementAny(this XContainer source, string tag)
        {
            return source.Elements().SingleOrDefault(x => x.Name.LocalName == tag);
        }
    }
}
