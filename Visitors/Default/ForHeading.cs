using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel;
using PDFScaffold.Styling;
using PDFScaffold.Texts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFScaffold.Metrics;

namespace PDFScaffold.Visitors.Default
{
    internal static class ForHeading
    {
        public static void DoForHeading(this SVisitor visitor, SHeading heading)
        {
            SStyle style = visitor.GetOrCreateStyle(heading.Style, heading.FathersStyle!, heading.UseStyle);
            SDimensions parentsDimensions = heading.FathersStyle!.Dimensions!;
            var (tf, p) = SVisitorUtils.GetMigradocObjectsForParagraph(visitor, style.Width != null && style.Height != null);
            style.Dimensions = parentsDimensions.Copy();

            if (heading.Name != null)
            {
                p.AddBookmark('#' + heading.Name, false);
            }

            SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
            SVisitorUtils.SetFormat(p.Format, style, style.Dimensions!, true, heading.Level);
            SVisitorUtils.SetBorders(p.Format.Borders, style, style.Dimensions!);
            SVisitorUtils.SetShading(p.Format.Shading, style);
            
            visitor.VisitedObjects.Push(p);
            foreach (STextElement item in heading.Content ?? [])
            {
                item.FathersStyle = style;
                item.Accept(visitor);
            }

            visitor.VisitedObjects.Pop();
            heading.Dimensions = style.Dimensions;
        }
    }
}
