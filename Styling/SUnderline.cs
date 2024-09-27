using MigraDoc.DocumentObjectModel;

namespace PDFScaffold.Styling;

public enum SUnderline {
    None, Dash, DotDash, DotDotDash, Dotted, Single, OnWords
}


internal static class SUnderlineUtils {

    public static Underline GetUnderline(this SUnderline underline) {
        return underline switch {
            SUnderline.None => Underline.None,
            SUnderline.Dash => Underline.Dash,
            SUnderline.DotDash => Underline.DotDash,
            SUnderline.DotDotDash => Underline.DotDotDash,
            SUnderline.Dotted => Underline.Dotted,
            SUnderline.OnWords => Underline.Words,
            SUnderline.Single => Underline.Single,
            _ => Underline.None
        };
    }

}