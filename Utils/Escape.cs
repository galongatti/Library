using System.Text;

namespace Library.Utils;

public class Escape
{
    public static string EscapeLike(string input)
    {
        // Escapa %, _ e \ para uso seguro em LIKE com ESCAPE '\'
        var sb = new StringBuilder(input.Length);
        foreach (var ch in input)
        {
            switch (ch)
            {
                case '%':
                case '_':
                case '\\':
                    sb.Append('\\');
                    sb.Append(ch);
                    break;
                default:
                    sb.Append(ch);
                    break;
            }
        }

        return $"%{sb}%";
    }
}