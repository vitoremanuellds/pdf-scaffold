using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;
using PDFScaffold.Texts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFScaffold.Visitors.Default
{
    internal static class ForLink
    {
        internal static void DoForLink(this SVisitor visitor, SLink link)
        {
            SStyle style = visitor.GetOrCreateStyle(link.Style, link.FathersStyle!, link.UseStyle);
            SDimensions parentsDimensions = link.FathersStyle!.Dimensions!;
            Hyperlink l = SVisitorUtils.GetMigradocObjectForLinks(visitor, link);
            style.Dimensions = parentsDimensions;

            SVisitorUtils.SetBookmark(l, link.Name);
            FormattedText t = l.AddFormattedText(link.Text ?? link.Link);

            if (link.BreakLine)
            {
                l.AddText("\n");
            }

            SVisitorUtils.SetFormat(t, style, style.Dimensions, true);
        }
    }
}
