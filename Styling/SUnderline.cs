using MigraDoc.DocumentObjectModel;

namespace PDFScaffold.Styling;

public enum SUnderline {
    None, Dash, DotDash, DotDotDash, Dotted, Single, OnWords
}


internal static class SUnderlineUtils {

    internal static IDictionary<SUnderline, Underline> MigradocUnderlines = new Dictionary<SUnderline, Underline> {
        { SUnderline.None, Underline.None },
        { SUnderline.Dash, Underline.Dash },
        { SUnderline.DotDash, Underline.DotDash },
        { SUnderline.DotDotDash, Underline.DotDotDash },
        { SUnderline.Dotted, Underline.Dotted },
        { SUnderline.OnWords, Underline.Words },
        { SUnderline.Single, Underline.Single }
    };

    public static Underline GetUnderline(this SUnderline underline) {
        return MigradocUnderlines[underline];
    }

}