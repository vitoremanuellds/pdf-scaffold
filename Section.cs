using MigraDoc.DocumentObjectModel;
using pdf_scaffold.Metrics;
using pdf_scaffold.Visitors;

namespace pdf_scaffold;

public class Section(
    Styling.Style? style = null,
    string? useStyle = null,
    PageFormat? pageFormat = null,
    Margin? margin = null,
    ICollection<ISectionElement>? elements = null
) : IPdfScaffoldElement {
    public Styling.Style? Style { get; } = style;
    public string? UseStyle { get; } = useStyle;
    public PageFormat? PageFormat { get; } = pageFormat;
    public Margin? Margin { get; } = margin;
    public ICollection<ISectionElement>? Elements { get; } = elements;

    public MigraDoc.DocumentObjectModel.Section Build(IDictionary<string, Styling.Style> styles)
    {
        var section = new MigraDoc.DocumentObjectModel.Section();
        DefinePageSetup(section);
        DefineMargins(section);
        var elements = Elements ?? [];
        
        Styling.Style? style = Style;
        if (Style == null && UseStyle != null) {
            styles.TryGetValue(UseStyle, out style);
        }


        foreach (ISectionElement element in elements) {
            var sectionElement = element.Build(style, styles);
        }

        return section;
    }

    private void DefinePageSetup(MigraDoc.DocumentObjectModel.Section section) {
        PageFormat pageFormat = PageFormat ?? PageFormat.A4();

        if (pageFormat.Height.IsPercentage || pageFormat.Width.IsPercentage) {
            throw new Exception("The percentage measure can not be used to define the size of the section!");
        }

        section.PageSetup.PageWidth = Unit.FromPoint(pageFormat.Width.Value);
        section.PageSetup.PageHeight = Unit.FromPoint(pageFormat.Height.Value);
    }

    private void DefineMargins(MigraDoc.DocumentObjectModel.Section section) {
        Margin margin = Margin ?? Margin.All(new Measure(inches: 1));

        var width = PageFormat!.Width.Value;
        var height = PageFormat!.Height.Value;

        var left = margin.Left ?? new Measure(inches: 1);
        var right = margin.Right ?? new Measure(inches: 1);
        var top = margin.Top ?? new Measure(inches: 1);
        var bottom = margin.Bottom ?? new Measure(inches: 1);

        if (left.IsPercentage) {
            section.PageSetup.LeftMargin = Unit.FromPoint(left.Value * width);
        } else {
            section.PageSetup.LeftMargin = Unit.FromPoint(left.Value);
        }
    
        if (right.IsPercentage) {
            section.PageSetup.RightMargin = Unit.FromPoint(right.Value * width);
        } else {
            section.PageSetup.RightMargin = Unit.FromPoint(right.Value);
        }

        if (top.IsPercentage) {
            section.PageSetup.TopMargin = Unit.FromPoint(top.Value * height);
        } else {
            section.PageSetup.TopMargin = Unit.FromPoint(top.Value);
        }

        if (bottom.IsPercentage) {
            section.PageSetup.BottomMargin = Unit.FromPoint(bottom.Value * height);
        } else {
            section.PageSetup.BottomMargin = Unit.FromPoint(bottom.Value);
        }
    }

    public void Accept(IPdfScaffoldVisitor visitor)
    {
        
    }
}