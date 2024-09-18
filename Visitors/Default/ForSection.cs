using PDFScaffold.Scaffold;
using MigraDoc.DocumentObjectModel;
using PDFScaffold.Metrics;
using PDFScaffold.Styling;

namespace PDFScaffold.Visitors.Default;

internal static class ForSection
{

    internal static void DoForSection(this SVisitor visitor, SSection section)
    {
        var sec = visitor.Document.AddSection();

        SPageFormat pageFormat = section.PageFormat ?? SPageFormat.A4();

        if (pageFormat.Height.IsPercentage || pageFormat.Width.IsPercentage)
        {
            throw new Exception("The percentage measure can not be used to define the size of the section!");
        }

        sec.PageSetup.PageWidth = Unit.FromPoint(pageFormat.Width.Value);
        sec.PageSetup.PageHeight = Unit.FromPoint(pageFormat.Height.Value);

        SMargin margin = section.Margin
            ?? new SMargin(new SMeasure(inches: 1));

        var width = pageFormat!.Width.Value;
        var height = pageFormat!.Height.Value;

        var left = margin.Left ?? new SMeasure(inches: 0);
        var right = margin.Right ?? new SMeasure(inches: 0);
        var top = margin.Top ?? new SMeasure(inches: 0);
        var bottom = margin.Bottom ?? new SMeasure(inches: 0);

        // Setting to null, I don't know if this is going to work.
        sec.PageSetup.LeftMargin = SMetricsUtil.GetUnitValue(left, width);
        sec.PageSetup.RightMargin = SMetricsUtil.GetUnitValue(right, width);
        sec.PageSetup.TopMargin = SMetricsUtil.GetUnitValue(top, height);
        sec.PageSetup.BottomMargin = SMetricsUtil.GetUnitValue(bottom, height);

        var elements = section.Elements ?? [];

        var x = width - left.Value - right.Value;
        var y = height - top.Value - bottom.Value;

        SStyle style =
            visitor.GetStyle(section.Style, section.UseStyle) ?? new SStyle();

        var dimensions = new SDimensions(x: x, y: y);
        style.Dimensions = dimensions;

        visitor.VisitedObjects.Push(sec);

        foreach (SSectionElement element in elements)
        {
            element.FathersStyle = style;
            element.Accept(visitor);
            // element.GetType().GetMethod("Accept")!.Invoke(element, [visitor]);
        }

        visitor.VisitedObjects.Pop();
    }
}
