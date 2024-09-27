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
    internal static class ForParagraph
    {
        internal static void DoForParagraph(this SVisitor visitor, SParagraph paragraph)
        {
            SStyle style = visitor.GetOrCreateStyle(paragraph.Style, paragraph.FathersStyle!, paragraph.UseStyle);
            SDimensions parentsDimensions = paragraph.FathersStyle!.Dimensions!;
            bool dimensionsSet = style.Width != null && style.Height != null;
            var (tf, p) = SVisitorUtils.GetMigradocObjectsForParagraph(visitor,dimensionsSet);
            style.Dimensions = parentsDimensions.Copy();

            if (paragraph.Name != null)
            {
                p.AddBookmark('#' + paragraph.Name, false);
            }

            SVisitorUtils.SetWidthAndHeight(tf, style, parentsDimensions);
            SVisitorUtils.SetBorders(p.Format.Borders, style, style.Dimensions!);
            SVisitorUtils.SetFormat(p.Format, style, style.Dimensions!);
            SVisitorUtils.SetShading(p.Format.Shading, style);

            visitor.VisitedObjects.Push(p);

            foreach (STextElement item in paragraph.Content ?? [])
            {
                item.FathersStyle = style;
                item.Accept(visitor);
            }

            visitor.VisitedObjects.Pop();
            paragraph.Dimensions = style.Dimensions;
        }
    }
}
