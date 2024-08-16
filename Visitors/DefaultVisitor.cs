
using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;
using pdf_scaffold.Styling;

namespace pdf_scaffold.Visitors;

public class DefaultVisitor : IPdfScaffoldVisitor
{
    public required MigraDoc.DocumentObjectModel.Document Document { get; set; }
    private Dictionary<string, Styling.Style> Styles = [];

    public void ForDocument(Document document)
    {
        if (document.Author != null) { Document.Info.Author = document.Author; }
        if (document.Title != null) { Document.Info.Title = document.Title; }
        if (document.Subject != null) { Document.Info.Subject = document.Subject; }
        if (document.Keywords != null) { Document.Info.Keywords = string.Join(" ", document.Keywords); }

        if (document.Styles != null) {
            foreach(Styling.Style style in document.Styles) {
                if (style.Name != null) {
                    Styles.Add(style.Name, style);
                }
            }
        }

        if (document.Sections == null) {
            throw new Exception("There must be at least one section in the document!");
        }

        foreach(Section section in document.Sections) {
            section.Accept(this);
        }
    }

    public void ForSection(Section section) {
        var sec = Document.AddSection();

        PageFormat pageFormat = section.PageFormat ?? PageFormat.A4();

        if (pageFormat.Height.IsPercentage || pageFormat.Width.IsPercentage) {
            throw new Exception("The percentage measure can not be used to define the size of the section!");
        }

        sec.PageSetup.PageWidth = Unit.FromPoint(pageFormat.Width.Value);
        sec.PageSetup.PageHeight = Unit.FromPoint(pageFormat.Height.Value);

        Margin margin = section.Margin
            ?? Margin.All(new Measure(inches: 1));

        var width = pageFormat!.Width.Value;                        
        var height = pageFormat!.Height.Value;

        var left = margin.Left ?? new Measure(inches: 1);
        var right = margin.Right ?? new Measure(inches: 1);
        var top = margin.Top ?? new Measure(inches: 1);
        var bottom = margin.Bottom ?? new Measure(inches: 1);

        if (left.IsPercentage) {
            sec.PageSetup.LeftMargin = Unit.FromPoint(left.Value * width);
        } else {
            sec.PageSetup.LeftMargin = Unit.FromPoint(left.Value);
        }
    
        if (right.IsPercentage) {
            sec.PageSetup.RightMargin = Unit.FromPoint(right.Value * width);
        } else {
            sec.PageSetup.RightMargin = Unit.FromPoint(right.Value);
        }

        if (top.IsPercentage) {
            sec.PageSetup.TopMargin = Unit.FromPoint(top.Value * height);
        } else {
            sec.PageSetup.TopMargin = Unit.FromPoint(top.Value);
        }

        if (bottom.IsPercentage) {
            sec.PageSetup.BottomMargin = Unit.FromPoint(bottom.Value * height);
        } else {
            sec.PageSetup.BottomMargin = Unit.FromPoint(bottom.Value);
        }

        var elements = section.Elements ?? [];
        
        Styling.Style? style = section.Style;
        if (style == null && section.UseStyle != null) {
            Styles.TryGetValue(section.UseStyle, out style);
        }

        foreach (ISectionElement element in elements) {
            element.MergeStyles(style);
            element.Accept(this);
        }
    }
}