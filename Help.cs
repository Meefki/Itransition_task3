using Alba.CsConsoleFormat;

namespace task3;

internal class Help
{
    private readonly Rules _rules;

    public Help(Rules rules)
    {
        _rules = rules;
    }

    private Document CreateDoc(IList<string> headers)
    {
        Grid view = InitView(headers);
        FillViewContent(view, headers);

        Document doc = new(view);
        return doc;
    }

    private IList<string> InitHeaders()
    {
        string header = @"v PC\User >";
        List<string> headers = new() { header };
        headers.AddRange(_rules.Moves);
        
        return headers;
    }

    private Grid InitView(IList<string> headers)
    {
        Grid view = new()
        {
            Stroke = LineThickness.Double,
            StrokeColor = ConsoleColor.DarkGray,
        };

        AddHeaders(view, headers);

        return view;
    }

    private void AddHeaders(Grid view, IList<string> headers)
    {
        for (int i = 0; i < headers.Count(); i++)
        {
            view.Columns.Add(new Column { Width = GridLength.Auto });
            view.Children.Add(new Cell(headers[i]));
        }
    }

    private void FillViewContent(Grid view, IList<string> headers)
    {
        List<Cell[]> content = new();
        for (int i = 1; i < headers.Count - 1; i++)
        {
            Cell[] row = new Cell[headers.Count];

            for (int j = 0; j < headers.Count; j++)
            {
                if (j == 0)
                {
                    row[j] = new Cell(headers[i]);
                    continue;
                }

                row[j] = new Cell(ConvertResultToString(i - 1, j - 1));
            }

            content.Add(row);
        }

        view.Children.Add(content);
    }

    public void PrintHelp()
    {
        var headers = InitHeaders();
        Document doc = CreateDoc(headers);

        ConsoleRenderer.RenderDocument(doc);
    }

    private string ConvertResultToString(int i, int j) => _rules.Rule[i, j] switch
    {
        -1 => "Lose",
        0 => "Draw",
        1 => "Win",
        _ => throw new ArgumentException("Wrong result value")
    };
}
