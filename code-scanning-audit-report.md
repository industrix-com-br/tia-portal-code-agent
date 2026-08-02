<!-- code-scanning-audit-20260802 -->
# Code scanning audit

- Main commit inspected: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Open alerts: **714**

## Alert #751 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/751
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:333-361`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   325	
   326	            var wpfRow = new WpfDocuments.TableRow();
   327	            var rowBackground = tableRow.IsHeader
   328	                ? theme.TableHeaderBrush
   329	                : rowIndex % 2 == 1
   330	                    ? theme.TableAlternateBrush
   331	                    : Brushes.Transparent;
   332	
   333	            foreach (var cell in tableRow)
   334	            {
   335	                if (cell is not TableCell tableCell)
   336	                    continue;
   337	
   338	                var wpfCell = new WpfDocuments.TableCell
   339	                {
   340	                    BorderBrush = theme.TableBorderBrush,
   341	                    BorderThickness = new Thickness(0, 0, 1, 1),
   342	                    Padding = theme.TableCellPadding,
   343	                    Background = rowBackground
   344	                };
   345	
   346	                foreach (var child in tableCell)
   347	                {
   348	                    if (child is ParagraphBlock paragraphBlock)
   349	                    {
   350	                        var paragraph = CreateParagraph(paragraphBlock, theme);
   351	                        paragraph.Margin = new Thickness(0);
   352	                        paragraph.FontWeight = tableRow.IsHeader ? FontWeights.SemiBold : FontWeights.Normal;
   353	                        wpfCell.Blocks.Add(paragraph);
   354	                    }
   355	                    else
   356	                    {
   357	                        RenderBlock(wpfCell.Blocks, child, theme);
   358	                    }
   359	                }
   360	                wpfRow.Cells.Add(wpfCell);
   361	            }
   362	
   363	            rowGroup.Rows.Add(wpfRow);
   364	            rowIndex++;
   365	        }
   366	        wpfTable.RowGroups.Add(rowGroup);
   367	        return wpfTable;
   368	    }
   369	
```

</details>

## Alert #750 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/750
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:321-365`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   313	            }
   314	        }
   315	
   316	        for (var index = 0; index < Math.Max(columnCount, 1); index++)
   317	            wpfTable.Columns.Add(new WpfDocuments.TableColumn());
   318	
   319	        var rowGroup = new WpfDocuments.TableRowGroup();
   320	        var rowIndex = 0;
   321	        foreach (var row in table)
   322	        {
   323	            if (row is not TableRow tableRow)
   324	                continue;
   325	
   326	            var wpfRow = new WpfDocuments.TableRow();
   327	            var rowBackground = tableRow.IsHeader
   328	                ? theme.TableHeaderBrush
   329	                : rowIndex % 2 == 1
   330	                    ? theme.TableAlternateBrush
   331	                    : Brushes.Transparent;
   332	
   333	            foreach (var cell in tableRow)
   334	            {
   335	                if (cell is not TableCell tableCell)
   336	                    continue;
   337	
   338	                var wpfCell = new WpfDocuments.TableCell
   339	                {
   340	                    BorderBrush = theme.TableBorderBrush,
   341	                    BorderThickness = new Thickness(0, 0, 1, 1),
   342	                    Padding = theme.TableCellPadding,
   343	                    Background = rowBackground
   344	                };
   345	
   346	                foreach (var child in tableCell)
   347	                {
   348	                    if (child is ParagraphBlock paragraphBlock)
   349	                    {
   350	                        var paragraph = CreateParagraph(paragraphBlock, theme);
   351	                        paragraph.Margin = new Thickness(0);
   352	                        paragraph.FontWeight = tableRow.IsHeader ? FontWeights.SemiBold : FontWeights.Normal;
   353	                        wpfCell.Blocks.Add(paragraph);
   354	                    }
   355	                    else
   356	                    {
   357	                        RenderBlock(wpfCell.Blocks, child, theme);
   358	                    }
   359	                }
   360	                wpfRow.Cells.Add(wpfCell);
   361	            }
   362	
   363	            rowGroup.Rows.Add(wpfRow);
   364	            rowIndex++;
   365	        }
   366	        wpfTable.RowGroups.Add(rowGroup);
   367	        return wpfTable;
   368	    }
   369	
   370	    private static void RenderInlineChildren(
   371	        WpfDocuments.InlineCollection target,
   372	        ContainerInline? container,
   373	        MarkdownTheme theme)
```

</details>

## Alert #749 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/749
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:306-313`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   298	            BorderThickness = new Thickness(1),
   299	            CellSpacing = 0,
   300	            Margin = new Thickness(0, 4, 0, 10)
   301	        };
   302	
   303	        var columnCount = table.ColumnDefinitions?.Count ?? 0;
   304	        if (columnCount == 0)
   305	        {
   306	            foreach (var row in table)
   307	            {
   308	                if (row is TableRow tableRow)
   309	                {
   310	                    columnCount = tableRow.Count;
   311	                    break;
   312	                }
   313	            }
   314	        }
   315	
   316	        for (var index = 0; index < Math.Max(columnCount, 1); index++)
   317	            wpfTable.Columns.Add(new WpfDocuments.TableColumn());
   318	
   319	        var rowGroup = new WpfDocuments.TableRowGroup();
   320	        var rowIndex = 0;
   321	        foreach (var row in table)
```

</details>

## Alert #748 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/748
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:178-210`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   170	
   171	    private static void RenderList(
   172	        WpfDocuments.BlockCollection target,
   173	        ListBlock list,
   174	        int depth,
   175	        MarkdownTheme theme)
   176	    {
   177	        var index = 1;
   178	        foreach (var item in list)
   179	        {
   180	            if (item is not ListItemBlock listItem)
   181	                continue;
   182	
   183	            foreach (var child in listItem)
   184	            {
   185	                switch (child)
   186	                {
   187	                    case ParagraphBlock paragraphBlock:
   188	                        var paragraph = new WpfDocuments.Paragraph
   189	                        {
   190	                            Foreground = theme.BodyBrush,
   191	                            Margin = new Thickness(18 + depth * 18, 0, 0, 5)
   192	                        };
   193	                        paragraph.Inlines.Add(new WpfDocuments.Run(list.IsOrdered ? $"{index}. " : "• ")
   194	                        {
   195	                            Foreground = theme.BulletBrush,
   196	                            FontWeight = FontWeights.SemiBold
   197	                        });
   198	                        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline, theme);
   199	                        target.Add(paragraph);
   200	                        break;
   201	                    case ListBlock nestedList:
   202	                        RenderList(target, nestedList, depth + 1, theme);
   203	                        break;
   204	                    default:
   205	                        RenderBlock(target, child, theme);
   206	                        break;
   207	                }
   208	            }
   209	            index++;
   210	        }
   211	    }
   212	
   213	    private static void AddCodeBlock(
   214	        WpfDocuments.BlockCollection target,
   215	        CodeBlock codeBlock,
   216	        MarkdownTheme theme)
   217	    {
   218	        var code = codeBlock.Lines.ToString().TrimEnd();
```

</details>

## Alert #747 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/747
- Location: `src/TiaAgent.Contracts/Diagnostics/TextPayloadDiagnostics.cs:106-110`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 200 lines

<details><summary>Current code context</summary>

```text
    98	            }
    99	            count++;
   100	        }
   101	        return count;
   102	    }
   103	
   104	    private static bool ContainsKnownMojibake(string text)
   105	    {
   106	        foreach (var pattern in KnownMojibakePatterns)
   107	        {
   108	            if (text.IndexOf(pattern, StringComparison.Ordinal) >= 0)
   109	                return true;
   110	        }
   111	        return false;
   112	    }
   113	
   114	    private static string BuildEscapedPreview(string text, int scalarLimit)
   115	    {
   116	        var sb = new StringBuilder();
   117	        var scalarCount = 0;
   118	
```

</details>

## Alert #746 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/746
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:519-523`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   511	    {
   512	        var sanitized = SanitizeForPipeName(tiaInstanceId);
   513	        return $"TiaAgent_RC_{sanitized}";
   514	    }
   515	
   516	    private static string SanitizeForPipeName(string id)
   517	    {
   518	        var sb = new StringBuilder(id.Length);
   519	        foreach (var c in id)
   520	        {
   521	            if (char.IsLetterOrDigit(c) || c == '_' || c == '-')
   522	                sb.Append(c);
   523	        }
   524	        return sb.ToString();
   525	    }
   526	
   527	    public void Dispose()
   528	    {
   529	        _instances.Clear();
   530	        _instanceLocks.Clear();
   531	        _readinessListener.Dispose();
```

</details>

## Alert #745 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/745
- Location: `tests/TiaAgent.ResponseCenter.Tests/MarkdownRendererTests.cs:129-133`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 284 lines

<details><summary>Current code context</summary>

```text
   121	            "```\nunclosed code block",
   122	            "[broken link](",
   123	            "![",
   124	            "**unclosed bold",
   125	            "| incomplete | table",
   126	            new string('x', 10000),
   127	        };
   128	
   129	        foreach (var markdown in cases)
   130	        {
   131	            var act = () => MarkdownRenderer.Render(markdown);
   132	            act.Should().NotThrow();
   133	        }
   134	    }
   135	
   136	    [Fact]
   137	    public void Render_HeadingDoesNotEmitLinkReferenceDefinitionText()
   138	    {
   139	        var document = MarkdownRenderer.Render("# Heading 1\n## Heading 2\n### Heading 3");
   140	        
   141	        document.Should().NotBeNull();
```

</details>

## Alert #744 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/744
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeHelpers.cs:36-40`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 43 lines

<details><summary>Current code context</summary>

```text
    28	    /// <summary>
    29	    /// Returns the full path if the file is found on PATH, null otherwise.
    30	    /// </summary>
    31	    internal static string? FindOnPath(string fileName)
    32	    {
    33	        var pathVar = Environment.GetEnvironmentVariable("PATH");
    34	        if (string.IsNullOrEmpty(pathVar)) return null;
    35	
    36	        foreach (var dir in pathVar.Split(Path.PathSeparator))
    37	        {
    38	            var full = Path.Combine(dir.Trim(), fileName);
    39	            if (File.Exists(full)) return full;
    40	        }
    41	        return null;
    42	    }
    43	}
```

</details>

## Alert #743 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/743
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:219-223`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   211	            // Check probing base directory
   212	            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
   213	            Info($"AppDomain.BaseDirectory: {baseDir ?? "(null)"}");
   214	
   215	            if (!string.IsNullOrEmpty(baseDir) && Directory.Exists(baseDir))
   216	            {
   217	                var dlls = Directory.GetFiles(baseDir, "*.dll");
   218	                Info($"DLLs in base directory: {dlls.Length}");
   219	                foreach (var dll in dlls)
   220	                {
   221	                    var fileName = Path.GetFileName(dll);
   222	                    Info($"  {fileName}");
   223	                }
   224	            }
   225	
   226	            // Check critical assemblies: on-disk presence + in-memory load state
   227	            var criticalAssemblies = new[]
   228	            {
   229	                "TiaAgent.AddIn",
   230	                "TiaAgent.Contracts"
   231	            };
```

</details>

## Alert #742 — cs/loss-of-precision

- Rule: `cs/loss-of-precision`
- Severity: **error**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/742
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:191-191`
- Message: Possible overflow: result of integer multiplication cast to double.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   183	            foreach (var child in listItem)
   184	            {
   185	                switch (child)
   186	                {
   187	                    case ParagraphBlock paragraphBlock:
   188	                        var paragraph = new WpfDocuments.Paragraph
   189	                        {
   190	                            Foreground = theme.BodyBrush,
   191	                            Margin = new Thickness(18 + depth * 18, 0, 0, 5)
   192	                        };
   193	                        paragraph.Inlines.Add(new WpfDocuments.Run(list.IsOrdered ? $"{index}. " : "• ")
   194	                        {
   195	                            Foreground = theme.BulletBrush,
   196	                            FontWeight = FontWeights.SemiBold
   197	                        });
   198	                        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline, theme);
   199	                        target.Add(paragraph);
```

</details>

## Alert #741 — cs/equality-on-floats

- Rule: `cs/equality-on-floats`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/741
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:127-127`
- Message: Equality checks on floating point values can yield unexpected results.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   119	    /// If the window is completely off-screen, resets it to center of the primary screen.
   120	    /// </summary>
   121	    private void EnsureOnScreen()
   122	    {
   123	        try
   124	        {
   125	            var dpi = VisualTreeHelper.GetDpi(this);
   126	
   127	            if (double.IsNaN(Left) || double.IsNaN(Top) || (Left == 0 && Top == 0))
   128	            {
   129	                ResponseCenterLogger.Info(
   130	                    "Window position is default (0,0 or NaN); centering on primary screen");
   131	                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
   132	                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
   133	                return;
   134	            }
   135	
```

</details>

## Alert #740 — cs/equality-on-floats

- Rule: `cs/equality-on-floats`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/740
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:127-127`
- Message: Equality checks on floating point values can yield unexpected results.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   119	    /// If the window is completely off-screen, resets it to center of the primary screen.
   120	    /// </summary>
   121	    private void EnsureOnScreen()
   122	    {
   123	        try
   124	        {
   125	            var dpi = VisualTreeHelper.GetDpi(this);
   126	
   127	            if (double.IsNaN(Left) || double.IsNaN(Top) || (Left == 0 && Top == 0))
   128	            {
   129	                ResponseCenterLogger.Info(
   130	                    "Window position is default (0,0 or NaN); centering on primary screen");
   131	                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
   132	                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
   133	                return;
   134	            }
   135	
```

</details>

## Alert #739 — cs/nested-if-statements

- Rule: `cs/nested-if-statements`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/739
- Location: `tests/TiaAgent.AddIn.Tests/JsonUnescapeTests.cs:102-112`
- Message: These 'if' statements can be combined.

- Current file exists on `main`: **yes**
- Current file length: 658 lines

<details><summary>Current code context</summary>

```text
    94	                            if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber,
    95	                                    System.Globalization.CultureInfo.InvariantCulture, out var codePoint))
    96	                            {
    97	                                // Check for surrogate pair: high surrogate (0xD800-0xDBFF)
    98	                                if (codePoint >= 0xD800 && codePoint <= 0xDBFF &&
    99	                                    i + 11 < raw.Length && raw[i + 6] == '\\' && raw[i + 7] == 'u')
   100	                                {
   101	                                    var lowHex = raw.Substring(i + 8, 4);
   102	                                    if (int.TryParse(lowHex, System.Globalization.NumberStyles.HexNumber,
   103	                                            System.Globalization.CultureInfo.InvariantCulture, out var lowCode))
   104	                                    {
   105	                                        if (lowCode >= 0xDC00 && lowCode <= 0xDFFF)
   106	                                        {
   107	                                            var fullCode = 0x10000 + (codePoint - 0xD800) * 0x400 + (lowCode - 0xDC00);
   108	                                            sb.Append(char.ConvertFromUtf32(fullCode));
   109	                                            i += 11;
   110	                                            break;
   111	                                        }
   112	                                    }
   113	                                }
   114	                                sb.Append((char)codePoint);
   115	                                i += 5;
   116	                            }
   117	                            else
   118	                            {
   119	                                sb.Append(raw[i]);
   120	                            }
```

</details>

## Alert #738 — cs/nested-if-statements

- Rule: `cs/nested-if-statements`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/738
- Location: `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:100-107`
- Message: These 'if' statements can be combined.

- Current file exists on `main`: **no**

## Alert #737 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/737
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:393-393`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
   385	            TiaInstanceId = request.TiaInstanceId,
   386	            IsVisible = true,
   387	            WindowState = "Normal"
   388	        };
   389	    }
   390	
   391	    public void Dispose()
   392	    {
   393	        foreach (var process in _startedProcesses)
   394	        {
   395	            try
   396	            {
   397	                if (!process.HasExited)
   398	                    process.Kill();
   399	            }
   400	            catch
   401	            {
```

</details>

## Alert #736 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/736
- Location: `src/TiaAgent.Bridge/Program.cs:95-95`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    87	
    88	        // Create task manager with runtime registry
    89	        var taskManager = new TaskManager(runtimeRegistry, config.MaxConcurrentTasks, logger);
    90	
    91	        // Create Response Center process manager
    92	        var rcProcessManager = new ResponseCenterProcessManager(logger);
    93	
    94	        // Create and start the controller
    95	        var controller = new BridgeController(config, logger, tokenProvider, runtimeRegistry, taskManager, rcProcessManager);
    96	
    97	        using var shutdownCts = new CancellationTokenSource();
    98	        Console.CancelKeyPress += (_, e) =>
    99	        {
   100	            e.Cancel = true;
   101	            shutdownCts.Cancel();
   102	            logger.Info("Shutdown signal received");
   103	        };
```

</details>

## Alert #735 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/735
- Location: `src/TiaAgent.Bridge/Program.cs:92-92`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    84	                : $"error={kvp.Value.Error}";
    85	            logger.Startup($"  {kvp.Key}: {status} ({detail})");
    86	        }
    87	
    88	        // Create task manager with runtime registry
    89	        var taskManager = new TaskManager(runtimeRegistry, config.MaxConcurrentTasks, logger);
    90	
    91	        // Create Response Center process manager
    92	        var rcProcessManager = new ResponseCenterProcessManager(logger);
    93	
    94	        // Create and start the controller
    95	        var controller = new BridgeController(config, logger, tokenProvider, runtimeRegistry, taskManager, rcProcessManager);
    96	
    97	        using var shutdownCts = new CancellationTokenSource();
    98	        Console.CancelKeyPress += (_, e) =>
    99	        {
   100	            e.Cancel = true;
```

</details>

## Alert #734 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/734
- Location: `src/TiaAgent.Bridge/Program.cs:89-89`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    81	            var status = kvp.Value.Available ? "available" : "unavailable";
    82	            var detail = kvp.Value.Available
    83	                ? $"version={kvp.Value.Version}, mode={kvp.Value.Mode}"
    84	                : $"error={kvp.Value.Error}";
    85	            logger.Startup($"  {kvp.Key}: {status} ({detail})");
    86	        }
    87	
    88	        // Create task manager with runtime registry
    89	        var taskManager = new TaskManager(runtimeRegistry, config.MaxConcurrentTasks, logger);
    90	
    91	        // Create Response Center process manager
    92	        var rcProcessManager = new ResponseCenterProcessManager(logger);
    93	
    94	        // Create and start the controller
    95	        var controller = new BridgeController(config, logger, tokenProvider, runtimeRegistry, taskManager, rcProcessManager);
    96	
    97	        using var shutdownCts = new CancellationTokenSource();
```

</details>

## Alert #733 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/733
- Location: `src/TiaAgent.ResponseCenter/Services/ReadinessReporter.cs:45-54`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **yes**
- Current file length: 121 lines

<details><summary>Current code context</summary>

```text
    37	        try
    38	        {
    39	            var pipeName = GetPipeName(context.TiaInstanceId ?? context.TaskId);
    40	            var effectiveTimeout = timeout ?? TimeSpan.FromSeconds(5);
    41	
    42	            // BuildReadinessInfo accesses WPF Window properties (IsVisible, WindowState,
    43	            // WindowInteropHelper) which must be called from the UI thread.
    44	            ReadinessInfo info;
    45	            if (window.Dispatcher.CheckAccess())
    46	            {
    47	                info = BuildReadinessInfo(context, window);
    48	            }
    49	            else
    50	            {
    51	                info = await window.Dispatcher.InvokeAsync(
    52	                    () => BuildReadinessInfo(context, window)).Task
    53	                    .ConfigureAwait(false);
    54	            }
    55	
    56	            var json = JsonSerializer.Serialize(info, s_jsonOptions);
    57	
    58	            ResponseCenterLogger.Info($"Sending readiness to Bridge via pipe '{pipeName}': {json}");
    59	
    60	            using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
    61	            using var cts = new CancellationTokenSource(effectiveTimeout);
    62	
```

</details>

## Alert #732 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/732
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:35-35`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    27	        }
    28	        else
    29	        {
    30	            Environment.SetEnvironmentVariable("TiaPublicApiDir", _originalTiaPublicApiDir);
    31	        }
    32	
    33	        if (Directory.Exists(_tempDirectory))
    34	        {
    35	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    36	        }
    37	        GC.SuppressFinalize(this);
    38	    }
    39	
    40	    [Fact]
    41	    public void Discover_WithCustomDir_ReturnsCustomDir()
    42	    {
    43	        var customDir = Path.Combine(_tempDirectory, "CustomAddIns");
```

</details>

## Alert #731 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/731
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:34-34`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
    28	    }
    29	
    30	    public void Dispose()
    31	    {
    32	        if (Directory.Exists(_tempDirectory))
    33	        {
    34	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    35	        }
    36	        GC.SuppressFinalize(this);
    37	    }
    38	
    39	    [Fact]
    40	    public void Deploy_AddInFound_DeploysToUserAddIns()
    41	    {
    42	        var addinDir = Path.Combine(_versionDir, "AddIn");
```

</details>

## Alert #730 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/730
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:446-446`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   438	
   439	            result.ExitCode.Should().Be(0);
   440	            result.StdOut.Should().Contain(testString,
   441	                "cmd.exe must preserve UTF-8 output without corruption");
   442	        }
   443	        finally
   444	        {
   445	            try { File.Delete(cmdFile); } catch { }
   446	            try { Directory.Delete(tempDir, true); } catch { }
   447	        }
   448	    }
   449	
   450	    #endregion
   451	
   452	    #region FakeRuntime (for integration testing)
   453	
   454	    [Fact]
```

</details>

## Alert #729 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/729
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:445-445`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   437	                TimeSpan.FromSeconds(10));
   438	
   439	            result.ExitCode.Should().Be(0);
   440	            result.StdOut.Should().Contain(testString,
   441	                "cmd.exe must preserve UTF-8 output without corruption");
   442	        }
   443	        finally
   444	        {
   445	            try { File.Delete(cmdFile); } catch { }
   446	            try { Directory.Delete(tempDir, true); } catch { }
   447	        }
   448	    }
   449	
   450	    #endregion
   451	
   452	    #region FakeRuntime (for integration testing)
   453	
```

</details>

## Alert #728 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/728
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:400-403`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
   392	    {
   393	        foreach (var process in _startedProcesses)
   394	        {
   395	            try
   396	            {
   397	                if (!process.HasExited)
   398	                    process.Kill();
   399	            }
   400	            catch
   401	            {
   402	                // Process already stopped.
   403	            }
   404	            finally
   405	            {
   406	                process.Dispose();
   407	            }
   408	        }
   409	
   410	        if (Directory.Exists(_root))
   411	            Directory.Delete(_root, recursive: true);
```

</details>

## Alert #727 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/727
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:214-214`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
   206	                progress: progress,
   207	                cancellationToken: CancellationToken.None);
   208	
   209	            result.ExitCode.Should().Be(0);
   210	            result.StdOut.Should().Be(input, "returned output must not be mutated by progress reporting");
   211	        }
   212	        finally
   213	        {
   214	            try { File.Delete(tempFile); } catch { }
   215	        }
   216	    }
   217	
   218	    [Fact]
   219	    public async Task LargePayload_IsPreserved()
   220	    {
   221	        var sb = new StringBuilder();
   222	        sb.AppendLine("# Large Response");
```

</details>

## Alert #726 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/726
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:75-75`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
    67	                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
    68	                null, TimeSpan.FromSeconds(15),
    69	                cancellationToken: CancellationToken.None);
    70	            result.ExitCode.Should().Be(0, because: $"stderr: {result.StdErr}");
    71	            return result.StdOut;
    72	        }
    73	        finally
    74	        {
    75	            try { File.Delete(tempFile); } catch { }
    76	        }
    77	    }
    78	
    79	    [Fact]
    80	    public async Task NoFinalNewline_RemainsNoFinalNewline()
    81	    {
    82	        var input = "line1\nline2\nline3";
    83	        var output = await WriteRawUtf8(input);
```

</details>

## Alert #725 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/725
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:523-526`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
   515	        }
   516	
   517	        private static T Find<T>(FrameworkElement? owner, string key, T fallback)
   518	        {
   519	            try
   520	            {
   521	                return owner?.TryFindResource(key) is T value ? value : fallback;
   522	            }
   523	            catch
   524	            {
   525	                return fallback;
   526	            }
   527	        }
   528	    }
   529	
   530	    private static SolidColorBrush CreateFrozenBrush(byte red, byte green, byte blue)
   531	    {
   532	        var brush = new SolidColorBrush(Color.FromRgb(red, green, blue));
   533	        brush.Freeze();
   534	        return brush;
```

</details>

## Alert #724 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/724
- Location: `tests/TiaAgent.AddIn.Tests/StaTestHelper.cs:55-58`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    47	        Exception? threadException = null;
    48	        T? result = default;
    49	        var thread = new Thread(() =>
    50	        {
    51	            try
    52	            {
    53	                result = func();
    54	            }
    55	            catch (Exception ex)
    56	            {
    57	                threadException = ex;
    58	            }
    59	        });
    60	
    61	        thread.SetApartmentState(ApartmentState.STA);
    62	        thread.Start();
    63	        thread.Join();
    64	
    65	        if (threadException != null)
    66	        {
```

</details>

## Alert #723 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/723
- Location: `tests/TiaAgent.AddIn.Tests/StaTestHelper.cs:25-28`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    17	    {
    18	        Exception? threadException = null;
    19	        var thread = new Thread(() =>
    20	        {
    21	            try
    22	            {
    23	                action();
    24	            }
    25	            catch (Exception ex)
    26	            {
    27	                threadException = ex;
    28	            }
    29	        });
    30	
    31	        thread.SetApartmentState(ApartmentState.STA);
    32	        thread.Start();
    33	        thread.Join();
    34	
    35	        if (threadException != null)
    36	        {
```

</details>

## Alert #722 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/722
- Location: `src/TiaAgent.ResponseCenter/Views/MarkdownRenderer.cs:46-49`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 536 lines

<details><summary>Current code context</summary>

```text
    38	            var parsedDocument = Markdown.Parse(markdown, s_pipeline);
    39	            var flowDocument = CreateDocument(theme, useMonospaceFont: false);
    40	
    41	            foreach (var block in parsedDocument)
    42	                RenderBlock(flowDocument.Blocks, block, theme);
    43	
    44	            return flowDocument;
    45	        }
    46	        catch
    47	        {
    48	            return null;
    49	        }
    50	    }
    51	
    52	    public static WpfDocuments.FlowDocument CreatePlainTextFallback(string text) =>
    53	        CreatePlainTextFallback(text, null);
    54	
    55	    public static WpfDocuments.FlowDocument CreatePlainTextFallback(
    56	        string text,
    57	        FrameworkElement? resourceOwner)
```

</details>

## Alert #721 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/721
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:210-213`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   202	            ? Strings.EmptyResponseMessage
   203	            : markdown;
   204	
   205	        try
   206	        {
   207	            ResponseViewer.Document = MarkdownRenderer.Render(displayContent, ResponseViewer)
   208	                ?? MarkdownRenderer.CreatePlainTextFallback(displayContent, ResponseViewer);
   209	        }
   210	        catch
   211	        {
   212	            ResponseViewer.Document = MarkdownRenderer.CreatePlainTextFallback(displayContent, ResponseViewer);
   213	        }
   214	    }
   215	
   216	    private void OnViewModelRequestClose()
   217	    {
   218	        Dispatcher.Invoke(Close);
   219	    }
   220	
   221	    private void OnWindowClosing(object? sender, CancelEventArgs e)
```

</details>

## Alert #720 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/720
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:173-176`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   165	            if (!intersectsAnyMonitor && monitorCount > 0)
   166	            {
   167	                ResponseCenterLogger.Warn(
   168	                    $"Window is off-screen (pos={Left:F0},{Top:F0}); resetting to center");
   169	                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
   170	                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
   171	            }
   172	        }
   173	        catch (Exception ex)
   174	        {
   175	            ResponseCenterLogger.Warn($"EnsureOnScreen failed: {ex.Message}");
   176	        }
   177	    }
   178	
   179	    private static bool RectanglesOverlap(RECT a, RECT b)
   180	    {
   181	        return a.Left < b.Right && a.Right > b.Left && a.Top < b.Bottom && a.Bottom > b.Top;
   182	    }
   183	
   184	    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
```

</details>

## Alert #719 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/719
- Location: `src/TiaAgent.ResponseCenter/ViewModels/AgentResponseViewModel.cs:333-336`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 602 lines

<details><summary>Current code context</summary>

```text
   325	    {
   326	        try
   327	        {
   328	            if (!string.IsNullOrEmpty(ResponseContent))
   329	            {
   330	                Clipboard.SetText(ResponseContent);
   331	            }
   332	        }
   333	        catch
   334	        {
   335	            // Clipboard access can fail in some environments
   336	        }
   337	    }
   338	
   339	    private void ExecuteCopyResponse()
   340	    {
   341	        ExecuteCopy();
   342	    }
   343	
   344	    private void ExecuteSend()
```

</details>

## Alert #718 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/718
- Location: `src/TiaAgent.ResponseCenter/Services/ResponseCenterPipeListener.cs:93-104`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
    85	                }
    86	
    87	                NewTaskRequested?.Invoke(request);
    88	            }
    89	            catch (OperationCanceledException)
    90	            {
    91	                break;
    92	            }
    93	            catch (Exception ex)
    94	            {
    95	                ResponseCenterLogger.Warn($"Activation pipe listener failed: {ex.Message}");
    96	                try
    97	                {
    98	                    await Task.Delay(1000, ct).ConfigureAwait(false);
    99	                }
   100	                catch (OperationCanceledException)
   101	                {
   102	                    break;
   103	                }
   104	            }
   105	        }
   106	    }
   107	
   108	    public void Dispose()
   109	    {
   110	        _cts?.Cancel();
   111	        _cts?.Dispose();
   112	        _cts = null;
```

</details>

## Alert #717 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/717
- Location: `src/TiaAgent.ResponseCenter/Program.cs:128-132`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 332 lines

<details><summary>Current code context</summary>

```text
   120	                        try
   121	                        {
   122	                            await ReadinessReporter.SendReadinessAsync(
   123	                                    readyContext,
   124	                                    window,
   125	                                    TimeSpan.FromSeconds(5))
   126	                                .ConfigureAwait(false);
   127	                        }
   128	                        catch (Exception ex)
   129	                        {
   130	                            ResponseCenterLogger.Warn(
   131	                                $"Activation readiness send failed: {ex.Message}");
   132	                        }
   133	                    });
   134	                };
   135	                pipeListener.Start();
   136	            }
   137	
   138	            viewModel.StartMonitoring();
   139	            ResponseCenterLogger.Info("Window Show called");
   140	            window.Show();
```

</details>

## Alert #716 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/716
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeTaskMonitor.cs:253-263`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 296 lines

<details><summary>Current code context</summary>

```text
   245	                PollingError?.Invoke("Request timed out.");
   246	
   247	                if (consecutiveErrors >= maxConsecutiveErrors)
   248	                {
   249	                    StateChanged?.Invoke(AgentTaskState.Disconnected, null, null);
   250	                    return;
   251	                }
   252	            }
   253	            catch (Exception ex)
   254	            {
   255	                consecutiveErrors++;
   256	                PollingError?.Invoke($"Unexpected error: {ex.Message}");
   257	
   258	                if (consecutiveErrors >= maxConsecutiveErrors)
   259	                {
   260	                    StateChanged?.Invoke(AgentTaskState.Disconnected, null, null);
   261	                    return;
   262	                }
   263	            }
   264	        }
   265	    }
   266	
   267	    private async Task<BridgeTaskStatus?> FetchTaskStatusAsync(CancellationToken cancellationToken)
   268	    {
   269	        var response = await _httpClient.GetAsync(
   270	            $"v1/tasks/{_context.TaskId}",
   271	            cancellationToken).ConfigureAwait(false);
```

</details>

## Alert #715 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/715
- Location: `src/TiaAgent.ResponseCenter/Services/ReadinessReporter.cs:75-78`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 121 lines

<details><summary>Current code context</summary>

```text
    67	            await pipeClient.FlushAsync(cts.Token).ConfigureAwait(false);
    68	
    69	            ResponseCenterLogger.Info("Readiness sent successfully");
    70	        }
    71	        catch (OperationCanceledException)
    72	        {
    73	            ResponseCenterLogger.Warn("Readiness send timed out — Bridge may not be listening");
    74	        }
    75	        catch (Exception ex)
    76	        {
    77	            ResponseCenterLogger.Warn($"Failed to send readiness: {ex.Message}");
    78	        }
    79	    }
    80	
    81	    internal static ReadinessInfo BuildReadinessInfo(AgentResponseContext context, Window window)
    82	    {
    83	        var process = Process.GetCurrentProcess();
    84	        var helper = new WindowInteropHelper(window);
    85	        var hwnd = helper.Handle;
    86	
```

</details>

## Alert #714 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/714
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeTaskMonitor.cs:105-108`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 296 lines

<details><summary>Current code context</summary>

```text
    97	            using var cancelCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    98	            try
    99	            {
   100	                await _httpClient.PostAsync(
   101	                    $"v1/tasks/{_context.TaskId}/cancel",
   102	                    null,
   103	                    cancelCts.Token).ConfigureAwait(false);
   104	            }
   105	            catch
   106	            {
   107	                // Best-effort: if the cancel request fails, we still stop polling
   108	            }
   109	
   110	            StateChanged?.Invoke(AgentTaskState.Cancelled, null, null);
   111	        }
   112	        catch (Exception ex)
   113	        {
   114	            PollingError?.Invoke($"Cancel request failed: {ex.Message}");
   115	        }
   116	    }
```

</details>

## Alert #713 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/713
- Location: `src/TiaAgent.ResponseCenter/Program.cs:155-158`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 332 lines

<details><summary>Current code context</summary>

```text
   147	                try
   148	                {
   149	                    await ReadinessReporter.SendReadinessAsync(
   150	                            context,
   151	                            window,
   152	                            TimeSpan.FromSeconds(5))
   153	                        .ConfigureAwait(false);
   154	                }
   155	                catch (Exception ex)
   156	                {
   157	                    ResponseCenterLogger.Warn($"Readiness reporter failed: {ex.Message}");
   158	                }
   159	            });
   160	
   161	            ResponseCenterLogger.Info("Application.Run entered");
   162	            app.Run(window);
   163	
   164	            ResponseCenterLogger.Info("Application shutting down");
   165	            pipeListener?.Dispose();
   166	            return 0;
```

</details>

## Alert #712 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/712
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeTaskMonitor.cs:112-115`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 296 lines

<details><summary>Current code context</summary>

```text
   104	            }
   105	            catch
   106	            {
   107	                // Best-effort: if the cancel request fails, we still stop polling
   108	            }
   109	
   110	            StateChanged?.Invoke(AgentTaskState.Cancelled, null, null);
   111	        }
   112	        catch (Exception ex)
   113	        {
   114	            PollingError?.Invoke($"Cancel request failed: {ex.Message}");
   115	        }
   116	    }
   117	
   118	    /// <summary>Stops the monitor without sending a cancel request.</summary>
   119	    public void Stop()
   120	    {
   121	        _cts.Cancel();
   122	    }
   123	
```

</details>

## Alert #711 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/711
- Location: `src/TiaAgent.ResponseCenter/Program.cs:168-177`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 332 lines

<details><summary>Current code context</summary>

```text
   160	
   161	            ResponseCenterLogger.Info("Application.Run entered");
   162	            app.Run(window);
   163	
   164	            ResponseCenterLogger.Info("Application shutting down");
   165	            pipeListener?.Dispose();
   166	            return 0;
   167	        }
   168	        catch (Exception ex)
   169	        {
   170	            ResponseCenterLogger.Error("Fatal exception in Main", ex);
   171	            MessageBox.Show(
   172	                $"Failed to start TIA Agent Response Center:\n\n{ex.Message}",
   173	                "TIA Agent - Error",
   174	                MessageBoxButton.OK,
   175	                MessageBoxImage.Error);
   176	            return 1;
   177	        }
   178	    }
   179	
   180	    public static string BuildMutexName(AgentResponseContext context)
   181	    {
   182	        ArgumentNullException.ThrowIfNull(context);
   183	
   184	        var identity = string.IsNullOrWhiteSpace(context.TiaInstanceId)
   185	            ? context.TaskId
```

</details>

## Alert #710 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/710
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:111-114`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
   103	                        Directory.CreateDirectory(dir);
   104	
   105	                    var logFile = Path.Combine(dir, $"response-center-{DateTime.Now:yyyyMMdd}.log");
   106	                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
   107	                    var threadId = Environment.CurrentManagedThreadId;
   108	                    var entry = $"[{timestamp}] [{level}] [T{threadId}] {message}";
   109	                    File.AppendAllText(logFile, entry + Environment.NewLine);
   110	                }
   111	                catch
   112	                {
   113	                    _fileLoggingDisabled = true;
   114	                }
   115	            }
   116	        }
   117	        catch
   118	        {
   119	            // Logging must never crash the application
   120	        }
   121	    }
   122	}
```

</details>

## Alert #709 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/709
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:117-120`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
   109	                    File.AppendAllText(logFile, entry + Environment.NewLine);
   110	                }
   111	                catch
   112	                {
   113	                    _fileLoggingDisabled = true;
   114	                }
   115	            }
   116	        }
   117	        catch
   118	        {
   119	            // Logging must never crash the application
   120	        }
   121	    }
   122	}
```

</details>

## Alert #708 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/708
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:68-71`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
    60	            Info($"PID: {Environment.ProcessId}");
    61	            Info($"Thread ID: {Environment.CurrentManagedThreadId}");
    62	            Info($"Apartment state: {Thread.CurrentThread.GetApartmentState()}");
    63	            Info($"64-bit process: {Environment.Is64BitProcess}");
    64	            Info($"OS: {Environment.OSVersion}");
    65	            Info($"CLR: {Environment.Version}");
    66	            Info("=== Startup diagnostics complete ===");
    67	        }
    68	        catch
    69	        {
    70	            // Best-effort
    71	        }
    72	    }
    73	
    74	    public static void LogWindowState(string label, long hwnd, bool isVisible, string windowState,
    75	        double left, double top, double screenWidth, double screenHeight, string? taskId = null, string? tiaInstanceId = null)
    76	    {
    77	        var detail = $"hwnd={hwnd}, isVisible={isVisible}, state={windowState}, " +
    78	                     $"pos=({left:F0},{top:F0}), screen=({screenWidth:F0}x{screenHeight:F0})";
    79	        if (taskId != null) detail += $", taskId={taskId}";
```

</details>

## Alert #707 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/707
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:37-40`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
    29	
    30	            _logDirResolved = true;
    31	            try
    32	            {
    33	                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    34	                if (!string.IsNullOrEmpty(localAppData))
    35	                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
    36	            }
    37	            catch
    38	            {
    39	                // Permission denied — file logging will be disabled
    40	            }
    41	            return _logDir;
    42	        }
    43	    }
    44	
    45	    public static void Info(string message) => Log("INFO", message);
    46	    public static void Warn(string message) => Log("WARN", message);
    47	    public static void Debug(string message) => Log("DEBUG", message);
    48	
```

</details>

## Alert #706 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/706
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:230-233`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
   222	            }
   223	
   224	            try
   225	            {
   226	                File.Delete(file);
   227	                removed.Add(fileName);
   228	                stdout.WriteLine($"Removed stale Add-In: {fileName}");
   229	            }
   230	            catch (Exception ex)
   231	            {
   232	                stdout.WriteLine($"Warning: Failed to remove stale Add-In '{fileName}': {ex.Message}");
   233	            }
   234	        }
   235	
   236	        return removed;
   237	    }
   238	
   239	    /// <summary>
   240	    /// Preserves the Add-In file locally for manual installation.
   241	    /// Returns the fallback directory path.
```

</details>

## Alert #705 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/705
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:102-117`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
    94	            };
    95	        }
    96	
    97	        // UserAddIns directory is available — deploy
    98	        try
    99	        {
   100	            Directory.CreateDirectory(discovery.UserAddInsDirectory);
   101	        }
   102	        catch (Exception ex)
   103	        {
   104	            stdout.WriteLine();
   105	            stdout.WriteLine($"Failed to create UserAddIns directory '{discovery.UserAddInsDirectory}': {ex.Message}");
   106	            stdout.WriteLine($"Add-In preserved for manual installation at: {fallbackPath}");
   107	            stdout.WriteLine();
   108	
   109	            return new AddInDeploymentResult
   110	            {
   111	                Status = AddInDeploymentStatus.UserAddInsDirMissing,
   112	                FallbackDirectory = fallbackDir,
   113	                FallbackAddInPath = fallbackPath,
   114	                ErrorMessage = ex.Message,
   115	                InstalledAddInVersion = ExtractVersion(addInFiles[0])
   116	            };
   117	        }
   118	
   119	        // Clean up stale Add-In versions before deploying
   120	        var removedStale = RemoveStaleAddIns(discovery.UserAddInsDirectory, Path.GetFileName(addInFiles[0]), stdout);
   121	
   122	        // Copy the new Add-In
   123	        var destFile = Path.Combine(discovery.UserAddInsDirectory, Path.GetFileName(addInFiles[0]));
   124	        try
   125	        {
```

</details>

## Alert #704 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/704
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:54-57`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
    46	        // Preserve locally as fallback (always, regardless of TIA Portal detection)
    47	        string? fallbackDir = null;
    48	        string? fallbackPath = null;
    49	        try
    50	        {
    51	            fallbackDir = PreserveLocally(addInFiles[0], fallbackBaseDir, stdout);
    52	            fallbackPath = Path.Combine(fallbackDir, Path.GetFileName(addInFiles[0]));
    53	        }
    54	        catch (Exception ex)
    55	        {
    56	            stdout.WriteLine($"Warning: Could not preserve Add-In locally: {ex.Message}");
    57	        }
    58	
    59	        // Discover TIA Portal V21
    60	        var discovery = TiaPortalDiscovery.Discover(customUserAddInsDir);
    61	
    62	        // Deploy to UserAddIns
    63	        if (!discovery.UserAddInsDirectoryExists && !discovery.TiaPortalDetected)
    64	        {
    65	            stdout.WriteLine();
```

</details>

## Alert #703 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/703
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:281-284`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   273	        catch
   274	        {
   275	            // Some Windows/.NET combinations cannot kill an entire tree.
   276	            try
   277	            {
   278	                if (!process.HasExited)
   279	                    process.Kill();
   280	            }
   281	            catch
   282	            {
   283	                // Best effort only.
   284	            }
   285	        }
   286	    }
   287	
   288	    public static string StripAnsiEscapes(string text)
   289	    {
   290	        if (string.IsNullOrEmpty(text)) return text;
   291	        return AnsiEscapePattern.Replace(text, string.Empty);
   292	    }
```

</details>

## Alert #702 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/702
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:273-285`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   265	
   266	    private static void KillProcessTree(Process process)
   267	    {
   268	        try
   269	        {
   270	            if (!process.HasExited)
   271	                process.Kill(entireProcessTree: true);
   272	        }
   273	        catch
   274	        {
   275	            // Some Windows/.NET combinations cannot kill an entire tree.
   276	            try
   277	            {
   278	                if (!process.HasExited)
   279	                    process.Kill();
   280	            }
   281	            catch
   282	            {
   283	                // Best effort only.
   284	            }
   285	        }
   286	    }
   287	
   288	    public static string StripAnsiEscapes(string text)
   289	    {
   290	        if (string.IsNullOrEmpty(text)) return text;
   291	        return AnsiEscapePattern.Replace(text, string.Empty);
   292	    }
   293	
```

</details>

## Alert #701 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/701
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:378-382`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   370	
   371	    private ResponseCenterReadinessInfo? WaitForReadiness(string tiaInstanceId, TimeSpan timeout)
   372	    {
   373	        try
   374	        {
   375	            return _readinessListener.WaitForReadinessAsync(tiaInstanceId, timeout)
   376	                .GetAwaiter().GetResult();
   377	        }
   378	        catch (Exception ex)
   379	        {
   380	            _logger.Warn($"Error waiting for readiness: {ex.Message}");
   381	            return null;
   382	        }
   383	    }
   384	
   385	    private static bool IsValidReadiness(
   386	        LaunchResponseCenterRequest request,
   387	        ResponseCenterReadinessInfo? readiness,
   388	        int? expectedProcessId)
   389	    {
   390	        if (readiness == null || !readiness.IsVisible || readiness.ProcessId <= 0)
```

</details>

## Alert #700 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/700
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:344-354`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   336	
   337	    private (string? Path, ResponseCenterLaunchResult? Error) ResolveExecutableForLaunch()
   338	    {
   339	        string? executablePath;
   340	        try
   341	        {
   342	            executablePath = ResolveExecutablePath(_installationRoot);
   343	        }
   344	        catch (Exception ex)
   345	        {
   346	            _logger.Error("Failed to resolve Response Center executable path", ex);
   347	            return (
   348	                null,
   349	                new ResponseCenterLaunchResult
   350	                {
   351	                    Status = ResponseCenterLaunchStatus.ExecutableNotFound,
   352	                    ErrorMessage = $"Could not resolve Response Center executable: {ex.Message}"
   353	                });
   354	        }
   355	
   356	        if (string.IsNullOrWhiteSpace(executablePath) || !File.Exists(executablePath))
   357	        {
   358	            _logger.Warn($"Response Center executable not found at '{executablePath}'");
   359	            return (
   360	                null,
   361	                new ResponseCenterLaunchResult
   362	                {
```

</details>

## Alert #699 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/699
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:326-334`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   318	
   319	            return new ResponseCenterLaunchResult
   320	            {
   321	                Status = ResponseCenterLaunchStatus.StartupFailure,
   322	                ProcessId = process.Id,
   323	                ErrorMessage = "Response Center started but did not confirm the requested task and window visibility within the timeout period."
   324	            };
   325	        }
   326	        catch (Exception ex)
   327	        {
   328	            _logger.Error("Failed to start Response Center process", ex);
   329	            return new ResponseCenterLaunchResult
   330	            {
   331	                Status = ResponseCenterLaunchStatus.StartupFailure,
   332	                ErrorMessage = $"Failed to start Response Center: {ex.Message}"
   333	            };
   334	        }
   335	    }
   336	
   337	    private (string? Path, ResponseCenterLaunchResult? Error) ResolveExecutableForLaunch()
   338	    {
   339	        string? executablePath;
   340	        try
   341	        {
   342	            executablePath = ResolveExecutablePath(_installationRoot);
```

</details>

## Alert #698 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/698
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:98-103`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
    90	            pipeClient.Connect(2000);
    91	
    92	            var payload = JsonSerializer.Serialize(request, s_jsonOptions) + "\n";
    93	            var buffer = Encoding.UTF8.GetBytes(payload);
    94	            pipeClient.Write(buffer, 0, buffer.Length);
    95	            pipeClient.Flush();
    96	            return true;
    97	        }
    98	        catch (Exception ex)
    99	        {
   100	            _logger.Debug(
   101	                $"Named pipe activation failed for {request.TiaInstanceId}: {ex.Message}");
   102	            return false;
   103	        }
   104	    }
   105	}
   106	
   107	public sealed class ResponseCenterProcessManager : IDisposable
   108	{
   109	    private const string ExecutableName = "TiaAgent.ResponseCenter.exe";
   110	    internal static readonly TimeSpan DefaultReadinessTimeout = TimeSpan.FromSeconds(15);
   111	
```

</details>

## Alert #697 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/697
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:52-55`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
    44	    public void Kill(int processId)
    45	    {
    46	        try
    47	        {
    48	            var process = Process.GetProcessById(processId);
    49	            if (!process.HasExited)
    50	                process.Kill();
    51	        }
    52	        catch
    53	        {
    54	            // Process already exited or inaccessible.
    55	        }
    56	    }
    57	}
    58	
    59	/// <summary>
    60	/// Sends activation requests to an already running Response Center instance.
    61	/// </summary>
    62	public interface IResponseCenterActivationClient
    63	{
```

</details>

## Alert #696 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/696
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:38-41`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
    30	
    31	    public bool IsAlive(int processId)
    32	    {
    33	        try
    34	        {
    35	            var process = Process.GetProcessById(processId);
    36	            return !process.HasExited;
    37	        }
    38	        catch
    39	        {
    40	            return false;
    41	        }
    42	    }
    43	
    44	    public void Kill(int processId)
    45	    {
    46	        try
    47	        {
    48	            var process = Process.GetProcessById(processId);
    49	            if (!process.HasExited)
```

</details>

## Alert #695 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/695
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:281-285`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   273	    private void LogResolvedProcess(string requested, ResolvedCommand resolved, string context)
   274	    {
   275	        try
   276	        {
   277	            var consoleCodePage = Console.OutputEncoding.CodePage;
   278	            var outputEncodingName = Console.OutputEncoding.EncodingName;
   279	            _logger.Info($"ClaudeCodeRuntime [{context}]: resolved exe={resolved.FileName}, target={resolved.ResolvedTargetPath}, requested={requested}, wrapper={resolved.Wrapper}, consoleCodePage={consoleCodePage}, outputEncoding={outputEncodingName}");
   280	        }
   281	        catch
   282	        {
   283	            // Logging should never crash the process resolution
   284	            _logger.Info($"ClaudeCodeRuntime [{context}]: resolved exe={resolved.FileName}, target={resolved.ResolvedTargetPath}, requested={requested}, wrapper={resolved.Wrapper}");
   285	        }
   286	    }
   287	
   288	    /// <summary>
   289	    /// Builds fixed, short command-line arguments for claude.
   290	    /// The dynamic prompt is NOT placed here — it goes through stdin.
   291	    /// </summary>
   292	    private string BuildArguments()
   293	    {
```

</details>

## Alert #694 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/694
- Location: `src/TiaAgent.Bridge/ResponseCenter/ReadinessListener.cs:84-88`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
    76	
    77	            return info;
    78	        }
    79	        catch (OperationCanceledException)
    80	        {
    81	            _logger.Warn($"Readiness wait timed out after {timeout.TotalSeconds}s for '{instanceId}'");
    82	            return null;
    83	        }
    84	        catch (Exception ex)
    85	        {
    86	            _logger.Warn($"Readiness listener error: {ex.Message}");
    87	            return null;
    88	        }
    89	    }
    90	
    91	    internal static string GetPipeName(string instanceId)
    92	    {
    93	        var sanitized = new string(instanceId.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-').ToArray());
    94	        return $"TiaAgent_RCReady_{sanitized}";
    95	    }
    96	
```

</details>

## Alert #693 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/693
- Location: `src/TiaAgent.Bridge/Program.cs:170-173`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
   162	                logger.Startup($"Bridge file size: {fileInfo.Length} bytes");
   163	                logger.Startup($"Bridge file SHA-256: {ComputeFileSha256(location)}");
   164	            }
   165	            else
   166	            {
   167	                logger.Warn("Bridge assembly location is unavailable or does not exist on disk.");
   168	            }
   169	        }
   170	        catch (Exception ex)
   171	        {
   172	            logger.Warn($"Could not log Bridge binary identity: {ex.Message}");
   173	        }
   174	    }
   175	
   176	    private static string ComputeFileSha256(string path)
   177	    {
   178	        using var stream = File.OpenRead(path);
   179	        using var sha256 = SHA256.Create();
   180	        return Convert.ToHexString(sha256.ComputeHash(stream)).ToLowerInvariant();
   181	    }
```

</details>

## Alert #692 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/692
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:82-82`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    74	    }
    75	
    76	    public async Task AbortSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    77	    {
    78	        try
    79	        {
    80	            await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/abort", null, cancellationToken).ConfigureAwait(false);
    81	        }
    82	        catch { }
    83	    }
    84	
    85	    public void Dispose() => _httpClient.Dispose();
    86	
    87	    /// <summary>
    88	    /// Reads HTTP response content as a string using explicit UTF-8 encoding.
    89	    /// Prevents encoding corruption when the server response lacks a charset
    90	    /// in the Content-Type header.
```

</details>

## Alert #691 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/691
- Location: `src/TiaAgent.AddIn/Ui/ResponseCenterLauncher.cs:111-119`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 165 lines

<details><summary>Current code context</summary>

```text
   103	            }
   104	
   105	            return new ResponseCenterLaunchResult(
   106	                success,
   107	                response.Status,
   108	                response.ErrorMessage,
   109	                response.ActivatedExistingInstance);
   110	        }
   111	        catch (Exception ex)
   112	        {
   113	            AddInLogger.Error("Failed to request Response Center launch from Bridge.", ex);
   114	            return new ResponseCenterLaunchResult(
   115	                false,
   116	                ResponseCenterLaunchStatus.StartupFailure,
   117	                ex.Message,
   118	                false);
   119	        }
   120	    }
   121	
   122	    internal static string ResolveExecutablePath(string? installationRoot = null)
   123	    {
   124	        var root = installationRoot;
   125	        if (string.IsNullOrWhiteSpace(root))
   126	        {
   127	            root = Path.Combine(
```

</details>

## Alert #690 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/690
- Location: `src/TiaAgent.AddIn/Bridge/AgentBridgeClient.cs:412-415`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 606 lines

<details><summary>Current code context</summary>

```text
   404	        {
   405	            var latin1 = Encoding.GetEncoding(28591);
   406	            var bytes = latin1.GetBytes(text);
   407	            var repaired = Encoding.UTF8.GetString(bytes);
   408	
   409	            if (repaired != text && !repaired.Contains('�'))
   410	                return repaired;
   411	        }
   412	        catch
   413	        {
   414	            // Compatibility helper only; production does not depend on it.
   415	        }
   416	
   417	        return text;
   418	    }
   419	
   420	    private static int ExtractJsonInt(string json, string key, int defaultValue = 0)
   421	    {
   422	        var search = "\"" + key + "\"";
   423	        var idx = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
```

</details>

## Alert #689 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/689
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:305-310`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   297	                        $"addin-{DateTime.Now:yyyyMMdd}.log");
   298	
   299	                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
   300	                    var threadId = Environment.CurrentManagedThreadId;
   301	                    var entry = $"[{timestamp}] [{level}] [T{threadId}] {message}";
   302	
   303	                    File.AppendAllText(logFile, entry + Environment.NewLine);
   304	                }
   305	                catch
   306	                {
   307	                    // First file I/O failure: disable file logging permanently.
   308	                    // This prevents repeated SecurityException / IOException spam.
   309	                    _fileLoggingDisabled = true;
   310	                }
   311	            }
   312	        }
   313	        catch
   314	        {
   315	            // Catch-all: logging must never crash the Add-In.
   316	        }
   317	    }
   318	}
```

</details>

## Alert #688 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/688
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:313-316`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   305	                catch
   306	                {
   307	                    // First file I/O failure: disable file logging permanently.
   308	                    // This prevents repeated SecurityException / IOException spam.
   309	                    _fileLoggingDisabled = true;
   310	                }
   311	            }
   312	        }
   313	        catch
   314	        {
   315	            // Catch-all: logging must never crash the Add-In.
   316	        }
   317	    }
   318	}
```

</details>

## Alert #687 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/687
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:264-267`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   256	                    {
   257	                        Warn($"Assembly '{name}' NOT loaded and DLL NOT found at {dllPath ?? "(no base dir)"}");
   258	                    }
   259	                }
   260	            }
   261	
   262	            Info("--- Third-party assembly diagnostics complete ---");
   263	        }
   264	        catch (Exception ex)
   265	        {
   266	            Warn($"Third-party assembly diagnostics failed (best-effort): {ex.Message}");
   267	        }
   268	    }
   269	
   270	    /// <summary>
   271	    /// Writes a log entry. On first file I/O failure, silently disables file logging
   272	    /// for the rest of the session (no recurring failures).
   273	    /// Never throws.
   274	    /// </summary>
   275	    private static void Log(string level, string message)
```

</details>

## Alert #686 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/686
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:187-190`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   179	                Info($"Add-In file size: {fileInfo.Length} bytes");
   180	                Info($"Add-In file SHA-256: {ComputeFileSha256(location)}");
   181	            }
   182	            else
   183	            {
   184	                Warn("Add-In assembly location is unavailable or does not exist on disk.");
   185	            }
   186	        }
   187	        catch (Exception ex)
   188	        {
   189	            Warn($"Could not log Add-In binary identity: {ex.Message}");
   190	        }
   191	    }
   192	
   193	    private static string ComputeFileSha256(string path)
   194	    {
   195	        using var stream = File.OpenRead(path);
   196	        using var sha256 = SHA256.Create();
   197	        var hash = sha256.ComputeHash(stream);
   198	        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
```

</details>

## Alert #685 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/685
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:144-147`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   136	            };
   137	            foreach (var name in wpfAssemblies)
   138	            {
   139	                try
   140	                {
   141	                    var asm = System.Reflection.Assembly.Load(name);
   142	                    Info($"WPF assembly loaded: {asm.FullName} @ {asm.Location}");
   143	                }
   144	                catch (Exception loadEx)
   145	                {
   146	                    Warn($"WPF assembly '{name}' not loaded: {loadEx.Message}");
   147	                }
   148	            }
   149	        }
   150	        catch (Exception asmEx)
   151	        {
   152	            Warn($"Failed to enumerate WPF assemblies: {asmEx.Message}");
   153	        }
   154	
   155	        // Log critical assembly availability
```

</details>

## Alert #684 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/684
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:150-153`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   142	                    Info($"WPF assembly loaded: {asm.FullName} @ {asm.Location}");
   143	                }
   144	                catch (Exception loadEx)
   145	                {
   146	                    Warn($"WPF assembly '{name}' not loaded: {loadEx.Message}");
   147	                }
   148	            }
   149	        }
   150	        catch (Exception asmEx)
   151	        {
   152	            Warn($"Failed to enumerate WPF assemblies: {asmEx.Message}");
   153	        }
   154	
   155	        // Log critical assembly availability
   156	        LogThirdPartyAssemblyDiagnostics();
   157	
   158	        Info("=== Startup diagnostics complete ===");
   159	    }
   160	
   161	    private static void LogLoadedBinaryIdentity()
```

</details>

## Alert #683 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/683
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:117-120`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   109	        {
   110	            var process = Process.GetCurrentProcess();
   111	            Info($"Process: {process.ProcessName} (PID {process.Id})");
   112	            Info($"Process start time: {process.StartTime:O}");
   113	            try
   114	            {
   115	                Info($"Process path: {process.MainModule?.FileName ?? "(unknown)"}");
   116	            }
   117	            catch (Exception pathEx)
   118	            {
   119	                Warn($"Could not read process path: {pathEx.Message}");
   120	            }
   121	        }
   122	        catch (Exception ex)
   123	        {
   124	            Warn($"Could not read process info: {ex.Message}");
   125	        }
   126	
   127	        // Log WPF assembly availability
   128	        try
```

</details>

## Alert #682 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/682
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:122-125`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   114	            {
   115	                Info($"Process path: {process.MainModule?.FileName ?? "(unknown)"}");
   116	            }
   117	            catch (Exception pathEx)
   118	            {
   119	                Warn($"Could not read process path: {pathEx.Message}");
   120	            }
   121	        }
   122	        catch (Exception ex)
   123	        {
   124	            Warn($"Could not read process info: {ex.Message}");
   125	        }
   126	
   127	        // Log WPF assembly availability
   128	        try
   129	        {
   130	            var wpfAssemblies = new[]
   131	            {
   132	                "PresentationFramework",
   133	                "PresentationCore",
```

</details>

## Alert #681 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/681
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:84-88`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
    76	    /// Logs startup diagnostics. Must NEVER throw.
    77	    /// </summary>
    78	    public static void Startup()
    79	    {
    80	        try
    81	        {
    82	            LogStartupDiagnostics();
    83	        }
    84	        catch
    85	        {
    86	            // Startup diagnostics are best-effort.
    87	            // Never prevent Add-In loading because of logging.
    88	        }
    89	    }
    90	
    91	    private static void LogStartupDiagnostics()
    92	    {
    93	        var arch = System.IntPtr.Size == 8 ? "x64" : "x86";
    94	
    95	        Info("=== TIA Portal Add-In Startup ===");
    96	        Info($"Architecture: {arch}");
```

</details>

## Alert #680 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/680
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:56-59`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
    48	            {
    49	                var localAppData = Environment.GetFolderPath(
    50	                    Environment.SpecialFolder.LocalApplicationData);
    51	                if (!string.IsNullOrEmpty(localAppData))
    52	                {
    53	                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
    54	                }
    55	            }
    56	            catch
    57	            {
    58	                // EnvironmentPermission not granted — file logging will be disabled
    59	            }
    60	
    61	            return _logDir;
    62	        }
    63	    }
    64	
    65	    public static void Info(string message) => Log("INFO", message);
    66	    public static void Warn(string message) => Log("WARN", message);
    67	    public static void Debug(string message) => Log("DEBUG", message);
```

</details>

## Alert #679 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/679
- Location: `src/TiaAgent.AddIn/Bridge/AddInConfig.cs:70-73`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
    62	            {
    63	                var token = File.ReadAllText(TokenFilePath).Trim();
    64	                if (!string.IsNullOrEmpty(token))
    65	                {
    66	                    return token;
    67	                }
    68	            }
    69	        }
    70	        catch
    71	        {
    72	            // File I/O may be restricted in sandbox — requests will fail with 401
    73	        }
    74	
    75	        return null;
    76	    }
    77	
    78	    private static int ExtractPort(string json)
    79	    {
    80	        // Find "bridge" object, then "port" value
    81	        var bridgeIdx = json.IndexOf("\"bridge\"", StringComparison.OrdinalIgnoreCase);
```

</details>

## Alert #678 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/678
- Location: `src/TiaAgent.AddIn/Bridge/AddInConfig.cs:49-52`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
    41	                var json = File.ReadAllText(RuntimeManifestPath);
    42	                var port = ExtractPort(json);
    43	                if (port > 0)
    44	                {
    45	                    return $"http://127.0.0.1:{port}";
    46	                }
    47	            }
    48	        }
    49	        catch
    50	        {
    51	            // File I/O may be restricted in sandbox — use default port
    52	        }
    53	
    54	        return DefaultBridgeBaseUrl;
    55	    }
    56	
    57	    private static string? DiscoverAuthToken()
    58	    {
    59	        try
    60	        {
```

</details>

## Alert #677 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/677
- Location: `tests/TiaAgent.AddIn.Tests/JsonUnescapeTests.cs:451-451`
- Message: This assignment to repaired is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 658 lines

<details><summary>Current code context</summary>

```text
   443	        // — (U+2014) = UTF-8 bytes E2 80 94 → CP437: ΓÇö
   444	        var original = "—";
   445	        var corrupted = SimulateCp437Corruption(original);
   446	        corrupted.Should().NotBe(original); // Confirm corruption happened
   447	
   448	        // The current RepairMojibake (ISO-8859-1 based) may not fix CP437 mojibake.
   449	        // This test documents the gap: CP437 corruption requires the upstream fix
   450	        // (using cmd.exe/direct exe instead of PowerShell).
   451	        var repaired = AgentBridgeClient.RepairMojibake(corrupted);
   452	
   453	        // Log what actually happens for diagnostic purposes
   454	        // CP437 mojibake is NOT the same as ISO-8859-1 mojibake — the bytes differ.
   455	        // This test validates that the corruption IS CP437, not ISO-8859-1.
   456	        corrupted.Should().Contain("ΓÇö"); // CP437 corruption pattern for em-dash
   457	    }
   458	
   459	    [Fact]
```

</details>

## Alert #676 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/676
- Location: `src/TiaAgent.ResponseCenter/obj/Release/net8.0-windows/Views/AgentResponseWindow.g.cs:81-81`
- Message: This assignment to resourceLocater is useless, since its value is never read.

- Current file exists on `main`: **no**

## Alert #675 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/675
- Location: `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:239-239`
- Message: This assignment to timeout is useless, since its value is never read.

- Current file exists on `main`: **no**

## Alert #674 — cs/unmanaged-code

- Rule: `cs/unmanaged-code`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/674
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:253-253`
- Message: Minimise the use of unmanaged code.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   245	    #region Win32 Interop
   246	
   247	    [DllImport("user32.dll", CharSet = CharSet.Auto)]
   248	    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
   249	
   250	    private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
   251	
   252	    [DllImport("user32.dll")]
   253	    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
   254	
   255	    [StructLayout(LayoutKind.Sequential)]
   256	    private struct RECT
   257	    {
   258	        public int Left;
   259	        public int Top;
   260	        public int Right;
   261	        public int Bottom;
```

</details>

## Alert #673 — cs/unmanaged-code

- Rule: `cs/unmanaged-code`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/673
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:248-248`
- Message: Minimise the use of unmanaged code.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   240	    {
   241	        _viewModel.ShowTechnicalDetails = false;
   242	        DetailsToggleText.Text = Strings.ViewDetails;
   243	    }
   244	
   245	    #region Win32 Interop
   246	
   247	    [DllImport("user32.dll", CharSet = CharSet.Auto)]
   248	    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
   249	
   250	    private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
   251	
   252	    [DllImport("user32.dll")]
   253	    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
   254	
   255	    [StructLayout(LayoutKind.Sequential)]
   256	    private struct RECT
```

</details>

## Alert #672 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/672
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeConnectionDiscoveryTests.cs:23-23`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 59 lines

<details><summary>Current code context</summary>

```text
    15	
    16	    [Fact]
    17	    public void Resolve_ReadsRuntimePortAndToken()
    18	    {
    19	        Directory.CreateDirectory(Path.Combine(_root, "runtime"));
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "runtime", "runtime.json"),
    22	            "{\"bridge\":{\"port\":45231}}");
    23	        File.WriteAllText(Path.Combine(_root, "bridge.token"), " secret-token \r\n");
    24	
    25	        var settings = BridgeConnectionDiscovery.Resolve(null, null, _root);
    26	
    27	        settings.BridgeUrl.Should().Be("http://127.0.0.1:45231");
    28	        settings.AuthToken.Should().Be("secret-token");
    29	    }
    30	
    31	    [Fact]
```

</details>

## Alert #671 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/671
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeConnectionDiscoveryTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 59 lines

<details><summary>Current code context</summary>

```text
    13	        "tia-agent-bridge-discovery-tests",
    14	        Guid.NewGuid().ToString("N"));
    15	
    16	    [Fact]
    17	    public void Resolve_ReadsRuntimePortAndToken()
    18	    {
    19	        Directory.CreateDirectory(Path.Combine(_root, "runtime"));
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "runtime", "runtime.json"),
    22	            "{\"bridge\":{\"port\":45231}}");
    23	        File.WriteAllText(Path.Combine(_root, "bridge.token"), " secret-token \r\n");
    24	
    25	        var settings = BridgeConnectionDiscovery.Resolve(null, null, _root);
    26	
    27	        settings.BridgeUrl.Should().Be("http://127.0.0.1:45231");
    28	        settings.AuthToken.Should().Be("secret-token");
    29	    }
```

</details>

## Alert #670 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/670
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeConnectionDiscoveryTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 59 lines

<details><summary>Current code context</summary>

```text
    11	    private readonly string _root = Path.Combine(
    12	        Path.GetTempPath(),
    13	        "tia-agent-bridge-discovery-tests",
    14	        Guid.NewGuid().ToString("N"));
    15	
    16	    [Fact]
    17	    public void Resolve_ReadsRuntimePortAndToken()
    18	    {
    19	        Directory.CreateDirectory(Path.Combine(_root, "runtime"));
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "runtime", "runtime.json"),
    22	            "{\"bridge\":{\"port\":45231}}");
    23	        File.WriteAllText(Path.Combine(_root, "bridge.token"), " secret-token \r\n");
    24	
    25	        var settings = BridgeConnectionDiscovery.Resolve(null, null, _root);
    26	
    27	        settings.BridgeUrl.Should().Be("http://127.0.0.1:45231");
```

</details>

## Alert #669 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/669
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeConnectionDiscoveryTests.cs:11-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 59 lines

<details><summary>Current code context</summary>

```text
     3	using FluentAssertions;
     4	using TiaAgent.ResponseCenter.Services;
     5	using Xunit;
     6	
     7	namespace TiaAgent.ResponseCenter.Tests;
     8	
     9	public sealed class BridgeConnectionDiscoveryTests : IDisposable
    10	{
    11	    private readonly string _root = Path.Combine(
    12	        Path.GetTempPath(),
    13	        "tia-agent-bridge-discovery-tests",
    14	        Guid.NewGuid().ToString("N"));
    15	
    16	    [Fact]
    17	    public void Resolve_ReadsRuntimePortAndToken()
    18	    {
    19	        Directory.CreateDirectory(Path.Combine(_root, "runtime"));
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "runtime", "runtime.json"),
    22	            "{\"bridge\":{\"port\":45231}}");
```

</details>

## Alert #668 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/668
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:226-226`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   218	        Directory.CreateDirectory(addinDir);
   219	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   220	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   221	
   222	        var fallbackDir = AddInDeployer.PreserveLocally(addinFile, _fallbackBaseDir, TextWriter.Null);
   223	
   224	        fallbackDir.Should().Be(Path.Combine(_fallbackBaseDir, "AddIn"));
   225	        Directory.Exists(fallbackDir).Should().BeTrue();
   226	        File.Exists(Path.Combine(fallbackDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   227	    }
   228	}
```

</details>

## Alert #667 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/667
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:224-224`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   216	    {
   217	        var addinDir = Path.Combine(_versionDir, "AddIn");
   218	        Directory.CreateDirectory(addinDir);
   219	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   220	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   221	
   222	        var fallbackDir = AddInDeployer.PreserveLocally(addinFile, _fallbackBaseDir, TextWriter.Null);
   223	
   224	        fallbackDir.Should().Be(Path.Combine(_fallbackBaseDir, "AddIn"));
   225	        Directory.Exists(fallbackDir).Should().BeTrue();
   226	        File.Exists(Path.Combine(fallbackDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   227	    }
   228	}
```

</details>

## Alert #666 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/666
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:219-219`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
   212	    }
   213	
   214	    [Fact]
   215	    public void PreserveLocally_CreatesFallbackDirectory()
   216	    {
   217	        var addinDir = Path.Combine(_versionDir, "AddIn");
   218	        Directory.CreateDirectory(addinDir);
   219	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   220	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   221	
   222	        var fallbackDir = AddInDeployer.PreserveLocally(addinFile, _fallbackBaseDir, TextWriter.Null);
   223	
   224	        fallbackDir.Should().Be(Path.Combine(_fallbackBaseDir, "AddIn"));
   225	        Directory.Exists(fallbackDir).Should().BeTrue();
   226	        File.Exists(Path.Combine(fallbackDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   227	    }
```

</details>

## Alert #665 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/665
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:217-217`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
   212	    }
   213	
   214	    [Fact]
   215	    public void PreserveLocally_CreatesFallbackDirectory()
   216	    {
   217	        var addinDir = Path.Combine(_versionDir, "AddIn");
   218	        Directory.CreateDirectory(addinDir);
   219	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   220	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   221	
   222	        var fallbackDir = AddInDeployer.PreserveLocally(addinFile, _fallbackBaseDir, TextWriter.Null);
   223	
   224	        fallbackDir.Should().Be(Path.Combine(_fallbackBaseDir, "AddIn"));
   225	        Directory.Exists(fallbackDir).Should().BeTrue();
```

</details>

## Alert #664 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/664
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:211-211`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
   208	        removed.Should().Contain("TiaAgent-0.1.0.addin");
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
   212	    }
   213	
   214	    [Fact]
   215	    public void PreserveLocally_CreatesFallbackDirectory()
   216	    {
   217	        var addinDir = Path.Combine(_versionDir, "AddIn");
   218	        Directory.CreateDirectory(addinDir);
   219	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
```

</details>

## Alert #663 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/663
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:210-210`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
   208	        removed.Should().Contain("TiaAgent-0.1.0.addin");
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
   212	    }
   213	
   214	    [Fact]
   215	    public void PreserveLocally_CreatesFallbackDirectory()
   216	    {
   217	        var addinDir = Path.Combine(_versionDir, "AddIn");
   218	        Directory.CreateDirectory(addinDir);
```

</details>

## Alert #662 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/662
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:203-203`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   195	    public void RemoveStaleAddIns_RemovesOnlyTiaAgentFiles()
   196	    {
   197	        var addinDir = Path.Combine(_versionDir, "AddIn");
   198	        Directory.CreateDirectory(addinDir);
   199	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   200	
   201	        // Mix of TiaAgent and non-TiaAgent files
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
   208	        removed.Should().Contain("TiaAgent-0.1.0.addin");
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
```

</details>

## Alert #661 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/661
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:202-202`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   194	    [Fact]
   195	    public void RemoveStaleAddIns_RemovesOnlyTiaAgentFiles()
   196	    {
   197	        var addinDir = Path.Combine(_versionDir, "AddIn");
   198	        Directory.CreateDirectory(addinDir);
   199	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   200	
   201	        // Mix of TiaAgent and non-TiaAgent files
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
   208	        removed.Should().Contain("TiaAgent-0.1.0.addin");
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
```

</details>

## Alert #660 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/660
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:199-199`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   191	        version.Should().BeNull();
   192	    }
   193	
   194	    [Fact]
   195	    public void RemoveStaleAddIns_RemovesOnlyTiaAgentFiles()
   196	    {
   197	        var addinDir = Path.Combine(_versionDir, "AddIn");
   198	        Directory.CreateDirectory(addinDir);
   199	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   200	
   201	        // Mix of TiaAgent and non-TiaAgent files
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
```

</details>

## Alert #659 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/659
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:197-197`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   189	    {
   190	        var version = AddInDeployer.ExtractVersion("SomeOther.addin");
   191	        version.Should().BeNull();
   192	    }
   193	
   194	    [Fact]
   195	    public void RemoveStaleAddIns_RemovesOnlyTiaAgentFiles()
   196	    {
   197	        var addinDir = Path.Combine(_versionDir, "AddIn");
   198	        Directory.CreateDirectory(addinDir);
   199	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   200	
   201	        // Mix of TiaAgent and non-TiaAgent files
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
```

</details>

## Alert #658 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/658
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:107-107`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    99	    [Fact]
   100	    public void DeriveTiaRootFromApiDir_ReturnsCorrectRoot()
   101	    {
   102	        var apiDir = Path.Combine(_tempDirectory, "Portal V21", "PublicAPI", "V21", "net48");
   103	        Directory.CreateDirectory(apiDir);
   104	
   105	        var root = TiaPortalDiscovery.DeriveTiaRootFromApiDir(apiDir);
   106	
   107	        root.Should().Be(Path.Combine(_tempDirectory, "Portal V21"));
   108	    }
   109	
   110	    [Fact]
   111	    public void Discover_NullCustomDir_ReturnsConsistentResult()
   112	    {
   113	        // With no env var override, should return a consistent result
   114	        // (may detect TIA Portal if installed on the machine)
   115	        Environment.SetEnvironmentVariable("TiaPublicApiDir", null);
```

</details>

## Alert #657 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/657
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:173-173`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   165	        addinFiles.Should().BeEmpty();
   166	    }
   167	
   168	    [Fact]
   169	    public void FindAddInFiles_WithAddIn_ReturnsFile()
   170	    {
   171	        var addinDir = Path.Combine(_versionDir, "AddIn");
   172	        Directory.CreateDirectory(addinDir);
   173	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   174	
   175	        var addinFiles = AddInDeployer.FindAddInFiles(_versionDir);
   176	        addinFiles.Should().HaveCount(1);
   177	        addinFiles[0].Should().EndWith("TiaAgent-0.2.0.addin");
   178	    }
   179	
   180	    [Fact]
   181	    public void ExtractVersion_ValidFilename_ReturnsVersion()
```

</details>

## Alert #656 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/656
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:171-171`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   163	    {
   164	        var addinFiles = AddInDeployer.FindAddInFiles(_versionDir);
   165	        addinFiles.Should().BeEmpty();
   166	    }
   167	
   168	    [Fact]
   169	    public void FindAddInFiles_WithAddIn_ReturnsFile()
   170	    {
   171	        var addinDir = Path.Combine(_versionDir, "AddIn");
   172	        Directory.CreateDirectory(addinDir);
   173	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   174	
   175	        var addinFiles = AddInDeployer.FindAddInFiles(_versionDir);
   176	        addinFiles.Should().HaveCount(1);
   177	        addinFiles[0].Should().EndWith("TiaAgent-0.2.0.addin");
   178	    }
   179	
```

</details>

## Alert #655 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/655
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:102-102`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    94	
    95	        result.UserAddInsDirectory.Should().Be(customDir);
    96	        result.UserAddInsDirectoryExists.Should().BeTrue();
    97	    }
    98	
    99	    [Fact]
   100	    public void DeriveTiaRootFromApiDir_ReturnsCorrectRoot()
   101	    {
   102	        var apiDir = Path.Combine(_tempDirectory, "Portal V21", "PublicAPI", "V21", "net48");
   103	        Directory.CreateDirectory(apiDir);
   104	
   105	        var root = TiaPortalDiscovery.DeriveTiaRootFromApiDir(apiDir);
   106	
   107	        root.Should().Be(Path.Combine(_tempDirectory, "Portal V21"));
   108	    }
   109	
   110	    [Fact]
```

</details>

## Alert #654 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/654
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:158-158`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   150	        var spacedUserAddIns = Path.Combine(_tempDirectory, "User Ins Dir");
   151	        // Pre-create the directory since the deployer will try to detect TIA Portal
   152	        // and may not find it, but with a custom dir it should deploy
   153	        Directory.CreateDirectory(spacedUserAddIns);
   154	
   155	        var result = AddInDeployer.Deploy(spacedVersionDir, spacedUserAddIns, _fallbackBaseDir, TextWriter.Null);
   156	
   157	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   158	        File.Exists(Path.Combine(spacedUserAddIns, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   159	    }
   160	
   161	    [Fact]
   162	    public void FindAddInFiles_EmptyDir_ReturnsEmpty()
   163	    {
   164	        var addinFiles = AddInDeployer.FindAddInFiles(_versionDir);
   165	        addinFiles.Should().BeEmpty();
   166	    }
```

</details>

## Alert #653 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/653
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:90-90`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    82	        result.UserAddInsDirectory.Should().Be(
    83	            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    84	                "Siemens", "Automation", "Portal V21", "UserAddIns"));
    85	    }
    86	
    87	    [Fact]
    88	    public void Discover_WithPathWithSpaces_HandlesCorrectly()
    89	    {
    90	        var customDir = Path.Combine(_tempDirectory, "Path With Spaces", "UserAddIns");
    91	        Directory.CreateDirectory(customDir);
    92	
    93	        var result = TiaPortalDiscovery.Discover(customDir);
    94	
    95	        result.UserAddInsDirectory.Should().Be(customDir);
    96	        result.UserAddInsDirectoryExists.Should().BeTrue();
    97	    }
    98	
```

</details>

## Alert #652 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/652
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:150-150`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   142	    {
   143	        var spacedVersionDir = Path.Combine(_tempDirectory, "versions", "0.2.0 beta");
   144	        Directory.CreateDirectory(spacedVersionDir);
   145	        var addinDir = Path.Combine(spacedVersionDir, "AddIn");
   146	        Directory.CreateDirectory(addinDir);
   147	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   148	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   149	
   150	        var spacedUserAddIns = Path.Combine(_tempDirectory, "User Ins Dir");
   151	        // Pre-create the directory since the deployer will try to detect TIA Portal
   152	        // and may not find it, but with a custom dir it should deploy
   153	        Directory.CreateDirectory(spacedUserAddIns);
   154	
   155	        var result = AddInDeployer.Deploy(spacedVersionDir, spacedUserAddIns, _fallbackBaseDir, TextWriter.Null);
   156	
   157	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   158	        File.Exists(Path.Combine(spacedUserAddIns, "TiaAgent-0.2.0.addin")).Should().BeTrue();
```

</details>

## Alert #651 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/651
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:83-84`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    75	
    76	        var result = TiaPortalDiscovery.Discover();
    77	
    78	        result.TiaPortalDetected.Should().BeTrue();
    79	        result.DetectionSource.Should().Be("env-var");
    80	        result.UserAddInsDirectory.Should().Contain("UserAddIns");
    81	        // UserAddIns may or may not exist depending on whether TIA Portal is installed on the machine
    82	        result.UserAddInsDirectory.Should().Be(
    83	            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    84	                "Siemens", "Automation", "Portal V21", "UserAddIns"));
    85	    }
    86	
    87	    [Fact]
    88	    public void Discover_WithPathWithSpaces_HandlesCorrectly()
    89	    {
    90	        var customDir = Path.Combine(_tempDirectory, "Path With Spaces", "UserAddIns");
    91	        Directory.CreateDirectory(customDir);
    92	
```

</details>

## Alert #650 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/650
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:147-147`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   139	
   140	    [Fact]
   141	    public void Deploy_PathWithSpaces_DeploysCorrectly()
   142	    {
   143	        var spacedVersionDir = Path.Combine(_tempDirectory, "versions", "0.2.0 beta");
   144	        Directory.CreateDirectory(spacedVersionDir);
   145	        var addinDir = Path.Combine(spacedVersionDir, "AddIn");
   146	        Directory.CreateDirectory(addinDir);
   147	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   148	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   149	
   150	        var spacedUserAddIns = Path.Combine(_tempDirectory, "User Ins Dir");
   151	        // Pre-create the directory since the deployer will try to detect TIA Portal
   152	        // and may not find it, but with a custom dir it should deploy
   153	        Directory.CreateDirectory(spacedUserAddIns);
   154	
   155	        var result = AddInDeployer.Deploy(spacedVersionDir, spacedUserAddIns, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #649 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/649
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:145-145`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   137	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   138	    }
   139	
   140	    [Fact]
   141	    public void Deploy_PathWithSpaces_DeploysCorrectly()
   142	    {
   143	        var spacedVersionDir = Path.Combine(_tempDirectory, "versions", "0.2.0 beta");
   144	        Directory.CreateDirectory(spacedVersionDir);
   145	        var addinDir = Path.Combine(spacedVersionDir, "AddIn");
   146	        Directory.CreateDirectory(addinDir);
   147	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   148	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   149	
   150	        var spacedUserAddIns = Path.Combine(_tempDirectory, "User Ins Dir");
   151	        // Pre-create the directory since the deployer will try to detect TIA Portal
   152	        // and may not find it, but with a custom dir it should deploy
   153	        Directory.CreateDirectory(spacedUserAddIns);
```

</details>

## Alert #648 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/648
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:143-143`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   135	        var result2 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
   136	        result2.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   137	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   138	    }
   139	
   140	    [Fact]
   141	    public void Deploy_PathWithSpaces_DeploysCorrectly()
   142	    {
   143	        var spacedVersionDir = Path.Combine(_tempDirectory, "versions", "0.2.0 beta");
   144	        Directory.CreateDirectory(spacedVersionDir);
   145	        var addinDir = Path.Combine(spacedVersionDir, "AddIn");
   146	        Directory.CreateDirectory(addinDir);
   147	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   148	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   149	
   150	        var spacedUserAddIns = Path.Combine(_tempDirectory, "User Ins Dir");
   151	        // Pre-create the directory since the deployer will try to detect TIA Portal
```

</details>

## Alert #647 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/647
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:72-72`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    64	    }
    65	
    66	    [Fact]
    67	    public void Discover_WithEnvVar_DetectsTiaPortal()
    68	    {
    69	        // Create a fake TIA Portal API directory structure
    70	        var fakeApiDir = Path.Combine(_tempDirectory, "Portal V21", "PublicAPI", "V21", "net48");
    71	        Directory.CreateDirectory(fakeApiDir);
    72	        File.WriteAllText(Path.Combine(fakeApiDir, "Siemens.Engineering.Base.dll"), "fake");
    73	
    74	        Environment.SetEnvironmentVariable("TiaPublicApiDir", fakeApiDir);
    75	
    76	        var result = TiaPortalDiscovery.Discover();
    77	
    78	        result.TiaPortalDetected.Should().BeTrue();
    79	        result.DetectionSource.Should().Be("env-var");
    80	        result.UserAddInsDirectory.Should().Contain("UserAddIns");
```

</details>

## Alert #646 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/646
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:70-70`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    62	        result.DetectionSource.Should().Be("cli-override");
    63	        result.TiaPortalDetected.Should().BeTrue();
    64	    }
    65	
    66	    [Fact]
    67	    public void Discover_WithEnvVar_DetectsTiaPortal()
    68	    {
    69	        // Create a fake TIA Portal API directory structure
    70	        var fakeApiDir = Path.Combine(_tempDirectory, "Portal V21", "PublicAPI", "V21", "net48");
    71	        Directory.CreateDirectory(fakeApiDir);
    72	        File.WriteAllText(Path.Combine(fakeApiDir, "Siemens.Engineering.Base.dll"), "fake");
    73	
    74	        Environment.SetEnvironmentVariable("TiaPublicApiDir", fakeApiDir);
    75	
    76	        var result = TiaPortalDiscovery.Discover();
    77	
    78	        result.TiaPortalDetected.Should().BeTrue();
```

</details>

## Alert #645 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/645
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:137-137`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   129	
   130	        // First deployment
   131	        var result1 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
   132	        result1.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   133	
   134	        // Second deployment (idempotent)
   135	        var result2 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
   136	        result2.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   137	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   138	    }
   139	
   140	    [Fact]
   141	    public void Deploy_PathWithSpaces_DeploysCorrectly()
   142	    {
   143	        var spacedVersionDir = Path.Combine(_tempDirectory, "versions", "0.2.0 beta");
   144	        Directory.CreateDirectory(spacedVersionDir);
   145	        var addinDir = Path.Combine(spacedVersionDir, "AddIn");
```

</details>

## Alert #644 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/644
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:56-56`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    48	        result.UserAddInsDirectory.Should().Be(customDir);
    49	        result.UserAddInsDirectoryExists.Should().BeTrue();
    50	        result.DetectionSource.Should().Be("cli-override");
    51	    }
    52	
    53	    [Fact]
    54	    public void Discover_WithCustomDir_NotExists_ReturnsOverrideButNotExists()
    55	    {
    56	        var customDir = Path.Combine(_tempDirectory, "NonExistent");
    57	
    58	        var result = TiaPortalDiscovery.Discover(customDir);
    59	
    60	        result.UserAddInsDirectory.Should().Be(customDir);
    61	        result.UserAddInsDirectoryExists.Should().BeFalse();
    62	        result.DetectionSource.Should().Be("cli-override");
    63	        result.TiaPortalDetected.Should().BeTrue();
    64	    }
```

</details>

## Alert #643 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/643
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:127-127`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   119	        File.Exists(result.FallbackAddInPath).Should().BeTrue();
   120	    }
   121	
   122	    [Fact]
   123	    public void Deploy_IdempotentRepeatedInstallation()
   124	    {
   125	        var addinDir = Path.Combine(_versionDir, "AddIn");
   126	        Directory.CreateDirectory(addinDir);
   127	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   128	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   129	
   130	        // First deployment
   131	        var result1 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
   132	        result1.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   133	
   134	        // Second deployment (idempotent)
   135	        var result2 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #642 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/642
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:125-125`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   117	        // For this test, we verify the fallback is always preserved.
   118	        result.FallbackAddInPath.Should().NotBeNull();
   119	        File.Exists(result.FallbackAddInPath).Should().BeTrue();
   120	    }
   121	
   122	    [Fact]
   123	    public void Deploy_IdempotentRepeatedInstallation()
   124	    {
   125	        var addinDir = Path.Combine(_versionDir, "AddIn");
   126	        Directory.CreateDirectory(addinDir);
   127	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   128	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   129	
   130	        // First deployment
   131	        var result1 = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
   132	        result1.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
   133	
```

</details>

## Alert #641 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/641
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:112-112`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   104	    public void Deploy_UserAddInsDirMissing_ReturnsFallbackOnly()
   105	    {
   106	        var addinDir = Path.Combine(_versionDir, "AddIn");
   107	        Directory.CreateDirectory(addinDir);
   108	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   109	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   110	
   111	        // Use a non-existent custom dir that can't be created (simulating permission issue)
   112	        var lockedDir = Path.Combine(_tempDirectory, "locked", "UserAddIns");
   113	
   114	        var result = AddInDeployer.Deploy(_versionDir, lockedDir, _fallbackBaseDir, TextWriter.Null);
   115	
   116	        // The deployer will try to create the directory. If it can, it will deploy.
   117	        // For this test, we verify the fallback is always preserved.
   118	        result.FallbackAddInPath.Should().NotBeNull();
   119	        File.Exists(result.FallbackAddInPath).Should().BeTrue();
   120	    }
```

</details>

## Alert #640 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/640
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:43-43`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    35	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    36	        }
    37	        GC.SuppressFinalize(this);
    38	    }
    39	
    40	    [Fact]
    41	    public void Discover_WithCustomDir_ReturnsCustomDir()
    42	    {
    43	        var customDir = Path.Combine(_tempDirectory, "CustomAddIns");
    44	        Directory.CreateDirectory(customDir);
    45	
    46	        var result = TiaPortalDiscovery.Discover(customDir);
    47	
    48	        result.UserAddInsDirectory.Should().Be(customDir);
    49	        result.UserAddInsDirectoryExists.Should().BeTrue();
    50	        result.DetectionSource.Should().Be("cli-override");
    51	    }
```

</details>

## Alert #639 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/639
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:108-108`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   100	        result.IsAvailable.Should().BeFalse();
   101	    }
   102	
   103	    [Fact]
   104	    public void Deploy_UserAddInsDirMissing_ReturnsFallbackOnly()
   105	    {
   106	        var addinDir = Path.Combine(_versionDir, "AddIn");
   107	        Directory.CreateDirectory(addinDir);
   108	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   109	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   110	
   111	        // Use a non-existent custom dir that can't be created (simulating permission issue)
   112	        var lockedDir = Path.Combine(_tempDirectory, "locked", "UserAddIns");
   113	
   114	        var result = AddInDeployer.Deploy(_versionDir, lockedDir, _fallbackBaseDir, TextWriter.Null);
   115	
   116	        // The deployer will try to create the directory. If it can, it will deploy.
```

</details>

## Alert #638 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/638
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:106-106`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    98	        result.Status.Should().Be(AddInDeploymentStatus.NoAddInPackage);
    99	        result.IsFullyDeployed.Should().BeFalse();
   100	        result.IsAvailable.Should().BeFalse();
   101	    }
   102	
   103	    [Fact]
   104	    public void Deploy_UserAddInsDirMissing_ReturnsFallbackOnly()
   105	    {
   106	        var addinDir = Path.Combine(_versionDir, "AddIn");
   107	        Directory.CreateDirectory(addinDir);
   108	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   109	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
   110	
   111	        // Use a non-existent custom dir that can't be created (simulating permission issue)
   112	        var lockedDir = Path.Combine(_tempDirectory, "locked", "UserAddIns");
   113	
   114	        var result = AddInDeployer.Deploy(_versionDir, lockedDir, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #637 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/637
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:89-89`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    81	        // Stale Add-In from previous version
    82	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("Old Version"));
    83	
    84	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    85	
    86	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    87	        result.RemovedStaleFiles.Should().Contain("TiaAgent-0.1.0.addin");
    88	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
    89	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    90	    }
    91	
    92	    [Fact]
    93	    public void Deploy_NoAddInPackage_ReturnsNoAddInPackage()
    94	    {
    95	        // No AddIn/ directory
    96	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    97	
```

</details>

## Alert #636 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/636
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
     8	
     9	public sealed class TiaPortalDiscoveryTests : IDisposable
    10	{
    11	    private readonly string _tempDirectory;
    12	    private readonly string _originalTiaPublicApiDir;
    13	
    14	    public TiaPortalDiscoveryTests()
    15	    {
    16	        _tempDirectory = Path.Combine(Path.GetTempPath(), "TiaPortalDiscoveryTests_" + Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_tempDirectory);
    18	        _originalTiaPublicApiDir = Environment.GetEnvironmentVariable("TiaPublicApiDir") ?? string.Empty;
    19	    }
    20	
    21	    public void Dispose()
    22	    {
    23	        // Restore original env var
    24	        if (string.IsNullOrEmpty(_originalTiaPublicApiDir))
```

</details>

## Alert #635 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/635
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:88-88`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    80	
    81	        // Stale Add-In from previous version
    82	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("Old Version"));
    83	
    84	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    85	
    86	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    87	        result.RemovedStaleFiles.Should().Contain("TiaAgent-0.1.0.addin");
    88	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
    89	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    90	    }
    91	
    92	    [Fact]
    93	    public void Deploy_NoAddInPackage_ReturnsNoAddInPackage()
    94	    {
    95	        // No AddIn/ directory
    96	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #634 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/634
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:82-82`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    74	    public void Deploy_RemovesStaleVersions()
    75	    {
    76	        var addinDir = Path.Combine(_versionDir, "AddIn");
    77	        Directory.CreateDirectory(addinDir);
    78	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    79	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    80	
    81	        // Stale Add-In from previous version
    82	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("Old Version"));
    83	
    84	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    85	
    86	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    87	        result.RemovedStaleFiles.Should().Contain("TiaAgent-0.1.0.addin");
    88	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
    89	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    90	    }
```

</details>

## Alert #633 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/633
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:78-78`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    70	        File.ReadAllText(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().Be("New Content");
    71	    }
    72	
    73	    [Fact]
    74	    public void Deploy_RemovesStaleVersions()
    75	    {
    76	        var addinDir = Path.Combine(_versionDir, "AddIn");
    77	        Directory.CreateDirectory(addinDir);
    78	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    79	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    80	
    81	        // Stale Add-In from previous version
    82	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("Old Version"));
    83	
    84	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    85	
    86	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
```

</details>

## Alert #632 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/632
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:76-76`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    68	
    69	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    70	        File.ReadAllText(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().Be("New Content");
    71	    }
    72	
    73	    [Fact]
    74	    public void Deploy_RemovesStaleVersions()
    75	    {
    76	        var addinDir = Path.Combine(_versionDir, "AddIn");
    77	        Directory.CreateDirectory(addinDir);
    78	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    79	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    80	
    81	        // Stale Add-In from previous version
    82	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("Old Version"));
    83	
    84	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #631 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/631
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:70-70`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    62	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    63	
    64	        // Pre-existing Add-In
    65	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("Old Content"));
    66	
    67	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    68	
    69	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    70	        File.ReadAllText(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().Be("New Content");
    71	    }
    72	
    73	    [Fact]
    74	    public void Deploy_RemovesStaleVersions()
    75	    {
    76	        var addinDir = Path.Combine(_versionDir, "AddIn");
    77	        Directory.CreateDirectory(addinDir);
    78	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
```

</details>

## Alert #630 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/630
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:65-65`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    57	    public void Deploy_ReplacesExistingVersion()
    58	    {
    59	        var addinDir = Path.Combine(_versionDir, "AddIn");
    60	        Directory.CreateDirectory(addinDir);
    61	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    62	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    63	
    64	        // Pre-existing Add-In
    65	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("Old Content"));
    66	
    67	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    68	
    69	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    70	        File.ReadAllText(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().Be("New Content");
    71	    }
    72	
    73	    [Fact]
```

</details>

## Alert #629 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/629
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:61-61`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    53	        File.Exists(Path.Combine(_fallbackBaseDir, "AddIn", "TiaAgent-0.2.0.addin")).Should().BeTrue();
    54	    }
    55	
    56	    [Fact]
    57	    public void Deploy_ReplacesExistingVersion()
    58	    {
    59	        var addinDir = Path.Combine(_versionDir, "AddIn");
    60	        Directory.CreateDirectory(addinDir);
    61	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    62	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    63	
    64	        // Pre-existing Add-In
    65	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("Old Content"));
    66	
    67	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    68	
    69	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
```

</details>

## Alert #628 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/628
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:59-59`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    51	        result.InstalledAddInVersion.Should().Be("0.2.0");
    52	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    53	        File.Exists(Path.Combine(_fallbackBaseDir, "AddIn", "TiaAgent-0.2.0.addin")).Should().BeTrue();
    54	    }
    55	
    56	    [Fact]
    57	    public void Deploy_ReplacesExistingVersion()
    58	    {
    59	        var addinDir = Path.Combine(_versionDir, "AddIn");
    60	        Directory.CreateDirectory(addinDir);
    61	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    62	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("New Content"));
    63	
    64	        // Pre-existing Add-In
    65	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("Old Content"));
    66	
    67	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
```

</details>

## Alert #627 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/627
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:53-53`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    45	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
    46	
    47	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    48	
    49	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    50	        result.IsFullyDeployed.Should().BeTrue();
    51	        result.InstalledAddInVersion.Should().Be("0.2.0");
    52	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    53	        File.Exists(Path.Combine(_fallbackBaseDir, "AddIn", "TiaAgent-0.2.0.addin")).Should().BeTrue();
    54	    }
    55	
    56	    [Fact]
    57	    public void Deploy_ReplacesExistingVersion()
    58	    {
    59	        var addinDir = Path.Combine(_versionDir, "AddIn");
    60	        Directory.CreateDirectory(addinDir);
    61	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
```

</details>

## Alert #626 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/626
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:52-52`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    44	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    45	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
    46	
    47	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    48	
    49	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    50	        result.IsFullyDeployed.Should().BeTrue();
    51	        result.InstalledAddInVersion.Should().Be("0.2.0");
    52	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    53	        File.Exists(Path.Combine(_fallbackBaseDir, "AddIn", "TiaAgent-0.2.0.addin")).Should().BeTrue();
    54	    }
    55	
    56	    [Fact]
    57	    public void Deploy_ReplacesExistingVersion()
    58	    {
    59	        var addinDir = Path.Combine(_versionDir, "AddIn");
    60	        Directory.CreateDirectory(addinDir);
```

</details>

## Alert #625 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/625
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:44-44`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    36	        GC.SuppressFinalize(this);
    37	    }
    38	
    39	    [Fact]
    40	    public void Deploy_AddInFound_DeploysToUserAddIns()
    41	    {
    42	        var addinDir = Path.Combine(_versionDir, "AddIn");
    43	        Directory.CreateDirectory(addinDir);
    44	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    45	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
    46	
    47	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    48	
    49	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    50	        result.IsFullyDeployed.Should().BeTrue();
    51	        result.InstalledAddInVersion.Should().Be("0.2.0");
    52	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
```

</details>

## Alert #624 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/624
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:42-42`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    34	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    35	        }
    36	        GC.SuppressFinalize(this);
    37	    }
    38	
    39	    [Fact]
    40	    public void Deploy_AddInFound_DeploysToUserAddIns()
    41	    {
    42	        var addinDir = Path.Combine(_versionDir, "AddIn");
    43	        Directory.CreateDirectory(addinDir);
    44	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
    45	        File.WriteAllBytes(addinFile, Encoding.UTF8.GetBytes("AddIn Content"));
    46	
    47	        var result = AddInDeployer.Deploy(_versionDir, _userAddInsDir, _fallbackBaseDir, TextWriter.Null);
    48	
    49	        result.Status.Should().Be(AddInDeploymentStatus.DeployedWithFallback);
    50	        result.IsFullyDeployed.Should().BeTrue();
```

</details>

## Alert #623 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/623
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:22-22`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    14	    private readonly string _userAddInsDir;
    15	    private readonly string _fallbackBaseDir;
    16	
    17	    public AddInDeployerTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "AddInDeployerTests_" + Guid.NewGuid().ToString("N"));
    20	        _versionDir = Path.Combine(_tempDirectory, "versions", "0.2.0");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	        _fallbackBaseDir = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	
    24	        Directory.CreateDirectory(_tempDirectory);
    25	        Directory.CreateDirectory(_versionDir);
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
    28	    }
    29	
    30	    public void Dispose()
```

</details>

## Alert #622 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/622
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    13	    private readonly string _versionDir;
    14	    private readonly string _userAddInsDir;
    15	    private readonly string _fallbackBaseDir;
    16	
    17	    public AddInDeployerTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "AddInDeployerTests_" + Guid.NewGuid().ToString("N"));
    20	        _versionDir = Path.Combine(_tempDirectory, "versions", "0.2.0");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	        _fallbackBaseDir = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	
    24	        Directory.CreateDirectory(_tempDirectory);
    25	        Directory.CreateDirectory(_versionDir);
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
    28	    }
    29	
```

</details>

## Alert #621 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/621
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    12	    private readonly string _tempDirectory;
    13	    private readonly string _versionDir;
    14	    private readonly string _userAddInsDir;
    15	    private readonly string _fallbackBaseDir;
    16	
    17	    public AddInDeployerTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "AddInDeployerTests_" + Guid.NewGuid().ToString("N"));
    20	        _versionDir = Path.Combine(_tempDirectory, "versions", "0.2.0");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	        _fallbackBaseDir = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	
    24	        Directory.CreateDirectory(_tempDirectory);
    25	        Directory.CreateDirectory(_versionDir);
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
    28	    }
```

</details>

## Alert #620 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/620
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    11	{
    12	    private readonly string _tempDirectory;
    13	    private readonly string _versionDir;
    14	    private readonly string _userAddInsDir;
    15	    private readonly string _fallbackBaseDir;
    16	
    17	    public AddInDeployerTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "AddInDeployerTests_" + Guid.NewGuid().ToString("N"));
    20	        _versionDir = Path.Combine(_tempDirectory, "versions", "0.2.0");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	        _fallbackBaseDir = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	
    24	        Directory.CreateDirectory(_tempDirectory);
    25	        Directory.CreateDirectory(_versionDir);
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
```

</details>

## Alert #619 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/619
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:336-336`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   328	        };
   329	
   330	        using var stdout = new StringWriter();
   331	        var exitCode = InstallCommand.Execute(options, stdout, TextWriter.Null);
   332	
   333	        exitCode.Should().Be(0);
   334	        stdout.ToString().Should().Contain("Successfully installed");
   335	        // Should not have deployed to UserAddIns since no .addin was present
   336	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeFalse();
   337	    }
   338	
   339	    private static void CreateDummyPayload(string payloadDir, string version)
   340	    {
   341	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   342	        var addinDir = Path.Combine(payloadDir, "AddIn");
   343	        Directory.CreateDirectory(bridgeDir);
   344	        Directory.CreateDirectory(addinDir);
```

</details>

## Alert #618 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/618
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:295-295`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   287	    public void InstallCommand_NoAddInPayload_SucceedsWithFallbackMessage()
   288	    {
   289	        // Create payload without AddIn directory
   290	        var emptyPayloadDir = Path.Combine(_tempDirectory, "payload_no_addin");
   291	        var bridgeDir = Path.Combine(emptyPayloadDir, "Bridge");
   292	        Directory.CreateDirectory(bridgeDir);
   293	        File.WriteAllBytes(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"), new byte[] { 1, 2, 3 });
   294	
   295	        var bridgeHash = PayloadStore.ComputeSha256(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"));
   296	        var manifest = new PayloadManifest
   297	        {
   298	            ProductVersion = "0.2.0-beta.1",
   299	            CommitSha = "testsha",
   300	            Components =
   301	            {
   302	                ["bridge"] = new PayloadComponentMetadata
   303	                {
```

</details>

## Alert #617 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/617
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:293-293`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   285	
   286	    [Fact]
   287	    public void InstallCommand_NoAddInPayload_SucceedsWithFallbackMessage()
   288	    {
   289	        // Create payload without AddIn directory
   290	        var emptyPayloadDir = Path.Combine(_tempDirectory, "payload_no_addin");
   291	        var bridgeDir = Path.Combine(emptyPayloadDir, "Bridge");
   292	        Directory.CreateDirectory(bridgeDir);
   293	        File.WriteAllBytes(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"), new byte[] { 1, 2, 3 });
   294	
   295	        var bridgeHash = PayloadStore.ComputeSha256(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"));
   296	        var manifest = new PayloadManifest
   297	        {
   298	            ProductVersion = "0.2.0-beta.1",
   299	            CommitSha = "testsha",
   300	            Components =
   301	            {
```

</details>

## Alert #616 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/616
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:291-291`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   283	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   284	    }
   285	
   286	    [Fact]
   287	    public void InstallCommand_NoAddInPayload_SucceedsWithFallbackMessage()
   288	    {
   289	        // Create payload without AddIn directory
   290	        var emptyPayloadDir = Path.Combine(_tempDirectory, "payload_no_addin");
   291	        var bridgeDir = Path.Combine(emptyPayloadDir, "Bridge");
   292	        Directory.CreateDirectory(bridgeDir);
   293	        File.WriteAllBytes(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"), new byte[] { 1, 2, 3 });
   294	
   295	        var bridgeHash = PayloadStore.ComputeSha256(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"));
   296	        var manifest = new PayloadManifest
   297	        {
   298	            ProductVersion = "0.2.0-beta.1",
   299	            CommitSha = "testsha",
```

</details>

## Alert #615 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/615
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:290-290`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   282	        stdout.ToString().Should().Contain("is already installed");
   283	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   284	    }
   285	
   286	    [Fact]
   287	    public void InstallCommand_NoAddInPayload_SucceedsWithFallbackMessage()
   288	    {
   289	        // Create payload without AddIn directory
   290	        var emptyPayloadDir = Path.Combine(_tempDirectory, "payload_no_addin");
   291	        var bridgeDir = Path.Combine(emptyPayloadDir, "Bridge");
   292	        Directory.CreateDirectory(bridgeDir);
   293	        File.WriteAllBytes(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"), new byte[] { 1, 2, 3 });
   294	
   295	        var bridgeHash = PayloadStore.ComputeSha256(Path.Combine(bridgeDir, "TiaAgent.Bridge.dll"));
   296	        var manifest = new PayloadManifest
   297	        {
   298	            ProductVersion = "0.2.0-beta.1",
```

</details>

## Alert #614 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/614
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:283-283`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   275	        var exitCode1 = InstallCommand.Execute(options, TextWriter.Null, TextWriter.Null);
   276	        exitCode1.Should().Be(0);
   277	
   278	        // Second install (already installed)
   279	        using var stdout = new StringWriter();
   280	        var exitCode2 = InstallCommand.Execute(options, stdout, TextWriter.Null);
   281	        exitCode2.Should().Be(0);
   282	        stdout.ToString().Should().Contain("is already installed");
   283	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   284	    }
   285	
   286	    [Fact]
   287	    public void InstallCommand_NoAddInPayload_SucceedsWithFallbackMessage()
   288	    {
   289	        // Create payload without AddIn directory
   290	        var emptyPayloadDir = Path.Combine(_tempDirectory, "payload_no_addin");
   291	        var bridgeDir = Path.Combine(emptyPayloadDir, "Bridge");
```

</details>

## Alert #613 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/613
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:259-259`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   251	            CustomRoot = _customRoot,
   252	            UserAddInsDir = _userAddInsDir
   253	        };
   254	
   255	        using var stdout = new StringWriter();
   256	        InstallCommand.Execute(options, stdout, TextWriter.Null);
   257	
   258	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   259	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   260	        stdout.ToString().Should().Contain("Removed stale Add-In: TiaAgent-0.1.0.addin");
   261	    }
   262	
   263	    [Fact]
   264	    public void InstallCommand_IdempotentRepeatedInstallation()
   265	    {
   266	        var options = new InstallOptions
   267	        {
```

</details>

## Alert #612 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/612
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:258-258`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   250	            PayloadDir = _payloadDir,
   251	            CustomRoot = _customRoot,
   252	            UserAddInsDir = _userAddInsDir
   253	        };
   254	
   255	        using var stdout = new StringWriter();
   256	        InstallCommand.Execute(options, stdout, TextWriter.Null);
   257	
   258	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   259	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   260	        stdout.ToString().Should().Contain("Removed stale Add-In: TiaAgent-0.1.0.addin");
   261	    }
   262	
   263	    [Fact]
   264	    public void InstallCommand_IdempotentRepeatedInstallation()
   265	    {
   266	        var options = new InstallOptions
```

</details>

## Alert #611 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/611
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:245-245`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   237	        stdout.ToString().Should().Contain("Deployed Add-In");
   238	        stdout.ToString().Should().Contain("Installed Add-In version: 0.2.0");
   239	    }
   240	
   241	    [Fact]
   242	    public void InstallCommand_WithAddIn_RemovesStaleFiles()
   243	    {
   244	        // Pre-install an older version
   245	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), new byte[] { 1, 2, 3 });
   246	
   247	        var options = new InstallOptions
   248	        {
   249	            Version = "0.2.0-beta.1",
   250	            PayloadDir = _payloadDir,
   251	            CustomRoot = _customRoot,
   252	            UserAddInsDir = _userAddInsDir
   253	        };
```

</details>

## Alert #610 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/610
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:236-236`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   228	            CustomRoot = _customRoot,
   229	            UserAddInsDir = _userAddInsDir
   230	        };
   231	
   232	        using var stdout = new StringWriter();
   233	        var exitCode = InstallCommand.Execute(options, stdout, TextWriter.Null);
   234	
   235	        exitCode.Should().Be(0);
   236	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
   237	        stdout.ToString().Should().Contain("Deployed Add-In");
   238	        stdout.ToString().Should().Contain("Installed Add-In version: 0.2.0");
   239	    }
   240	
   241	    [Fact]
   242	    public void InstallCommand_WithAddIn_RemovesStaleFiles()
   243	    {
   244	        // Pre-install an older version
```

</details>

## Alert #609 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/609
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:418-418`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   410	    {
   411	        // Integration test: cmd.exe does NOT re-encode child process stdout,
   412	        // so ProcessRunner reads raw UTF-8 bytes via StandardOutputEncoding = UTF8.
   413	        using var runner = new ProcessRunner(_logger);
   414	
   415	        // Create a temporary .cmd script that echoes the UTF-8 payload
   416	        var tempDir = Path.Combine(Path.GetTempPath(), $"tia-test-{Guid.NewGuid():N}");
   417	        Directory.CreateDirectory(tempDir);
   418	        var cmdFile = Path.Combine(tempDir, "utf8test.cmd");
   419	        var testString = "Ação — 🔴 🟡 🟢 → ─ ┐ ├ │";
   420	        // Write the .cmd script that uses chcp 65001 and echoes the test string
   421	        File.WriteAllText(cmdFile, $"@echo off\r\nchcp 65001 >nul\r\necho {testString}\r\n");
   422	
   423	        try
   424	        {
   425	            var resolved = CommandResolver.Resolve("utf8test", name => name switch
   426	            {
```

</details>

## Alert #608 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/608
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:416-416`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   408	    [Fact]
   409	    public async Task CommandResolver_CmdExe_PreservesUtf8_Integration()
   410	    {
   411	        // Integration test: cmd.exe does NOT re-encode child process stdout,
   412	        // so ProcessRunner reads raw UTF-8 bytes via StandardOutputEncoding = UTF8.
   413	        using var runner = new ProcessRunner(_logger);
   414	
   415	        // Create a temporary .cmd script that echoes the UTF-8 payload
   416	        var tempDir = Path.Combine(Path.GetTempPath(), $"tia-test-{Guid.NewGuid():N}");
   417	        Directory.CreateDirectory(tempDir);
   418	        var cmdFile = Path.Combine(tempDir, "utf8test.cmd");
   419	        var testString = "Ação — 🔴 🟡 🟢 → ─ ┐ ├ │";
   420	        // Write the .cmd script that uses chcp 65001 and echoes the test string
   421	        File.WriteAllText(cmdFile, $"@echo off\r\nchcp 65001 >nul\r\necho {testString}\r\n");
   422	
   423	        try
   424	        {
```

</details>

## Alert #607 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/607
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:345-345`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
   337	    {
   338	        Directory.CreateDirectory(_root);
   339	        File.WriteAllText(
   340	            Path.Combine(_root, "current.json"),
   341	            "{\"activeVersion\":\"1.0.0\"}");
   342	
   343	        var exeDir = Path.Combine(_root, "versions", "1.0.0", "ResponseCenter");
   344	        Directory.CreateDirectory(exeDir);
   345	        File.WriteAllText(Path.Combine(exeDir, "TiaAgent.ResponseCenter.exe"), "fake");
   346	    }
   347	
   348	    private Process StartLongRunningProcess()
   349	    {
   350	        var process = Process.Start(new ProcessStartInfo
   351	        {
   352	            FileName = "cmd.exe",
   353	            Arguments = "/c ping 127.0.0.1 -n 60",
```

</details>

## Alert #606 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/606
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:343-343`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
   335	
   336	    private void SetupFakeExe()
   337	    {
   338	        Directory.CreateDirectory(_root);
   339	        File.WriteAllText(
   340	            Path.Combine(_root, "current.json"),
   341	            "{\"activeVersion\":\"1.0.0\"}");
   342	
   343	        var exeDir = Path.Combine(_root, "versions", "1.0.0", "ResponseCenter");
   344	        Directory.CreateDirectory(exeDir);
   345	        File.WriteAllText(Path.Combine(exeDir, "TiaAgent.ResponseCenter.exe"), "fake");
   346	    }
   347	
   348	    private Process StartLongRunningProcess()
   349	    {
   350	        var process = Process.Start(new ProcessStartInfo
   351	        {
```

</details>

## Alert #605 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/605
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:340-340`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
   332	            activationClient ?? new SequenceActivationClient(false),
   333	            _root);
   334	    }
   335	
   336	    private void SetupFakeExe()
   337	    {
   338	        Directory.CreateDirectory(_root);
   339	        File.WriteAllText(
   340	            Path.Combine(_root, "current.json"),
   341	            "{\"activeVersion\":\"1.0.0\"}");
   342	
   343	        var exeDir = Path.Combine(_root, "versions", "1.0.0", "ResponseCenter");
   344	        Directory.CreateDirectory(exeDir);
   345	        File.WriteAllText(Path.Combine(exeDir, "TiaAgent.ResponseCenter.exe"), "fake");
   346	    }
   347	
   348	    private Process StartLongRunningProcess()
```

</details>

## Alert #604 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/604
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:43-44`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
    35	    {
    36	        Directory.CreateDirectory(_root);
    37	        File.WriteAllText(
    38	            Path.Combine(_root, "current.json"),
    39	            "{\"schemaVersion\":1,\"activeVersion\":\"0.5.0\"}");
    40	
    41	        var path = ResponseCenterProcessManager.ResolveExecutablePath(_root);
    42	
    43	        path.Should().Be(Path.Combine(
    44	            _root, "versions", "0.5.0", "ResponseCenter", "TiaAgent.ResponseCenter.exe"));
    45	    }
    46	
    47	    [Fact]
    48	    public void ParseActiveVersion_ExtractsVersion()
    49	    {
    50	        var version = ResponseCenterProcessManager.ParseActiveVersion(
    51	            "{\"activeVersion\":\"1.2.3-alpha.1\"}");
    52	
```

</details>

## Alert #603 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/603
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:38-38`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
    30	        path.Should().BeNull();
    31	    }
    32	
    33	    [Fact]
    34	    public void ResolveExecutablePath_UsesActiveVersion()
    35	    {
    36	        Directory.CreateDirectory(_root);
    37	        File.WriteAllText(
    38	            Path.Combine(_root, "current.json"),
    39	            "{\"schemaVersion\":1,\"activeVersion\":\"0.5.0\"}");
    40	
    41	        var path = ResponseCenterProcessManager.ResolveExecutablePath(_root);
    42	
    43	        path.Should().Be(Path.Combine(
    44	            _root, "versions", "0.5.0", "ResponseCenter", "TiaAgent.ResponseCenter.exe"));
    45	    }
    46	
```

</details>

## Alert #602 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/602
- Location: `tests/TiaAgent.Bridge.Tests/ResponseCenterProcessManagerTests.cs:18-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 513 lines

<details><summary>Current code context</summary>

```text
    10	using TiaAgent.Bridge.ResponseCenter;
    11	using TiaAgent.Contracts.Bridge;
    12	using Xunit;
    13	
    14	namespace TiaAgent.Bridge.Tests;
    15	
    16	public sealed class ResponseCenterProcessManagerTests : IDisposable
    17	{
    18	    private readonly string _root = Path.Combine(
    19	        Path.GetTempPath(),
    20	        "tia-agent-rc-process-tests",
    21	        Guid.NewGuid().ToString("N"));
    22	
    23	    private readonly Diagnostics.BridgeLogger _logger = new();
    24	    private readonly List<Process> _startedProcesses = new();
    25	
    26	    [Fact]
    27	    public void ResolveExecutablePath_ReturnsNull_WhenManifestMissing()
    28	    {
    29	        var path = ResponseCenterProcessManager.ResolveExecutablePath(_root);
```

</details>

## Alert #601 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/601
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:197-197`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
   189	
   190	        // Encode input as base64 to avoid PowerShell string escaping issues
   191	        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
   192	        var script = "$bytes = [Convert]::FromBase64String('" + b64 + "')\n" +
   193	                     "$stdout = [Console]::OpenStandardOutput()\n" +
   194	                     "$stdout.Write($bytes, 0, $bytes.Length)\n" +
   195	                     "$stdout.Flush()";
   196	
   197	        var tempFile = Path.Combine(Path.GetTempPath(), $"tia-raw-{Guid.NewGuid():N}.ps1");
   198	        try
   199	        {
   200	            File.WriteAllText(tempFile, script, new UTF8Encoding(false));
   201	
   202	            var result = await runner.RunAsync(
   203	                "pwsh",
   204	                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
   205	                null, TimeSpan.FromSeconds(15),
```

</details>

## Alert #600 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/600
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:59-59`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
    51	    private async Task<string> WriteRawUtf8(string text)
    52	    {
    53	        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    54	        var script = "$bytes = [Convert]::FromBase64String('" + b64 + "')\n" +
    55	                     "$stdout = [Console]::OpenStandardOutput()\n" +
    56	                     "$stdout.Write($bytes, 0, $bytes.Length)\n" +
    57	                     "$stdout.Flush()";
    58	
    59	        var tempFile = Path.Combine(Path.GetTempPath(), $"tia-raw-{Guid.NewGuid():N}.ps1");
    60	        try
    61	        {
    62	            File.WriteAllText(tempFile, script, new UTF8Encoding(false));
    63	
    64	            using var runner = new ProcessRunner(_logger);
    65	            var result = await runner.RunAsync(
    66	                "pwsh",
    67	                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
```

</details>

## Alert #599 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/599
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:102-102`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    94	
    95	        artifactNames.Should().OnlyHaveUniqueItems();
    96	    }
    97	
    98	    [Fact]
    99	    public void PackAddIn_is_atomic_and_never_deploys_to_AppData()
   100	    {
   101	        var root = FindRepositoryRoot();
   102	        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));
   103	        var packStart = targets.IndexOf("<Target Name=\"PackAddIn\">", StringComparison.Ordinal);
   104	        var packEnd = targets.IndexOf("</Target>", packStart, StringComparison.Ordinal);
   105	        var packTarget = targets.Substring(packStart, packEnd - packStart);
   106	
   107	        packTarget.Should().NotContain("AddInDeployDir");
   108	        packTarget.Should().NotContain("APPDATA");
   109	        packTarget.Should().Contain("AddInTempPackagePath");
   110	        packTarget.Should().Contain("Move-Item");
```

</details>

## Alert #598 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/598
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:72-72`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    64	            File.ReadAllText(sourceFile).Should().NotContain("<FileVersion>0.0.0.0</FileVersion>");
    65	        }
    66	    }
    67	
    68	    [Fact]
    69	    public void Siemens_manifest_version_is_numeric_while_artifact_version_preserves_prerelease()
    70	    {
    71	        var root = FindRepositoryRoot();
    72	        var config = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    73	        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));
    74	
    75	        config.Should().Contain("<Version>__ADDIN_MANIFEST_VERSION__</Version>");
    76	        ProductVersionLiteral.IsMatch(config).Should().BeFalse();
    77	
    78	        targets.Should().Contain("<AddInManifestVersion>");
    79	        targets.Should().Contain("<ArtifactVersion>$(Version)</ArtifactVersion>");
    80	        targets.Should().Contain("Replace('__ADDIN_MANIFEST_VERSION__', '$(AddInManifestVersion)')");
```

</details>

## Alert #597 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/597
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:41-41`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    33	        codeowners.Should().Contain("/.github/");
    34	        issueConfig.Should().Contain("Security Vulnerability Report");
    35	    }
    36	
    37	    [Fact]
    38	    public void AddIn_manifest_maintains_least_privilege()
    39	    {
    40	        var root = FindRepositoryRoot();
    41	        var document = XDocument.Load(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    42	
    43	        document.Descendants().Where(element => element.Name.LocalName == "UnrestrictedAccess").Should().BeEmpty();
    44	
    45	        var tiaPermissions = document.Descendants()
    46	            .FirstOrDefault(element => element.Name.LocalName == "TIAPermissions")
    47	            ?.Elements().Select(element => element.Name.LocalName).ToList() ?? new List<string>();
    48	        tiaPermissions.Should().NotBeEmpty();
    49	
```

</details>

## Alert #596 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/596
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:30-30`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    22	        content.Should().Contain("security@industrix.com.br");
    23	    }
    24	
    25	    [Fact]
    26	    public void Repository_security_ownership_and_private_reporting_are_configured()
    27	    {
    28	        var root = FindRepositoryRoot();
    29	        var codeowners = File.ReadAllText(Path.Combine(root, ".github", "CODEOWNERS"));
    30	        var issueConfig = File.ReadAllText(Path.Combine(root, ".github", "ISSUE_TEMPLATE", "config.yml"));
    31	
    32	        codeowners.Should().Contain("/SECURITY.md");
    33	        codeowners.Should().Contain("/.github/");
    34	        issueConfig.Should().Contain("Security Vulnerability Report");
    35	    }
    36	
    37	    [Fact]
    38	    public void AddIn_manifest_maintains_least_privilege()
```

</details>

## Alert #595 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/595
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:29-29`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    21	        content.Should().Contain("Security > Advisories > New draft security advisory");
    22	        content.Should().Contain("security@industrix.com.br");
    23	    }
    24	
    25	    [Fact]
    26	    public void Repository_security_ownership_and_private_reporting_are_configured()
    27	    {
    28	        var root = FindRepositoryRoot();
    29	        var codeowners = File.ReadAllText(Path.Combine(root, ".github", "CODEOWNERS"));
    30	        var issueConfig = File.ReadAllText(Path.Combine(root, ".github", "ISSUE_TEMPLATE", "config.yml"));
    31	
    32	        codeowners.Should().Contain("/SECURITY.md");
    33	        codeowners.Should().Contain("/.github/");
    34	        issueConfig.Should().Contain("Security Vulnerability Report");
    35	    }
    36	
    37	    [Fact]
```

</details>

## Alert #594 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/594
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:14-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
     6	
     7	public sealed class RepositoryHealthAndSecurityTests
     8	{
     9	    [Fact]
    10	    public void Security_policy_and_authoritative_model_exist()
    11	    {
    12	        var root = FindRepositoryRoot();
    13	        var securityMdPath = Path.Combine(root, "SECURITY.md");
    14	        var securityModelPath = Path.Combine(root, "docs", "spec", "SECURITY_MODEL.md");
    15	
    16	        File.Exists(securityMdPath).Should().BeTrue();
    17	        File.Exists(securityModelPath).Should().BeTrue();
    18	
    19	        var content = File.ReadAllText(securityMdPath);
    20	        content.Should().Contain("docs/spec/SECURITY_MODEL.md");
    21	        content.Should().Contain("Security > Advisories > New draft security advisory");
    22	        content.Should().Contain("security@industrix.com.br");
```

</details>

## Alert #593 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/593
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:13-13`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
     5	namespace TiaAgent.ArchitectureTests;
     6	
     7	public sealed class RepositoryHealthAndSecurityTests
     8	{
     9	    [Fact]
    10	    public void Security_policy_and_authoritative_model_exist()
    11	    {
    12	        var root = FindRepositoryRoot();
    13	        var securityMdPath = Path.Combine(root, "SECURITY.md");
    14	        var securityModelPath = Path.Combine(root, "docs", "spec", "SECURITY_MODEL.md");
    15	
    16	        File.Exists(securityMdPath).Should().BeTrue();
    17	        File.Exists(securityModelPath).Should().BeTrue();
    18	
    19	        var content = File.ReadAllText(securityMdPath);
    20	        content.Should().Contain("docs/spec/SECURITY_MODEL.md");
    21	        content.Should().Contain("Security > Advisories > New draft security advisory");
```

</details>

## Alert #592 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/592
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:49-49`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
    47	    {
    48	        var root = FindRepositoryRoot();
    49	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
    50	
    51	        buildScriptContent.Should().Contain("Test-NuGetPayload");
    52	        buildScriptContent.Should().Contain("Test-NuGetInstall");
    53	        buildScriptContent.Should().Contain("dotnet tool install TiaAgent.Cli");
    54	        buildScriptContent.Should().Contain("TiaAgent.Cli.$ProductVersion.nupkg");
    55	    }
    56	
    57	    private static string FindRepositoryRoot()
```

</details>

## Alert #591 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/591
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:42-42`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
    47	    {
    48	        var root = FindRepositoryRoot();
    49	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
    50	
```

</details>

## Alert #590 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/590
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:41-41`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
    47	    {
    48	        var root = FindRepositoryRoot();
    49	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
```

</details>

## Alert #589 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/589
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:40-40`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
    47	    {
    48	        var root = FindRepositoryRoot();
```

</details>

## Alert #588 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/588
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:39-39`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    31	        var root = FindRepositoryRoot();
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
    47	    {
```

</details>

## Alert #587 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/587
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:38-38`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    30	    {
    31	        var root = FindRepositoryRoot();
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
    42	        File.Exists(Path.Combine(addInUiPath, "SimpleMarkdownFlowDocumentRenderer.cs")).Should().BeFalse();
    43	    }
    44	
    45	    [Fact]
    46	    public void Pack_verifies_payload_contents_and_tool_installation()
```

</details>

## Alert #586 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/586
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:33-33`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    25	        buildScriptContent.Should().Contain("Siemens.*.dll");
    26	    }
    27	
    28	    [Fact]
    29	    public void Response_center_is_the_single_task_result_ui()
    30	    {
    31	        var root = FindRepositoryRoot();
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
    41	        File.Exists(Path.Combine(addInUiPath, "WpfThreadHost.cs")).Should().BeFalse();
```

</details>

## Alert #585 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/585
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:32-32`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    24	        buildScriptContent.Should().Contain("THIRD_PARTY_NOTICES.md");
    25	        buildScriptContent.Should().Contain("Siemens.*.dll");
    26	    }
    27	
    28	    [Fact]
    29	    public void Response_center_is_the_single_task_result_ui()
    30	    {
    31	        var root = FindRepositoryRoot();
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
    35	        solutionContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    36	        solutionContent.Should().Contain("tests\\TiaAgent.ResponseCenter.Tests\\TiaAgent.ResponseCenter.Tests.csproj");
    37	
    38	        File.Exists(Path.Combine(addInUiPath, "ResponseCenterLauncher.cs")).Should().BeTrue();
    39	        File.Exists(Path.Combine(addInUiPath, "AssistantExecutionWindow.cs")).Should().BeFalse();
    40	        File.Exists(Path.Combine(addInUiPath, "AssistantPanelFactory.cs")).Should().BeFalse();
```

</details>

## Alert #584 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/584
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:13-13`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
     5	
     6	public sealed class PayloadBundlingTests
     7	{
     8	    [Fact]
     9	    public void Cli_package_includes_the_complete_installation_payload()
    10	    {
    11	        var root = FindRepositoryRoot();
    12	        var csprojContent = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.Cli", "TiaAgent.Cli.csproj"));
    13	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
    14	
    15	        csprojContent.Should().Contain("tools/net8.0/any/payload/");
    16	        csprojContent.Should().Contain("payload\\**\\*");
    17	        csprojContent.Should().Contain("Pack=\"true\"");
    18	
    19	        buildScriptContent.Should().Contain("payload-manifest.json");
    20	        buildScriptContent.Should().Contain("Bridge\\TiaAgent.Bridge.dll");
    21	        buildScriptContent.Should().Contain("ResponseCenter\\TiaAgent.ResponseCenter.exe");
```

</details>

## Alert #583 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/583
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:12-12`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
     4	namespace TiaAgent.ArchitectureTests;
     5	
     6	public sealed class PayloadBundlingTests
     7	{
     8	    [Fact]
     9	    public void Cli_package_includes_the_complete_installation_payload()
    10	    {
    11	        var root = FindRepositoryRoot();
    12	        var csprojContent = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.Cli", "TiaAgent.Cli.csproj"));
    13	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
    14	
    15	        csprojContent.Should().Contain("tools/net8.0/any/payload/");
    16	        csprojContent.Should().Contain("payload\\**\\*");
    17	        csprojContent.Should().Contain("Pack=\"true\"");
    18	
    19	        buildScriptContent.Should().Contain("payload-manifest.json");
    20	        buildScriptContent.Should().Contain("Bridge\\TiaAgent.Bridge.dll");
```

</details>

## Alert #582 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/582
- Location: `tests/TiaAgent.AddIn.Tests/ResponseCenterLauncherTests.cs:69-69`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    61	        act.Should().Throw<FileNotFoundException>();
    62	    }
    63	
    64	    [Fact]
    65	    public void ResolveExecutablePath_Throws_WhenActiveVersionMissing()
    66	    {
    67	        Directory.CreateDirectory(_root);
    68	        File.WriteAllText(
    69	            Path.Combine(_root, "current.json"),
    70	            "{\"schemaVersion\":1}");
    71	
    72	        Action act = () => ResponseCenterLauncher.ResolveExecutablePath(_root);
    73	        act.Should().Throw<InvalidDataException>();
    74	    }
    75	
    76	    public void Dispose()
    77	    {
```

</details>

## Alert #581 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/581
- Location: `tests/TiaAgent.AddIn.Tests/ResponseCenterLauncherTests.cs:59-59`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    51	    public void ParseActiveVersion_ReturnsNull_WhenMissing()
    52	    {
    53	        ResponseCenterLauncher.ParseActiveVersion("{\"schemaVersion\":1}").Should().BeNull();
    54	    }
    55	
    56	    [Fact]
    57	    public void ResolveExecutablePath_Throws_WhenManifestMissing()
    58	    {
    59	        var nonExistent = Path.Combine(_root, "no-such-dir");
    60	        Action act = () => ResponseCenterLauncher.ResolveExecutablePath(nonExistent);
    61	        act.Should().Throw<FileNotFoundException>();
    62	    }
    63	
    64	    [Fact]
    65	    public void ResolveExecutablePath_Throws_WhenActiveVersionMissing()
    66	    {
    67	        Directory.CreateDirectory(_root);
```

</details>

## Alert #580 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/580
- Location: `tests/TiaAgent.AddIn.Tests/ResponseCenterLauncherTests.cs:26-31`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    18	    {
    19	        Directory.CreateDirectory(_root);
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "current.json"),
    22	            "{\"schemaVersion\":1,\"activeVersion\":\"0.4.0-beta.2\"}");
    23	
    24	        var path = ResponseCenterLauncher.ResolveExecutablePath(_root);
    25	
    26	        path.Should().Be(Path.Combine(
    27	            _root,
    28	            "versions",
    29	            "0.4.0-beta.2",
    30	            "ResponseCenter",
    31	            ResponseCenterLauncher.ExecutableName));
    32	    }
    33	
    34	    [Fact]
    35	    public void ParseActiveVersion_IsCaseInsensitive()
    36	    {
    37	        var version = ResponseCenterLauncher.ParseActiveVersion(
    38	            "{\"ActiveVersion\":\"0.3.2\"}");
    39	
```

</details>

## Alert #579 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/579
- Location: `tests/TiaAgent.AddIn.Tests/ResponseCenterLauncherTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    13	        "tia-agent-response-center-tests",
    14	        Guid.NewGuid().ToString("N"));
    15	
    16	    [Fact]
    17	    public void ResolveExecutablePath_UsesActiveInstalledVersion()
    18	    {
    19	        Directory.CreateDirectory(_root);
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "current.json"),
    22	            "{\"schemaVersion\":1,\"activeVersion\":\"0.4.0-beta.2\"}");
    23	
    24	        var path = ResponseCenterLauncher.ResolveExecutablePath(_root);
    25	
    26	        path.Should().Be(Path.Combine(
    27	            _root,
    28	            "versions",
    29	            "0.4.0-beta.2",
```

</details>

## Alert #578 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/578
- Location: `tests/TiaAgent.AddIn.Tests/ResponseCenterLauncherTests.cs:11-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
     3	using FluentAssertions;
     4	using TiaAgent.AddIn.Ui;
     5	using Xunit;
     6	
     7	namespace TiaAgent.AddIn.Tests;
     8	
     9	public sealed class ResponseCenterLauncherTests : IDisposable
    10	{
    11	    private readonly string _root = Path.Combine(
    12	        Path.GetTempPath(),
    13	        "tia-agent-response-center-tests",
    14	        Guid.NewGuid().ToString("N"));
    15	
    16	    [Fact]
    17	    public void ResolveExecutablePath_UsesActiveInstalledVersion()
    18	    {
    19	        Directory.CreateDirectory(_root);
    20	        File.WriteAllText(
    21	            Path.Combine(_root, "current.json"),
    22	            "{\"schemaVersion\":1,\"activeVersion\":\"0.4.0-beta.2\"}");
```

</details>

## Alert #577 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/577
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeConnectionDiscovery.cs:78-78`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 98 lines

<details><summary>Current code context</summary>

```text
    70	            // Surface connection failure through the normal Response Center error state.
    71	        }
    72	
    73	        return DefaultBridgeUrl;
    74	    }
    75	
    76	    internal static string? DiscoverAuthToken(string installationRoot)
    77	    {
    78	        var tokenPath = Path.Combine(installationRoot, "bridge.token");
    79	        try
    80	        {
    81	            if (!File.Exists(tokenPath))
    82	            {
    83	                return null;
    84	            }
    85	
    86	            var token = File.ReadAllText(tokenPath).Trim();
```

</details>

## Alert #576 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/576
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeConnectionDiscovery.cs:47-47`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 98 lines

<details><summary>Current code context</summary>

```text
    39	            ? DiscoverAuthToken(root)
    40	            : explicitAuthToken;
    41	
    42	        return new BridgeConnectionSettings(bridgeUrl, token);
    43	    }
    44	
    45	    internal static string DiscoverBridgeUrl(string installationRoot)
    46	    {
    47	        var runtimeManifestPath = Path.Combine(installationRoot, "runtime", "runtime.json");
    48	        try
    49	        {
    50	            if (!File.Exists(runtimeManifestPath))
    51	            {
    52	                return DefaultBridgeUrl;
    53	            }
    54	
    55	            var manifest = File.ReadAllText(runtimeManifestPath);
```

</details>

## Alert #575 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/575
- Location: `src/TiaAgent.ResponseCenter/Services/BridgeConnectionDiscovery.cs:29-31`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 98 lines

<details><summary>Current code context</summary>

```text
    21	    public static BridgeConnectionSettings Resolve(
    22	        string? explicitBridgeUrl,
    23	        string? explicitAuthToken,
    24	        string? installationRoot = null)
    25	    {
    26	        var root = installationRoot;
    27	        if (string.IsNullOrWhiteSpace(root))
    28	        {
    29	            root = Path.Combine(
    30	                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    31	                "TiaAgent");
    32	        }
    33	
    34	        var bridgeUrl = string.IsNullOrWhiteSpace(explicitBridgeUrl)
    35	            ? DiscoverBridgeUrl(root)
    36	            : explicitBridgeUrl.TrimEnd('/');
    37	
    38	        var token = string.IsNullOrWhiteSpace(explicitAuthToken)
    39	            ? DiscoverAuthToken(root)
```

</details>

## Alert #574 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/574
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:105-105`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
    97	
    98	            lock (Lock)
    99	            {
   100	                try
   101	                {
   102	                    if (!Directory.Exists(dir))
   103	                        Directory.CreateDirectory(dir);
   104	
   105	                    var logFile = Path.Combine(dir, $"response-center-{DateTime.Now:yyyyMMdd}.log");
   106	                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
   107	                    var threadId = Environment.CurrentManagedThreadId;
   108	                    var entry = $"[{timestamp}] [{level}] [T{threadId}] {message}";
   109	                    File.AppendAllText(logFile, entry + Environment.NewLine);
   110	                }
   111	                catch
   112	                {
   113	                    _fileLoggingDisabled = true;
```

</details>

## Alert #573 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/573
- Location: `src/TiaAgent.ResponseCenter/Diagnostics/ResponseCenterLogger.cs:35-35`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 122 lines

<details><summary>Current code context</summary>

```text
    27	            if (_logDirResolved)
    28	                return _logDir;
    29	
    30	            _logDirResolved = true;
    31	            try
    32	            {
    33	                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    34	                if (!string.IsNullOrEmpty(localAppData))
    35	                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
    36	            }
    37	            catch
    38	            {
    39	                // Permission denied — file logging will be disabled
    40	            }
    41	            return _logDir;
    42	        }
    43	    }
```

</details>

## Alert #572 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/572
- Location: `src/TiaAgent.Cli/Installation/TiaPortalDiscovery.cs:63-65`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 140 lines

<details><summary>Current code context</summary>

```text
    55	                TiaPortalDetected = true,
    56	                UserAddInsDirectoryExists = dirExists,
    57	                UserAddInsDirectory = customUserAddInsDir,
    58	                TiaPortalInstallPath = null,
    59	                DetectionSource = "cli-override"
    60	            };
    61	        }
    62	
    63	        var appDataUserAddIns = Path.Combine(
    64	            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    65	            UserAddInsRelativePath);
    66	
    67	        // 2. Check TiaPublicApiDir environment variable
    68	        var envApiDir = Environment.GetEnvironmentVariable("TiaPublicApiDir");
    69	        if (!string.IsNullOrWhiteSpace(envApiDir) && Directory.Exists(envApiDir))
    70	        {
    71	            var tiaRoot = DeriveTiaRootFromApiDir(envApiDir);
    72	            var dirExists = Directory.Exists(appDataUserAddIns);
    73	
```

</details>

## Alert #571 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/571
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:248-248`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
   240	    /// Preserves the Add-In file locally for manual installation.
   241	    /// Returns the fallback directory path.
   242	    /// </summary>
   243	    public static string PreserveLocally(string addinSourcePath, string fallbackBaseDir, TextWriter stdout)
   244	    {
   245	        var fallbackDir = Path.Combine(fallbackBaseDir, "AddIn");
   246	        Directory.CreateDirectory(fallbackDir);
   247	
   248	        var destFile = Path.Combine(fallbackDir, Path.GetFileName(addinSourcePath));
   249	        File.Copy(addinSourcePath, destFile, overwrite: true);
   250	
   251	        return fallbackDir;
   252	    }
   253	
   254	    /// <summary>
   255	    /// Extracts the version string from an Add-In filename.
   256	    /// E.g., "TiaAgent-0.2.0.addin" → "0.2.0"
```

</details>

## Alert #570 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/570
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:245-245`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
   237	    }
   238	
   239	    /// <summary>
   240	    /// Preserves the Add-In file locally for manual installation.
   241	    /// Returns the fallback directory path.
   242	    /// </summary>
   243	    public static string PreserveLocally(string addinSourcePath, string fallbackBaseDir, TextWriter stdout)
   244	    {
   245	        var fallbackDir = Path.Combine(fallbackBaseDir, "AddIn");
   246	        Directory.CreateDirectory(fallbackDir);
   247	
   248	        var destFile = Path.Combine(fallbackDir, Path.GetFileName(addinSourcePath));
   249	        File.Copy(addinSourcePath, destFile, overwrite: true);
   250	
   251	        return fallbackDir;
   252	    }
   253	
```

</details>

## Alert #569 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/569
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:185-185`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
   177	        };
   178	    }
   179	
   180	    /// <summary>
   181	    /// Finds all .addin files in the version's AddIn subdirectory.
   182	    /// </summary>
   183	    public static IReadOnlyList<string> FindAddInFiles(string versionDir)
   184	    {
   185	        var addinSubDir = Path.Combine(versionDir, "AddIn");
   186	        if (!Directory.Exists(addinSubDir))
   187	        {
   188	            return Array.Empty<string>();
   189	        }
   190	
   191	        return Directory.GetFiles(addinSubDir, "*.addin");
   192	    }
   193	
```

</details>

## Alert #568 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/568
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:123-123`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
   115	                InstalledAddInVersion = ExtractVersion(addInFiles[0])
   116	            };
   117	        }
   118	
   119	        // Clean up stale Add-In versions before deploying
   120	        var removedStale = RemoveStaleAddIns(discovery.UserAddInsDirectory, Path.GetFileName(addInFiles[0]), stdout);
   121	
   122	        // Copy the new Add-In
   123	        var destFile = Path.Combine(discovery.UserAddInsDirectory, Path.GetFileName(addInFiles[0]));
   124	        try
   125	        {
   126	            File.Copy(addInFiles[0], destFile, overwrite: true);
   127	        }
   128	        catch (IOException ex)
   129	        {
   130	            stdout.WriteLine();
   131	            stdout.WriteLine($"Failed to deploy Add-In to '{destFile}': {ex.Message}");
```

</details>

## Alert #567 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/567
- Location: `src/TiaAgent.Cli/Installation/AddInDeployer.cs:52-52`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 267 lines

<details><summary>Current code context</summary>

```text
    44	        }
    45	
    46	        // Preserve locally as fallback (always, regardless of TIA Portal detection)
    47	        string? fallbackDir = null;
    48	        string? fallbackPath = null;
    49	        try
    50	        {
    51	            fallbackDir = PreserveLocally(addInFiles[0], fallbackBaseDir, stdout);
    52	            fallbackPath = Path.Combine(fallbackDir, Path.GetFileName(addInFiles[0]));
    53	        }
    54	        catch (Exception ex)
    55	        {
    56	            stdout.WriteLine($"Warning: Could not preserve Add-In locally: {ex.Message}");
    57	        }
    58	
    59	        // Discover TIA Portal V21
    60	        var discovery = TiaPortalDiscovery.Discover(customUserAddInsDir);
```

</details>

## Alert #566 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/566
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeHelpers.cs:38-38`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 43 lines

<details><summary>Current code context</summary>

```text
    30	    /// </summary>
    31	    internal static string? FindOnPath(string fileName)
    32	    {
    33	        var pathVar = Environment.GetEnvironmentVariable("PATH");
    34	        if (string.IsNullOrEmpty(pathVar)) return null;
    35	
    36	        foreach (var dir in pathVar.Split(Path.PathSeparator))
    37	        {
    38	            var full = Path.Combine(dir.Trim(), fileName);
    39	            if (File.Exists(full)) return full;
    40	        }
    41	        return null;
    42	    }
    43	}
```

</details>

## Alert #565 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/565
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:435-435`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   427	        if (!File.Exists(currentManifestPath))
   428	            return null;
   429	
   430	        var currentManifest = File.ReadAllText(currentManifestPath);
   431	        var activeVersion = ParseActiveVersion(currentManifest);
   432	        if (string.IsNullOrWhiteSpace(activeVersion))
   433	            return null;
   434	
   435	        return Path.Combine(root, "versions", activeVersion, "ResponseCenter", ExecutableName);
   436	    }
   437	
   438	    internal static string? ParseActiveVersion(string json)
   439	    {
   440	        if (string.IsNullOrWhiteSpace(json))
   441	            return null;
   442	
   443	        var match = s_activeVersionRegex.Match(json);
```

</details>

## Alert #564 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/564
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:426-426`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   418	        var root = installationRoot;
   419	        if (string.IsNullOrWhiteSpace(root))
   420	        {
   421	            root = Path.Combine(
   422	                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
   423	                "TiaAgent");
   424	        }
   425	
   426	        var currentManifestPath = Path.Combine(root, "current.json");
   427	        if (!File.Exists(currentManifestPath))
   428	            return null;
   429	
   430	        var currentManifest = File.ReadAllText(currentManifestPath);
   431	        var activeVersion = ParseActiveVersion(currentManifest);
   432	        if (string.IsNullOrWhiteSpace(activeVersion))
   433	            return null;
   434	
```

</details>

## Alert #563 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/563
- Location: `src/TiaAgent.Bridge/ResponseCenter/ResponseCenterProcessManager.cs:421-423`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 549 lines

<details><summary>Current code context</summary>

```text
   413	        };
   414	    }
   415	
   416	    internal static string? ResolveExecutablePath(string? installationRoot = null)
   417	    {
   418	        var root = installationRoot;
   419	        if (string.IsNullOrWhiteSpace(root))
   420	        {
   421	            root = Path.Combine(
   422	                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
   423	                "TiaAgent");
   424	        }
   425	
   426	        var currentManifestPath = Path.Combine(root, "current.json");
   427	        if (!File.Exists(currentManifestPath))
   428	            return null;
   429	
   430	        var currentManifest = File.ReadAllText(currentManifestPath);
   431	        var activeVersion = ParseActiveVersion(currentManifest);
```

</details>

## Alert #562 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/562
- Location: `src/TiaAgent.AddIn/Ui/ResponseCenterLauncher.cs:147-152`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 165 lines

<details><summary>Current code context</summary>

```text
   139	
   140	        var currentManifest = File.ReadAllText(currentManifestPath);
   141	        var activeVersion = ParseActiveVersion(currentManifest);
   142	        if (string.IsNullOrWhiteSpace(activeVersion))
   143	        {
   144	            throw new InvalidDataException($"Active version is missing from '{currentManifestPath}'.");
   145	        }
   146	
   147	        return Path.Combine(
   148	            root,
   149	            "versions",
   150	            activeVersion,
   151	            "ResponseCenter",
   152	            ExecutableName);
   153	    }
   154	
   155	    internal static string? ParseActiveVersion(string json)
   156	    {
   157	        if (string.IsNullOrWhiteSpace(json))
   158	        {
   159	            return null;
   160	        }
```

</details>

## Alert #561 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/561
- Location: `src/TiaAgent.AddIn/Ui/ResponseCenterLauncher.cs:132-132`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 165 lines

<details><summary>Current code context</summary>

```text
   124	        var root = installationRoot;
   125	        if (string.IsNullOrWhiteSpace(root))
   126	        {
   127	            root = Path.Combine(
   128	                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
   129	                "TiaAgent");
   130	        }
   131	
   132	        var currentManifestPath = Path.Combine(root, "current.json");
   133	        if (!File.Exists(currentManifestPath))
   134	        {
   135	            throw new FileNotFoundException(
   136	                "TIA Agent active-version manifest was not found. Run 'tia-agent install' or 'tia-agent update'.",
   137	                currentManifestPath);
   138	        }
   139	
   140	        var currentManifest = File.ReadAllText(currentManifestPath);
```

</details>

## Alert #560 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/560
- Location: `src/TiaAgent.AddIn/Ui/ResponseCenterLauncher.cs:127-129`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 165 lines

<details><summary>Current code context</summary>

```text
   119	        }
   120	    }
   121	
   122	    internal static string ResolveExecutablePath(string? installationRoot = null)
   123	    {
   124	        var root = installationRoot;
   125	        if (string.IsNullOrWhiteSpace(root))
   126	        {
   127	            root = Path.Combine(
   128	                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
   129	                "TiaAgent");
   130	        }
   131	
   132	        var currentManifestPath = Path.Combine(root, "current.json");
   133	        if (!File.Exists(currentManifestPath))
   134	        {
   135	            throw new FileNotFoundException(
   136	                "TIA Agent active-version manifest was not found. Run 'tia-agent install' or 'tia-agent update'.",
   137	                currentManifestPath);
```

</details>

## Alert #559 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/559
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:296-297`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   288	
   289	            lock (Lock)
   290	            {
   291	                try
   292	                {
   293	                    if (!Directory.Exists(dir))
   294	                        Directory.CreateDirectory(dir);
   295	
   296	                    var logFile = Path.Combine(dir,
   297	                        $"addin-{DateTime.Now:yyyyMMdd}.log");
   298	
   299	                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
   300	                    var threadId = Environment.CurrentManagedThreadId;
   301	                    var entry = $"[{timestamp}] [{level}] [T{threadId}] {message}";
   302	
   303	                    File.AppendAllText(logFile, entry + Environment.NewLine);
   304	                }
   305	                catch
```

</details>

## Alert #558 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/558
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:248-248`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
   240	                if (loaded != null)
   241	                {
   242	                    Info($"Assembly '{name}' loaded: {loaded.FullName} @ {loaded.Location}");
   243	                }
   244	                else
   245	                {
   246	                    // Check if the DLL exists on disk in the base directory
   247	                    var dllPath = !string.IsNullOrEmpty(baseDir)
   248	                        ? Path.Combine(baseDir, name + ".dll")
   249	                        : null;
   250	
   251	                    if (dllPath != null && File.Exists(dllPath))
   252	                    {
   253	                        Warn($"Assembly '{name}' NOT loaded but DLL exists at {dllPath}");
   254	                    }
   255	                    else
   256	                    {
```

</details>

## Alert #557 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/557
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:53-53`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
    45	            _logDirResolved = true;
    46	
    47	            try
    48	            {
    49	                var localAppData = Environment.GetFolderPath(
    50	                    Environment.SpecialFolder.LocalApplicationData);
    51	                if (!string.IsNullOrEmpty(localAppData))
    52	                {
    53	                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
    54	                }
    55	            }
    56	            catch
    57	            {
    58	                // EnvironmentPermission not granted — file logging will be disabled
    59	            }
    60	
    61	            return _logDir;
```

</details>

## Alert #556 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/556
- Location: `src/TiaAgent.AddIn/Bridge/AddInConfig.cs:24-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
    16	{
    17	    private static readonly string RuntimeDir = Path.Combine(
    18	        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    19	        "TiaAgent", "runtime");
    20	
    21	    private static readonly string RuntimeManifestPath = Path.Combine(RuntimeDir, "runtime.json");
    22	
    23	    // Token file is one directory up from the runtime dir, at %LOCALAPPDATA%\TiaAgent\bridge.token
    24	    private static readonly string TokenFilePath = Path.Combine(
    25	        Path.GetDirectoryName(RuntimeDir)!, "bridge.token");
    26	
    27	    private const string DefaultBridgeBaseUrl = "http://127.0.0.1:43119";
    28	
    29	    public string BridgeBaseUrl => DiscoverBridgeBaseUrl();
    30	    public int RequestTimeoutSeconds { get; set; } = 15;
    31	    public int PollingIntervalMilliseconds { get; set; } = 500;
    32	    public int TaskTimeoutSeconds { get; set; } = 300;
    33	    public string? AuthToken => DiscoverAuthToken();
```

</details>

## Alert #555 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/555
- Location: `src/TiaAgent.AddIn/Bridge/AddInConfig.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
    13	/// a TIA Portal restart.
    14	/// </summary>
    15	public sealed class AddInConfig
    16	{
    17	    private static readonly string RuntimeDir = Path.Combine(
    18	        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    19	        "TiaAgent", "runtime");
    20	
    21	    private static readonly string RuntimeManifestPath = Path.Combine(RuntimeDir, "runtime.json");
    22	
    23	    // Token file is one directory up from the runtime dir, at %LOCALAPPDATA%\TiaAgent\bridge.token
    24	    private static readonly string TokenFilePath = Path.Combine(
    25	        Path.GetDirectoryName(RuntimeDir)!, "bridge.token");
    26	
    27	    private const string DefaultBridgeBaseUrl = "http://127.0.0.1:43119";
    28	
    29	    public string BridgeBaseUrl => DiscoverBridgeBaseUrl();
```

</details>

## Alert #554 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/554
- Location: `src/TiaAgent.AddIn/Bridge/AddInConfig.cs:17-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 101 lines

<details><summary>Current code context</summary>

```text
     9	/// and token file written by the supervisor.
    10	///
    11	/// The Bridge URL is re-read from the manifest on each access so that
    12	/// a Bridge restart on a different port is picked up without requiring
    13	/// a TIA Portal restart.
    14	/// </summary>
    15	public sealed class AddInConfig
    16	{
    17	    private static readonly string RuntimeDir = Path.Combine(
    18	        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    19	        "TiaAgent", "runtime");
    20	
    21	    private static readonly string RuntimeManifestPath = Path.Combine(RuntimeDir, "runtime.json");
    22	
    23	    // Token file is one directory up from the runtime dir, at %LOCALAPPDATA%\TiaAgent\bridge.token
    24	    private static readonly string TokenFilePath = Path.Combine(
    25	        Path.GetDirectoryName(RuntimeDir)!, "bridge.token");
    26	
    27	    private const string DefaultBridgeBaseUrl = "http://127.0.0.1:43119";
```

</details>

## Alert #553 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/553
- Location: `tests/TiaAgent.Cli.Tests/Installation/TiaPortalDiscoveryTests.cs:35-35`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 123 lines

<details><summary>Current code context</summary>

```text
    27	        }
    28	        else
    29	        {
    30	            Environment.SetEnvironmentVariable("TiaPublicApiDir", _originalTiaPublicApiDir);
    31	        }
    32	
    33	        if (Directory.Exists(_tempDirectory))
    34	        {
    35	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    36	        }
    37	        GC.SuppressFinalize(this);
    38	    }
    39	
    40	    [Fact]
    41	    public void Discover_WithCustomDir_ReturnsCustomDir()
    42	    {
    43	        var customDir = Path.Combine(_tempDirectory, "CustomAddIns");
```

</details>

## Alert #552 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/552
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:34-34`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
    26	        Directory.CreateDirectory(_userAddInsDir);
    27	        Directory.CreateDirectory(_fallbackBaseDir);
    28	    }
    29	
    30	    public void Dispose()
    31	    {
    32	        if (Directory.Exists(_tempDirectory))
    33	        {
    34	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    35	        }
    36	        GC.SuppressFinalize(this);
    37	    }
    38	
    39	    [Fact]
    40	    public void Deploy_AddInFound_DeploysToUserAddIns()
    41	    {
    42	        var addinDir = Path.Combine(_versionDir, "AddIn");
```

</details>

## Alert #551 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/551
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:446-446`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   438	
   439	            result.ExitCode.Should().Be(0);
   440	            result.StdOut.Should().Contain(testString,
   441	                "cmd.exe must preserve UTF-8 output without corruption");
   442	        }
   443	        finally
   444	        {
   445	            try { File.Delete(cmdFile); } catch { }
   446	            try { Directory.Delete(tempDir, true); } catch { }
   447	        }
   448	    }
   449	
   450	    #endregion
   451	
   452	    #region FakeRuntime (for integration testing)
   453	
   454	    [Fact]
```

</details>

## Alert #550 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/550
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeAdapterTests.cs:445-445`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 947 lines

<details><summary>Current code context</summary>

```text
   437	                TimeSpan.FromSeconds(10));
   438	
   439	            result.ExitCode.Should().Be(0);
   440	            result.StdOut.Should().Contain(testString,
   441	                "cmd.exe must preserve UTF-8 output without corruption");
   442	        }
   443	        finally
   444	        {
   445	            try { File.Delete(cmdFile); } catch { }
   446	            try { Directory.Delete(tempDir, true); } catch { }
   447	        }
   448	    }
   449	
   450	    #endregion
   451	
   452	    #region FakeRuntime (for integration testing)
   453	
```

</details>

## Alert #549 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/549
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:214-214`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
   206	                progress: progress,
   207	                cancellationToken: CancellationToken.None);
   208	
   209	            result.ExitCode.Should().Be(0);
   210	            result.StdOut.Should().Be(input, "returned output must not be mutated by progress reporting");
   211	        }
   212	        finally
   213	        {
   214	            try { File.Delete(tempFile); } catch { }
   215	        }
   216	    }
   217	
   218	    [Fact]
   219	    public async Task LargePayload_IsPreserved()
   220	    {
   221	        var sb = new StringBuilder();
   222	        sb.AppendLine("# Large Response");
```

</details>

## Alert #548 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/548
- Location: `tests/TiaAgent.Bridge.Tests/ProcessRunnerOutputPreservationTests.cs:75-75`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 244 lines

<details><summary>Current code context</summary>

```text
    67	                $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFile}\"",
    68	                null, TimeSpan.FromSeconds(15),
    69	                cancellationToken: CancellationToken.None);
    70	            result.ExitCode.Should().Be(0, because: $"stderr: {result.StdErr}");
    71	            return result.StdOut;
    72	        }
    73	        finally
    74	        {
    75	            try { File.Delete(tempFile); } catch { }
    76	        }
    77	    }
    78	
    79	    [Fact]
    80	    public async Task NoFinalNewline_RemainsNoFinalNewline()
    81	    {
    82	        var input = "line1\nline2\nline3";
    83	        var output = await WriteRawUtf8(input);
```

</details>

## Alert #547 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/547
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:82-82`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    74	    }
    75	
    76	    public async Task AbortSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    77	    {
    78	        try
    79	        {
    80	            await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/abort", null, cancellationToken).ConfigureAwait(false);
    81	        }
    82	        catch { }
    83	    }
    84	
    85	    public void Dispose() => _httpClient.Dispose();
    86	
    87	    /// <summary>
    88	    /// Reads HTTP response content as a string using explicit UTF-8 encoding.
    89	    /// Prevents encoding corruption when the server response lacks a charset
    90	    /// in the Content-Type header.
```

</details>

## Alert #546 — cs/call-to-unmanaged-code

- Rule: `cs/call-to-unmanaged-code`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/546
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:157-157`
- Message: Replace this call with a call to managed code if possible.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   149	            var intersectsAnyMonitor = false;
   150	            var monitorCount = 0;
   151	
   152	            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
   153	                (IntPtr hMonitor, IntPtr _, ref RECT _, IntPtr _) =>
   154	                {
   155	                    monitorCount++;
   156	                    var info = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
   157	                    if (GetMonitorInfo(hMonitor, ref info)
   158	                        && RectanglesOverlap(windowRect, info.rcWork))
   159	                    {
   160	                        intersectsAnyMonitor = true;
   161	                    }
   162	                    return true;
   163	                }, IntPtr.Zero);
   164	
   165	            if (!intersectsAnyMonitor && monitorCount > 0)
```

</details>

## Alert #545 — cs/call-to-unmanaged-code

- Rule: `cs/call-to-unmanaged-code`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/545
- Location: `src/TiaAgent.ResponseCenter/Views/AgentResponseWindow.xaml.cs:152-163`
- Message: Replace this call with a call to managed code if possible.

- Current file exists on `main`: **yes**
- Current file length: 274 lines

<details><summary>Current code context</summary>

```text
   144	                Top = windowTop,
   145	                Right = windowRight,
   146	                Bottom = windowBottom
   147	            };
   148	
   149	            var intersectsAnyMonitor = false;
   150	            var monitorCount = 0;
   151	
   152	            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
   153	                (IntPtr hMonitor, IntPtr _, ref RECT _, IntPtr _) =>
   154	                {
   155	                    monitorCount++;
   156	                    var info = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
   157	                    if (GetMonitorInfo(hMonitor, ref info)
   158	                        && RectanglesOverlap(windowRect, info.rcWork))
   159	                    {
   160	                        intersectsAnyMonitor = true;
   161	                    }
   162	                    return true;
   163	                }, IntPtr.Zero);
   164	
   165	            if (!intersectsAnyMonitor && monitorCount > 0)
   166	            {
   167	                ResponseCenterLogger.Warn(
   168	                    $"Window is off-screen (pos={Left:F0},{Top:F0}); resetting to center");
   169	                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
   170	                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
   171	            }
```

</details>

## Alert #544 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/544
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:263-266`
- Message: Disposable 'HttpResponseMessage' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   255	        {
   256	            var json = _responses.Dequeue();
   257	            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
   258	            {
   259	                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
   260	            });
   261	        }
   262	
   263	        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
   264	        {
   265	            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
   266	        });
   267	    }
   268	}
```

</details>

## Alert #543 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/543
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:257-260`
- Message: Disposable 'HttpResponseMessage' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   249	            {
   250	                Content = new StringContent("Service unavailable")
   251	            });
   252	        }
   253	
   254	        if (_responses.Count > 0)
   255	        {
   256	            var json = _responses.Dequeue();
   257	            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
   258	            {
   259	                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
   260	            });
   261	        }
   262	
   263	        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
   264	        {
   265	            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
   266	        });
   267	    }
   268	}
```

</details>

## Alert #542 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/542
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:248-251`
- Message: Disposable 'HttpResponseMessage' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   240	        _responses = responses;
   241	        _alwaysFail = alwaysFail;
   242	    }
   243	
   244	    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
   245	    {
   246	        if (_alwaysFail)
   247	        {
   248	            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
   249	            {
   250	                Content = new StringContent("Service unavailable")
   251	            });
   252	        }
   253	
   254	        if (_responses.Count > 0)
   255	        {
   256	            var json = _responses.Dequeue();
   257	            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
   258	            {
   259	                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
```

</details>

## Alert #541 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/541
- Location: `tests/TiaAgent.Cli.Tests/Installation/AddInDeployerTests.cs:205-205`
- Message: Disposable 'StringWriter' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 228 lines

<details><summary>Current code context</summary>

```text
   197	        var addinDir = Path.Combine(_versionDir, "AddIn");
   198	        Directory.CreateDirectory(addinDir);
   199	        File.WriteAllBytes(Path.Combine(addinDir, "TiaAgent-0.2.0.addin"), Encoding.UTF8.GetBytes("content"));
   200	
   201	        // Mix of TiaAgent and non-TiaAgent files
   202	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin"), Encoding.UTF8.GetBytes("old"));
   203	        File.WriteAllBytes(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin"), Encoding.UTF8.GetBytes("other"));
   204	
   205	        var stdout = new StringWriter();
   206	        var removed = AddInDeployer.RemoveStaleAddIns(_userAddInsDir, "TiaAgent-0.2.0.addin", stdout);
   207	
   208	        removed.Should().Contain("TiaAgent-0.1.0.addin");
   209	        removed.Should().NotContain("ThirdParty-1.0.addin");
   210	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.1.0.addin")).Should().BeFalse();
   211	        File.Exists(Path.Combine(_userAddInsDir, "ThirdParty-1.0.addin")).Should().BeTrue();
   212	    }
   213	
```

</details>

## Alert #540 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/540
- Location: `src/TiaAgent.AddIn/Bridge/AgentBridgeClient.cs:191-191`
- Message: Disposable 'ByteArrayContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 606 lines

<details><summary>Current code context</summary>

```text
   183	
   184	    public async Task<LaunchResponseCenterResponse> LaunchResponseCenterAsync(
   185	        LaunchResponseCenterRequest request, CancellationToken cancellationToken)
   186	    {
   187	        EnsureCurrentClient();
   188	        var json = BuildLaunchRequestJson(request);
   189	
   190	        var jsonBytes = Encoding.UTF8.GetBytes(json);
   191	        var content = new ByteArrayContent(jsonBytes, 0, jsonBytes.Length);
   192	        content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
   193	        {
   194	            CharSet = "utf-8"
   195	        };
   196	
   197	        var response = await _httpClient.PostAsync("/v1/response-center/launch", content, cancellationToken).ConfigureAwait(false);
   198	        var responseJson = await ReadResponseUtf8Async(response).ConfigureAwait(false);
   199	
```

</details>

## Alert #539 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/539
- Location: `src/TiaAgent.AddIn/Bridge/AgentBridgeClient.cs:150-150`
- Message: Disposable 'ByteArrayContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 606 lines

<details><summary>Current code context</summary>

```text
   142	
   143	    public async Task<BridgeTaskAccepted> StartTaskAsync(BridgeTaskRequest request, CancellationToken cancellationToken)
   144	    {
   145	        EnsureCurrentClient();
   146	        var json = BuildTaskRequestJson(request);
   147	
   148	        // Use manually encoded UTF-8 bytes to make the request body unambiguous.
   149	        var jsonBytes = Encoding.UTF8.GetBytes(json);
   150	        var content = new ByteArrayContent(jsonBytes, 0, jsonBytes.Length);
   151	        content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
   152	        {
   153	            CharSet = "utf-8"
   154	        };
   155	
   156	        var response = await _httpClient.PostAsync("/v1/tasks", content, cancellationToken).ConfigureAwait(false);
   157	        var responseJson = await ReadResponseUtf8Async(response).ConfigureAwait(false);
   158	
```

</details>

## Alert #538 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/538
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:226-226`
- Message: Dispose missed if exception is thrown by call to method FromMilliseconds.
Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Dispose.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   218	
   219	        monitor.Start();
   220	        await Task.Delay(200);
   221	        monitor.Stop();
   222	
   223	        initialState.Should().Be(AgentTaskState.Running);
   224	
   225	        monitor.Dispose();
   226	        handler.Dispose();
   227	    }
   228	}
   229	
   230	/// <summary>
   231	/// Mock HTTP handler that returns pre-configured responses.
   232	/// </summary>
   233	internal sealed class MockHttpHandler : HttpMessageHandler
   234	{
```

</details>

## Alert #537 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/537
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:225-225`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   217	        };
   218	
   219	        monitor.Start();
   220	        await Task.Delay(200);
   221	        monitor.Stop();
   222	
   223	        initialState.Should().Be(AgentTaskState.Running);
   224	
   225	        monitor.Dispose();
   226	        handler.Dispose();
   227	    }
   228	}
   229	
   230	/// <summary>
   231	/// Mock HTTP handler that returns pre-configured responses.
   232	/// </summary>
   233	internal sealed class MockHttpHandler : HttpMessageHandler
```

</details>

## Alert #536 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/536
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:194-194`
- Message: Dispose missed if exception is thrown by call to method FromMilliseconds.
Dispose missed if exception is thrown by call to method FromSeconds.
Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Dispose.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   186	
   187	        monitor.Start();
   188	        await Task.Delay(5000);
   189	        monitor.Stop();
   190	
   191	        states.Should().Contain(AgentTaskState.Disconnected);
   192	
   193	        monitor.Dispose();
   194	        handler.Dispose();
   195	    }
   196	
   197	    [Fact]
   198	    public async Task Monitor_HandlesInitialStatus()
   199	    {
   200	        var responses = new Queue<string>();
   201	        // Return an empty JSON object so the monitor doesn't crash
   202	        responses.Enqueue("{}");
```

</details>

## Alert #535 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/535
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:193-193`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   185	        monitor.StateChanged += (state, stage, msg) => states.Add(state);
   186	
   187	        monitor.Start();
   188	        await Task.Delay(5000);
   189	        monitor.Stop();
   190	
   191	        states.Should().Contain(AgentTaskState.Disconnected);
   192	
   193	        monitor.Dispose();
   194	        handler.Dispose();
   195	    }
   196	
   197	    [Fact]
   198	    public async Task Monitor_HandlesInitialStatus()
   199	    {
   200	        var responses = new Queue<string>();
   201	        // Return an empty JSON object so the monitor doesn't crash
```

</details>

## Alert #534 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/534
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:169-169`
- Message: Dispose missed if exception is thrown by call to method FromMilliseconds.
Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Dispose.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   161	
   162	        monitor.Start();
   163	        await Task.Delay(1000);
   164	        monitor.Stop();
   165	
   166	        states.Should().Contain(AgentTaskState.Cancelled);
   167	
   168	        monitor.Dispose();
   169	        handler.Dispose();
   170	    }
   171	
   172	    [Fact]
   173	    public async Task Monitor_TransitionsToDisconnected_AfterConsecutiveErrors()
   174	    {
   175	        var handler = new MockHttpHandler(new Queue<string>(), alwaysFail: true);
   176	        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:9999/") };
   177	
```

</details>

## Alert #533 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/533
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:168-168`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   160	        monitor.StateChanged += (state, stage, msg) => states.Add(state);
   161	
   162	        monitor.Start();
   163	        await Task.Delay(1000);
   164	        monitor.Stop();
   165	
   166	        states.Should().Contain(AgentTaskState.Cancelled);
   167	
   168	        monitor.Dispose();
   169	        handler.Dispose();
   170	    }
   171	
   172	    [Fact]
   173	    public async Task Monitor_TransitionsToDisconnected_AfterConsecutiveErrors()
   174	    {
   175	        var handler = new MockHttpHandler(new Queue<string>(), alwaysFail: true);
   176	        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:9999/") };
```

</details>

## Alert #532 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/532
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:140-140`
- Message: Dispose missed if exception is thrown by call to method FromMilliseconds.
Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should.
Dispose missed if exception is thrown by call to method BeTrue.
Dispose missed if exception is thrown by call to method Should.
Dispose missed if exception is thrown by call to method Dispose.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   132	        await Task.Delay(1000);
   133	        monitor.Stop();
   134	
   135	        states.Should().Contain(AgentTaskState.Failed);
   136	        errorMsg.Should().Be("Runtime crashed");
   137	        retryable.Should().BeTrue();
   138	
   139	        monitor.Dispose();
   140	        handler.Dispose();
   141	    }
   142	
   143	    [Fact]
   144	    public async Task Monitor_TransitionsToCancelled_WhenBridgeReturnsCancelled()
   145	    {
   146	        var responses = new Queue<string>();
   147	        responses.Enqueue(JsonSerializer.Serialize(new BridgeTaskStatus
   148	        {
```

</details>

## Alert #531 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/531
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:139-139`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should.
Dispose missed if exception is thrown by call to method BeTrue.
Dispose missed if exception is thrown by call to method Should.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
   131	        monitor.Start();
   132	        await Task.Delay(1000);
   133	        monitor.Stop();
   134	
   135	        states.Should().Contain(AgentTaskState.Failed);
   136	        errorMsg.Should().Be("Runtime crashed");
   137	        retryable.Should().BeTrue();
   138	
   139	        monitor.Dispose();
   140	        handler.Dispose();
   141	    }
   142	
   143	    [Fact]
   144	    public async Task Monitor_TransitionsToCancelled_WhenBridgeReturnsCancelled()
   145	    {
   146	        var responses = new Queue<string>();
   147	        responses.Enqueue(JsonSerializer.Serialize(new BridgeTaskStatus
```

</details>

## Alert #530 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/530
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:99-99`
- Message: Dispose missed if exception is thrown by call to method FromMilliseconds.
Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should.
Dispose missed if exception is thrown by call to method Dispose.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
    91	        await Task.Delay(1000);
    92	        monitor.Stop();
    93	
    94	        states.Should().Contain(AgentTaskState.Running);
    95	        states.Should().Contain(AgentTaskState.Completed);
    96	        response.Should().Be("Here is the result.");
    97	
    98	        monitor.Dispose();
    99	        handler.Dispose();
   100	    }
   101	
   102	    [Fact]
   103	    public async Task Monitor_TransitionsToFailed_WhenBridgeReturnsFailed()
   104	    {
   105	        var responses = new Queue<string>();
   106	        responses.Enqueue(JsonSerializer.Serialize(new BridgeTaskStatus
   107	        {
```

</details>

## Alert #529 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/529
- Location: `tests/TiaAgent.ResponseCenter.Tests/BridgeTaskMonitorTests.cs:98-98`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Delay.
Dispose missed if exception is thrown by call to method Stop.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Contain.
Dispose missed if exception is thrown by call to method Should<AgentTaskState>.
Dispose missed if exception is thrown by call to method Be.
Dispose missed if exception is thrown by call to method Should.

- Current file exists on `main`: **yes**
- Current file length: 268 lines

<details><summary>Current code context</summary>

```text
    90	        monitor.Start();
    91	        await Task.Delay(1000);
    92	        monitor.Stop();
    93	
    94	        states.Should().Contain(AgentTaskState.Running);
    95	        states.Should().Contain(AgentTaskState.Completed);
    96	        response.Should().Be("Here is the result.");
    97	
    98	        monitor.Dispose();
    99	        handler.Dispose();
   100	    }
   101	
   102	    [Fact]
   103	    public async Task Monitor_TransitionsToFailed_WhenBridgeReturnsFailed()
   104	    {
   105	        var responses = new Queue<string>();
   106	        responses.Enqueue(JsonSerializer.Serialize(new BridgeTaskStatus
```

</details>

## Alert #528 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-08-02T16:11:50Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/528
- Location: `src/TiaAgent.ResponseCenter/Program.cs:165-165`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method StartMonitoring.
Dispose missed if exception is thrown by call to method Show.
Dispose missed if exception is thrown by call to method Activate.
Dispose missed if exception is thrown by call to method Focus.
Dispose missed if exception is thrown by call to method Run.
Dispose missed if exception is thrown by call to method Run.

- Current file exists on `main`: **yes**
- Current file length: 332 lines

<details><summary>Current code context</summary>

```text
   157	                    ResponseCenterLogger.Warn($"Readiness reporter failed: {ex.Message}");
   158	                }
   159	            });
   160	
   161	            ResponseCenterLogger.Info("Application.Run entered");
   162	            app.Run(window);
   163	
   164	            ResponseCenterLogger.Info("Application shutting down");
   165	            pipeListener?.Dispose();
   166	            return 0;
   167	        }
   168	        catch (Exception ex)
   169	        {
   170	            ResponseCenterLogger.Error("Fatal exception in Main", ex);
   171	            MessageBox.Show(
   172	                $"Failed to start TIA Agent Response Center:\n\n{ex.Message}",
   173	                "TIA Agent - Error",
```

</details>

## Alert #527 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T18:17:30Z
- Updated: 2026-07-23T18:18:30Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/527
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:232-232`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #526 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T18:17:30Z
- Updated: 2026-07-23T18:18:30Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/526
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:213-213`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #525 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T18:17:30Z
- Updated: 2026-07-23T18:18:30Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/525
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:198-198`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #524 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T18:17:30Z
- Updated: 2026-07-23T18:18:30Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/524
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:183-183`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #523 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/523
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:50-50`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    42	        var config = new TiaAgentConfig { UpdateChannel = "beta" };
    43	        ManifestStore.WriteAtomic(layout.ConfigPath, config);
    44	    }
    45	
    46	    public void Dispose()
    47	    {
    48	        if (Directory.Exists(_tempDirectory))
    49	        {
    50	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    51	        }
    52	        GC.SuppressFinalize(this);
    53	    }
    54	
    55	    [Fact]
    56	    public void VersionsCommand_List_OutputsInstalledVersionsAndActiveMarker()
    57	    {
    58	        InstallVersion("0.2.0-beta.1", _payloadDirV1);
```

</details>

## Alert #522 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/522
- Location: `tests/TiaAgent.Cli.Tests/Commands/ChannelCommandTests.cs:30-30`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 351 lines

<details><summary>Current code context</summary>

```text
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
    29	        {
    30	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    31	        }
    32	        GC.SuppressFinalize(this);
    33	    }
    34	
    35	    [Fact]
    36	    public void ChannelShow_DefaultChannel_ShowsStable()
    37	    {
    38	        var options = new ChannelOptions
```

</details>

## Alert #521 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/521
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:330-333`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   322	            if (fileName.Contains(version, StringComparison.OrdinalIgnoreCase) ||
   323	                fileName.Contains(pubVersion, StringComparison.OrdinalIgnoreCase))
   324	            {
   325	                try
   326	                {
   327	                    File.Delete(file);
   328	                    stdout.WriteLine($"Removed Add-In artifact '{fileName}' from '{userAddInsDir}'.");
   329	                }
   330	                catch (Exception ex)
   331	                {
   332	                    stderr.WriteLine($"Warning: Failed to remove Add-In artifact '{fileName}': {ex.Message}");
   333	                }
   334	            }
   335	        }
   336	    }
   337	}
```

</details>

## Alert #520 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/520
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:286-286`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   278	                };
   279	                ManifestStore.WriteAtomic(layout.CurrentManifestPath, newCurrent);
   280	                stdout.WriteLine($"Switched active version to '{nextActive}'.");
   281	            }
   282	            else
   283	            {
   284	                if (File.Exists(layout.CurrentManifestPath))
   285	                {
   286	                    try { File.Delete(layout.CurrentManifestPath); } catch { }
   287	                }
   288	            }
   289	        }
   290	        else if (string.Equals(targetVersion, current.PreviousVersion, StringComparison.OrdinalIgnoreCase))
   291	        {
   292	            // Update PreviousVersion if targetVersion was previous
   293	            var newPrevious = installations.Versions.Keys
   294	                .Where(v => !string.Equals(v, current.ActiveVersion, StringComparison.OrdinalIgnoreCase))
```

</details>

## Alert #519 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/519
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:249-262`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   241	            {
   242	                Directory.Delete(versionDir, recursive: true);
   243	            }
   244	
   245	            RemoveAddInFilesForVersion(targetVersion, userAddInsDir, stdout, stderr);
   246	            installations.Versions.Remove(targetVersion);
   247	            ManifestStore.WriteAtomic(layout.InstallationsManifestPath, installations);
   248	        }
   249	        catch (Exception ex)
   250	        {
   251	            if (options.Force)
   252	            {
   253	                stderr.WriteLine($"Warning: Failed to cleanly remove version '{targetVersion}': {ex.Message}");
   254	                installations.Versions.Remove(targetVersion);
   255	                ManifestStore.WriteAtomic(layout.InstallationsManifestPath, installations);
   256	            }
   257	            else
   258	            {
   259	                stderr.WriteLine($"Error removing version '{targetVersion}': {ex.Message}");
   260	                return 1;
   261	            }
   262	        }
   263	
   264	        // Handle active version update if active version was removed with --force
   265	        if (isActive)
   266	        {
   267	            var remainingVersions = installations.Versions.Keys.ToList();
   268	            if (remainingVersions.Count > 0)
   269	            {
   270	                var nextActive = remainingVersions.First();
```

</details>

## Alert #518 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/518
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:190-193`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   182	            installations = new InstallationsManifest();
   183	        }
   184	
   185	        CurrentManifest current;
   186	        try
   187	        {
   188	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   189	        }
   190	        catch
   191	        {
   192	            current = new CurrentManifest();
   193	        }
   194	
   195	        var targetVersion = options.Version;
   196	        if (string.IsNullOrWhiteSpace(targetVersion))
   197	        {
   198	            stderr.WriteLine("Version to remove must be specified. Usage: tia-agent versions remove <version>");
   199	            return 1;
   200	        }
   201	
```

</details>

## Alert #517 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/517
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:180-183`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   172	    {
   173	        var layout = new TiaAgentLayout(options.CustomRoot);
   174	
   175	        InstallationsManifest installations;
   176	        try
   177	        {
   178	            installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
   179	        }
   180	        catch
   181	        {
   182	            installations = new InstallationsManifest();
   183	        }
   184	
   185	        CurrentManifest current;
   186	        try
   187	        {
   188	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   189	        }
   190	        catch
   191	        {
```

</details>

## Alert #516 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/516
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:120-123`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   112	            });
   113	        }
   114	
   115	        TiaAgentConfig config;
   116	        try
   117	        {
   118	            config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   119	        }
   120	        catch
   121	        {
   122	            config = new TiaAgentConfig();
   123	        }
   124	
   125	        var updateChannel = ChannelUtils.NormalizeChannel(config.UpdateChannel) ?? "stable";
   126	
   127	        var report = new VersionsReport
   128	        {
   129	            ProductVersion = productVersion,
   130	            ActiveVersion = current.ActiveVersion,
   131	            RollbackVersion = rollbackVersion,
```

</details>

## Alert #515 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/515
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:84-87`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
    76	            current = new CurrentManifest();
    77	        }
    78	
    79	        InstallationsManifest installations;
    80	        try
    81	        {
    82	            installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    83	        }
    84	        catch
    85	        {
    86	            installations = new InstallationsManifest();
    87	        }
    88	
    89	        string? rollbackVersion = current.PreviousVersion;
    90	        if (string.IsNullOrWhiteSpace(rollbackVersion))
    91	        {
    92	            rollbackVersion = installations.Versions.Keys
    93	                .Where(v => !string.Equals(v, current.ActiveVersion, StringComparison.OrdinalIgnoreCase))
    94	                .OrderByDescending(v => installations.Versions[v].InstalledAt)
    95	                .FirstOrDefault();
```

</details>

## Alert #514 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/514
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:74-77`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
    66	        var layout = new TiaAgentLayout(options.CustomRoot);
    67	        var productVersion = Program.GetProductVersion();
    68	
    69	        CurrentManifest current;
    70	        try
    71	        {
    72	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    73	        }
    74	        catch
    75	        {
    76	            current = new CurrentManifest();
    77	        }
    78	
    79	        InstallationsManifest installations;
    80	        try
    81	        {
    82	            installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    83	        }
    84	        catch
    85	        {
```

</details>

## Alert #513 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/513
- Location: `src/TiaAgent.Cli/Commands/UpdateCommand.cs:113-116`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   105	        // Channel validation: warn if the target version is incompatible with the configured channel
   106	        if (!string.IsNullOrWhiteSpace(targetVersion))
   107	        {
   108	            TiaAgentConfig config;
   109	            try
   110	            {
   111	                config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   112	            }
   113	            catch
   114	            {
   115	                config = new TiaAgentConfig();
   116	            }
   117	
   118	            var channel = ChannelUtils.NormalizeChannel(config.UpdateChannel) ?? "stable";
   119	            if (!ChannelUtils.IsVersionCompatibleWithChannel(targetVersion, channel))
   120	            {
   121	                var versionChannel = ReleaseStore.ResolveChannel(targetVersion);
   122	                var err = $"Version '{targetVersion}' (channel: {versionChannel}) is not compatible with the configured update channel '{channel}'. Use --force to override.";
   123	                if (options.Force)
   124	                {
```

</details>

## Alert #512 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/512
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:324-324`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   316	
   317	        if (normalizedChannel != null)
   318	        {
   319	            CurrentManifest? current = null;
   320	            try
   321	            {
   322	                current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   323	            }
   324	            catch { }
   325	
   326	            var activeVersion = current?.ActiveVersion;
   327	            var activeChannel = !string.IsNullOrWhiteSpace(activeVersion)
   328	                ? ReleaseStore.ResolveChannel(activeVersion)
   329	                : null;
   330	
   331	            var details = $"Update channel: {normalizedChannel}";
   332	            if (!string.IsNullOrWhiteSpace(activeVersion))
```

</details>

## Alert #511 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/511
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:311-311`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   303	    {
   304	        TiaAgentConfig? config = null;
   305	        if (File.Exists(layout.ConfigPath))
   306	        {
   307	            try
   308	            {
   309	                config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   310	            }
   311	            catch { }
   312	        }
   313	
   314	        var configuredChannel = config?.UpdateChannel;
   315	        var normalizedChannel = ChannelUtils.NormalizeChannel(configuredChannel);
   316	
   317	        if (normalizedChannel != null)
   318	        {
   319	            CurrentManifest? current = null;
```

</details>

## Alert #510 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/510
- Location: `src/TiaAgent.Cli/Commands/ChannelCommand.cs:70-73`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 205 lines

<details><summary>Current code context</summary>

```text
    62	        var config = ConfigCommand.LoadConfig(layout.ConfigPath);
    63	        var currentChannel = ChannelUtils.NormalizeChannel(config.UpdateChannel) ?? "stable";
    64	
    65	        CurrentManifest current;
    66	        try
    67	        {
    68	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    69	        }
    70	        catch
    71	        {
    72	            current = new CurrentManifest();
    73	        }
    74	
    75	        var activeVersion = current.ActiveVersion;
    76	        var activeVersionChannel = !string.IsNullOrWhiteSpace(activeVersion)
    77	            ? ReleaseStore.ResolveChannel(activeVersion)
    78	            : null;
    79	
    80	        var report = new ChannelShowReport
    81	        {
```

</details>

## Alert #509 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/509
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:273-273`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
   265	        var addinDir = Path.Combine(payloadDir, "AddIn");
   266	        Directory.CreateDirectory(bridgeDir);
   267	        Directory.CreateDirectory(addinDir);
   268	
   269	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   270	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   271	        File.WriteAllBytes(bridgeDll, bridgeContent);
   272	
   273	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   274	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   275	        File.WriteAllBytes(addinFile, addinContent);
   276	
   277	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
   278	        var addinHash = PayloadStore.ComputeSha256(addinFile);
   279	
   280	        var manifest = new PayloadManifest
   281	        {
```

</details>

## Alert #508 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/508
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:269-269`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
   261	
   262	    private static void CreateDummyPayload(string payloadDir, string version)
   263	    {
   264	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   265	        var addinDir = Path.Combine(payloadDir, "AddIn");
   266	        Directory.CreateDirectory(bridgeDir);
   267	        Directory.CreateDirectory(addinDir);
   268	
   269	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   270	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   271	        File.WriteAllBytes(bridgeDll, bridgeContent);
   272	
   273	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   274	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   275	        File.WriteAllBytes(addinFile, addinContent);
   276	
   277	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
```

</details>

## Alert #507 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/507
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:265-265`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
   257	            UserAddInsDir = _userAddInsDir
   258	        };
   259	        UpdateCommand.Execute(updateOptions, TextWriter.Null, TextWriter.Null);
   260	    }
   261	
   262	    private static void CreateDummyPayload(string payloadDir, string version)
   263	    {
   264	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   265	        var addinDir = Path.Combine(payloadDir, "AddIn");
   266	        Directory.CreateDirectory(bridgeDir);
   267	        Directory.CreateDirectory(addinDir);
   268	
   269	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   270	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   271	        File.WriteAllBytes(bridgeDll, bridgeContent);
   272	
   273	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
```

</details>

## Alert #506 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/506
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:264-264`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
   256	            CustomRoot = _customRoot,
   257	            UserAddInsDir = _userAddInsDir
   258	        };
   259	        UpdateCommand.Execute(updateOptions, TextWriter.Null, TextWriter.Null);
   260	    }
   261	
   262	    private static void CreateDummyPayload(string payloadDir, string version)
   263	    {
   264	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   265	        var addinDir = Path.Combine(payloadDir, "AddIn");
   266	        Directory.CreateDirectory(bridgeDir);
   267	        Directory.CreateDirectory(addinDir);
   268	
   269	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   270	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   271	        File.WriteAllBytes(bridgeDll, bridgeContent);
   272	
```

</details>

## Alert #505 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/505
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    20	    private readonly string _payloadDirV2;
    21	
    22	    public VersionsCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionsCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
    36	        CreateDummyPayload(_payloadDirV1, "0.2.0-beta.1");
```

</details>

## Alert #504 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/504
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public VersionsCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionsCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
```

</details>

## Alert #503 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/503
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:26-26`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public VersionsCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionsCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
```

</details>

## Alert #502 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/502
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:25-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public VersionsCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionsCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
```

</details>

## Alert #501 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/501
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    16	    private readonly string _tempDirectory;
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public VersionsCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionsCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
```

</details>

## Alert #500 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/500
- Location: `tests/TiaAgent.Cli.Tests/Commands/ChannelCommandTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 351 lines

<details><summary>Current code context</summary>

```text
    13	public sealed class ChannelCommandTests : IDisposable
    14	{
    15	    private readonly string _tempDirectory;
    16	    private readonly string _customRoot;
    17	
    18	    public ChannelCommandTests()
    19	    {
    20	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ChannelCommandTests_" + Guid.NewGuid().ToString("N"));
    21	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
    29	        {
```

</details>

## Alert #499 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/499
- Location: `tests/TiaAgent.Cli.Tests/Commands/ChannelCommandTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 351 lines

<details><summary>Current code context</summary>

```text
    12	
    13	public sealed class ChannelCommandTests : IDisposable
    14	{
    15	    private readonly string _tempDirectory;
    16	    private readonly string _customRoot;
    17	
    18	    public ChannelCommandTests()
    19	    {
    20	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ChannelCommandTests_" + Guid.NewGuid().ToString("N"));
    21	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
```

</details>

## Alert #498 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/498
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:234-234`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   226	            stderr.WriteLine($"Cannot remove version '{targetVersion}' because it is the only known-good rollback version. Use --force to override preservation rule.");
   227	            return 1;
   228	        }
   229	
   230	        var userAddInsDir = options.UserAddInsDir;
   231	        if (string.IsNullOrWhiteSpace(userAddInsDir))
   232	        {
   233	            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
   234	            userAddInsDir = Path.Combine(appData, "Siemens", "Automation", "Portal V21", "UserAddIns");
   235	        }
   236	
   237	        try
   238	        {
   239	            var versionDir = layout.GetVersionPath(targetVersion);
   240	            if (Directory.Exists(versionDir))
   241	            {
   242	                Directory.Delete(versionDir, recursive: true);
```

</details>

## Alert #497 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/497
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionsCommandTests.cs:50-50`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 320 lines

<details><summary>Current code context</summary>

```text
    42	        var config = new TiaAgentConfig { UpdateChannel = "beta" };
    43	        ManifestStore.WriteAtomic(layout.ConfigPath, config);
    44	    }
    45	
    46	    public void Dispose()
    47	    {
    48	        if (Directory.Exists(_tempDirectory))
    49	        {
    50	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    51	        }
    52	        GC.SuppressFinalize(this);
    53	    }
    54	
    55	    [Fact]
    56	    public void VersionsCommand_List_OutputsInstalledVersionsAndActiveMarker()
    57	    {
    58	        InstallVersion("0.2.0-beta.1", _payloadDirV1);
```

</details>

## Alert #496 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/496
- Location: `tests/TiaAgent.Cli.Tests/Commands/ChannelCommandTests.cs:30-30`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 351 lines

<details><summary>Current code context</summary>

```text
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
    29	        {
    30	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    31	        }
    32	        GC.SuppressFinalize(this);
    33	    }
    34	
    35	    [Fact]
    36	    public void ChannelShow_DefaultChannel_ShowsStable()
    37	    {
    38	        var options = new ChannelOptions
```

</details>

## Alert #495 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/495
- Location: `src/TiaAgent.Cli/Commands/VersionsCommand.cs:286-286`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 337 lines

<details><summary>Current code context</summary>

```text
   278	                };
   279	                ManifestStore.WriteAtomic(layout.CurrentManifestPath, newCurrent);
   280	                stdout.WriteLine($"Switched active version to '{nextActive}'.");
   281	            }
   282	            else
   283	            {
   284	                if (File.Exists(layout.CurrentManifestPath))
   285	                {
   286	                    try { File.Delete(layout.CurrentManifestPath); } catch { }
   287	                }
   288	            }
   289	        }
   290	        else if (string.Equals(targetVersion, current.PreviousVersion, StringComparison.OrdinalIgnoreCase))
   291	        {
   292	            // Update PreviousVersion if targetVersion was previous
   293	            var newPrevious = installations.Versions.Keys
   294	                .Where(v => !string.Equals(v, current.ActiveVersion, StringComparison.OrdinalIgnoreCase))
```

</details>

## Alert #494 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/494
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:324-324`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   316	
   317	        if (normalizedChannel != null)
   318	        {
   319	            CurrentManifest? current = null;
   320	            try
   321	            {
   322	                current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   323	            }
   324	            catch { }
   325	
   326	            var activeVersion = current?.ActiveVersion;
   327	            var activeChannel = !string.IsNullOrWhiteSpace(activeVersion)
   328	                ? ReleaseStore.ResolveChannel(activeVersion)
   329	                : null;
   330	
   331	            var details = $"Update channel: {normalizedChannel}";
   332	            if (!string.IsNullOrWhiteSpace(activeVersion))
```

</details>

## Alert #493 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T15:19:37Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/493
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:311-311`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   303	    {
   304	        TiaAgentConfig? config = null;
   305	        if (File.Exists(layout.ConfigPath))
   306	        {
   307	            try
   308	            {
   309	                config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   310	            }
   311	            catch { }
   312	        }
   313	
   314	        var configuredChannel = config?.UpdateChannel;
   315	        var normalizedChannel = ChannelUtils.NormalizeChannel(configuredChannel);
   316	
   317	        if (normalizedChannel != null)
   318	        {
   319	            CurrentManifest? current = null;
```

</details>

## Alert #492 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/492
- Location: `src/TiaAgent.Cli/Release/ReleaseStore.cs:80-92`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 22 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #491 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/491
- Location: `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:174-181`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **no**

## Alert #490 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/490
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:196-204`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **no**

## Alert #489 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/489
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25-25`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #488 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/488
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:142-145`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #487 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/487
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:104-107`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #486 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/486
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:41-45`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #485 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/485
- Location: `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:72-76`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #484 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/484
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:148-148`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #483 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/483
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:147-147`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #482 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/482
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:128-128`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #481 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/481
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:127-127`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #480 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/480
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:117-117`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #479 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/479
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:113-113`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #478 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/478
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:97-97`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #477 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/477
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:58-58`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #476 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/476
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:57-57`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #475 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/475
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:17-17`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #474 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/474
- Location: `src/TiaAgent.Cli/Release/SbomGenerator.cs:142-142`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #473 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/473
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:163-163`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #472 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/472
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:128-128`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #471 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/471
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:111-111`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #470 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/470
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:87-87`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #469 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/469
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:80-80`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #468 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/468
- Location: `src/TiaAgent.Cli/Release/ReleaseValidator.cs:29-29`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #467 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/467
- Location: `src/TiaAgent.Cli/Release/ReleaseStore.cs:63-63`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 22 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #466 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/466
- Location: `src/TiaAgent.Cli/Release/ReleaseStore.cs:47-47`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 22 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #465 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/465
- Location: `src/TiaAgent.Cli/Release/ReleaseStore.cs:33-33`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 22 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #464 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/464
- Location: `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:172-172`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #463 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/463
- Location: `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:171-171`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #462 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/462
- Location: `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:168-168`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #461 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/461
- Location: `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:162-162`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #460 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/460
- Location: `src/TiaAgent.Cli/Commands/VerifyReleaseCommand.cs:43-43`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #459 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/459
- Location: `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:33-33`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #458 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/458
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25-25`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **no**

## Alert #457 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/457
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:151-151`
- Message: Disposable 'StringWriter' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #456 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/456
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:150-150`
- Message: Disposable 'StringWriter' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #455 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/455
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:132-132`
- Message: Disposable 'StringWriter' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #454 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:55:30Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/454
- Location: `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:131-131`
- Message: Disposable 'StringWriter' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #453 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:45:09Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/453
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:167-167`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #452 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:45:09Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/452
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:152-152`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #451 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:39:35Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/451
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:134-134`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #450 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:33:18Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/450
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:121-121`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #449 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:33:18Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/449
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:107-107`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #448 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:33:18Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/448
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:91-91`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
```

</details>

## Alert #447 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/447
- Location: `src/TiaAgent.Cli/Commands/CommandHelpers.cs:59-59`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 61 lines

<details><summary>Current code context</summary>

```text
    51	        {
    52	            // Silent — no Add-In to deploy is expected for dev builds
    53	            return;
    54	        }
    55	
    56	        if (result.Status == AddInDeploymentStatus.Error)
    57	        {
    58	            stdout.WriteLine($"Warning: Add-In deployment encountered an error: {result.ErrorMessage}");
    59	        }
    60	    }
    61	}
```

</details>

## Alert #446 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/446
- Location: `src/TiaAgent.Cli/Commands/CommandHelpers.cs:44-44`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 61 lines

<details><summary>Current code context</summary>

```text
    36	    /// </summary>
    37	    internal static void DeployAddInIfPresent(string versionDir, string? customUserAddInsDir, TextWriter stdout)
    38	    {
    39	        if (!Directory.Exists(versionDir))
    40	        {
    41	            return;
    42	        }
    43	
    44	        // Derive fallback base directory from versionDir (versions/VERSION → versions → root)
    45	        var fallbackBaseDir = Path.GetDirectoryName(Path.GetDirectoryName(versionDir))
    46	            ?? Path.GetTempPath();
    47	
    48	        var result = AddInDeployer.Deploy(versionDir, customUserAddInsDir, fallbackBaseDir, stdout);
    49	
    50	        if (result.Status == AddInDeploymentStatus.NoAddInPackage)
    51	        {
    52	            // Silent — no Add-In to deploy is expected for dev builds
```

</details>

## Alert #445 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/445
- Location: `src/TiaAgent.Cli/Commands/CommandHelpers.cs:29-29`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 61 lines

<details><summary>Current code context</summary>

```text
    21	    internal static string ResolveUserAddInsDir(string? customUserAddInsDir)
    22	    {
    23	        if (!string.IsNullOrWhiteSpace(customUserAddInsDir))
    24	        {
    25	            return customUserAddInsDir;
    26	        }
    27	
    28	        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    29	        return Path.Combine(appData, DefaultUserAddInsRelativePath);
    30	    }
    31	
    32	    /// <summary>
    33	    /// Deploys .addin files from <paramref name="versionDir"/>/AddIn to the Siemens UserAddIns directory.
    34	    /// Uses the shared <see cref="AddInDeployer"/> service for consistent deployment behavior.
    35	    /// Logs each deployment to <paramref name="stdout"/>. Failures are logged as warnings and do not abort.
    36	    /// </summary>
    37	    internal static void DeployAddInIfPresent(string versionDir, string? customUserAddInsDir, TextWriter stdout)
```

</details>

## Alert #444 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/444
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:100-100`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
    92	                else
    93	                {
    94	                    previousVersion = existingCurrent.PreviousVersion;
    95	                }
    96	            }
    97	            catch (FileNotFoundException) { }
    98	            catch (DirectoryNotFoundException) { }
    99	            catch (JsonException) { }
   100	            catch (IOException) { }
   101	        }
   102	
   103	        if (installations.Versions.ContainsKey(targetVersion) && Directory.Exists(versionDir) && !options.Force)
   104	        {
   105	            var currentManifest = new CurrentManifest
   106	            {
   107	                SchemaVersion = 1,
   108	                ActiveVersion = targetVersion,
```

</details>

## Alert #443 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/443
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:99-99`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
    91	                }
    92	                else
    93	                {
    94	                    previousVersion = existingCurrent.PreviousVersion;
    95	                }
    96	            }
    97	            catch (FileNotFoundException) { }
    98	            catch (DirectoryNotFoundException) { }
    99	            catch (JsonException) { }
   100	            catch (IOException) { }
   101	        }
   102	
   103	        if (installations.Versions.ContainsKey(targetVersion) && Directory.Exists(versionDir) && !options.Force)
   104	        {
   105	            var currentManifest = new CurrentManifest
   106	            {
   107	                SchemaVersion = 1,
```

</details>

## Alert #442 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/442
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:98-98`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
    90	                    previousVersion = existingCurrent.ActiveVersion;
    91	                }
    92	                else
    93	                {
    94	                    previousVersion = existingCurrent.PreviousVersion;
    95	                }
    96	            }
    97	            catch (FileNotFoundException) { }
    98	            catch (DirectoryNotFoundException) { }
    99	            catch (JsonException) { }
   100	            catch (IOException) { }
   101	        }
   102	
   103	        if (installations.Versions.ContainsKey(targetVersion) && Directory.Exists(versionDir) && !options.Force)
   104	        {
   105	            var currentManifest = new CurrentManifest
   106	            {
```

</details>

## Alert #441 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/441
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:97-97`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
    89	                {
    90	                    previousVersion = existingCurrent.ActiveVersion;
    91	                }
    92	                else
    93	                {
    94	                    previousVersion = existingCurrent.PreviousVersion;
    95	                }
    96	            }
    97	            catch (FileNotFoundException) { }
    98	            catch (DirectoryNotFoundException) { }
    99	            catch (JsonException) { }
   100	            catch (IOException) { }
   101	        }
   102	
   103	        if (installations.Versions.ContainsKey(targetVersion) && Directory.Exists(versionDir) && !options.Force)
   104	        {
   105	            var currentManifest = new CurrentManifest
```

</details>

## Alert #440 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/440
- Location: `src/TiaAgent.Cli/Commands/ActivateCommand.cs:127-127`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 163 lines

<details><summary>Current code context</summary>

```text
   119	                else
   120	                {
   121	                    previousVersion = existingCurrent.PreviousVersion;
   122	                }
   123	            }
   124	            catch (FileNotFoundException) { }
   125	            catch (DirectoryNotFoundException) { }
   126	            catch (JsonException) { }
   127	            catch (IOException) { }
   128	        }
   129	
   130	        var currentManifest = new CurrentManifest
   131	        {
   132	            SchemaVersion = 1,
   133	            ActiveVersion = targetVersion,
   134	            PreviousVersion = previousVersion,
   135	            ActivatedAt = DateTimeOffset.UtcNow,
```

</details>

## Alert #439 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/439
- Location: `src/TiaAgent.Cli/Commands/ActivateCommand.cs:126-126`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 163 lines

<details><summary>Current code context</summary>

```text
   118	                }
   119	                else
   120	                {
   121	                    previousVersion = existingCurrent.PreviousVersion;
   122	                }
   123	            }
   124	            catch (FileNotFoundException) { }
   125	            catch (DirectoryNotFoundException) { }
   126	            catch (JsonException) { }
   127	            catch (IOException) { }
   128	        }
   129	
   130	        var currentManifest = new CurrentManifest
   131	        {
   132	            SchemaVersion = 1,
   133	            ActiveVersion = targetVersion,
   134	            PreviousVersion = previousVersion,
```

</details>

## Alert #438 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/438
- Location: `src/TiaAgent.Cli/Commands/ActivateCommand.cs:125-125`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 163 lines

<details><summary>Current code context</summary>

```text
   117	                    previousVersion = existingCurrent.ActiveVersion;
   118	                }
   119	                else
   120	                {
   121	                    previousVersion = existingCurrent.PreviousVersion;
   122	                }
   123	            }
   124	            catch (FileNotFoundException) { }
   125	            catch (DirectoryNotFoundException) { }
   126	            catch (JsonException) { }
   127	            catch (IOException) { }
   128	        }
   129	
   130	        var currentManifest = new CurrentManifest
   131	        {
   132	            SchemaVersion = 1,
   133	            ActiveVersion = targetVersion,
```

</details>

## Alert #437 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:28:33Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/437
- Location: `src/TiaAgent.Cli/Commands/ActivateCommand.cs:124-124`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 163 lines

<details><summary>Current code context</summary>

```text
   116	                {
   117	                    previousVersion = existingCurrent.ActiveVersion;
   118	                }
   119	                else
   120	                {
   121	                    previousVersion = existingCurrent.PreviousVersion;
   122	                }
   123	            }
   124	            catch (FileNotFoundException) { }
   125	            catch (DirectoryNotFoundException) { }
   126	            catch (JsonException) { }
   127	            catch (IOException) { }
   128	        }
   129	
   130	        var currentManifest = new CurrentManifest
   131	        {
   132	            SchemaVersion = 1,
```

</details>

## Alert #436 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/436
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:87-95`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
    79	        }
    80	
    81	        string? previousVersion = null;
    82	        if (File.Exists(layout.CurrentManifestPath))
    83	        {
    84	            try
    85	            {
    86	                var existingCurrent = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    87	                if (!string.IsNullOrWhiteSpace(existingCurrent.ActiveVersion) &&
    88	                    !string.Equals(existingCurrent.ActiveVersion, targetVersion, StringComparison.OrdinalIgnoreCase))
    89	                {
    90	                    previousVersion = existingCurrent.ActiveVersion;
    91	                }
    92	                else
    93	                {
    94	                    previousVersion = existingCurrent.PreviousVersion;
    95	                }
    96	            }
    97	            catch (FileNotFoundException) { }
    98	            catch (DirectoryNotFoundException) { }
    99	            catch (JsonException) { }
   100	            catch (IOException) { }
   101	        }
   102	
   103	        if (installations.Versions.ContainsKey(targetVersion) && Directory.Exists(versionDir) && !options.Force)
```

</details>

## Alert #435 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/435
- Location: `src/TiaAgent.Cli/Commands/ActivateCommand.cs:114-122`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **yes**
- Current file length: 163 lines

<details><summary>Current code context</summary>

```text
   106	        }
   107	
   108	        string? previousVersion = null;
   109	        if (File.Exists(layout.CurrentManifestPath))
   110	        {
   111	            try
   112	            {
   113	                var existingCurrent = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   114	                if (!string.IsNullOrWhiteSpace(existingCurrent.ActiveVersion) &&
   115	                    !string.Equals(existingCurrent.ActiveVersion, targetVersion, StringComparison.OrdinalIgnoreCase))
   116	                {
   117	                    previousVersion = existingCurrent.ActiveVersion;
   118	                }
   119	                else
   120	                {
   121	                    previousVersion = existingCurrent.PreviousVersion;
   122	                }
   123	            }
   124	            catch (FileNotFoundException) { }
   125	            catch (DirectoryNotFoundException) { }
   126	            catch (JsonException) { }
   127	            catch (IOException) { }
   128	        }
   129	
   130	        var currentManifest = new CurrentManifest
```

</details>

## Alert #434 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/434
- Location: `src/TiaAgent.Cli/Commands/UpdateCommand.cs:195-195`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   187	                    }
   188	                    return installResult;
   189	                }
   190	
   191	                try
   192	                {
   193	                    current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   194	                }
   195	                catch { }
   196	            }
   197	            else if (!options.Force)
   198	            {
   199	                var err = $"Version '{targetVersion}' is not installed and no valid payload was found to install it.";
   200	                if (options.Json)
   201	                {
   202	                    stdout.WriteLine(JsonSerializer.Serialize(new UpdateReport { Success = false, Error = err }, s_jsonOptions));
   203	                }
```

</details>

## Alert #431 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/431
- Location: `src/TiaAgent.Cli/Commands/UpdateCommand.cs:52-55`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    44	        var layout = new TiaAgentLayout(options.CustomRoot);
    45	        layout.EnsureDirectoriesExist();
    46	
    47	        CurrentManifest current;
    48	        try
    49	        {
    50	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    51	        }
    52	        catch
    53	        {
    54	            current = new CurrentManifest();
    55	        }
    56	
    57	        InstallationsManifest installations;
    58	        try
    59	        {
    60	            installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    61	        }
    62	        catch (FileNotFoundException)
    63	        {
```

</details>

## Alert #426 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/426
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:238-238`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
   230	        var addinDir = Path.Combine(payloadDir, "AddIn");
   231	        Directory.CreateDirectory(bridgeDir);
   232	        Directory.CreateDirectory(addinDir);
   233	
   234	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   235	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   236	        File.WriteAllBytes(bridgeDll, bridgeContent);
   237	
   238	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   239	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   240	        File.WriteAllBytes(addinFile, addinContent);
   241	
   242	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
   243	        var addinHash = PayloadStore.ComputeSha256(addinFile);
   244	
   245	        var manifest = new PayloadManifest
   246	        {
```

</details>

## Alert #425 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/425
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:234-234`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
   226	
   227	    private static void CreateDummyPayload(string payloadDir, string version)
   228	    {
   229	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   230	        var addinDir = Path.Combine(payloadDir, "AddIn");
   231	        Directory.CreateDirectory(bridgeDir);
   232	        Directory.CreateDirectory(addinDir);
   233	
   234	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   235	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   236	        File.WriteAllBytes(bridgeDll, bridgeContent);
   237	
   238	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   239	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   240	        File.WriteAllBytes(addinFile, addinContent);
   241	
   242	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
```

</details>

## Alert #424 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/424
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:230-230`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
   222	            UserAddInsDir = _userAddInsDir
   223	        };
   224	        ActivateCommand.Execute(activateOptions, TextWriter.Null, TextWriter.Null);
   225	    }
   226	
   227	    private static void CreateDummyPayload(string payloadDir, string version)
   228	    {
   229	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   230	        var addinDir = Path.Combine(payloadDir, "AddIn");
   231	        Directory.CreateDirectory(bridgeDir);
   232	        Directory.CreateDirectory(addinDir);
   233	
   234	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   235	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   236	        File.WriteAllBytes(bridgeDll, bridgeContent);
   237	
   238	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
```

</details>

## Alert #423 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/423
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:229-229`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
   221	            CustomRoot = _customRoot,
   222	            UserAddInsDir = _userAddInsDir
   223	        };
   224	        ActivateCommand.Execute(activateOptions, TextWriter.Null, TextWriter.Null);
   225	    }
   226	
   227	    private static void CreateDummyPayload(string payloadDir, string version)
   228	    {
   229	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   230	        var addinDir = Path.Combine(payloadDir, "AddIn");
   231	        Directory.CreateDirectory(bridgeDir);
   232	        Directory.CreateDirectory(addinDir);
   233	
   234	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   235	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   236	        File.WriteAllBytes(bridgeDll, bridgeContent);
   237	
```

</details>

## Alert #422 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/422
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:29-29`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
    21	    private readonly string _payloadDirV2;
    22	
    23	    public UpdateCommandTests()
    24	    {
    25	        _tempDirectory = Path.Combine(Path.GetTempPath(), "UpdateCommandTests_" + Guid.NewGuid().ToString("N"));
    26	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    27	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    28	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    29	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    30	
    31	        Directory.CreateDirectory(_tempDirectory);
    32	        Directory.CreateDirectory(_customRoot);
    33	        Directory.CreateDirectory(_userAddInsDir);
    34	        Directory.CreateDirectory(_payloadDirV1);
    35	        Directory.CreateDirectory(_payloadDirV2);
    36	
    37	        CreateDummyPayload(_payloadDirV1, "0.2.0-beta.1");
```

</details>

## Alert #421 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/421
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
    20	    private readonly string _payloadDirV1;
    21	    private readonly string _payloadDirV2;
    22	
    23	    public UpdateCommandTests()
    24	    {
    25	        _tempDirectory = Path.Combine(Path.GetTempPath(), "UpdateCommandTests_" + Guid.NewGuid().ToString("N"));
    26	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    27	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    28	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    29	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    30	
    31	        Directory.CreateDirectory(_tempDirectory);
    32	        Directory.CreateDirectory(_customRoot);
    33	        Directory.CreateDirectory(_userAddInsDir);
    34	        Directory.CreateDirectory(_payloadDirV1);
    35	        Directory.CreateDirectory(_payloadDirV2);
    36	
```

</details>

## Alert #420 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/420
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
    19	    private readonly string _userAddInsDir;
    20	    private readonly string _payloadDirV1;
    21	    private readonly string _payloadDirV2;
    22	
    23	    public UpdateCommandTests()
    24	    {
    25	        _tempDirectory = Path.Combine(Path.GetTempPath(), "UpdateCommandTests_" + Guid.NewGuid().ToString("N"));
    26	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    27	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    28	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    29	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    30	
    31	        Directory.CreateDirectory(_tempDirectory);
    32	        Directory.CreateDirectory(_customRoot);
    33	        Directory.CreateDirectory(_userAddInsDir);
    34	        Directory.CreateDirectory(_payloadDirV1);
    35	        Directory.CreateDirectory(_payloadDirV2);
```

</details>

## Alert #419 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/419
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:26-26`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
    18	    private readonly string _customRoot;
    19	    private readonly string _userAddInsDir;
    20	    private readonly string _payloadDirV1;
    21	    private readonly string _payloadDirV2;
    22	
    23	    public UpdateCommandTests()
    24	    {
    25	        _tempDirectory = Path.Combine(Path.GetTempPath(), "UpdateCommandTests_" + Guid.NewGuid().ToString("N"));
    26	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    27	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    28	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    29	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    30	
    31	        Directory.CreateDirectory(_tempDirectory);
    32	        Directory.CreateDirectory(_customRoot);
    33	        Directory.CreateDirectory(_userAddInsDir);
    34	        Directory.CreateDirectory(_payloadDirV1);
```

</details>

## Alert #418 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/418
- Location: `tests/TiaAgent.Cli.Tests/Commands/UpdateCommandTests.cs:25-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 285 lines

<details><summary>Current code context</summary>

```text
    17	    private readonly string _tempDirectory;
    18	    private readonly string _customRoot;
    19	    private readonly string _userAddInsDir;
    20	    private readonly string _payloadDirV1;
    21	    private readonly string _payloadDirV2;
    22	
    23	    public UpdateCommandTests()
    24	    {
    25	        _tempDirectory = Path.Combine(Path.GetTempPath(), "UpdateCommandTests_" + Guid.NewGuid().ToString("N"));
    26	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    27	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    28	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    29	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    30	
    31	        Directory.CreateDirectory(_tempDirectory);
    32	        Directory.CreateDirectory(_customRoot);
    33	        Directory.CreateDirectory(_userAddInsDir);
```

</details>

## Alert #417 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/417
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:208-208`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
   200	        var addinDir = Path.Combine(payloadDir, "AddIn");
   201	        Directory.CreateDirectory(bridgeDir);
   202	        Directory.CreateDirectory(addinDir);
   203	
   204	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   205	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   206	        File.WriteAllBytes(bridgeDll, bridgeContent);
   207	
   208	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   209	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   210	        File.WriteAllBytes(addinFile, addinContent);
   211	
   212	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
   213	        var addinHash = PayloadStore.ComputeSha256(addinFile);
   214	
   215	        var manifest = new PayloadManifest
   216	        {
```

</details>

## Alert #416 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/416
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:204-204`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
   196	
   197	    private static void CreateDummyPayload(string payloadDir, string version)
   198	    {
   199	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   200	        var addinDir = Path.Combine(payloadDir, "AddIn");
   201	        Directory.CreateDirectory(bridgeDir);
   202	        Directory.CreateDirectory(addinDir);
   203	
   204	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   205	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   206	        File.WriteAllBytes(bridgeDll, bridgeContent);
   207	
   208	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   209	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   210	        File.WriteAllBytes(addinFile, addinContent);
   211	
   212	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
```

</details>

## Alert #415 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/415
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:200-200`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
   192	            UserAddInsDir = _userAddInsDir
   193	        };
   194	        InstallCommand.Execute(installOptions, TextWriter.Null, TextWriter.Null);
   195	    }
   196	
   197	    private static void CreateDummyPayload(string payloadDir, string version)
   198	    {
   199	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   200	        var addinDir = Path.Combine(payloadDir, "AddIn");
   201	        Directory.CreateDirectory(bridgeDir);
   202	        Directory.CreateDirectory(addinDir);
   203	
   204	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   205	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   206	        File.WriteAllBytes(bridgeDll, bridgeContent);
   207	
   208	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
```

</details>

## Alert #414 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/414
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:199-199`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
   191	            CustomRoot = _customRoot,
   192	            UserAddInsDir = _userAddInsDir
   193	        };
   194	        InstallCommand.Execute(installOptions, TextWriter.Null, TextWriter.Null);
   195	    }
   196	
   197	    private static void CreateDummyPayload(string payloadDir, string version)
   198	    {
   199	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   200	        var addinDir = Path.Combine(payloadDir, "AddIn");
   201	        Directory.CreateDirectory(bridgeDir);
   202	        Directory.CreateDirectory(addinDir);
   203	
   204	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   205	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   206	        File.WriteAllBytes(bridgeDll, bridgeContent);
   207	
```

</details>

## Alert #413 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/413
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
    20	    private readonly string _payloadDirV2;
    21	
    22	    public RollbackCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "RollbackCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
    36	        CreateDummyPayload(_payloadDirV1, "0.2.0-beta.1");
```

</details>

## Alert #412 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/412
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public RollbackCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "RollbackCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
```

</details>

## Alert #411 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/411
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:26-26`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public RollbackCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "RollbackCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
```

</details>

## Alert #410 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/410
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:25-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public RollbackCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "RollbackCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
```

</details>

## Alert #409 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/409
- Location: `tests/TiaAgent.Cli.Tests/Commands/RollbackCommandTests.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 255 lines

<details><summary>Current code context</summary>

```text
    16	    private readonly string _tempDirectory;
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public RollbackCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "RollbackCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
```

</details>

## Alert #402 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T13:10:11Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/402
- Location: `src/TiaAgent.Cli/Commands/UpdateCommand.cs:195-195`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   187	                    }
   188	                    return installResult;
   189	                }
   190	
   191	                try
   192	                {
   193	                    current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   194	                }
   195	                catch { }
   196	            }
   197	            else if (!options.Force)
   198	            {
   199	                var err = $"Version '{targetVersion}' is not installed and no valid payload was found to install it.";
   200	                if (options.Json)
   201	                {
   202	                    stdout.WriteLine(JsonSerializer.Serialize(new UpdateReport { Success = false, Error = err }, s_jsonOptions));
   203	                }
```

</details>

## Alert #396 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/396
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:224-224`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   216	        var addinDir = Path.Combine(payloadDir, "AddIn");
   217	        Directory.CreateDirectory(bridgeDir);
   218	        Directory.CreateDirectory(addinDir);
   219	
   220	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   221	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   222	        File.WriteAllBytes(bridgeDll, bridgeContent);
   223	
   224	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   225	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   226	        File.WriteAllBytes(addinFile, addinContent);
   227	
   228	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
   229	        var addinHash = PayloadStore.ComputeSha256(addinFile);
   230	
   231	        var manifest = new PayloadManifest
   232	        {
```

</details>

## Alert #395 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/395
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:220-220`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   212	
   213	    private static void CreateDummyPayload(string payloadDir, string version)
   214	    {
   215	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   216	        var addinDir = Path.Combine(payloadDir, "AddIn");
   217	        Directory.CreateDirectory(bridgeDir);
   218	        Directory.CreateDirectory(addinDir);
   219	
   220	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   221	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   222	        File.WriteAllBytes(bridgeDll, bridgeContent);
   223	
   224	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
   225	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content " + version);
   226	        File.WriteAllBytes(addinFile, addinContent);
   227	
   228	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
```

</details>

## Alert #394 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/394
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:216-216`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   208	            UserAddInsDir = _userAddInsDir
   209	        };
   210	        InstallCommand.Execute(installOptions, TextWriter.Null, TextWriter.Null);
   211	    }
   212	
   213	    private static void CreateDummyPayload(string payloadDir, string version)
   214	    {
   215	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   216	        var addinDir = Path.Combine(payloadDir, "AddIn");
   217	        Directory.CreateDirectory(bridgeDir);
   218	        Directory.CreateDirectory(addinDir);
   219	
   220	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   221	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   222	        File.WriteAllBytes(bridgeDll, bridgeContent);
   223	
   224	        var addinFile = Path.Combine(addinDir, $"TiaAgent-{version}.addin");
```

</details>

## Alert #393 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/393
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:215-215`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
   207	            CustomRoot = _customRoot,
   208	            UserAddInsDir = _userAddInsDir
   209	        };
   210	        InstallCommand.Execute(installOptions, TextWriter.Null, TextWriter.Null);
   211	    }
   212	
   213	    private static void CreateDummyPayload(string payloadDir, string version)
   214	    {
   215	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   216	        var addinDir = Path.Combine(payloadDir, "AddIn");
   217	        Directory.CreateDirectory(bridgeDir);
   218	        Directory.CreateDirectory(addinDir);
   219	
   220	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   221	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge Content " + version);
   222	        File.WriteAllBytes(bridgeDll, bridgeContent);
   223	
```

</details>

## Alert #392 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/392
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    20	    private readonly string _payloadDirV2;
    21	
    22	    public ActivateCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ActivateCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
    36	        CreateDummyPayload(_payloadDirV1, "0.2.0-beta.1");
```

</details>

## Alert #391 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/391
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public ActivateCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ActivateCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
    35	
```

</details>

## Alert #390 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/390
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:26-26`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public ActivateCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ActivateCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
    34	        Directory.CreateDirectory(_payloadDirV2);
```

</details>

## Alert #389 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/389
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:25-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public ActivateCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ActivateCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
    33	        Directory.CreateDirectory(_payloadDirV1);
```

</details>

## Alert #388 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:12:17Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/388
- Location: `tests/TiaAgent.Cli.Tests/Commands/ActivateCommandTests.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 271 lines

<details><summary>Current code context</summary>

```text
    16	    private readonly string _tempDirectory;
    17	    private readonly string _customRoot;
    18	    private readonly string _userAddInsDir;
    19	    private readonly string _payloadDirV1;
    20	    private readonly string _payloadDirV2;
    21	
    22	    public ActivateCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ActivateCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    27	        _payloadDirV1 = Path.Combine(_tempDirectory, "payload_v1");
    28	        _payloadDirV2 = Path.Combine(_tempDirectory, "payload_v2");
    29	
    30	        Directory.CreateDirectory(_tempDirectory);
    31	        Directory.CreateDirectory(_customRoot);
    32	        Directory.CreateDirectory(_userAddInsDir);
```

</details>

## Alert #383 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/383
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:623-623`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   615	                {
   616	                    runtimeMode = "server";
   617	                }
   618	                else
   619	                {
   620	                    runtimeMode = "cli";
   621	                }
   622	            }
   623	            catch { }
   624	        }
   625	
   626	        if (RuntimeCompatibilityRegistry.GetMetadata(defaultRuntime) != null)
   627	        {
   628	            var meta = RuntimeCompatibilityRegistry.GetMetadata(defaultRuntime)!;
   629	            if (!meta.SupportedModes.Contains(runtimeMode))
   630	            {
   631	                runtimeMode = meta.DefaultMode;
```

</details>

## Alert #382 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/382
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:476-476`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   468	        TiaAgentConfig? config = null;
   469	        if (File.Exists(layout.ConfigPath))
   470	        {
   471	            try
   472	            {
   473	                config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   474	                defaultRuntime = config.DefaultRuntime ?? "opencode";
   475	            }
   476	            catch { }
   477	        }
   478	
   479	        var envRuntime = Environment.GetEnvironmentVariable("TIA_AGENT_RUNTIME");
   480	        if (!string.IsNullOrWhiteSpace(envRuntime))
   481	        {
   482	            defaultRuntime = envRuntime;
   483	        }
   484	
```

</details>

## Alert #381 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/381
- Location: `tests/TiaAgent.Cli.Tests/Commands/RuntimeCommandTests.cs:35-35`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 250 lines

<details><summary>Current code context</summary>

```text
    27	    }
    28	
    29	    public void Dispose()
    30	    {
    31	        if (Directory.Exists(_tempDirectory))
    32	        {
    33	            try { Directory.Delete(_tempDirectory, recursive: true); }
    34	            catch (IOException) { }
    35	            catch (UnauthorizedAccessException) { }
    36	        }
    37	        GC.SuppressFinalize(this);
    38	    }
    39	
    40	    [Fact]
    41	    public void RuntimeCommand_List_DisplaysAllRegisteredRuntimes()
    42	    {
    43	        var options = new RuntimeOptions
```

</details>

## Alert #380 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/380
- Location: `tests/TiaAgent.Cli.Tests/Commands/RuntimeCommandTests.cs:34-34`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 250 lines

<details><summary>Current code context</summary>

```text
    26	        Directory.CreateDirectory(_customRoot);
    27	    }
    28	
    29	    public void Dispose()
    30	    {
    31	        if (Directory.Exists(_tempDirectory))
    32	        {
    33	            try { Directory.Delete(_tempDirectory, recursive: true); }
    34	            catch (IOException) { }
    35	            catch (UnauthorizedAccessException) { }
    36	        }
    37	        GC.SuppressFinalize(this);
    38	    }
    39	
    40	    [Fact]
    41	    public void RuntimeCommand_List_DisplaysAllRegisteredRuntimes()
    42	    {
```

</details>

## Alert #379 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/379
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:623-623`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   615	                {
   616	                    runtimeMode = "server";
   617	                }
   618	                else
   619	                {
   620	                    runtimeMode = "cli";
   621	                }
   622	            }
   623	            catch { }
   624	        }
   625	
   626	        if (RuntimeCompatibilityRegistry.GetMetadata(defaultRuntime) != null)
   627	        {
   628	            var meta = RuntimeCompatibilityRegistry.GetMetadata(defaultRuntime)!;
   629	            if (!meta.SupportedModes.Contains(runtimeMode))
   630	            {
   631	                runtimeMode = meta.DefaultMode;
```

</details>

## Alert #378 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T12:08:34Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/378
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:476-476`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   468	        TiaAgentConfig? config = null;
   469	        if (File.Exists(layout.ConfigPath))
   470	        {
   471	            try
   472	            {
   473	                config = ManifestStore.Read<TiaAgentConfig>(layout.ConfigPath);
   474	                defaultRuntime = config.DefaultRuntime ?? "opencode";
   475	            }
   476	            catch { }
   477	        }
   478	
   479	        var envRuntime = Environment.GetEnvironmentVariable("TIA_AGENT_RUNTIME");
   480	        if (!string.IsNullOrWhiteSpace(envRuntime))
   481	        {
   482	            defaultRuntime = envRuntime;
   483	        }
   484	
```

</details>

## Alert #369 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:51:10Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/369
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:809-812`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   801	                        if (errLine != null) await writer.WriteLineAsync(errLine).ConfigureAwait(false);
   802	                    }
   803	                }
   804	                catch { }
   805	            });
   806	
   807	            return proc;
   808	        }
   809	        catch
   810	        {
   811	            return null;
   812	        }
   813	    }
   814	
   815	    private static void StopProcessById(int pid, bool force)
   816	    {
   817	        try
   818	        {
   819	            using var proc = Process.GetProcessById(pid);
   820	            if (!proc.HasExited)
```

</details>

## Alert #368 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:51:10Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/368
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:490-490`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   482	        {
   483	            if (manifest.SupervisorPid > 0)
   484	            {
   485	                try
   486	                {
   487	                    using var proc = Process.GetProcessById(manifest.SupervisorPid);
   488	                    supervisorRunning = !proc.HasExited;
   489	                }
   490	                catch { }
   491	            }
   492	
   493	            if (manifest.Services.Bridge.Pid > 0)
   494	            {
   495	                try
   496	                {
   497	                    using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid);
   498	                    bridgeRunning = !proc.HasExited;
```

</details>

## Alert #367 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:51:10Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/367
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:60-63`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
    52	
    53	        // If mutex is still null after fallback attempts, try once more
    54	        if (mutex == null)
    55	        {
    56	            try
    57	            {
    58	                mutex = new Mutex(false, MutexName, out createdNew);
    59	            }
    60	            catch
    61	            {
    62	                throw new InvalidOperationException("Unable to create supervisor mutex. Another supervisor may be running or the system may be unstable.");
    63	            }
    64	        }
    65	
    66	        var instanceId = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss") + "-" + Random.Shared.Next(1000, 10000);
    67	        var currentPid = Environment.ProcessId;
    68	
    69	        var lockData = new SupervisorLockData
    70	        {
    71	            InstanceId = instanceId,
```

</details>

## Alert #366 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:51:10Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/366
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:490-490`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   482	        {
   483	            if (manifest.SupervisorPid > 0)
   484	            {
   485	                try
   486	                {
   487	                    using var proc = Process.GetProcessById(manifest.SupervisorPid);
   488	                    supervisorRunning = !proc.HasExited;
   489	                }
   490	                catch { }
   491	            }
   492	
   493	            if (manifest.Services.Bridge.Pid > 0)
   494	            {
   495	                try
   496	                {
   497	                    using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid);
   498	                    bridgeRunning = !proc.HasExited;
```

</details>

## Alert #365 — cs/missed-readonly-modifier

- Rule: `cs/missed-readonly-modifier`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/365
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:14-14`
- Message: Field '_hasMutex' can be 'readonly'.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
     6	using TiaAgent.Cli.Layout;
     7	
     8	namespace TiaAgent.Cli.Supervisor;
     9	
    10	public sealed class SupervisorLock : IDisposable
    11	{
    12	    private const string MutexName = @"Local\TiaAgent.Supervisor";
    13	    private Mutex? _mutex;
    14	    private bool _hasMutex;
    15	
    16	    public string InstanceId { get; }
    17	    public string LockFilePath { get; }
    18	
    19	    private SupervisorLock(Mutex mutex, bool hasMutex, string instanceId, string lockFilePath)
    20	    {
    21	        _mutex = mutex;
    22	        _hasMutex = hasMutex;
```

</details>

## Alert #364 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/364
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:55-55`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    47	    public void Dispose()
    48	    {
    49	        var stopOptions = new StopOptions { CustomRoot = _customRoot, Force = true };
    50	        using var sw = new StringWriter();
    51	        StopCommand.Execute(stopOptions, sw, sw);
    52	
    53	        if (Directory.Exists(_tempDirectory))
    54	        {
    55	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    56	        }
    57	        GC.SuppressFinalize(this);
    58	    }
    59	
    60	    [Fact]
    61	    public void StatusCommand_NoManifest_OutputsNotRunningStatus()
    62	    {
    63	        var options = new StatusOptions { CustomRoot = _customRoot };
```

</details>

## Alert #363 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/363
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:804-804`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   796	                    while (!proc.HasExited)
   797	                    {
   798	                        var line = await proc.StandardOutput.ReadLineAsync().ConfigureAwait(false);
   799	                        if (line != null) await writer.WriteLineAsync(line).ConfigureAwait(false);
   800	                        var errLine = await proc.StandardError.ReadLineAsync().ConfigureAwait(false);
   801	                        if (errLine != null) await writer.WriteLineAsync(errLine).ConfigureAwait(false);
   802	                    }
   803	                }
   804	                catch { }
   805	            });
   806	
   807	            return proc;
   808	        }
   809	        catch
   810	        {
   811	            return null;
   812	        }
```

</details>

## Alert #362 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/362
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:762-762`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   754	                while (!proc.HasExited)
   755	                {
   756	                    var line = await proc.StandardOutput.ReadLineAsync().ConfigureAwait(false);
   757	                    if (line != null) await writer.WriteLineAsync(line).ConfigureAwait(false);
   758	                    var errLine = await proc.StandardError.ReadLineAsync().ConfigureAwait(false);
   759	                    if (errLine != null) await writer.WriteLineAsync(errLine).ConfigureAwait(false);
   760	                }
   761	            }
   762	            catch { }
   763	        });
   764	
   765	        return proc;
   766	    }
   767	
   768	    private static Process? StartRuntimeServer(string runtimeId, int port, TiaAgentLayout layout, string logFile, string instanceId)
   769	    {
   770	        var exeName = runtimeId == "opencode" ? "mimo" : runtimeId;
```

</details>

## Alert #361 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/361
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:895-895`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   887	
   888	    private static void LogEvent(string logPath, string instanceId, string level, string eventName, string message)
   889	    {
   890	        try
   891	        {
   892	            var logEntry = $"{DateTime.UtcNow:o} [{level}] [{instanceId}] {eventName}: {message}{Environment.NewLine}";
   893	            File.AppendAllText(logPath, logEntry, Encoding.UTF8);
   894	        }
   895	        catch { }
   896	    }
   897	}
```

</details>

## Alert #360 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/360
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:879-879`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   871	                if (manifest.Services.Bridge.Pid > 0)
   872	                {
   873	                    try { using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid); }
   874	                    catch { manifest.Services.Bridge.Pid = 0; manifest.Services.Bridge.Status = "stopped"; }
   875	                }
   876	                if (manifest.Services.OpenCode.Pid > 0)
   877	                {
   878	                    try { using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid); }
   879	                    catch { manifest.Services.OpenCode.Pid = 0; manifest.Services.OpenCode.Status = "stopped"; }
   880	                }
   881	                manifest.Status = "stopped";
   882	                ManifestStore.WriteAtomic(manifestPath, manifest);
   883	            }
   884	            catch { }
   885	        }
   886	    }
   887	
```

</details>

## Alert #359 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/359
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:874-874`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   866	        if (File.Exists(manifestPath))
   867	        {
   868	            try
   869	            {
   870	                var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   871	                if (manifest.Services.Bridge.Pid > 0)
   872	                {
   873	                    try { using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid); }
   874	                    catch { manifest.Services.Bridge.Pid = 0; manifest.Services.Bridge.Status = "stopped"; }
   875	                }
   876	                if (manifest.Services.OpenCode.Pid > 0)
   877	                {
   878	                    try { using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid); }
   879	                    catch { manifest.Services.OpenCode.Pid = 0; manifest.Services.OpenCode.Status = "stopped"; }
   880	                }
   881	                manifest.Status = "stopped";
   882	                ManifestStore.WriteAtomic(manifestPath, manifest);
```

</details>

## Alert #358 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/358
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:884-884`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   876	                if (manifest.Services.OpenCode.Pid > 0)
   877	                {
   878	                    try { using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid); }
   879	                    catch { manifest.Services.OpenCode.Pid = 0; manifest.Services.OpenCode.Status = "stopped"; }
   880	                }
   881	                manifest.Status = "stopped";
   882	                ManifestStore.WriteAtomic(manifestPath, manifest);
   883	            }
   884	            catch { }
   885	        }
   886	    }
   887	
   888	    private static void LogEvent(string logPath, string instanceId, string level, string eventName, string message)
   889	    {
   890	        try
   891	        {
   892	            var logEntry = $"{DateTime.UtcNow:o} [{level}] [{instanceId}] {eventName}: {message}{Environment.NewLine}";
```

</details>

## Alert #357 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/357
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:858-858`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
   852	    {
   853	        var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   854	        if (Directory.Exists(secretsDir))
   855	        {
   856	            foreach (var file in Directory.GetFiles(secretsDir))
   857	            {
   858	                try { File.Delete(file); } catch { }
   859	            }
   860	        }
   861	    }
   862	
   863	    private static void CleanStaleRuntime(TiaAgentLayout layout, string instanceId)
   864	    {
   865	        var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   866	        if (File.Exists(manifestPath))
```

</details>

## Alert #356 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/356
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:847-847`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
   845	        if (bridge != null && !bridge.HasExited)
   846	        {
   847	            try { bridge.Kill(); } catch { }
   848	        }
   849	    }
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
   852	    {
   853	        var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   854	        if (Directory.Exists(secretsDir))
   855	        {
```

</details>

## Alert #355 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/355
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:843-843`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   835	        }
   836	        catch { }
   837	    }
   838	
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
   845	        if (bridge != null && !bridge.HasExited)
   846	        {
   847	            try { bridge.Kill(); } catch { }
   848	        }
   849	    }
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
```

</details>

## Alert #354 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/354
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:836-836`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   828	                    proc.CloseMainWindow();
   829	                    if (!proc.WaitForExit(5000))
   830	                    {
   831	                        proc.Kill();
   832	                    }
   833	                }
   834	            }
   835	        }
   836	        catch { }
   837	    }
   838	
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
```

</details>

## Alert #352 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/352
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:697-697`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   689	                {
   690	                    var versionPath = layout.GetVersionPath(current.ActiveVersion);
   691	                    var installedDll = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.dll");
   692	                    if (File.Exists(installedDll)) return installedDll;
   693	                    var installedExe = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.exe");
   694	                    if (File.Exists(installedExe)) return installedExe;
   695	                }
   696	            }
   697	            catch { }
   698	        }
   699	
   700	        // 2. Check repo root if passed or detected
   701	        if (!string.IsNullOrWhiteSpace(repoRoot))
   702	        {
   703	            var relDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll");
   704	            if (File.Exists(relDll)) return relDll;
   705	            var dbgDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll");
```

</details>

## Alert #351 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/351
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:666-666`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   658	                    if (portsProp.TryGetProperty("opencode", out var oProp) && oProp.TryGetInt32(out var oVal)) prefRuntime = oVal;
   659	                }
   660	                if (doc.RootElement.TryGetProperty("portRange", out var rangeProp))
   661	                {
   662	                    if (rangeProp.TryGetProperty("start", out var sProp) && sProp.TryGetInt32(out var sVal)) rangeStart = sVal;
   663	                    if (rangeProp.TryGetProperty("end", out var eProp) && eProp.TryGetInt32(out var eVal)) rangeEnd = eVal;
   664	                }
   665	            }
   666	            catch { }
   667	        }
   668	
   669	        return (prefBridge, prefRuntime, rangeStart, rangeEnd);
   670	    }
   671	
   672	    private static string GetRuntimeDisplayName(string runtimeId) => runtimeId switch
   673	    {
   674	        "mimo" => "Mimo CLI",
```

</details>

## Alert #349 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/349
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:515-515`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   507	
   508	            if (manifest.Services.OpenCode.Pid > 0)
   509	            {
   510	                try
   511	                {
   512	                    using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid);
   513	                    opencodeRunning = !proc.HasExited;
   514	                }
   515	                catch { }
   516	
   517	                if (opencodeRunning && !string.IsNullOrEmpty(manifest.Services.OpenCode.HealthUrl))
   518	                {
   519	                    opencodeHealthy = await HealthChecker.IsHealthyAsync(manifest.Services.OpenCode.HealthUrl, manifest.Services.OpenCode.Port).ConfigureAwait(false);
   520	                }
   521	            }
   522	        }
   523	
```

</details>

## Alert #348 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/348
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:500-500`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   492	
   493	            if (manifest.Services.Bridge.Pid > 0)
   494	            {
   495	                try
   496	                {
   497	                    using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid);
   498	                    bridgeRunning = !proc.HasExited;
   499	                }
   500	                catch { }
   501	
   502	                if (bridgeRunning && !string.IsNullOrEmpty(manifest.Services.Bridge.HealthUrl))
   503	                {
   504	                    bridgeHealthy = await HealthChecker.IsHealthyAsync(manifest.Services.Bridge.HealthUrl).ConfigureAwait(false);
   505	                }
   506	            }
   507	
   508	            if (manifest.Services.OpenCode.Pid > 0)
```

</details>

## Alert #346 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/346
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:460-472`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   452	
   453	        RuntimeManifest? manifest = null;
   454	        if (File.Exists(manifestPath))
   455	        {
   456	            try
   457	            {
   458	                manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   459	            }
   460	            catch (Exception ex)
   461	            {
   462	                if (options.Json)
   463	                {
   464	                    var errObj = new { error = $"Failed to parse runtime manifest: {ex.Message}" };
   465	                    stdout.WriteLine(JsonSerializer.Serialize(errObj, s_jsonOptions));
   466	                }
   467	                else
   468	                {
   469	                    stderr.WriteLine($"Failed to parse runtime manifest: {ex.Message}");
   470	                }
   471	                return 1;
   472	            }
   473	        }
   474	
   475	        bool supervisorRunning = false;
   476	        bool bridgeRunning = false;
   477	        bool bridgeHealthy = false;
   478	        bool opencodeRunning = false;
   479	        bool opencodeHealthy = false;
   480	
```

</details>

## Alert #345 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/345
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:422-422`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   414	            StopProcessById(manifest.Services.Bridge.Pid, options.Force);
   415	        }
   416	
   417	        // Clean secrets and lock
   418	        CleanSecrets(layout);
   419	        var lockFilePath = Path.Combine(layout.RuntimePath, "supervisor.lock");
   420	        if (File.Exists(lockFilePath))
   421	        {
   422	            try { File.Delete(lockFilePath); } catch { }
   423	        }
   424	
   425	        manifest.Status = "stopped";
   426	        manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   427	        ManifestStore.WriteAtomic(manifestPath, manifest);
   428	
   429	        stdout.WriteLine();
   430	        stdout.WriteLine("======================================");
```

</details>

## Alert #344 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/344
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:384-388`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   376	            return 0;
   377	        }
   378	
   379	        RuntimeManifest manifest;
   380	        try
   381	        {
   382	            manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   383	        }
   384	        catch (Exception ex)
   385	        {
   386	            stderr.WriteLine($"Failed to parse runtime manifest: {ex.Message}");
   387	            return 1;
   388	        }
   389	
   390	        if (string.Equals(manifest.Status, "stopped", StringComparison.OrdinalIgnoreCase))
   391	        {
   392	            stdout.WriteLine("Runtime already stopped.");
   393	            return 0;
   394	        }
   395	
   396	        stdout.WriteLine($"Instance: {manifest.InstanceId}");
```

</details>

## Alert #343 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/343
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:353-353`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   345	                if (File.Exists(manifestPath))
   346	                {
   347	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   348	                    manifest.Status = "stopped";
   349	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   350	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   351	                }
   352	            }
   353	            catch { }
   354	
   355	            CleanSecrets(layout);
   356	        }
   357	    }
   358	
   359	    public static int Stop(
   360	        StopOptions options,
   361	        TextWriter stdout,
```

</details>

## Alert #342 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/342
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:332-332`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   324	                if (File.Exists(manifestPath))
   325	                {
   326	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   327	                    manifest.Status = "failed";
   328	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   329	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   330	                }
   331	            }
   332	            catch { }
   333	
   334	            return 1;
   335	        }
   336	        finally
   337	        {
   338	            // Cleanup on shutdown
   339	            stdout.WriteLine("Cleaning up services...");
   340	            StopProcesses(bridgeProcess, runtimeProcess);
```

</details>

## Alert #341 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/341
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:315-335`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   307	                    break;
   308	                }
   309	            }
   310	
   311	            stdout.WriteLine();
   312	            stdout.WriteLine("Shutdown requested...");
   313	            return 0;
   314	        }
   315	        catch (Exception ex)
   316	        {
   317	            stderr.WriteLine();
   318	            stderr.WriteLine($"FAILED: {ex.Message}");
   319	            LogEvent(Path.Combine(layout.LogsPath, "supervisor.log"), instanceId, "ERROR", "startup_error", ex.Message);
   320	
   321	            try
   322	            {
   323	                var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   324	                if (File.Exists(manifestPath))
   325	                {
   326	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   327	                    manifest.Status = "failed";
   328	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   329	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   330	                }
   331	            }
   332	            catch { }
   333	
   334	            return 1;
   335	        }
   336	        finally
   337	        {
   338	            // Cleanup on shutdown
   339	            stdout.WriteLine("Cleaning up services...");
   340	            StopProcesses(bridgeProcess, runtimeProcess);
   341	
   342	            try
   343	            {
```

</details>

## Alert #340 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/340
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:49-53`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
    41	
    42	        SupervisorLock lockHandle;
    43	        try
    44	        {
    45	            stdout.WriteLine("[1/14] Acquiring supervisor mutex...");
    46	            lockHandle = SupervisorLock.Acquire(layout);
    47	            stdout.WriteLine($"  Instance: {lockHandle.InstanceId}");
    48	        }
    49	        catch (Exception ex)
    50	        {
    51	            stderr.WriteLine($"FAILED: {ex.Message}");
    52	            return 1;
    53	        }
    54	
    55	        using var supLock = lockHandle;
    56	        var instanceId = lockHandle.InstanceId;
    57	        var currentPid = Environment.ProcessId;
    58	
    59	        Process? bridgeProcess = null;
    60	        Process? runtimeProcess = null;
    61	
```

</details>

## Alert #339 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/339
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:145-145`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
   145	            try { _mutex.Dispose(); } catch { }
   146	            _mutex = null;
   147	        }
   148	    }
   149	}
```

</details>

## Alert #338 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/338
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:143-143`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   135	        {
   136	            try { File.Delete(LockFilePath); } catch { }
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
   145	            try { _mutex.Dispose(); } catch { }
   146	            _mutex = null;
   147	        }
   148	    }
   149	}
```

</details>

## Alert #337 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/337
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:136-136`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   128	            }
   129	        }
   130	    }
   131	
   132	    public void Dispose()
   133	    {
   134	        if (File.Exists(LockFilePath))
   135	        {
   136	            try { File.Delete(LockFilePath); } catch { }
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
```

</details>

## Alert #336 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/336
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:127-127`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   119	
   120	            if (mutex != null && !createdNew)
   121	            {
   122	                try
   123	                {
   124	                    mutex.Dispose();
   125	                    mutex = new Mutex(false, MutexName, out createdNew);
   126	                }
   127	                catch { }
   128	            }
   129	        }
   130	    }
   131	
   132	    public void Dispose()
   133	    {
   134	        if (File.Exists(LockFilePath))
   135	        {
```

</details>

## Alert #335 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/335
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:118-118`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   110	                throw;
   111	            }
   112	            catch
   113	            {
   114	                // Unparseable or stale lock file
   115	            }
   116	
   117	            // Stale lock — remove file
   118	            try { File.Delete(lockFilePath); } catch { }
   119	
   120	            if (mutex != null && !createdNew)
   121	            {
   122	                try
   123	                {
   124	                    mutex.Dispose();
   125	                    mutex = new Mutex(false, MutexName, out createdNew);
   126	                }
```

</details>

## Alert #334 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/334
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:112-115`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   104	                        // Process is dead — stale lock
   105	                    }
   106	                }
   107	            }
   108	            catch (InvalidOperationException)
   109	            {
   110	                throw;
   111	            }
   112	            catch
   113	            {
   114	                // Unparseable or stale lock file
   115	            }
   116	
   117	            // Stale lock — remove file
   118	            try { File.Delete(lockFilePath); } catch { }
   119	
   120	            if (mutex != null && !createdNew)
   121	            {
   122	                try
   123	                {
```

</details>

## Alert #333 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/333
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:39-42`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
    31	
    32	        Mutex? mutex = null;
    33	        bool createdNew = false;
    34	
    35	        try
    36	        {
    37	            mutex = new Mutex(false, MutexName, out createdNew);
    38	        }
    39	        catch
    40	        {
    41	            // On platform where named mutex fails, fallback to file-locking check
    42	        }
    43	
    44	        if (mutex != null && !createdNew)
    45	        {
    46	            CheckAndCleanStaleLock(lockFilePath, ref mutex, ref createdNew);
    47	        }
    48	        else if (mutex == null)
    49	        {
    50	            CheckAndCleanStaleLock(lockFilePath, ref mutex, ref createdNew);
```

</details>

## Alert #332 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/332
- Location: `src/TiaAgent.Cli/Supervisor/PortAllocator.cs:24-27`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 47 lines

<details><summary>Current code context</summary>

```text
    16	        if (port <= 0 || port > 65535) return false;
    17	        try
    18	        {
    19	            using var listener = new TcpListener(IPAddress.Loopback, port);
    20	            listener.Start();
    21	            listener.Stop();
    22	            return true;
    23	        }
    24	        catch
    25	        {
    26	            return false;
    27	        }
    28	    }
    29	
    30	    public static int GetAvailablePort(int preferredPort, int rangeStart = DefaultRangeStart, int rangeEnd = DefaultRangeEnd)
    31	    {
    32	        if (preferredPort > 0 && IsPortAvailable(preferredPort))
    33	        {
    34	            return preferredPort;
    35	        }
```

</details>

## Alert #331 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/331
- Location: `src/TiaAgent.Cli/Supervisor/HealthChecker.cs:106-106`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 110 lines

<details><summary>Current code context</summary>

```text
    98	            var delayTask = Task.Delay(1000, cancellationToken);
    99	            var completed = await Task.WhenAny(connectTask, delayTask).ConfigureAwait(false);
   100	
   101	            if (completed == connectTask && client.Connected)
   102	            {
   103	                return true;
   104	            }
   105	        }
   106	        catch { }
   107	
   108	        return false;
   109	    }
   110	}
```

</details>

## Alert #330 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/330
- Location: `src/TiaAgent.Cli/Supervisor/HealthChecker.cs:38-42`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 110 lines

<details><summary>Current code context</summary>

```text
    30	                {
    31	                    using var doc = JsonDocument.Parse(content);
    32	                    if (doc.RootElement.TryGetProperty("status", out var statusProp))
    33	                    {
    34	                        var statusStr = statusProp.GetString()?.ToLowerInvariant();
    35	                        if (statusStr == "healthy" || statusStr == "ok") return true;
    36	                    }
    37	                }
    38	                catch
    39	                {
    40	                    // Body not JSON, HTTP 2xx status code is sufficient
    41	                    return true;
    42	                }
    43	
    44	                return true;
    45	            }
    46	            else if ((int)response.StatusCode >= 500 && tcpPortFallback > 0)
    47	            {
    48	                // Fallback TCP port check for non-2xx responses (like mimo serve 503 when Web UI is absent)
    49	                return await IsTcpPortOpenAsync(tcpPortFallback, cancellationToken).ConfigureAwait(false);
    50	            }
```

</details>

## Alert #329 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/329
- Location: `src/TiaAgent.Cli/Supervisor/HealthChecker.cs:52-58`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 110 lines

<details><summary>Current code context</summary>

```text
    44	                return true;
    45	            }
    46	            else if ((int)response.StatusCode >= 500 && tcpPortFallback > 0)
    47	            {
    48	                // Fallback TCP port check for non-2xx responses (like mimo serve 503 when Web UI is absent)
    49	                return await IsTcpPortOpenAsync(tcpPortFallback, cancellationToken).ConfigureAwait(false);
    50	            }
    51	        }
    52	        catch
    53	        {
    54	            if (tcpPortFallback > 0)
    55	            {
    56	                return await IsTcpPortOpenAsync(tcpPortFallback, cancellationToken).ConfigureAwait(false);
    57	            }
    58	        }
    59	
    60	        return false;
    61	    }
    62	
    63	    public static async Task<bool> WaitUntilHealthyAsync(
    64	        string healthUrl,
    65	        int timeoutSeconds = 30,
    66	        int retryIntervalMs = 500,
```

</details>

## Alert #328 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/328
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:117-117`
- Message: This assignment to exitCode is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
   109	        var options = new StartOptions
   110	        {
   111	            CustomRoot = _customRoot,
   112	            NoMonitor = true
   113	        };
   114	        using var stdout = new StringWriter();
   115	        using var stderr = new StringWriter();
   116	
   117	        var exitCode = StartCommand.Execute(options, stdout, stderr);
   118	
   119	        var manifestPath = Path.Combine(_customRoot, "runtime", "runtime.json");
   120	        File.Exists(manifestPath).Should().BeTrue();
   121	
   122	        var json = File.ReadAllText(manifestPath);
   123	        json.Should().Contain("instanceId");
   124	    }
   125	
```

</details>

## Alert #327 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/327
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:119-119`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
   111	            CustomRoot = _customRoot,
   112	            NoMonitor = true
   113	        };
   114	        using var stdout = new StringWriter();
   115	        using var stderr = new StringWriter();
   116	
   117	        var exitCode = StartCommand.Execute(options, stdout, stderr);
   118	
   119	        var manifestPath = Path.Combine(_customRoot, "runtime", "runtime.json");
   120	        File.Exists(manifestPath).Should().BeTrue();
   121	
   122	        var json = File.ReadAllText(manifestPath);
   123	        json.Should().Contain("instanceId");
   124	    }
   125	
   126	    [Fact]
   127	    public void PortAllocator_ReturnsAvailablePortInRange()
```

</details>

## Alert #326 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/326
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:44-44`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    36	            "opencode": 43191
    37	          },
    38	          "portRange": {
    39	            "start": 43190,
    40	            "end": 43199
    41	          }
    42	        }
    43	        """;
    44	        File.WriteAllText(Path.Combine(configDir, "settings.json"), settingsJson);
    45	    }
    46	
    47	    public void Dispose()
    48	    {
    49	        var stopOptions = new StopOptions { CustomRoot = _customRoot, Force = true };
    50	        using var sw = new StringWriter();
    51	        StopCommand.Execute(stopOptions, sw, sw);
    52	
```

</details>

## Alert #325 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/325
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:30-30`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    22	    public SupervisorCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "SupervisorCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	
    27	        Directory.CreateDirectory(_tempDirectory);
    28	        Directory.CreateDirectory(_customRoot);
    29	
    30	        var configDir = Path.Combine(_customRoot, "config");
    31	        Directory.CreateDirectory(configDir);
    32	        var settingsJson = """
    33	        {
    34	          "preferredPorts": {
    35	            "bridge": 43190,
    36	            "opencode": 43191
    37	          },
    38	          "portRange": {
```

</details>

## Alert #324 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/324
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:25-25`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    17	    private static readonly string[] s_statusHelpArgs = ["status", "--help"];
    18	
    19	    private readonly string _tempDirectory;
    20	    private readonly string _customRoot;
    21	
    22	    public SupervisorCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "SupervisorCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	
    27	        Directory.CreateDirectory(_tempDirectory);
    28	        Directory.CreateDirectory(_customRoot);
    29	
    30	        var configDir = Path.Combine(_customRoot, "config");
    31	        Directory.CreateDirectory(configDir);
    32	        var settingsJson = """
    33	        {
```

</details>

## Alert #323 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/323
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    16	    private static readonly string[] s_stopHelpArgs = ["stop", "--help"];
    17	    private static readonly string[] s_statusHelpArgs = ["status", "--help"];
    18	
    19	    private readonly string _tempDirectory;
    20	    private readonly string _customRoot;
    21	
    22	    public SupervisorCommandTests()
    23	    {
    24	        _tempDirectory = Path.Combine(Path.GetTempPath(), "SupervisorCommandTests_" + Guid.NewGuid().ToString("N"));
    25	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    26	
    27	        Directory.CreateDirectory(_tempDirectory);
    28	        Directory.CreateDirectory(_customRoot);
    29	
    30	        var configDir = Path.Combine(_customRoot, "config");
    31	        Directory.CreateDirectory(configDir);
    32	        var settingsJson = """
```

</details>

## Alert #322 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/322
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:865-865`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   857	            {
   858	                try { File.Delete(file); } catch { }
   859	            }
   860	        }
   861	    }
   862	
   863	    private static void CleanStaleRuntime(TiaAgentLayout layout, string instanceId)
   864	    {
   865	        var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   866	        if (File.Exists(manifestPath))
   867	        {
   868	            try
   869	            {
   870	                var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   871	                if (manifest.Services.Bridge.Pid > 0)
   872	                {
   873	                    try { using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid); }
```

</details>

## Alert #321 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/321
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:853-853`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   845	        if (bridge != null && !bridge.HasExited)
   846	        {
   847	            try { bridge.Kill(); } catch { }
   848	        }
   849	    }
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
   852	    {
   853	        var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   854	        if (Directory.Exists(secretsDir))
   855	        {
   856	            foreach (var file in Directory.GetFiles(secretsDir))
   857	            {
   858	                try { File.Delete(file); } catch { }
   859	            }
   860	        }
   861	    }
```

</details>

## Alert #320 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/320
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:775-775`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   767	
   768	    private static Process? StartRuntimeServer(string runtimeId, int port, TiaAgentLayout layout, string logFile, string instanceId)
   769	    {
   770	        var exeName = runtimeId == "opencode" ? "mimo" : runtimeId;
   771	        var psi = new ProcessStartInfo
   772	        {
   773	            FileName = exeName,
   774	            Arguments = $"serve --port {port}",
   775	            WorkingDirectory = Path.Combine(layout.RuntimePath, $"{runtimeId}-workdir"),
   776	            UseShellExecute = false,
   777	            RedirectStandardOutput = true,
   778	            RedirectStandardError = true,
   779	            StandardOutputEncoding = System.Text.Encoding.UTF8,
   780	            StandardErrorEncoding = System.Text.Encoding.UTF8,
   781	            CreateNoWindow = true
   782	        };
   783	        Directory.CreateDirectory(psi.WorkingDirectory);
```

</details>

## Alert #319 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/319
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:717-717`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   709	        // 3. Check relative to CLI application directory
   710	        var baseDir = AppContext.BaseDirectory;
   711	        var nextToCli = Path.Combine(baseDir, "TiaAgent.Bridge.dll");
   712	        if (File.Exists(nextToCli)) return nextToCli;
   713	
   714	        var repoDevRel = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll"));
   715	        if (File.Exists(repoDevRel)) return repoDevRel;
   716	
   717	        var repoDevDbg = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll"));
   718	        if (File.Exists(repoDevDbg)) return repoDevDbg;
   719	
   720	        return nextToCli;
   721	    }
   722	
   723	    private static Process StartProcess(string executableOrDll, string logFile, string instanceId, string? workingDir)
   724	    {
   725	        var psi = new ProcessStartInfo();
```

</details>

## Alert #318 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/318
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:714-714`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   706	            if (File.Exists(dbgDll)) return dbgDll;
   707	        }
   708	
   709	        // 3. Check relative to CLI application directory
   710	        var baseDir = AppContext.BaseDirectory;
   711	        var nextToCli = Path.Combine(baseDir, "TiaAgent.Bridge.dll");
   712	        if (File.Exists(nextToCli)) return nextToCli;
   713	
   714	        var repoDevRel = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll"));
   715	        if (File.Exists(repoDevRel)) return repoDevRel;
   716	
   717	        var repoDevDbg = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll"));
   718	        if (File.Exists(repoDevDbg)) return repoDevDbg;
   719	
   720	        return nextToCli;
   721	    }
   722	
```

</details>

## Alert #317 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/317
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:711-711`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   703	            var relDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll");
   704	            if (File.Exists(relDll)) return relDll;
   705	            var dbgDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll");
   706	            if (File.Exists(dbgDll)) return dbgDll;
   707	        }
   708	
   709	        // 3. Check relative to CLI application directory
   710	        var baseDir = AppContext.BaseDirectory;
   711	        var nextToCli = Path.Combine(baseDir, "TiaAgent.Bridge.dll");
   712	        if (File.Exists(nextToCli)) return nextToCli;
   713	
   714	        var repoDevRel = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll"));
   715	        if (File.Exists(repoDevRel)) return repoDevRel;
   716	
   717	        var repoDevDbg = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll"));
   718	        if (File.Exists(repoDevDbg)) return repoDevDbg;
   719	
```

</details>

## Alert #316 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/316
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:705-705`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   697	            catch { }
   698	        }
   699	
   700	        // 2. Check repo root if passed or detected
   701	        if (!string.IsNullOrWhiteSpace(repoRoot))
   702	        {
   703	            var relDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll");
   704	            if (File.Exists(relDll)) return relDll;
   705	            var dbgDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll");
   706	            if (File.Exists(dbgDll)) return dbgDll;
   707	        }
   708	
   709	        // 3. Check relative to CLI application directory
   710	        var baseDir = AppContext.BaseDirectory;
   711	        var nextToCli = Path.Combine(baseDir, "TiaAgent.Bridge.dll");
   712	        if (File.Exists(nextToCli)) return nextToCli;
   713	
```

</details>

## Alert #315 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/315
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:703-703`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   695	                }
   696	            }
   697	            catch { }
   698	        }
   699	
   700	        // 2. Check repo root if passed or detected
   701	        if (!string.IsNullOrWhiteSpace(repoRoot))
   702	        {
   703	            var relDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll");
   704	            if (File.Exists(relDll)) return relDll;
   705	            var dbgDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll");
   706	            if (File.Exists(dbgDll)) return dbgDll;
   707	        }
   708	
   709	        // 3. Check relative to CLI application directory
   710	        var baseDir = AppContext.BaseDirectory;
   711	        var nextToCli = Path.Combine(baseDir, "TiaAgent.Bridge.dll");
```

</details>

## Alert #314 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/314
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:693-693`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   685	            try
   686	            {
   687	                var current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   688	                if (!string.IsNullOrWhiteSpace(current.ActiveVersion))
   689	                {
   690	                    var versionPath = layout.GetVersionPath(current.ActiveVersion);
   691	                    var installedDll = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.dll");
   692	                    if (File.Exists(installedDll)) return installedDll;
   693	                    var installedExe = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.exe");
   694	                    if (File.Exists(installedExe)) return installedExe;
   695	                }
   696	            }
   697	            catch { }
   698	        }
   699	
   700	        // 2. Check repo root if passed or detected
   701	        if (!string.IsNullOrWhiteSpace(repoRoot))
```

</details>

## Alert #313 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/313
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:691-691`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   683	        if (File.Exists(layout.CurrentManifestPath))
   684	        {
   685	            try
   686	            {
   687	                var current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
   688	                if (!string.IsNullOrWhiteSpace(current.ActiveVersion))
   689	                {
   690	                    var versionPath = layout.GetVersionPath(current.ActiveVersion);
   691	                    var installedDll = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.dll");
   692	                    if (File.Exists(installedDll)) return installedDll;
   693	                    var installedExe = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.exe");
   694	                    if (File.Exists(installedExe)) return installedExe;
   695	                }
   696	            }
   697	            catch { }
   698	        }
   699	
```

</details>

## Alert #312 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/312
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:642-642`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   634	
   635	        return (defaultRuntime, runtimeMode);
   636	    }
   637	
   638	    private static (int PreferredBridgePort, int PreferredRuntimePort, int RangeStart, int RangeEnd) LoadSettings(TiaAgentLayout layout, string? customConfigPath)
   639	    {
   640	        var path = !string.IsNullOrWhiteSpace(customConfigPath)
   641	            ? customConfigPath
   642	            : Path.Combine(layout.RootPath, "config", "settings.json");
   643	
   644	        int prefBridge = PortAllocator.DefaultBridgePort;
   645	        int prefRuntime = PortAllocator.DefaultRuntimePort;
   646	        int rangeStart = PortAllocator.DefaultRangeStart;
   647	        int rangeEnd = PortAllocator.DefaultRangeEnd;
   648	
   649	        if (File.Exists(path))
   650	        {
```

</details>

## Alert #311 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/311
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:451-451`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   443	    }
   444	
   445	    public static async Task<int> GetStatusAsync(
   446	        StatusOptions options,
   447	        TextWriter stdout,
   448	        TextWriter stderr)
   449	    {
   450	        var layout = new TiaAgentLayout(options.CustomRoot);
   451	        var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   452	
   453	        RuntimeManifest? manifest = null;
   454	        if (File.Exists(manifestPath))
   455	        {
   456	            try
   457	            {
   458	                manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   459	            }
```

</details>

## Alert #310 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/310
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:419-419`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   411	        if (manifest.Services.Bridge.Pid > 0)
   412	        {
   413	            stdout.WriteLine($"Stopping Bridge (PID: {manifest.Services.Bridge.Pid})...");
   414	            StopProcessById(manifest.Services.Bridge.Pid, options.Force);
   415	        }
   416	
   417	        // Clean secrets and lock
   418	        CleanSecrets(layout);
   419	        var lockFilePath = Path.Combine(layout.RuntimePath, "supervisor.lock");
   420	        if (File.Exists(lockFilePath))
   421	        {
   422	            try { File.Delete(lockFilePath); } catch { }
   423	        }
   424	
   425	        manifest.Status = "stopped";
   426	        manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   427	        ManifestStore.WriteAtomic(manifestPath, manifest);
```

</details>

## Alert #309 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/309
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:365-365`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   357	    }
   358	
   359	    public static int Stop(
   360	        StopOptions options,
   361	        TextWriter stdout,
   362	        TextWriter stderr)
   363	    {
   364	        var layout = new TiaAgentLayout(options.CustomRoot);
   365	        var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   366	
   367	        stdout.WriteLine();
   368	        stdout.WriteLine("======================================");
   369	        stdout.WriteLine("  TIA Agent Runtime Shutdown");
   370	        stdout.WriteLine("======================================");
   371	        stdout.WriteLine();
   372	
   373	        if (!File.Exists(manifestPath))
```

</details>

## Alert #308 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/308
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:344-344`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   336	        finally
   337	        {
   338	            // Cleanup on shutdown
   339	            stdout.WriteLine("Cleaning up services...");
   340	            StopProcesses(bridgeProcess, runtimeProcess);
   341	
   342	            try
   343	            {
   344	                var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   345	                if (File.Exists(manifestPath))
   346	                {
   347	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   348	                    manifest.Status = "stopped";
   349	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   350	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   351	                }
   352	            }
```

</details>

## Alert #307 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/307
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:278-278`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   270	            // Step 14: Monitoring loop
   271	            if (options.NoMonitor)
   272	            {
   273	                stdout.WriteLine("Exiting (NoMonitor mode)...");
   274	                return 0;
   275	            }
   276	
   277	            stdout.WriteLine("Monitoring services (Ctrl+C to stop)...");
   278	            var supLogPath = Path.Combine(layout.LogsPath, "supervisor.log");
   279	
   280	            while (!cancellationToken.IsCancellationRequested)
   281	            {
   282	                if (bridgeProcess != null && bridgeProcess.HasExited)
   283	                {
   284	                    LogEvent(supLogPath, instanceId, "ERROR", "bridge_exited", $"Bridge exited with code {bridgeProcess.ExitCode}");
   285	                    manifest.Status = "degraded";
   286	                    manifest.Services.Bridge.Status = "failed";
```

</details>

## Alert #306 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/306
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:201-201`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   193	            manifest.Services.Bridge.Status = "healthy";
   194	            ManifestStore.WriteAtomic(manifestPath, manifest);
   195	            stdout.WriteLine("  Bridge healthy");
   196	
   197	            // Step 10: Start Runtime Server if needed
   198	            if (runtimeNeedsServer)
   199	            {
   200	                stdout.WriteLine($"[10/14] Starting runtime server ({defaultRuntime})...");
   201	                var runtimeLog = Path.Combine(layout.LogsPath, $"{defaultRuntime}.log");
   202	                runtimeProcess = StartRuntimeServer(defaultRuntime, runtimePort, layout, runtimeLog, instanceId);
   203	                if (runtimeProcess != null)
   204	                {
   205	                    manifest.Services.OpenCode.Pid = runtimeProcess.Id;
   206	                    manifest.Services.OpenCode.Status = "starting";
   207	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   208	                    stdout.WriteLine($"  Runtime server started (PID: {runtimeProcess.Id})");
   209	                }
```

</details>

## Alert #305 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/305
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:171-171`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   163	                ["maxConcurrentTasks"] = 5,
   164	                ["maxRequestBodyBytes"] = 1048576
   165	            };
   166	            var bridgeConfigJson = JsonSerializer.Serialize(bridgeConfig, s_jsonOptions);
   167	            File.WriteAllText(Path.Combine(layout.RootPath, "bridge.json"), bridgeConfigJson, Encoding.UTF8);
   168	
   169	            // Step 8: Start Bridge process
   170	            stdout.WriteLine("[8/14] Starting Bridge...");
   171	            var bridgeLog = Path.Combine(layout.LogsPath, "bridge.log");
   172	            bridgeProcess = StartProcess(bridgePath, bridgeLog, instanceId, options.RepoRoot);
   173	            manifest.Services.Bridge.Pid = bridgeProcess.Id;
   174	            ManifestStore.WriteAtomic(manifestPath, manifest);
   175	            stdout.WriteLine($"  Bridge started (PID: {bridgeProcess.Id})");
   176	
   177	            // Step 9: Wait for Bridge health
   178	            stdout.WriteLine("[9/14] Waiting for Bridge health...");
   179	            bool bridgeHealthy = await HealthChecker.WaitUntilHealthyAsync(
```

</details>

## Alert #304 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/304
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:167-167`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   159	            {
   160	                ["port"] = bridgePort,
   161	                ["openCodeBaseUrl"] = runtimeNeedsServer ? $"http://127.0.0.1:{runtimePort}" : string.Empty,
   162	                ["taskTimeoutSeconds"] = 300,
   163	                ["maxConcurrentTasks"] = 5,
   164	                ["maxRequestBodyBytes"] = 1048576
   165	            };
   166	            var bridgeConfigJson = JsonSerializer.Serialize(bridgeConfig, s_jsonOptions);
   167	            File.WriteAllText(Path.Combine(layout.RootPath, "bridge.json"), bridgeConfigJson, Encoding.UTF8);
   168	
   169	            // Step 8: Start Bridge process
   170	            stdout.WriteLine("[8/14] Starting Bridge...");
   171	            var bridgeLog = Path.Combine(layout.LogsPath, "bridge.log");
   172	            bridgeProcess = StartProcess(bridgePath, bridgeLog, instanceId, options.RepoRoot);
   173	            manifest.Services.Bridge.Pid = bridgeProcess.Id;
   174	            ManifestStore.WriteAtomic(manifestPath, manifest);
   175	            stdout.WriteLine($"  Bridge started (PID: {bridgeProcess.Id})");
```

</details>

## Alert #303 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/303
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:112-112`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   104	            var tokenBytes = new byte[32];
   105	            RandomNumberGenerator.Fill(tokenBytes);
   106	            var mcpToken = Convert.ToBase64String(tokenBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
   107	            File.WriteAllText(Path.Combine(secretsDir, "mcp.token"), mcpToken, Encoding.UTF8);
   108	            stdout.WriteLine("  MCP token generated");
   109	
   110	            // Step 6: Publish initial manifest
   111	            stdout.WriteLine("[6/14] Publishing runtime manifest...");
   112	            var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   113	            var runtimeDisplayName = GetRuntimeDisplayName(defaultRuntime);
   114	
   115	            var manifest = new RuntimeManifest
   116	            {
   117	                SchemaVersion = 1,
   118	                InstanceId = instanceId,
   119	                Status = "starting",
   120	                SupervisorPid = currentPid,
```

</details>

## Alert #302 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/302
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:107-107`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
    99	
   100	            // Step 5: Generate credentials
   101	            stdout.WriteLine("[5/14] Generating credentials...");
   102	            var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   103	            Directory.CreateDirectory(secretsDir);
   104	            var tokenBytes = new byte[32];
   105	            RandomNumberGenerator.Fill(tokenBytes);
   106	            var mcpToken = Convert.ToBase64String(tokenBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
   107	            File.WriteAllText(Path.Combine(secretsDir, "mcp.token"), mcpToken, Encoding.UTF8);
   108	            stdout.WriteLine("  MCP token generated");
   109	
   110	            // Step 6: Publish initial manifest
   111	            stdout.WriteLine("[6/14] Publishing runtime manifest...");
   112	            var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   113	            var runtimeDisplayName = GetRuntimeDisplayName(defaultRuntime);
   114	
   115	            var manifest = new RuntimeManifest
```

</details>

## Alert #301 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/301
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:102-102`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
    94	            }
    95	            else
    96	            {
    97	                stdout.WriteLine("  Runtime server: not needed (CLI mode)");
    98	            }
    99	
   100	            // Step 5: Generate credentials
   101	            stdout.WriteLine("[5/14] Generating credentials...");
   102	            var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   103	            Directory.CreateDirectory(secretsDir);
   104	            var tokenBytes = new byte[32];
   105	            RandomNumberGenerator.Fill(tokenBytes);
   106	            var mcpToken = Convert.ToBase64String(tokenBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
   107	            File.WriteAllText(Path.Combine(secretsDir, "mcp.token"), mcpToken, Encoding.UTF8);
   108	            stdout.WriteLine("  MCP token generated");
   109	
   110	            // Step 6: Publish initial manifest
```

</details>

## Alert #300 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/300
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:323-323`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   315	        catch (Exception ex)
   316	        {
   317	            stderr.WriteLine();
   318	            stderr.WriteLine($"FAILED: {ex.Message}");
   319	            LogEvent(Path.Combine(layout.LogsPath, "supervisor.log"), instanceId, "ERROR", "startup_error", ex.Message);
   320	
   321	            try
   322	            {
   323	                var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   324	                if (File.Exists(manifestPath))
   325	                {
   326	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   327	                    manifest.Status = "failed";
   328	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   329	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   330	                }
   331	            }
```

</details>

## Alert #299 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/299
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:319-319`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   311	            stdout.WriteLine();
   312	            stdout.WriteLine("Shutdown requested...");
   313	            return 0;
   314	        }
   315	        catch (Exception ex)
   316	        {
   317	            stderr.WriteLine();
   318	            stderr.WriteLine($"FAILED: {ex.Message}");
   319	            LogEvent(Path.Combine(layout.LogsPath, "supervisor.log"), instanceId, "ERROR", "startup_error", ex.Message);
   320	
   321	            try
   322	            {
   323	                var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   324	                if (File.Exists(manifestPath))
   325	                {
   326	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   327	                    manifest.Status = "failed";
```

</details>

## Alert #298 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/298
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:30-30`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
    22	        _hasMutex = hasMutex;
    23	        InstanceId = instanceId;
    24	        LockFilePath = lockFilePath;
    25	    }
    26	
    27	    public static SupervisorLock Acquire(TiaAgentLayout layout)
    28	    {
    29	        layout.EnsureDirectoriesExist();
    30	        var lockFilePath = Path.Combine(layout.RuntimePath, "supervisor.lock");
    31	
    32	        Mutex? mutex = null;
    33	        bool createdNew = false;
    34	
    35	        try
    36	        {
    37	            mutex = new Mutex(false, MutexName, out createdNew);
    38	        }
```

</details>

## Alert #297 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/297
- Location: `tests/TiaAgent.Cli.Tests/Commands/SupervisorCommandTests.cs:55-55`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 191 lines

<details><summary>Current code context</summary>

```text
    47	    public void Dispose()
    48	    {
    49	        var stopOptions = new StopOptions { CustomRoot = _customRoot, Force = true };
    50	        using var sw = new StringWriter();
    51	        StopCommand.Execute(stopOptions, sw, sw);
    52	
    53	        if (Directory.Exists(_tempDirectory))
    54	        {
    55	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    56	        }
    57	        GC.SuppressFinalize(this);
    58	    }
    59	
    60	    [Fact]
    61	    public void StatusCommand_NoManifest_OutputsNotRunningStatus()
    62	    {
    63	        var options = new StatusOptions { CustomRoot = _customRoot };
```

</details>

## Alert #296 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/296
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:804-804`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   796	                    while (!proc.HasExited)
   797	                    {
   798	                        var line = await proc.StandardOutput.ReadLineAsync().ConfigureAwait(false);
   799	                        if (line != null) await writer.WriteLineAsync(line).ConfigureAwait(false);
   800	                        var errLine = await proc.StandardError.ReadLineAsync().ConfigureAwait(false);
   801	                        if (errLine != null) await writer.WriteLineAsync(errLine).ConfigureAwait(false);
   802	                    }
   803	                }
   804	                catch { }
   805	            });
   806	
   807	            return proc;
   808	        }
   809	        catch
   810	        {
   811	            return null;
   812	        }
```

</details>

## Alert #295 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/295
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:762-762`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   754	                while (!proc.HasExited)
   755	                {
   756	                    var line = await proc.StandardOutput.ReadLineAsync().ConfigureAwait(false);
   757	                    if (line != null) await writer.WriteLineAsync(line).ConfigureAwait(false);
   758	                    var errLine = await proc.StandardError.ReadLineAsync().ConfigureAwait(false);
   759	                    if (errLine != null) await writer.WriteLineAsync(errLine).ConfigureAwait(false);
   760	                }
   761	            }
   762	            catch { }
   763	        });
   764	
   765	        return proc;
   766	    }
   767	
   768	    private static Process? StartRuntimeServer(string runtimeId, int port, TiaAgentLayout layout, string logFile, string instanceId)
   769	    {
   770	        var exeName = runtimeId == "opencode" ? "mimo" : runtimeId;
```

</details>

## Alert #294 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/294
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:895-895`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   887	
   888	    private static void LogEvent(string logPath, string instanceId, string level, string eventName, string message)
   889	    {
   890	        try
   891	        {
   892	            var logEntry = $"{DateTime.UtcNow:o} [{level}] [{instanceId}] {eventName}: {message}{Environment.NewLine}";
   893	            File.AppendAllText(logPath, logEntry, Encoding.UTF8);
   894	        }
   895	        catch { }
   896	    }
   897	}
```

</details>

## Alert #293 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/293
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:884-884`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   876	                if (manifest.Services.OpenCode.Pid > 0)
   877	                {
   878	                    try { using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid); }
   879	                    catch { manifest.Services.OpenCode.Pid = 0; manifest.Services.OpenCode.Status = "stopped"; }
   880	                }
   881	                manifest.Status = "stopped";
   882	                ManifestStore.WriteAtomic(manifestPath, manifest);
   883	            }
   884	            catch { }
   885	        }
   886	    }
   887	
   888	    private static void LogEvent(string logPath, string instanceId, string level, string eventName, string message)
   889	    {
   890	        try
   891	        {
   892	            var logEntry = $"{DateTime.UtcNow:o} [{level}] [{instanceId}] {eventName}: {message}{Environment.NewLine}";
```

</details>

## Alert #292 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/292
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:858-858`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
   852	    {
   853	        var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   854	        if (Directory.Exists(secretsDir))
   855	        {
   856	            foreach (var file in Directory.GetFiles(secretsDir))
   857	            {
   858	                try { File.Delete(file); } catch { }
   859	            }
   860	        }
   861	    }
   862	
   863	    private static void CleanStaleRuntime(TiaAgentLayout layout, string instanceId)
   864	    {
   865	        var manifestPath = Path.Combine(layout.RuntimePath, "runtime.json");
   866	        if (File.Exists(manifestPath))
```

</details>

## Alert #291 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/291
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:847-847`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
   845	        if (bridge != null && !bridge.HasExited)
   846	        {
   847	            try { bridge.Kill(); } catch { }
   848	        }
   849	    }
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
   852	    {
   853	        var secretsDir = Path.Combine(layout.RuntimePath, "secrets");
   854	        if (Directory.Exists(secretsDir))
   855	        {
```

</details>

## Alert #290 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/290
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:843-843`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   835	        }
   836	        catch { }
   837	    }
   838	
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
   845	        if (bridge != null && !bridge.HasExited)
   846	        {
   847	            try { bridge.Kill(); } catch { }
   848	        }
   849	    }
   850	
   851	    private static void CleanSecrets(TiaAgentLayout layout)
```

</details>

## Alert #289 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/289
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:836-836`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   828	                    proc.CloseMainWindow();
   829	                    if (!proc.WaitForExit(5000))
   830	                    {
   831	                        proc.Kill();
   832	                    }
   833	                }
   834	            }
   835	        }
   836	        catch { }
   837	    }
   838	
   839	    private static void StopProcesses(Process? bridge, Process? runtime)
   840	    {
   841	        if (runtime != null && !runtime.HasExited)
   842	        {
   843	            try { runtime.Kill(); } catch { }
   844	        }
```

</details>

## Alert #288 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/288
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:697-697`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   689	                {
   690	                    var versionPath = layout.GetVersionPath(current.ActiveVersion);
   691	                    var installedDll = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.dll");
   692	                    if (File.Exists(installedDll)) return installedDll;
   693	                    var installedExe = Path.Combine(versionPath, "bridge", "TiaAgent.Bridge.exe");
   694	                    if (File.Exists(installedExe)) return installedExe;
   695	                }
   696	            }
   697	            catch { }
   698	        }
   699	
   700	        // 2. Check repo root if passed or detected
   701	        if (!string.IsNullOrWhiteSpace(repoRoot))
   702	        {
   703	            var relDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Release", "net8.0", "TiaAgent.Bridge.dll");
   704	            if (File.Exists(relDll)) return relDll;
   705	            var dbgDll = Path.Combine(repoRoot, "src", "TiaAgent.Bridge", "bin", "Debug", "net8.0", "TiaAgent.Bridge.dll");
```

</details>

## Alert #287 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/287
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:666-666`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   658	                    if (portsProp.TryGetProperty("opencode", out var oProp) && oProp.TryGetInt32(out var oVal)) prefRuntime = oVal;
   659	                }
   660	                if (doc.RootElement.TryGetProperty("portRange", out var rangeProp))
   661	                {
   662	                    if (rangeProp.TryGetProperty("start", out var sProp) && sProp.TryGetInt32(out var sVal)) rangeStart = sVal;
   663	                    if (rangeProp.TryGetProperty("end", out var eProp) && eProp.TryGetInt32(out var eVal)) rangeEnd = eVal;
   664	                }
   665	            }
   666	            catch { }
   667	        }
   668	
   669	        return (prefBridge, prefRuntime, rangeStart, rangeEnd);
   670	    }
   671	
   672	    private static string GetRuntimeDisplayName(string runtimeId) => runtimeId switch
   673	    {
   674	        "mimo" => "Mimo CLI",
```

</details>

## Alert #285 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/285
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:515-515`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   507	
   508	            if (manifest.Services.OpenCode.Pid > 0)
   509	            {
   510	                try
   511	                {
   512	                    using var proc = Process.GetProcessById(manifest.Services.OpenCode.Pid);
   513	                    opencodeRunning = !proc.HasExited;
   514	                }
   515	                catch { }
   516	
   517	                if (opencodeRunning && !string.IsNullOrEmpty(manifest.Services.OpenCode.HealthUrl))
   518	                {
   519	                    opencodeHealthy = await HealthChecker.IsHealthyAsync(manifest.Services.OpenCode.HealthUrl, manifest.Services.OpenCode.Port).ConfigureAwait(false);
   520	                }
   521	            }
   522	        }
   523	
```

</details>

## Alert #284 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/284
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:500-500`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   492	
   493	            if (manifest.Services.Bridge.Pid > 0)
   494	            {
   495	                try
   496	                {
   497	                    using var proc = Process.GetProcessById(manifest.Services.Bridge.Pid);
   498	                    bridgeRunning = !proc.HasExited;
   499	                }
   500	                catch { }
   501	
   502	                if (bridgeRunning && !string.IsNullOrEmpty(manifest.Services.Bridge.HealthUrl))
   503	                {
   504	                    bridgeHealthy = await HealthChecker.IsHealthyAsync(manifest.Services.Bridge.HealthUrl).ConfigureAwait(false);
   505	                }
   506	            }
   507	
   508	            if (manifest.Services.OpenCode.Pid > 0)
```

</details>

## Alert #282 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/282
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:422-422`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   414	            StopProcessById(manifest.Services.Bridge.Pid, options.Force);
   415	        }
   416	
   417	        // Clean secrets and lock
   418	        CleanSecrets(layout);
   419	        var lockFilePath = Path.Combine(layout.RuntimePath, "supervisor.lock");
   420	        if (File.Exists(lockFilePath))
   421	        {
   422	            try { File.Delete(lockFilePath); } catch { }
   423	        }
   424	
   425	        manifest.Status = "stopped";
   426	        manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   427	        ManifestStore.WriteAtomic(manifestPath, manifest);
   428	
   429	        stdout.WriteLine();
   430	        stdout.WriteLine("======================================");
```

</details>

## Alert #281 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/281
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:353-353`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   345	                if (File.Exists(manifestPath))
   346	                {
   347	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   348	                    manifest.Status = "stopped";
   349	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   350	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   351	                }
   352	            }
   353	            catch { }
   354	
   355	            CleanSecrets(layout);
   356	        }
   357	    }
   358	
   359	    public static int Stop(
   360	        StopOptions options,
   361	        TextWriter stdout,
```

</details>

## Alert #280 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/280
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorEngine.cs:332-332`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 897 lines

<details><summary>Current code context</summary>

```text
   324	                if (File.Exists(manifestPath))
   325	                {
   326	                    var manifest = ManifestStore.Read<RuntimeManifest>(manifestPath);
   327	                    manifest.Status = "failed";
   328	                    manifest.UpdatedAt = DateTime.UtcNow.ToString("o");
   329	                    ManifestStore.WriteAtomic(manifestPath, manifest);
   330	                }
   331	            }
   332	            catch { }
   333	
   334	            return 1;
   335	        }
   336	        finally
   337	        {
   338	            // Cleanup on shutdown
   339	            stdout.WriteLine("Cleaning up services...");
   340	            StopProcesses(bridgeProcess, runtimeProcess);
```

</details>

## Alert #279 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/279
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:145-145`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
   145	            try { _mutex.Dispose(); } catch { }
   146	            _mutex = null;
   147	        }
   148	    }
   149	}
```

</details>

## Alert #278 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/278
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:143-143`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   135	        {
   136	            try { File.Delete(LockFilePath); } catch { }
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
   145	            try { _mutex.Dispose(); } catch { }
   146	            _mutex = null;
   147	        }
   148	    }
   149	}
```

</details>

## Alert #277 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/277
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:136-136`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   128	            }
   129	        }
   130	    }
   131	
   132	    public void Dispose()
   133	    {
   134	        if (File.Exists(LockFilePath))
   135	        {
   136	            try { File.Delete(LockFilePath); } catch { }
   137	        }
   138	
   139	        if (_mutex != null)
   140	        {
   141	            if (_hasMutex)
   142	            {
   143	                try { _mutex.ReleaseMutex(); } catch { }
   144	            }
```

</details>

## Alert #276 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/276
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:127-127`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   119	
   120	            if (mutex != null && !createdNew)
   121	            {
   122	                try
   123	                {
   124	                    mutex.Dispose();
   125	                    mutex = new Mutex(false, MutexName, out createdNew);
   126	                }
   127	                catch { }
   128	            }
   129	        }
   130	    }
   131	
   132	    public void Dispose()
   133	    {
   134	        if (File.Exists(LockFilePath))
   135	        {
```

</details>

## Alert #275 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/275
- Location: `src/TiaAgent.Cli/Supervisor/SupervisorLock.cs:118-118`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 149 lines

<details><summary>Current code context</summary>

```text
   110	                throw;
   111	            }
   112	            catch
   113	            {
   114	                // Unparseable or stale lock file
   115	            }
   116	
   117	            // Stale lock — remove file
   118	            try { File.Delete(lockFilePath); } catch { }
   119	
   120	            if (mutex != null && !createdNew)
   121	            {
   122	                try
   123	                {
   124	                    mutex.Dispose();
   125	                    mutex = new Mutex(false, MutexName, out createdNew);
   126	                }
```

</details>

## Alert #274 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:43:09Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/274
- Location: `src/TiaAgent.Cli/Supervisor/HealthChecker.cs:106-106`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 110 lines

<details><summary>Current code context</summary>

```text
    98	            var delayTask = Task.Delay(1000, cancellationToken);
    99	            var completed = await Task.WhenAny(connectTask, delayTask).ConfigureAwait(false);
   100	
   101	            if (completed == connectTask && client.Connected)
   102	            {
   103	                return true;
   104	            }
   105	        }
   106	        catch { }
   107	
   108	        return false;
   109	    }
   110	}
```

</details>

## Alert #273 — cs/useless-tostring-call

- Rule: `cs/useless-tostring-call`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/273
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57-57`
- Message: Redundant call to 'ToString'.

- Current file exists on `main`: **no**

## Alert #272 — cs/useless-tostring-call

- Rule: `cs/useless-tostring-call`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/272
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57-57`
- Message: Redundant call to 'ToString'.

- Current file exists on `main`: **no**

## Alert #271 — cs/useless-tostring-call

- Rule: `cs/useless-tostring-call`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/271
- Location: `src/TiaAgent.OpenCode/Client/SimpleJson.cs:77-77`
- Message: Redundant call to 'ToString'.

- Current file exists on `main`: **no**

## Alert #270 — cs/useless-tostring-call

- Rule: `cs/useless-tostring-call`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/270
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57-57`
- Message: Redundant call to 'ToString'.

- Current file exists on `main`: **no**

## Alert #269 — cs/useless-tostring-call

- Rule: `cs/useless-tostring-call`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/269
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57-57`
- Message: Redundant call to 'ToString'.

- Current file exists on `main`: **no**

## Alert #268 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/268
- Location: `src/TiaAgent.OpenCode/Client/SimpleJson.cs:378-396`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **no**

## Alert #267 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/267
- Location: `src/TiaAgent.OpenCode/Client/SimpleJson.cs:119-134`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **no**

## Alert #266 — cs/linq/missed-where

- Rule: `cs/linq/missed-where`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/266
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeRegistry.cs:147-153`
- Message: This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.

- Current file exists on `main`: **yes**
- Current file length: 156 lines

<details><summary>Current code context</summary>

```text
   139	            return _config.DefaultRuntime;
   140	
   141	        // Hardcoded default
   142	        return "opencode";
   143	    }
   144	
   145	    public void Dispose()
   146	    {
   147	        foreach (var runtime in _runtimes.Values)
   148	        {
   149	            if (runtime is IDisposable disposable)
   150	            {
   151	                try { disposable.Dispose(); } catch { }
   152	            }
   153	        }
   154	        _runtimes.Clear();
   155	    }
   156	}
```

</details>

## Alert #265 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/265
- Location: `src/TiaAgent.Cli/Payload/PayloadValidator.cs:64-72`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 118 lines

<details><summary>Current code context</summary>

```text
    56	            if (string.IsNullOrWhiteSpace(compMeta.Version))
    57	            {
    58	                errors.Add($"Component '{compName}' version declaration is empty.");
    59	            }
    60	        }
    61	
    62	        // Verify prohibited Siemens runtime assemblies are not included in payload
    63	        var allFiles = Directory.EnumerateFiles(payloadDirectory, "*", SearchOption.AllDirectories);
    64	        foreach (var file in allFiles)
    65	        {
    66	            var fileName = Path.GetFileName(file);
    67	            if (fileName.StartsWith("Siemens.Engineering", StringComparison.OrdinalIgnoreCase) ||
    68	                fileName.StartsWith("Siemens.Automation", StringComparison.OrdinalIgnoreCase))
    69	            {
    70	                errors.Add($"Prohibited Siemens runtime assembly found in payload: '{fileName}'. Siemens binaries must remain external.");
    71	            }
    72	        }
    73	
    74	        // Verify each registered payload file
    75	        if (manifest.Files.Count == 0)
    76	        {
    77	            errors.Add("Payload manifest does not contain any file entries.");
    78	        }
    79	
    80	        foreach (var fileEntry in manifest.Files)
```

</details>

## Alert #264 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/264
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:565-572`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   557	        if (string.IsNullOrWhiteSpace(pathEnv)) return false;
   558	
   559	        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
   560	        var extensions = isWindows ? new[] { "", ".exe", ".cmd", ".bat" } : new[] { "" };
   561	
   562	        var paths = pathEnv.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);
   563	        foreach (var dir in paths)
   564	        {
   565	            foreach (var ext in extensions)
   566	            {
   567	                var fullPath = Path.Combine(dir, executableName + ext);
   568	                if (File.Exists(fullPath))
   569	                {
   570	                    return true;
   571	                }
   572	            }
   573	        }
   574	
   575	        return false;
   576	    }
   577	
   578	    private static void PrintConsoleReport(DoctorReport report, TiaAgentLayout layout, TextWriter stdout, bool verbose)
   579	    {
   580	        stdout.WriteLine($"TIA Agent Doctor Diagnostics (v{report.ProductVersion})");
```

</details>

## Alert #263 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/263
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:357-383`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   349	    private string ParseOpenCodeOutput(string stdout, string stderr)
   350	    {
   351	        if (string.IsNullOrWhiteSpace(stdout))
   352	            return ProcessRunner.StripAnsiEscapes(stderr.Trim());
   353	
   354	        var lines = stdout.Split(s_lineSeparators, StringSplitOptions.RemoveEmptyEntries);
   355	        string? lastContent = null;
   356	
   357	        foreach (var line in lines)
   358	        {
   359	            var trimmed = line.Trim();
   360	            if (string.IsNullOrEmpty(trimmed)) continue;
   361	
   362	            try
   363	            {
   364	                using var doc = JsonDocument.Parse(trimmed);
   365	                var root = doc.RootElement;
   366	
   367	                if (root.TryGetProperty("type", out var typeProp))
   368	                {
   369	                    var type = typeProp.GetString();
   370	                    if (type == "assistant" || type == "result" || type == "message")
   371	                    {
   372	                        if (root.TryGetProperty("content", out var contentProp))
   373	                            lastContent = contentProp.GetString();
   374	                        else if (root.TryGetProperty("text", out var textProp))
   375	                            lastContent = textProp.GetString();
   376	                    }
   377	                }
   378	
   379	                if (root.TryGetProperty("result", out var resultProp) && resultProp.ValueKind == JsonValueKind.String)
   380	                    lastContent = resultProp.GetString();
   381	            }
   382	            catch (JsonException) { }
   383	        }
   384	
   385	        return lastContent ?? ProcessRunner.StripAnsiEscapes(stdout.Trim());
   386	    }
   387	
   388	    private static string EscapeShellArg(string arg) => RuntimeHelpers.EscapeShellArg(arg);
   389	
   390	    #endregion
   391	
```

</details>

## Alert #262 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/262
- Location: `src/TiaAgent.Bridge/Runtime/MimoCliRuntime.cs:226-269`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 286 lines

<details><summary>Current code context</summary>

```text
   218	        {
   219	            // Fallback: use stderr as response if stdout is empty
   220	            return ProcessRunner.StripAnsiEscapes(stderr.Trim());
   221	        }
   222	
   223	        var lines = stdout.Split(s_lineSeparators, StringSplitOptions.RemoveEmptyEntries);
   224	        string? lastContent = null;
   225	
   226	        foreach (var line in lines)
   227	        {
   228	            var trimmed = line.Trim();
   229	            if (string.IsNullOrEmpty(trimmed)) continue;
   230	
   231	            try
   232	            {
   233	                using var doc = JsonDocument.Parse(trimmed);
   234	                var root = doc.RootElement;
   235	
   236	                // Look for content in various event types
   237	                if (root.TryGetProperty("type", out var typeProp))
   238	                {
   239	                    var type = typeProp.GetString();
   240	
   241	                    // mimo JSON events: "assistant" type contains the response
   242	                    if (type == "assistant" || type == "result" || type == "message")
   243	                    {
   244	                        if (root.TryGetProperty("content", out var contentProp))
   245	                        {
   246	                            lastContent = contentProp.GetString();
   247	                        }
   248	                        else if (root.TryGetProperty("text", out var textProp))
   249	                        {
   250	                            lastContent = textProp.GetString();
   251	                        }
   252	                        else if (root.TryGetProperty("message", out var msgProp))
   253	                        {
   254	                            lastContent = msgProp.GetString();
   255	                        }
   256	                    }
   257	                }
   258	
   259	                // Also check for "result" at top level
   260	                if (root.TryGetProperty("result", out var resultProp) && resultProp.ValueKind == JsonValueKind.String)
   261	                {
   262	                    lastContent = resultProp.GetString();
   263	                }
   264	            }
   265	            catch (JsonException)
   266	            {
   267	                // Not valid JSON, skip
   268	            }
   269	        }
   270	
   271	        // If we couldn't parse structured events, use the full stdout
   272	        if (lastContent == null)
   273	        {
   274	            lastContent = ProcessRunner.StripAnsiEscapes(stdout.Trim());
   275	        }
   276	
   277	        return lastContent ?? "";
```

</details>

## Alert #261 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/261
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:238-242`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   230	        }
   231	
   232	        // --- Response sanity checks ---
   233	        var responseError = ValidateResponse(response, request.Action);
   234	        if (responseError != null)
   235	        {
   236	            _logger.Warn($"ClaudeCodeRuntime: response validation failed: {responseError}");
   237	            return new AgentTaskResult
   238	            {
   239	                Success = false,
   240	                Error = responseError,
   241	                ErrorCode = "RUNTIME_INVALID_RESPONSE",
   242	                RuntimeId = Id,
   243	                RuntimeMode = "cli"
   244	            };
   245	        }
   246	
   247	        return new AgentTaskResult
   248	        {
   249	            Success = true,
   250	            Response = response,
```

</details>

## Alert #260 — cs/linq/missed-select

- Rule: `cs/linq/missed-select`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/260
- Location: `src/TiaAgent.Bridge/Program.cs:249-257`
- Message: This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
   241	            }
   242	
   243	            if (mcpCommand == null)
   244	            {
   245	                // Fallback: check if tia-mcp is on PATH (bare name)
   246	                var pathVar = Environment.GetEnvironmentVariable("PATH");
   247	                if (!string.IsNullOrEmpty(pathVar))
   248	                {
   249	                    foreach (var dir in pathVar.Split(Path.PathSeparator))
   250	                    {
   251	                        var candidate = Path.Combine(dir.Trim(), "tia-mcp.exe");
   252	                        if (File.Exists(candidate))
   253	                        {
   254	                            mcpCommand = candidate;
   255	                            break;
   256	                        }
   257	                    }
   258	                }
   259	            }
   260	
   261	            logger.Info($"RegisterRuntimes: tia-mcp {(mcpCommand != null ? $"found at '{mcpCommand}'" : "not found")}");
   262	
   263	            var claudeRuntime = new ClaudeCodeRuntime(
   264	                logger,
   265	                executable: claudeConfig?.Executable,
```

</details>

## Alert #259 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/259
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:65-65`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
    57	        string arguments,
    58	        string? workingDirectory,
    59	        TimeSpan timeout,
    60	        System.Collections.Generic.Dictionary<string, string>? environmentVariables = null,
    61	        IProgress<string>? progress = null,
    62	        string? stdinContent = null,
    63	        CancellationToken cancellationToken = default)
    64	    {
    65	        Process? process = null;
    66	
    67	        try
    68	        {
    69	            var startInfo = new ProcessStartInfo
    70	            {
    71	                FileName = executable,
    72	                Arguments = arguments,
    73	                UseShellExecute = false,
```

</details>

## Alert #258 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/258
- Location: `src/TiaAgent.Bridge/Program.cs:66-66`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    58	        logger.Startup($"Port: {config.Port}");
    59	        logger.Startup($"Auth token fingerprint: {TokenFingerprint(tokenProvider.Token)}");
    60	
    61	        // Load runtime configuration
    62	        var configLoader = new RuntimeConfigLoader(logger);
    63	        var runtimeConfig = configLoader.Load();
    64	
    65	        // Create and populate the runtime registry
    66	        var runtimeRegistry = new RuntimeRegistry(runtimeConfig, logger);
    67	
    68	        // Register all known runtime adapters
    69	        RegisterRuntimes(runtimeRegistry, runtimeConfig, config, logger);
    70	
    71	        // Log registered runtimes
    72	        var allRuntimes = runtimeRegistry.GetAllRuntimes();
    73	        logger.Startup($"Registered runtimes: {string.Join(", ", allRuntimes.Select(r => $"{r.Id} ({r.DisplayName})"))}");
    74	        logger.Startup($"Default runtime: {runtimeRegistry.GetDefaultRuntimeId()}");
```

</details>

## Alert #257 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/257
- Location: `src/TiaAgent.Bridge/Program.cs:63-63`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    55	
    56	        logger.Startup("=== TIA Agent Bridge starting ===");
    57	        LogLoadedBinaryIdentity(logger);
    58	        logger.Startup($"Port: {config.Port}");
    59	        logger.Startup($"Auth token fingerprint: {TokenFingerprint(tokenProvider.Token)}");
    60	
    61	        // Load runtime configuration
    62	        var configLoader = new RuntimeConfigLoader(logger);
    63	        var runtimeConfig = configLoader.Load();
    64	
    65	        // Create and populate the runtime registry
    66	        var runtimeRegistry = new RuntimeRegistry(runtimeConfig, logger);
    67	
    68	        // Register all known runtime adapters
    69	        RegisterRuntimes(runtimeRegistry, runtimeConfig, config, logger);
    70	
    71	        // Log registered runtimes
```

</details>

## Alert #256 — cs/missed-using-statement

- Rule: `cs/missed-using-statement`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/256
- Location: `src/TiaAgent.Bridge/Program.cs:66-66`
- Message: This variable is manually disposed in a finally block - consider a C# using statement as a preferable resource management technique.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    58	        logger.Startup($"Port: {config.Port}");
    59	        logger.Startup($"Auth token fingerprint: {TokenFingerprint(tokenProvider.Token)}");
    60	
    61	        // Load runtime configuration
    62	        var configLoader = new RuntimeConfigLoader(logger);
    63	        var runtimeConfig = configLoader.Load();
    64	
    65	        // Create and populate the runtime registry
    66	        var runtimeRegistry = new RuntimeRegistry(runtimeConfig, logger);
    67	
    68	        // Register all known runtime adapters
    69	        RegisterRuntimes(runtimeRegistry, runtimeConfig, config, logger);
    70	
    71	        // Log registered runtimes
    72	        var allRuntimes = runtimeRegistry.GetAllRuntimes();
    73	        logger.Startup($"Registered runtimes: {string.Join(", ", allRuntimes.Select(r => $"{r.Id} ({r.DisplayName})"))}");
    74	        logger.Startup($"Default runtime: {runtimeRegistry.GetDefaultRuntimeId()}");
```

</details>

## Alert #255 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/255
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88-91`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #254 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/254
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81-84`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #253 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/253
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86-89`
- Message: Both branches of this 'if' statement return - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #252 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/252
- Location: `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37-40`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #251 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/251
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86-89`
- Message: Both branches of this 'if' statement return - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #250 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/250
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88-91`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #249 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/249
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81-84`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #248 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/248
- Location: `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37-40`
- Message: Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.

- Current file exists on `main`: **no**

## Alert #247 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/247
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:77-80`
- Message: Both branches of this 'if' statement return - consider using '?' to express intent better.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    69	            return await CheckCliAvailabilityAsync(cancellationToken).ConfigureAwait(false);
    70	    }
    71	
    72	    public async Task<AgentTaskResult> ExecuteAsync(
    73	        AgentTaskRequest request,
    74	        IProgress<AgentTaskEvent>? progress,
    75	        CancellationToken cancellationToken)
    76	    {
    77	        if (_mode == "server")
    78	            return await ExecuteViaServerAsync(request, progress, cancellationToken).ConfigureAwait(false);
    79	        else
    80	            return await ExecuteViaCliAsync(request, progress, cancellationToken).ConfigureAwait(false);
    81	    }
    82	
    83	    public Task CancelAsync(string taskId, CancellationToken cancellationToken)
    84	    {
    85	        _logger.Info($"OpenCodeRuntime: cancel requested for task {taskId}");
    86	        // Cancellation is handled via CancellationToken in ExecuteAsync
    87	        return Task.CompletedTask;
    88	    }
```

</details>

## Alert #246 — cs/missed-ternary-operator

- Rule: `cs/missed-ternary-operator`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/246
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:66-69`
- Message: Both branches of this 'if' statement return - consider using '?' to express intent better.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    58	        else
    59	        {
    60	            _processRunner = new ProcessRunner(logger);
    61	        }
    62	    }
    63	
    64	    public async Task<RuntimeAvailabilityResult> CheckAvailabilityAsync(CancellationToken cancellationToken)
    65	    {
    66	        if (_mode == "server")
    67	            return await CheckServerAvailabilityAsync(cancellationToken).ConfigureAwait(false);
    68	        else
    69	            return await CheckCliAvailabilityAsync(cancellationToken).ConfigureAwait(false);
    70	    }
    71	
    72	    public async Task<AgentTaskResult> ExecuteAsync(
    73	        AgentTaskRequest request,
    74	        IProgress<AgentTaskEvent>? progress,
    75	        CancellationToken cancellationToken)
    76	    {
    77	        if (_mode == "server")
```

</details>

## Alert #245 — cs/missed-readonly-modifier

- Rule: `cs/missed-readonly-modifier`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/245
- Location: `src/TiaAgent.OpenCode/Client/SimpleJson.cs:357-357`
- Message: Field 'Value' can be 'readonly'.

- Current file exists on `main`: **no**

## Alert #244 — cs/missed-readonly-modifier

- Rule: `cs/missed-readonly-modifier`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/244
- Location: `src/TiaAgent.OpenCode/Client/SimpleJson.cs:356-356`
- Message: Field 'Type' can be 'readonly'.

- Current file exists on `main`: **no**

## Alert #243 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/243
- Location: `tests/TiaAgent.Runtime.Tests/PortAllocatorTests.cs:73-76`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 78 lines

<details><summary>Current code context</summary>

```text
    65	        try
    66	        {
    67	            using var listener = new System.Net.Sockets.TcpListener(
    68	                System.Net.IPAddress.Loopback, port);
    69	            listener.Start();
    70	            listener.Stop();
    71	            return true;
    72	        }
    73	        catch
    74	        {
    75	            return false;
    76	        }
    77	    }
    78	}
```

</details>

## Alert #242 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/242
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriter.cs:88-91`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 93 lines

<details><summary>Current code context</summary>

```text
    80	    {
    81	        if (path is null)
    82	            return;
    83	
    84	        try
    85	        {
    86	            File.Delete(path);
    87	        }
    88	        catch
    89	        {
    90	            // Best-effort cleanup — do not mask the original exception.
    91	        }
    92	    }
    93	}
```

</details>

## Alert #241 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/241
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:24-24`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
    16	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadValidatorTests_" + Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_tempDirectory);
    18	    }
    19	
    20	    public void Dispose()
    21	    {
    22	        if (Directory.Exists(_tempDirectory))
    23	        {
    24	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    25	        }
    26	        GC.SuppressFinalize(this);
    27	    }
    28	
    29	    [Fact]
    30	    public void ValidatePayload_WithValidPayload_ReturnsSuccess()
    31	    {
    32	        var bridgeDir = Path.Combine(_tempDirectory, "Bridge");
```

</details>

## Alert #240 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/240
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadManifestTests.cs:23-23`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadManifestTests_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    24	        }
    25	    }
    26	
    27	    [Fact]
    28	    public void PayloadManifest_DefaultValues_ShouldMatchSchema()
    29	    {
    30	        var manifest = new PayloadManifest();
    31	
```

</details>

## Alert #239 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/239
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:23-23`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "TiaAgentTest_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    24	        }
    25	        GC.SuppressFinalize(this);
    26	    }
    27	
    28	    [Fact]
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
```

</details>

## Alert #238 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/238
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionCommandTests.cs:29-29`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 106 lines

<details><summary>Current code context</summary>

```text
    21	        Directory.CreateDirectory(_tempDirectory);
    22	        Directory.CreateDirectory(_customRoot);
    23	    }
    24	
    25	    public void Dispose()
    26	    {
    27	        if (Directory.Exists(_tempDirectory))
    28	        {
    29	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    30	        }
    31	        GC.SuppressFinalize(this);
    32	    }
    33	
    34	    [Fact]
    35	    public void VersionCommand_Default_OutputsVersionString()
    36	    {
    37	        var options = new VersionOptions
```

</details>

## Alert #237 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/237
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:38-38`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    30	
    31	        CreateDummyPayload(_payloadDir, "0.2.0-beta.1");
    32	    }
    33	
    34	    public void Dispose()
    35	    {
    36	        if (Directory.Exists(_tempDirectory))
    37	        {
    38	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    39	        }
    40	        GC.SuppressFinalize(this);
    41	    }
    42	
    43	    [Fact]
    44	    public void InstallCommand_WithValidPayload_InstallsSuccessfully()
    45	    {
    46	        var options = new InstallOptions
```

</details>

## Alert #236 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/236
- Location: `tests/TiaAgent.Cli.Tests/Commands/DoctorCommandTests.cs:32-32`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 136 lines

<details><summary>Current code context</summary>

```text
    24	        Directory.CreateDirectory(_customRoot);
    25	        Directory.CreateDirectory(_userAddInsDir);
    26	    }
    27	
    28	    public void Dispose()
    29	    {
    30	        if (Directory.Exists(_tempDirectory))
    31	        {
    32	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    33	        }
    34	        GC.SuppressFinalize(this);
    35	    }
    36	
    37	    [Fact]
    38	    public void DoctorCommand_WithEmptyRoot_ReturnsZeroWithWarnings()
    39	    {
    40	        var options = new DoctorOptions
```

</details>

## Alert #235 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/235
- Location: `tests/TiaAgent.Cli.Tests/Commands/ConfigCommandTests.cs:30-30`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 183 lines

<details><summary>Current code context</summary>

```text
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
    29	        {
    30	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    31	        }
    32	        GC.SuppressFinalize(this);
    33	    }
    34	
    35	    [Fact]
    36	    public void ConfigCommand_List_DisplaysDefaultConfiguration()
    37	    {
    38	        var options = new ConfigOptions
```

</details>

## Alert #234 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/234
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230-230`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #233 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/233
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:212-215`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #232 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/232
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:190-193`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #231 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/231
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:161-164`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #230 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/230
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:150-153`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #229 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/229
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:118-121`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #228 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/228
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:69-72`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #227 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/227
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:104-107`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #226 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/226
- Location: `src/TiaAgent.Cli/Payload/PayloadValidator.cs:35-38`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 118 lines

<details><summary>Current code context</summary>

```text
    27	            return PayloadValidationResult.Failure($"Payload manifest file missing: {manifestPath}");
    28	        }
    29	
    30	        PayloadManifest manifest;
    31	        try
    32	        {
    33	            manifest = PayloadStore.ReadManifest(payloadDirectory);
    34	        }
    35	        catch (Exception ex)
    36	        {
    37	            return PayloadValidationResult.Failure($"Failed to read payload manifest: {ex.Message}");
    38	        }
    39	
    40	        var errors = new List<string>();
    41	
    42	        if (string.IsNullOrWhiteSpace(manifest.ProductVersion))
    43	        {
    44	            errors.Add("Payload manifest productVersion is empty.");
    45	        }
    46	
```

</details>

## Alert #225 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/225
- Location: `src/TiaAgent.Cli/Layout/ManifestStore.cs:86-86`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 91 lines

<details><summary>Current code context</summary>

```text
    78	            File.WriteAllText(tempPath, json);
    79	
    80	            File.Move(tempPath, filePath, overwrite: true);
    81	        }
    82	        catch
    83	        {
    84	            if (File.Exists(tempPath))
    85	            {
    86	                try { File.Delete(tempPath); } catch { }
    87	            }
    88	            throw;
    89	        }
    90	    }
    91	}
```

</details>

## Alert #224 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/224
- Location: `src/TiaAgent.Cli/Commands/VersionCommand.cs:76-76`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    68	                    installedVersions.Add(new VersionDetail
    69	                    {
    70	                        Version = ver,
    71	                        InstalledAt = meta.InstalledAt,
    72	                        CommitSha = meta.CommitSha
    73	                    });
    74	                }
    75	            }
    76	            catch { }
    77	        }
    78	
    79	        var report = new VersionReport
    80	        {
    81	            ProductVersion = productVersion,
    82	            ActiveVersion = activeVersion,
    83	            InstalledVersions = installedVersions,
    84	            ConfigPath = layout.ConfigPath,
```

</details>

## Alert #223 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/223
- Location: `src/TiaAgent.Cli/Commands/VersionCommand.cs:57-57`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    49	        string? activeVersion = null;
    50	        if (File.Exists(layout.CurrentManifestPath))
    51	        {
    52	            try
    53	            {
    54	                var current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    55	                activeVersion = current.ActiveVersion;
    56	            }
    57	            catch { }
    58	        }
    59	
    60	        var installedVersions = new List<VersionDetail>();
    61	        if (File.Exists(layout.InstallationsManifestPath))
    62	        {
    63	            try
    64	            {
    65	                var installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
```

</details>

## Alert #222 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/222
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:168-168`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
   160	            if (fileName.Contains(version, StringComparison.OrdinalIgnoreCase) ||
   161	                fileName.Contains(pubVersion, StringComparison.OrdinalIgnoreCase))
   162	            {
   163	                try
   164	                {
   165	                    File.Delete(file);
   166	                    stdout.WriteLine($"Removed Add-In artifact '{fileName}' from '{userAddInsDir}'.");
   167	                }
   168	                catch (Exception ex) { stderr.WriteLine($"Warning: Failed to remove Add-In artifact '{fileName}': {ex.Message}"); }
   169	            }
   170	        }
   171	    }
   172	}
```

</details>

## Alert #221 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/221
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:139-139`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
   131	                ManifestStore.WriteAtomic(layout.CurrentManifestPath, newCurrent);
   132	                stdout.WriteLine($"Switched active version to '{nextActive}'.");
   133	            }
   134	            else
   135	            {
   136	                if (File.Exists(layout.CurrentManifestPath))
   137	                {
   138	                    try { File.Delete(layout.CurrentManifestPath); }
   139	                    catch (Exception ex) { stderr.WriteLine($"Warning: Failed to delete '{layout.CurrentManifestPath}': {ex.Message}"); }
   140	                }
   141	            }
   142	        }
   143	
   144	        stdout.WriteLine($"Successfully uninstalled TIA Agent version(s): {string.Join(", ", uninstalledVersions)}.");
   145	        return 0;
   146	    }
   147	
```

</details>

## Alert #220 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/220
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:101-114`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
    93	                    Directory.Delete(versionDir, recursive: true);
    94	                }
    95	
    96	                RemoveAddInFilesForVersion(ver, userAddInsDir, stdout, stderr);
    97	
    98	                installations.Versions.Remove(ver);
    99	                uninstalledVersions.Add(ver);
   100	            }
   101	            catch (Exception ex)
   102	            {
   103	                if (options.Force)
   104	                {
   105	                    stderr.WriteLine($"Warning: Failed to cleanly remove version '{ver}': {ex.Message}");
   106	                    installations.Versions.Remove(ver);
   107	                    uninstalledVersions.Add(ver);
   108	                }
   109	                else
   110	                {
   111	                    stderr.WriteLine($"Error removing version '{ver}': {ex.Message}");
   112	                    return 1;
   113	                }
   114	            }
   115	        }
   116	
   117	        ManifestStore.WriteAtomic(layout.InstallationsManifestPath, installations);
   118	
   119	        if (uninstalledVersions.Contains(current.ActiveVersion, StringComparer.OrdinalIgnoreCase))
   120	        {
   121	            if (installations.Versions.Count > 0)
   122	            {
```

</details>

## Alert #219 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/219
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:42-45`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
    34	            installations = new InstallationsManifest();
    35	        }
    36	
    37	        CurrentManifest current;
    38	        try
    39	        {
    40	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    41	        }
    42	        catch
    43	        {
    44	            current = new CurrentManifest();
    45	        }
    46	
    47	        var targetVersions = new List<string>();
    48	
    49	        if (options.All)
    50	        {
    51	            targetVersions.AddRange(installations.Versions.Keys);
    52	        }
    53	        else if (!string.IsNullOrWhiteSpace(options.Version))
```

</details>

## Alert #218 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/218
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:32-35`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
    24	
    25	        var layout = new TiaAgentLayout(options.CustomRoot);
    26	
    27	        InstallationsManifest installations;
    28	        try
    29	        {
    30	            installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    31	        }
    32	        catch
    33	        {
    34	            installations = new InstallationsManifest();
    35	        }
    36	
    37	        CurrentManifest current;
    38	        try
    39	        {
    40	            current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    41	        }
    42	        catch
    43	        {
```

</details>

## Alert #216 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/216
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:289-298`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   281	                report.Checks.Add(new DoctorCheckResult
   282	                {
   283	                    Category = "Installation",
   284	                    Name = "Installations Registry",
   285	                    Status = "OK",
   286	                    Details = $"Found {installations.Versions.Count} registered version(s) in installations.json"
   287	                });
   288	            }
   289	            catch (Exception ex)
   290	            {
   291	                report.Checks.Add(new DoctorCheckResult
   292	                {
   293	                    Category = "Installation",
   294	                    Name = "Installations Registry",
   295	                    Status = "WARN",
   296	                    Details = $"Malformed installations.json: {ex.Message}"
   297	                });
   298	            }
   299	        }
   300	    }
   301	
   302	    private static void CheckUpdateChannel(TiaAgentLayout layout, DoctorReport report)
   303	    {
   304	        TiaAgentConfig? config = null;
   305	        if (File.Exists(layout.ConfigPath))
   306	        {
```

</details>

## Alert #215 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/215
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:251-261`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   243	                            Name = "Active Version",
   244	                            Status = "FAIL",
   245	                            Details = $"Active version set to 'v{activeVersion}' but folder '{versionPath}' is missing!",
   246	                            Recommendation = "Run 'tia-agent install --force' to repair installation."
   247	                        });
   248	                    }
   249	                }
   250	            }
   251	            catch (Exception ex)
   252	            {
   253	                report.Checks.Add(new DoctorCheckResult
   254	                {
   255	                    Category = "Installation",
   256	                    Name = "Active Version",
   257	                    Status = "FAIL",
   258	                    Details = $"Malformed current.json at '{layout.CurrentManifestPath}': {ex.Message}",
   259	                    Recommendation = "Run 'tia-agent install --force' to re-activate a version."
   260	                });
   261	            }
   262	        }
   263	        else
   264	        {
   265	            report.Checks.Add(new DoctorCheckResult
   266	            {
   267	                Category = "Installation",
   268	                Name = "Active Version",
   269	                Status = "WARN",
```

</details>

## Alert #214 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/214
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:182-192`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   174	                report.Checks.Add(new DoctorCheckResult
   175	                {
   176	                    Category = "Config",
   177	                    Name = "Configuration File",
   178	                    Status = "OK",
   179	                    Details = $"Valid config.json found at '{layout.ConfigPath}' (Default runtime: {config.DefaultRuntime})"
   180	                });
   181	            }
   182	            catch (Exception ex)
   183	            {
   184	                report.Checks.Add(new DoctorCheckResult
   185	                {
   186	                    Category = "Config",
   187	                    Name = "Configuration File",
   188	                    Status = "FAIL",
   189	                    Details = $"Malformed config.json at '{layout.ConfigPath}': {ex.Message}",
   190	                    Recommendation = "Run 'tia-agent config reset' or fix JSON formatting."
   191	                });
   192	            }
   193	        }
   194	        else
   195	        {
   196	            report.Checks.Add(new DoctorCheckResult
   197	            {
   198	                Category = "Config",
   199	                Name = "Configuration File",
   200	                Status = "WARN",
```

</details>

## Alert #212 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/212
- Location: `src/TiaAgent.Cli/Commands/ConfigCommand.cs:246-249`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 254 lines

<details><summary>Current code context</summary>

```text
   238	    public static TiaAgentConfig LoadConfig(string configPath)
   239	    {
   240	        if (File.Exists(configPath))
   241	        {
   242	            try
   243	            {
   244	                return ManifestStore.Read<TiaAgentConfig>(configPath);
   245	            }
   246	            catch
   247	            {
   248	                // Return default on corruption
   249	            }
   250	        }
   251	
   252	        return new TiaAgentConfig();
   253	    }
   254	}
```

</details>

## Alert #211 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/211
- Location: `src/TiaAgent.Bridge/Tasks/TaskManager.cs:237-247`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 341 lines

<details><summary>Current code context</summary>

```text
   229	            }
   230	        }
   231	        catch (OperationCanceledException)
   232	        {
   233	            _logger.Info($"Task {entry.TaskId}: cancelled");
   234	            entry.Status = BridgeTaskStatusValues.Cancelled;
   235	            entry.Message = "Task was cancelled";
   236	        }
   237	        catch (Exception ex)
   238	        {
   239	            _logger.Error($"Task {entry.TaskId}: failed with exception", ex);
   240	            entry.Status = BridgeTaskStatusValues.Failed;
   241	            entry.Error = new BridgeError
   242	            {
   243	                Code = "BRIDGE_INTERNAL_ERROR",
   244	                Message = ex.Message,
   245	                Retryable = false
   246	            };
   247	        }
   248	        finally
   249	        {
   250	            Interlocked.Decrement(ref _runningCount);
   251	            entry.CompletedAt = DateTime.UtcNow;
   252	        }
   253	    }
   254	
   255	    private static string BuildPrompt(BridgeTaskRequest request)
```

</details>

## Alert #210 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/210
- Location: `src/TiaAgent.Bridge/Tasks/TaskManager.cs:92-92`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 341 lines

<details><summary>Current code context</summary>

```text
    84	        // Also tell the runtime to cancel
    85	        if (!string.IsNullOrEmpty(entry.RuntimeId))
    86	        {
    87	            try
    88	            {
    89	                var runtime = _runtimeRegistry.GetRuntime(entry.RuntimeId);
    90	                _ = runtime.CancelAsync(taskId, CancellationToken.None);
    91	            }
    92	            catch { }
    93	        }
    94	
    95	        return true;
    96	    }
    97	
    98	    private async Task ExecuteTaskAsync(TaskEntry entry, CancellationToken cancellationToken)
    99	    {
   100	        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
```

</details>

## Alert #209 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/209
- Location: `src/TiaAgent.Bridge/Sessions/SessionManager.cs:59-59`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 69 lines

<details><summary>Current code context</summary>

```text
    51	        sessionId = null;
    52	        return false;
    53	    }
    54	
    55	    public void Dispose()
    56	    {
    57	        foreach (var kvp in _sessions)
    58	        {
    59	            try { _openCodeClient.AbortSessionAsync(kvp.Value.SessionId).GetAwaiter().GetResult(); } catch { }
    60	        }
    61	        _sessions.Clear();
    62	    }
    63	
    64	    private sealed class SessionEntry
    65	    {
    66	        public string SessionId { get; init; } = null!;
    67	        public DateTime CreatedAt { get; init; }
```

</details>

## Alert #208 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/208
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeConfigLoader.cs:72-76`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 115 lines

<details><summary>Current code context</summary>

```text
    64	                _logger.Warn("Runtime config deserialized to null, using defaults");
    65	                return new TiaAgentConfig();
    66	            }
    67	
    68	            Validate(config);
    69	            _logger.Info($"Runtime config loaded: defaultRuntime={config.DefaultRuntime}, runtimes=[{string.Join(", ", config.Runtimes.Keys)}]");
    70	            return config;
    71	        }
    72	        catch (Exception ex)
    73	        {
    74	            _logger.Error($"Failed to load runtime config from {configPath}, using defaults", ex);
    75	            return new TiaAgentConfig();
    76	        }
    77	    }
    78	
    79	    /// <summary>
    80	    /// Validates the configuration and logs warnings for issues.
    81	    /// </summary>
    82	    private void Validate(TiaAgentConfig config)
    83	    {
    84	        // Validate default runtime is a known ID
```

</details>

## Alert #207 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/207
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:54-54`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
    46	                var existing = File.ReadAllText(_tokenFilePath).Trim();
    47	                if (!string.IsNullOrEmpty(existing))
    48	                    return existing;
    49	            }
    50	        }
    51	        catch { }
    52	
    53	        var token = GenerateToken();
    54	        try { File.WriteAllText(_tokenFilePath, token); } catch { }
    55	        return token;
    56	    }
    57	
    58	    private static string GenerateToken()
    59	    {
    60	        Span<byte> bytes = stackalloc byte[32];
    61	        RandomNumberGenerator.Fill(bytes);
    62	        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
```

</details>

## Alert #206 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/206
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:51-51`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
    43	        {
    44	            if (File.Exists(_tokenFilePath))
    45	            {
    46	                var existing = File.ReadAllText(_tokenFilePath).Trim();
    47	                if (!string.IsNullOrEmpty(existing))
    48	                    return existing;
    49	            }
    50	        }
    51	        catch { }
    52	
    53	        var token = GenerateToken();
    54	        try { File.WriteAllText(_tokenFilePath, token); } catch { }
    55	        return token;
    56	    }
    57	
    58	    private static string GenerateToken()
    59	    {
```

</details>

## Alert #205 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/205
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeRegistry.cs:151-151`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 156 lines

<details><summary>Current code context</summary>

```text
   143	    }
   144	
   145	    public void Dispose()
   146	    {
   147	        foreach (var runtime in _runtimes.Values)
   148	        {
   149	            if (runtime is IDisposable disposable)
   150	            {
   151	                try { disposable.Dispose(); } catch { }
   152	            }
   153	        }
   154	        _runtimes.Clear();
   155	    }
   156	}
```

</details>

## Alert #204 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/204
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeRegistry.cs:113-121`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 156 lines

<details><summary>Current code context</summary>

```text
   105	        var results = new Dictionary<string, RuntimeAvailabilityResult>(StringComparer.OrdinalIgnoreCase);
   106	
   107	        foreach (var kvp in _runtimes)
   108	        {
   109	            try
   110	            {
   111	                results[kvp.Key] = await kvp.Value.CheckAvailabilityAsync(cancellationToken).ConfigureAwait(false);
   112	            }
   113	            catch (Exception ex)
   114	            {
   115	                _logger.Error($"RuntimeRegistry: availability check failed for '{kvp.Key}'", ex);
   116	                results[kvp.Key] = new RuntimeAvailabilityResult
   117	                {
   118	                    Available = false,
   119	                    Error = $"Availability check failed: {ex.Message}"
   120	                };
   121	            }
   122	        }
   123	
   124	        return results;
   125	    }
   126	
   127	    /// <summary>
   128	    /// Gets the configured default runtime ID.
   129	    /// </summary>
```

</details>

## Alert #203 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/203
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:209-209`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   201	                };
   202	            }
   203	
   204	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stdout.decoded", decodedStdout));
   205	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stderr.decoded", decodedStderr));
   206	
   207	            // Progress reporting is observational only; decodedStdout remains the source of truth.
   208	            if (progress != null)
   209	            {
   210	                var stdoutLines = decodedStdout.Split(s_newlineSeparators, StringSplitOptions.None);
   211	                foreach (var line in stdoutLines)
   212	                    progress.Report(line);
   213	            }
   214	
   215	            var exitCode = process.ExitCode;
   216	            _logger.Info($"ProcessRunner: process exited with code {exitCode}");
   217	
```

</details>

## Alert #202 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/202
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:206-210`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   198	                    RawStdoutBytes = stdoutBytes,
   199	                    RawStderrBytes = stderrBytes,
   200	                    Error = "Process stderr contained invalid UTF-8 bytes"
   201	                };
   202	            }
   203	
   204	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stdout.decoded", decodedStdout));
   205	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stderr.decoded", decodedStderr));
   206	
   207	            // Progress reporting is observational only; decodedStdout remains the source of truth.
   208	            if (progress != null)
   209	            {
   210	                var stdoutLines = decodedStdout.Split(s_newlineSeparators, StringSplitOptions.None);
   211	                foreach (var line in stdoutLines)
   212	                    progress.Report(line);
   213	            }
   214	
   215	            var exitCode = process.ExitCode;
   216	            _logger.Info($"ProcessRunner: process exited with code {exitCode}");
   217	
   218	            return new ProcessResult
```

</details>

## Alert #201 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/201
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:188-191`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   180	                    RawStderrBytes = stderrBytes,
   181	                    Error = "Process stdout contained invalid UTF-8 bytes"
   182	                };
   183	            }
   184	
   185	            string decodedStderr;
   186	            try
   187	            {
   188	                decodedStderr = s_strictUtf8.GetString(stderrBytes);
   189	            }
   190	            catch (DecoderFallbackException ex)
   191	            {
   192	                _logger.Error($"ProcessRunner: stderr is not valid UTF-8: {ex.Message}");
   193	                return new ProcessResult
   194	                {
   195	                    ExitCode = process.ExitCode,
   196	                    StdOut = decodedStdout,
   197	                    StdErr = string.Empty,
   198	                    RawStdoutBytes = stdoutBytes,
   199	                    RawStderrBytes = stderrBytes,
```

</details>

## Alert #200 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/200
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:107-107`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
    99	                var stdinBytes = Encoding.UTF8.GetBytes(stdinContent);
   100	                _logger.Info($"ProcessRunner: writing {stdinBytes.Length} UTF-8 bytes to stdin");
   101	
   102	                await process.StandardInput.BaseStream.WriteAsync(stdinBytes.AsMemory(), cancellationToken)
   103	                    .ConfigureAwait(false);
   104	                await process.StandardInput.BaseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
   105	            }
   106	
   107	            try { process.StandardInput.Close(); } catch { }
   108	
   109	            _logger.Info($"ProcessRunner: process started (PID={process.Id})");
   110	
   111	            using var timeoutCts = new CancellationTokenSource(timeout);
   112	            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
   113	                cancellationToken, timeoutCts.Token);
   114	
   115	            // BOUNDARY 1: read stdout and stderr concurrently as raw bytes.
```

</details>

## Alert #199 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/199
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:227-237`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   219	            {
   220	                ExitCode = exitCode,
   221	                StdOut = decodedStdout,
   222	                StdErr = decodedStderr,
   223	                RawStdoutBytes = stdoutBytes,
   224	                RawStderrBytes = stderrBytes
   225	            };
   226	        }
   227	        catch (Exception ex)
   228	        {
   229	            _logger.Error($"ProcessRunner: failed to start process '{executable}'", ex);
   230	            return new ProcessResult
   231	            {
   232	                ExitCode = -1,
   233	                StdOut = string.Empty,
   234	                StdErr = string.Empty,
   235	                Error = $"Failed to start process: {ex.Message}"
   236	            };
   237	        }
   238	        finally
   239	        {
   240	            try
   241	            {
   242	                if (process != null && !process.HasExited)
   243	                    KillProcessTree(process);
   244	            }
   245	            catch (InvalidOperationException)
```

</details>

## Alert #198 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/198
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:229-238`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   221	            return new RuntimeAvailabilityResult
   222	            {
   223	                Available = false,
   224	                Executable = exe,
   225	                Mode = "cli",
   226	                Error = $"opencode returned exit code {result.ExitCode}: {result.StdErr.Trim()}"
   227	            };
   228	        }
   229	        catch (Exception ex)
   230	        {
   231	            return new RuntimeAvailabilityResult
   232	            {
   233	                Available = false,
   234	                Executable = exe,
   235	                Mode = "cli",
   236	                Error = $"Executable not found: {exe}. {ex.Message}"
   237	            };
   238	        }
   239	    }
   240	
   241	    private async Task<AgentTaskResult> ExecuteViaCliAsync(
   242	        AgentTaskRequest request,
   243	        IProgress<AgentTaskEvent>? progress,
   244	        CancellationToken cancellationToken)
   245	    {
   246	        var exe = _executable ?? "opencode";
```

</details>

## Alert #197 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/197
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:182-193`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   174	            {
   175	                Success = false,
   176	                Error = "Task was cancelled",
   177	                ErrorCode = "TASK_CANCELLED",
   178	                RuntimeId = Id,
   179	                RuntimeMode = "server"
   180	            };
   181	        }
   182	        catch (Exception ex)
   183	        {
   184	            _logger.Error($"OpenCodeRuntime(server): task {request.TaskId} failed", ex);
   185	            return new AgentTaskResult
   186	            {
   187	                Success = false,
   188	                Error = ex.Message,
   189	                ErrorCode = "RUNTIME_EXECUTION_FAILED",
   190	                RuntimeId = Id,
   191	                RuntimeMode = "server"
   192	            };
   193	        }
   194	    }
   195	
   196	    #endregion
   197	
   198	    #region CLI Mode
   199	
   200	    private async Task<RuntimeAvailabilityResult> CheckCliAvailabilityAsync(CancellationToken cancellationToken)
   201	    {
```

</details>

## Alert #196 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/196
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:106-115`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    98	            {
    99	                Available = health.Available,
   100	                Executable = _serverUrl,
   101	                Version = health.Available ? "connected" : null,
   102	                Mode = "server",
   103	                Error = health.Available ? null : $"OpenCode server at {_serverUrl} is not responding"
   104	            };
   105	        }
   106	        catch (Exception ex)
   107	        {
   108	            return new RuntimeAvailabilityResult
   109	            {
   110	                Available = false,
   111	                Executable = _serverUrl,
   112	                Mode = "server",
   113	                Error = $"OpenCode server check failed: {ex.Message}"
   114	            };
   115	        }
   116	    }
   117	
   118	    private async Task<AgentTaskResult> ExecuteViaServerAsync(
   119	        AgentTaskRequest request,
   120	        IProgress<AgentTaskEvent>? progress,
   121	        CancellationToken cancellationToken)
   122	    {
   123	        _logger.Info($"OpenCodeRuntime(server): executing task {request.TaskId}");
```

</details>

## Alert #195 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/195
- Location: `src/TiaAgent.Bridge/Runtime/MimoCliRuntime.cs:68-77`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 286 lines

<details><summary>Current code context</summary>

```text
    60	            return new RuntimeAvailabilityResult
    61	            {
    62	                Available = false,
    63	                Executable = exe,
    64	                Mode = "cli",
    65	                Error = error
    66	            };
    67	        }
    68	        catch (Exception ex)
    69	        {
    70	            _logger.Warn($"MimoCliRuntime: executable not found: {ex.Message}");
    71	            return new RuntimeAvailabilityResult
    72	            {
    73	                Available = false,
    74	                Executable = exe,
    75	                Error = $"Executable not found: {exe}. {ex.Message}"
    76	            };
    77	        }
    78	    }
    79	
    80	    public async Task<AgentTaskResult> ExecuteAsync(
    81	        AgentTaskRequest request,
    82	        IProgress<AgentTaskEvent>? progress,
    83	        CancellationToken cancellationToken)
    84	    {
    85	        var exe = _executable ?? "mimo";
```

</details>

## Alert #194 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/194
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:502-502`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   494	
   495	    public void Dispose()
   496	    {
   497	        _processRunner.Dispose();
   498	
   499	        // Clean up generated MCP config
   500	        if (_generatedMcpConfigPath != null && File.Exists(_generatedMcpConfigPath))
   501	        {
   502	            try { File.Delete(_generatedMcpConfigPath); } catch { }
   503	        }
   504	    }
   505	}
```

</details>

## Alert #193 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/193
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:398-402`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   390	                }
   391	            };
   392	
   393	            var json = JsonSerializer.Serialize(config, s_jsonOptions);
   394	            File.WriteAllText(_generatedMcpConfigPath, json);
   395	            _logger.Info($"ClaudeCodeRuntime: generated MCP config at {_generatedMcpConfigPath}");
   396	            _logger.Info($"ClaudeCodeRuntime: MCP server command={_mcpServerCommand}, transport=stdio, auth=none (stdio transport)");
   397	        }
   398	        catch (Exception ex)
   399	        {
   400	            _logger.Error("ClaudeCodeRuntime: failed to generate MCP config", ex);
   401	            _generatedMcpConfigPath = null;
   402	        }
   403	    }
   404	
   405	    private static string EscapeShellArg(string arg) => RuntimeHelpers.EscapeShellArg(arg);
   406	
   407	    private static string? ValidateRequest(AgentTaskRequest request)
   408	    {
   409	        if (string.IsNullOrWhiteSpace(request.CorrelationId))
   410	            return "Missing correlation ID";
```

</details>

## Alert #192 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/192
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:90-100`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
    82	            return new RuntimeAvailabilityResult
    83	            {
    84	                Available = false,
    85	                Executable = exeName,
    86	                Mode = "cli",
    87	                Error = error
    88	            };
    89	        }
    90	        catch (Exception ex)
    91	        {
    92	            _logger.Warn($"ClaudeCodeRuntime: executable not found: {ex.Message}");
    93	            return new RuntimeAvailabilityResult
    94	            {
    95	                Available = false,
    96	                Executable = exeName,
    97	                Mode = "cli",
    98	                Error = $"Executable not found: {exeName}. {ex.Message}"
    99	            };
   100	        }
   101	    }
   102	
   103	    public async Task<AgentTaskResult> ExecuteAsync(
   104	        AgentTaskRequest request,
   105	        IProgress<AgentTaskEvent>? progress,
   106	        CancellationToken cancellationToken)
   107	    {
   108	        // --- Request validation ---
```

</details>

## Alert #191 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/191
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:82-82`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    74	    }
    75	
    76	    public async Task AbortSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    77	    {
    78	        try
    79	        {
    80	            await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/abort", null, cancellationToken).ConfigureAwait(false);
    81	        }
    82	        catch { }
    83	    }
    84	
    85	    public void Dispose() => _httpClient.Dispose();
    86	
    87	    /// <summary>
    88	    /// Reads HTTP response content as a string using explicit UTF-8 encoding.
    89	    /// Prevents encoding corruption when the server response lacks a charset
    90	    /// in the Content-Type header.
```

</details>

## Alert #190 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/190
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:53-60`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    45	            var body = await ReadResponseUtf8Async(response, cancellationToken).ConfigureAwait(false);
    46	            return new SessionResponse
    47	            {
    48	                Success = response.IsSuccessStatusCode,
    49	                SessionId = ExtractField(body, "sessionId"),
    50	                RawJson = body
    51	            };
    52	        }
    53	        catch (Exception ex)
    54	        {
    55	            return new SessionResponse
    56	            {
    57	                Success = false,
    58	                RawJson = $"Connection failed to {url}: {ex.Message}"
    59	            };
    60	        }
    61	    }
    62	
    63	    public async Task<MessageResponse> SendMessageAsync(string sessionId, string message, CancellationToken cancellationToken = default)
    64	    {
    65	        var payload = $"{{\"message\":\"{EscapeJson(message)}\"}}";
    66	        using var content = new StringContent(payload, Encoding.UTF8, "application/json");
    67	        var response = await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/messages", content, cancellationToken).ConfigureAwait(false);
    68	        var body = await ReadResponseUtf8Async(response, cancellationToken).ConfigureAwait(false);
```

</details>

## Alert #189 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/189
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:31-34`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    23	        {
    24	            var response = await _httpClient.GetAsync($"{_baseUrl}/health", cancellationToken).ConfigureAwait(false);
    25	            // Accept any HTTP response (including 503) as "server is running".
    26	            // mimo serve returns 503 for /health when Web UI is unavailable in headless mode,
    27	            // but the server itself is running and ready to accept MCP requests.
    28	            var body = await ReadResponseUtf8Async(response, cancellationToken).ConfigureAwait(false);
    29	            return new HealthResponse { Available = true, RawJson = body };
    30	        }
    31	        catch (Exception ex)
    32	        {
    33	            return new HealthResponse { Available = false, Error = ex.Message };
    34	        }
    35	    }
    36	
    37	    public async Task<SessionResponse> CreateSessionAsync(string agentId, string prompt, CancellationToken cancellationToken = default)
    38	    {
    39	        var payload = $"{{\"agent\":\"{EscapeJson(agentId)}\",\"prompt\":\"{EscapeJson(prompt)}\"}}";
    40	        var url = $"{_baseUrl}/sessions";
    41	        try
    42	        {
```

</details>

## Alert #188 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/188
- Location: `src/TiaAgent.Bridge/Diagnostics/BridgeLogger.cs:35-38`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 40 lines

<details><summary>Current code context</summary>

```text
    27	        try
    28	        {
    29	            var logLine = $"[{DateTime.UtcNow:O}] [{level}] {message}{Environment.NewLine}";
    30	            lock (_lock)
    31	            {
    32	                File.AppendAllText(_logFilePath, logLine);
    33	            }
    34	        }
    35	        catch
    36	        {
    37	            // Swallow exceptions — logging must never crash the bridge
    38	        }
    39	    }
    40	}
```

</details>

## Alert #187 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/187
- Location: `src/TiaAgent.Bridge/Configuration/BridgeConfig.cs:25-28`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 103 lines

<details><summary>Current code context</summary>

```text
    17	        if (!File.Exists(configPath))
    18	            return new BridgeConfig();
    19	
    20	        try
    21	        {
    22	            var json = File.ReadAllText(configPath);
    23	            return Parse(json);
    24	        }
    25	        catch
    26	        {
    27	            return new BridgeConfig();
    28	        }
    29	    }
    30	
    31	    private static BridgeConfig Parse(string json)
    32	    {
    33	        int port = 43119;
    34	        string openCodeBaseUrl = "http://127.0.0.1:43120";
    35	        int taskTimeoutSeconds = 300;
    36	        int maxConcurrentTasks = 5;
```

</details>

## Alert #186 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/186
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:663-667`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   655	        {
   656	            var request = System.Text.Json.JsonSerializer.Deserialize<BridgeTaskRequest>(json, s_jsonOptions);
   657	            if (request != null)
   658	            {
   659	                _logger.Info($"Deserialized request: action='{request.Action}', agentId='{request.AgentId}', runtime='{request.Runtime ?? "default"}', project={request.Project != null}, selection={request.Selection != null}");
   660	            }
   661	            return request;
   662	        }
   663	        catch (Exception ex)
   664	        {
   665	            _logger.Error($"JSON deserialization failed: {ex.Message}", ex);
   666	            return null;
   667	        }
   668	    }
   669	
   670	    /// <summary>
   671	    /// Returns true for paths that are safe to serve without authentication.
   672	    /// The set is defined by server-side constants so that the bypass decision
   673	    /// is never controlled by user-supplied data (CWE-807).
   674	    /// </summary>
   675	    private static bool IsPublicEndpoint(string path)
```

</details>

## Alert #185 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/185
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:216-216`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   208	        var defaultRuntimeId = _runtimeRegistry.GetDefaultRuntimeId();
   209	        IAgentRuntime? defaultRuntime = null;
   210	        RuntimeAvailabilityResult? availability = null;
   211	        try
   212	        {
   213	            defaultRuntime = _runtimeRegistry.GetRuntime(defaultRuntimeId);
   214	            availability = await defaultRuntime.CheckAvailabilityAsync(CancellationToken.None).ConfigureAwait(false);
   215	        }
   216	        catch { }
   217	
   218	        var healthJson = $"{{\"service\":\"tia-agent-bridge\",\"status\":\"healthy\",\"version\":\"1.0.0\",\"instanceId\":\"{EscapeJson(instanceId)}\",\"runtimeId\":\"{EscapeJson(defaultRuntimeId)}\",\"runtimeDisplayName\":\"{EscapeJson(defaultRuntime?.DisplayName ?? defaultRuntimeId)}\",\"runtimeAvailable\":{(availability?.Available == true ? "true" : "false")},\"runtimeVersion\":\"{EscapeJson(availability?.Version ?? "")}\"}}";
   219	        await WriteJsonResponseAsync(response, 200, healthJson).ConfigureAwait(false);
   220	    }
   221	
   222	    private async Task HandleCreateTaskAsync(HttpListenerRequest request, HttpListenerResponse response)
   223	    {
   224	        var body = await ReadRequestBodyAsync(request).ConfigureAwait(false);
```

</details>

## Alert #184 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/184
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:176-176`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   168	        }
   169	        catch (Exception ex)
   170	        {
   171	            _logger.Error($"Error handling {method} {path}", ex);
   172	            try
   173	            {
   174	                await WriteJsonResponseAsync(response, 500, "{\"error\":\"Internal server error\"}").ConfigureAwait(false);
   175	            }
   176	            catch { }
   177	        }
   178	        finally
   179	        {
   180	            response.Close();
   181	        }
   182	    }
   183	
   184	    private (bool success, string errorType, string message) AuthenticateRequest(HttpListenerRequest request)
```

</details>

## Alert #183 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/183
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:169-177`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   161	                case ("GET", "/diagnostics"):
   162	                    await HandleDiagnosticsAsync(response).ConfigureAwait(false);
   163	                    break;
   164	                default:
   165	                    await WriteJsonResponseAsync(response, 404, "{\"error\":\"Not found\"}").ConfigureAwait(false);
   166	                    break;
   167	            }
   168	        }
   169	        catch (Exception ex)
   170	        {
   171	            _logger.Error($"Error handling {method} {path}", ex);
   172	            try
   173	            {
   174	                await WriteJsonResponseAsync(response, 500, "{\"error\":\"Internal server error\"}").ConfigureAwait(false);
   175	            }
   176	            catch { }
   177	        }
   178	        finally
   179	        {
   180	            response.Close();
   181	        }
   182	    }
   183	
   184	    private (bool success, string errorType, string message) AuthenticateRequest(HttpListenerRequest request)
   185	    {
```

</details>

## Alert #182 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/182
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:89-92`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
    81	            catch (HttpListenerException) when (cancellationToken.IsCancellationRequested)
    82	            {
    83	                break;
    84	            }
    85	            catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
    86	            {
    87	                break;
    88	            }
    89	            catch (Exception ex)
    90	            {
    91	                _logger.Error("Error accepting connection", ex);
    92	            }
    93	        }
    94	    }
    95	
    96	    private async Task HandleRequestAsync(HttpListenerContext context)
    97	    {
    98	        var request = context.Request;
    99	        var response = context.Response;
   100	        response.Headers.Add("Access-Control-Allow-Origin", "*");
```

</details>

## Alert #181 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/181
- Location: `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:176-187`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #180 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/180
- Location: `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:35-39`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #179 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/179
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:51-54`
- Message: Generic catch clause.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
    43	                return _logDir;
    44	
    45	            _logDirResolved = true;
    46	
    47	            try
    48	            {
    49	                var localAppData = Environment.GetFolderPath(
    50	                    Environment.SpecialFolder.LocalApplicationData);
    51	                if (!string.IsNullOrEmpty(localAppData))
    52	                {
    53	                    _logDir = Path.Combine(localAppData, "TiaAgent", "logs");
    54	                }
    55	            }
    56	            catch
    57	            {
    58	                // EnvironmentPermission not granted — file logging will be disabled
    59	            }
    60	
    61	            return _logDir;
    62	        }
```

</details>

## Alert #178 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/178
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:162-165`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #177 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/177
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:87-90`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #176 — cs/catch-of-all-exceptions

- Rule: `cs/catch-of-all-exceptions`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/176
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:46-49`
- Message: Generic catch clause.

- Current file exists on `main`: **no**

## Alert #175 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/175
- Location: `tests/TiaAgent.Runtime.Tests/HealthCheckTests.cs:23-23`
- Message: This assignment to deadline is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 80 lines

<details><summary>Current code context</summary>

```text
    15	        maxRetries.Should().Be(30);
    16	    }
    17	
    18	    [Fact]
    19	    public void HealthCheck_TimeoutSeconds_IsRespected()
    20	    {
    21	        var timeoutSeconds = 5;
    22	        var startTime = DateTime.UtcNow;
    23	        var deadline = startTime.AddSeconds(timeoutSeconds);
    24	        var elapsed = (DateTime.UtcNow - startTime).TotalSeconds;
    25	
    26	        elapsed.Should().BeLessThanOrEqualTo(timeoutSeconds + 1); // Allow 1s tolerance
    27	    }
    28	
    29	    [Fact]
    30	    public void HealthCheck_RetryInterval_IsPositive()
    31	    {
```

</details>

## Alert #174 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/174
- Location: `tests/TiaAgent.Application.Tests/CorrelationContextTests.cs:48-48`
- Message: This assignment to inner is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 70 lines

<details><summary>Current code context</summary>

```text
    40	    public void SetCorrelationId_NestedScopes()
    41	    {
    42	        var context = new CorrelationContext();
    43	
    44	        using (var outer = context.SetCorrelationId("outer"))
    45	        {
    46	            Assert.Equal("outer", context.CurrentCorrelationId);
    47	
    48	            using (var inner = context.SetCorrelationId("inner"))
    49	            {
    50	                Assert.Equal("inner", context.CurrentCorrelationId);
    51	            }
    52	
    53	            Assert.Equal("outer", context.CurrentCorrelationId);
    54	        }
    55	
    56	        Assert.Equal("none", context.CurrentCorrelationId);
```

</details>

## Alert #173 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/173
- Location: `tests/TiaAgent.Application.Tests/CorrelationContextTests.cs:44-44`
- Message: This assignment to outer is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 70 lines

<details><summary>Current code context</summary>

```text
    36	        Assert.Equal("none", context.CurrentCorrelationId);
    37	    }
    38	
    39	    [Fact]
    40	    public void SetCorrelationId_NestedScopes()
    41	    {
    42	        var context = new CorrelationContext();
    43	
    44	        using (var outer = context.SetCorrelationId("outer"))
    45	        {
    46	            Assert.Equal("outer", context.CurrentCorrelationId);
    47	
    48	            using (var inner = context.SetCorrelationId("inner"))
    49	            {
    50	                Assert.Equal("inner", context.CurrentCorrelationId);
    51	            }
    52	
```

</details>

## Alert #172 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/172
- Location: `tests/TiaAgent.Application.Tests/CorrelationContextTests.cs:31-31`
- Message: This assignment to scope is useless, since its value is never read.

- Current file exists on `main`: **yes**
- Current file length: 70 lines

<details><summary>Current code context</summary>

```text
    23	        Assert.Equal("corr-001", context.CurrentCorrelationId);
    24	    }
    25	
    26	    [Fact]
    27	    public void SetCorrelationId_RestoresPreviousOnDispose()
    28	    {
    29	        var context = new CorrelationContext();
    30	
    31	        using (var scope = context.SetCorrelationId("corr-001"))
    32	        {
    33	            Assert.Equal("corr-001", context.CurrentCorrelationId);
    34	        }
    35	
    36	        Assert.Equal("none", context.CurrentCorrelationId);
    37	    }
    38	
    39	    [Fact]
```

</details>

## Alert #171 — cs/useless-assignment-to-local

- Rule: `cs/useless-assignment-to-local`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/171
- Location: `src/TiaAgent.AddIn/obj/Release/net48/Ui/AssistantPanel.g.cs:55-55`
- Message: This assignment to resourceLocater is useless, since its value is never read.

- Current file exists on `main`: **no**

## Alert #170 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/170
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:68-68`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    60	    }
    61	
    62	    [Fact]
    63	    public void StaleSecrets_OlderThan24Hours_AreCleaned()
    64	    {
    65	        var secretsDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"), "secrets");
    66	        Directory.CreateDirectory(secretsDir);
    67	
    68	        var secretFile = Path.Combine(secretsDir, "old.token");
    69	        File.WriteAllText(secretFile, "old-secret");
    70	        File.SetLastWriteTimeUtc(secretFile, DateTime.UtcNow.AddHours(-25));
    71	
    72	        var files = Directory.GetFiles(secretsDir);
    73	        var staleFiles = files.Where(f => (DateTime.UtcNow - File.GetLastWriteTimeUtc(f)) > TimeSpan.FromHours(24)).ToList();
    74	        staleFiles.Should().HaveCount(1);
    75	
    76	        // Cleanup
```

</details>

## Alert #169 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/169
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:65-65`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    57	        // Cleanup
    58	        File.Delete(lockPath);
    59	        Directory.Delete(lockDir);
    60	    }
    61	
    62	    [Fact]
    63	    public void StaleSecrets_OlderThan24Hours_AreCleaned()
    64	    {
    65	        var secretsDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"), "secrets");
    66	        Directory.CreateDirectory(secretsDir);
    67	
    68	        var secretFile = Path.Combine(secretsDir, "old.token");
    69	        File.WriteAllText(secretFile, "old-secret");
    70	        File.SetLastWriteTimeUtc(secretFile, DateTime.UtcNow.AddHours(-25));
    71	
    72	        var files = Directory.GetFiles(secretsDir);
    73	        var staleFiles = files.Where(f => (DateTime.UtcNow - File.GetLastWriteTimeUtc(f)) > TimeSpan.FromHours(24)).ToList();
```

</details>

## Alert #168 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/168
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:50-50`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    42	    }
    43	
    44	    [Fact]
    45	    public void StaleDetection_FreshLock_IsNotStale()
    46	    {
    47	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    48	        Directory.CreateDirectory(lockDir);
    49	
    50	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    51	        File.WriteAllText(lockPath, """{"instanceId":"new","supervisorPid":12345}""");
    52	
    53	        var lastWrite = File.GetLastWriteTimeUtc(lockPath);
    54	        var isStale = (DateTime.UtcNow - lastWrite) > TimeSpan.FromHours(24);
    55	        isStale.Should().BeFalse();
    56	
    57	        // Cleanup
    58	        File.Delete(lockPath);
```

</details>

## Alert #167 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/167
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:47-47`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    39	        // Cleanup
    40	        File.Delete(lockPath);
    41	        Directory.Delete(lockDir);
    42	    }
    43	
    44	    [Fact]
    45	    public void StaleDetection_FreshLock_IsNotStale()
    46	    {
    47	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    48	        Directory.CreateDirectory(lockDir);
    49	
    50	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    51	        File.WriteAllText(lockPath, """{"instanceId":"new","supervisorPid":12345}""");
    52	
    53	        var lastWrite = File.GetLastWriteTimeUtc(lockPath);
    54	        var isStale = (DateTime.UtcNow - lastWrite) > TimeSpan.FromHours(24);
    55	        isStale.Should().BeFalse();
```

</details>

## Alert #166 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/166
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:30-30`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    22	    }
    23	
    24	    [Fact]
    25	    public void StaleDetection_LockFileOlderThanTimeout_IsStale()
    26	    {
    27	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    28	        Directory.CreateDirectory(lockDir);
    29	
    30	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    31	        var oldTime = DateTime.UtcNow.AddHours(-25);
    32	        File.WriteAllText(lockPath, """{"instanceId":"old","supervisorPid":99999}""");
    33	        File.SetLastWriteTimeUtc(lockPath, oldTime);
    34	
    35	        var lastWrite = File.GetLastWriteTimeUtc(lockPath);
    36	        var isStale = (DateTime.UtcNow - lastWrite) > TimeSpan.FromHours(24);
    37	        isStale.Should().BeTrue();
    38	
```

</details>

## Alert #165 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/165
- Location: `tests/TiaAgent.Runtime.Tests/StaleRuntimeTests.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 79 lines

<details><summary>Current code context</summary>

```text
    19	        // PID 99999 is unlikely to exist - GetProcessById throws if not found
    20	        Action act = () => System.Diagnostics.Process.GetProcessById(99999);
    21	        act.Should().Throw<ArgumentException>();
    22	    }
    23	
    24	    [Fact]
    25	    public void StaleDetection_LockFileOlderThanTimeout_IsStale()
    26	    {
    27	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    28	        Directory.CreateDirectory(lockDir);
    29	
    30	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    31	        var oldTime = DateTime.UtcNow.AddHours(-25);
    32	        File.WriteAllText(lockPath, """{"instanceId":"old","supervisorPid":99999}""");
    33	        File.SetLastWriteTimeUtc(lockPath, oldTime);
    34	
    35	        var lastWrite = File.GetLastWriteTimeUtc(lockPath);
```

</details>

## Alert #164 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/164
- Location: `tests/TiaAgent.Runtime.Tests/ShutdownTests.cs:61-61`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 88 lines

<details><summary>Current code context</summary>

```text
    53	    }
    54	
    55	    [Fact]
    56	    public void Shutdown_LockFile_IsRemoved()
    57	    {
    58	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    59	        Directory.CreateDirectory(lockDir);
    60	
    61	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    62	        File.WriteAllText(lockPath, """{"instanceId":"test"}""");
    63	
    64	        File.Exists(lockPath).Should().BeTrue();
    65	
    66	        File.Delete(lockPath);
    67	        Directory.Delete(lockDir);
    68	
    69	        File.Exists(lockPath).Should().BeFalse();
```

</details>

## Alert #163 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/163
- Location: `tests/TiaAgent.Runtime.Tests/ShutdownTests.cs:58-58`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 88 lines

<details><summary>Current code context</summary>

```text
    50	        Directory.Delete(secretsDir);
    51	
    52	        File.Exists(secretFile).Should().BeFalse();
    53	    }
    54	
    55	    [Fact]
    56	    public void Shutdown_LockFile_IsRemoved()
    57	    {
    58	        var lockDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    59	        Directory.CreateDirectory(lockDir);
    60	
    61	        var lockPath = Path.Combine(lockDir, "supervisor.lock");
    62	        File.WriteAllText(lockPath, """{"instanceId":"test"}""");
    63	
    64	        File.Exists(lockPath).Should().BeTrue();
    65	
    66	        File.Delete(lockPath);
```

</details>

## Alert #162 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/162
- Location: `tests/TiaAgent.Runtime.Tests/ShutdownTests.cs:43-43`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 88 lines

<details><summary>Current code context</summary>

```text
    35	    }
    36	
    37	    [Fact]
    38	    public void Shutdown_Secrets_AreCleaned()
    39	    {
    40	        var secretsDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"), "secrets");
    41	        Directory.CreateDirectory(secretsDir);
    42	
    43	        var secretFile = Path.Combine(secretsDir, "mcp.token");
    44	        File.WriteAllText(secretFile, "test-secret");
    45	
    46	        File.Exists(secretFile).Should().BeTrue();
    47	
    48	        // Cleanup
    49	        File.Delete(secretFile);
    50	        Directory.Delete(secretsDir);
    51	
```

</details>

## Alert #161 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/161
- Location: `tests/TiaAgent.Runtime.Tests/ShutdownTests.cs:40-40`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 88 lines

<details><summary>Current code context</summary>

```text
    32	        var elapsed = (deadline - startTime).TotalSeconds;
    33	
    34	        elapsed.Should().Be(gracefulTimeoutSeconds);
    35	    }
    36	
    37	    [Fact]
    38	    public void Shutdown_Secrets_AreCleaned()
    39	    {
    40	        var secretsDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"), "secrets");
    41	        Directory.CreateDirectory(secretsDir);
    42	
    43	        var secretFile = Path.Combine(secretsDir, "mcp.token");
    44	        File.WriteAllText(secretFile, "test-secret");
    45	
    46	        File.Exists(secretFile).Should().BeTrue();
    47	
    48	        // Cleanup
```

</details>

## Alert #160 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/160
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:60-60`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    52	        // Validation would fail, so we don't move
    53	        var readBack = File.ReadAllText(manifestPath);
    54	        readBack.Should().Be(originalContent);
    55	    }
    56	
    57	    [Fact]
    58	    public async Task AtomicWrite_ConcurrentWrites_DoNotCorrupt()
    59	    {
    60	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    61	        var tasks = new List<Task>();
    62	
    63	        for (int i = 0; i < 10; i++)
    64	        {
    65	            var index = i;
    66	            tasks.Add(Task.Run(() =>
    67	            {
    68	                var content = $"{{\"schemaVersion\":1,\"status\":\"ready\",\"index\":{index}}}";
```

</details>

## Alert #159 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/159
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:49-49`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    41	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    42	        var originalContent = """{"schemaVersion":1,"status":"ready"}""";
    43	        var invalidContent = """{"schemaVersion":1,"status": INVALID JSON""";
    44	
    45	        // Write original
    46	        File.WriteAllText(manifestPath, originalContent);
    47	
    48	        // Try to write invalid content
    49	        var tempPath = Path.Combine(_testDir, "runtime.json.tmp.1234");
    50	        File.WriteAllText(tempPath, invalidContent);
    51	
    52	        // Validation would fail, so we don't move
    53	        var readBack = File.ReadAllText(manifestPath);
    54	        readBack.Should().Be(originalContent);
    55	    }
    56	
    57	    [Fact]
```

</details>

## Alert #158 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/158
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:41-41`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    33	
    34	        File.Exists(manifestPath).Should().BeTrue();
    35	        File.Exists(tempPath).Should().BeFalse();
    36	    }
    37	
    38	    [Fact]
    39	    public void AtomicWrite_FailedValidation_LeavesOriginalIntact()
    40	    {
    41	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    42	        var originalContent = """{"schemaVersion":1,"status":"ready"}""";
    43	        var invalidContent = """{"schemaVersion":1,"status": INVALID JSON""";
    44	
    45	        // Write original
    46	        File.WriteAllText(manifestPath, originalContent);
    47	
    48	        // Try to write invalid content
    49	        var tempPath = Path.Combine(_testDir, "runtime.json.tmp.1234");
```

</details>

## Alert #157 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/157
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    12	        _testDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    13	        Directory.CreateDirectory(_testDir);
    14	    }
    15	
    16	    [Fact]
    17	    public void AtomicWrite_CreatesTempFileThenMoves()
    18	    {
    19	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    20	        var tempPath = Path.Combine(_testDir, "runtime.json.tmp.1234");
    21	
    22	        var content = """{"schemaVersion":1,"status":"ready"}""";
    23	
    24	        // Write temp file
    25	        File.WriteAllText(tempPath, content);
    26	
    27	        // Validate
    28	        var readBack = File.ReadAllText(tempPath);
```

</details>

## Alert #156 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/156
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
    11	    {
    12	        _testDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    13	        Directory.CreateDirectory(_testDir);
    14	    }
    15	
    16	    [Fact]
    17	    public void AtomicWrite_CreatesTempFileThenMoves()
    18	    {
    19	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    20	        var tempPath = Path.Combine(_testDir, "runtime.json.tmp.1234");
    21	
    22	        var content = """{"schemaVersion":1,"status":"ready"}""";
    23	
    24	        // Write temp file
    25	        File.WriteAllText(tempPath, content);
    26	
    27	        // Validate
```

</details>

## Alert #155 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/155
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriterTests.cs:12-12`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 83 lines

<details><summary>Current code context</summary>

```text
     4	namespace TiaAgent.Runtime.Tests;
     5	
     6	public class ManifestWriterTests
     7	{
     8	    private readonly string _testDir;
     9	
    10	    public ManifestWriterTests()
    11	    {
    12	        _testDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    13	        Directory.CreateDirectory(_testDir);
    14	    }
    15	
    16	    [Fact]
    17	    public void AtomicWrite_CreatesTempFileThenMoves()
    18	    {
    19	        var manifestPath = Path.Combine(_testDir, "runtime.json");
    20	        var tempPath = Path.Combine(_testDir, "runtime.json.tmp.1234");
```

</details>

## Alert #154 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/154
- Location: `tests/TiaAgent.Runtime.Tests/ManifestWriter.cs:29-31`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 93 lines

<details><summary>Current code context</summary>

```text
    21	    /// 2. The temporary file replaces the destination under a serialized lock.
    22	    /// 3. Any leftover temporary file is cleaned up.
    23	    /// </summary>
    24	    public static void WriteAtomic(string destinationPath, string content)
    25	    {
    26	        var directory = Path.GetDirectoryName(destinationPath)
    27	            ?? throw new ArgumentException("Destination path has no directory.", nameof(destinationPath));
    28	
    29	        var tempPath = Path.Combine(
    30	            directory,
    31	            $"{Path.GetFileName(destinationPath)}.tmp.{Guid.NewGuid():N}");
    32	
    33	        try
    34	        {
    35	            // Stage 1: write to a unique temp file — concurrent, no lock needed.
    36	            File.WriteAllText(tempPath, content);
    37	
    38	            // Stage 2: serialize the final replace so only one writer
    39	            // touches the destination at a time.
```

</details>

## Alert #153 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/153
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:192-192`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
   184	
   185	        result.IsValid.Should().BeFalse();
   186	        result.Errors.Should().ContainMatch("*SHA256 hash mismatch*");
   187	    }
   188	
   189	    [Fact]
   190	    public void ValidatePayload_WithProhibitedSiemensAssembly_ReturnsFailure()
   191	    {
   192	        var siemensDllPath = Path.Combine(_tempDirectory, "Siemens.Engineering.dll");
   193	        File.WriteAllText(siemensDllPath, "Siemens Mock");
   194	
   195	        var manifest = new PayloadManifest
   196	        {
   197	            ProductVersion = "0.2.0-beta.1",
   198	            Files =
   199	            {
   200	                new PayloadFileEntry
```

</details>

## Alert #152 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/152
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:164-164`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
   156	
   157	        result.IsValid.Should().BeFalse();
   158	        result.Errors.Should().ContainMatch("*File size mismatch*");
   159	    }
   160	
   161	    [Fact]
   162	    public void ValidatePayload_WithHashMismatch_ReturnsFailure()
   163	    {
   164	        var filePath = Path.Combine(_tempDirectory, "file.txt");
   165	        File.WriteAllText(filePath, "Hello World");
   166	        var fileInfo = new FileInfo(filePath);
   167	
   168	        var manifest = new PayloadManifest
   169	        {
   170	            ProductVersion = "0.2.0-beta.1",
   171	            Files =
   172	            {
```

</details>

## Alert #151 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/151
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:137-137`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
   129	
   130	        result.IsValid.Should().BeFalse();
   131	        result.Errors.Should().ContainMatch("*Missing payload file*MissingFile.dll*");
   132	    }
   133	
   134	    [Fact]
   135	    public void ValidatePayload_WithSizeMismatch_ReturnsFailure()
   136	    {
   137	        var filePath = Path.Combine(_tempDirectory, "file.txt");
   138	        File.WriteAllText(filePath, "Hello World");
   139	
   140	        var manifest = new PayloadManifest
   141	        {
   142	            ProductVersion = "0.2.0-beta.1",
   143	            Files =
   144	            {
   145	                new PayloadFileEntry
```

</details>

## Alert #150 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/150
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:78-78`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
    70	        result.Errors.Should().BeEmpty();
    71	        result.Manifest.Should().NotBeNull();
    72	        result.Manifest!.ProductVersion.Should().Be("0.2.0-beta.1");
    73	    }
    74	
    75	    [Fact]
    76	    public void ValidatePayload_WithMissingDirectory_ReturnsFailure()
    77	    {
    78	        var nonExistentPath = Path.Combine(_tempDirectory, "nonexistent");
    79	        var result = PayloadValidator.ValidatePayload(nonExistentPath);
    80	
    81	        result.IsValid.Should().BeFalse();
    82	        result.Errors.Should().ContainMatch("*Payload directory does not exist*");
    83	    }
    84	
    85	    [Fact]
    86	    public void ValidatePayload_WithMissingManifest_ReturnsFailure()
```

</details>

## Alert #149 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/149
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:34-34`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
    26	        GC.SuppressFinalize(this);
    27	    }
    28	
    29	    [Fact]
    30	    public void ValidatePayload_WithValidPayload_ReturnsSuccess()
    31	    {
    32	        var bridgeDir = Path.Combine(_tempDirectory, "Bridge");
    33	        Directory.CreateDirectory(bridgeDir);
    34	        var bridgeDllPath = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
    35	        var bridgeContent = Encoding.UTF8.GetBytes("Fake Bridge DLL Content");
    36	        File.WriteAllBytes(bridgeDllPath, bridgeContent);
    37	
    38	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDllPath);
    39	
    40	        var manifest = new PayloadManifest
    41	        {
    42	            ProductVersion = "0.2.0-beta.1",
```

</details>

## Alert #148 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/148
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:32-32`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
    24	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    25	        }
    26	        GC.SuppressFinalize(this);
    27	    }
    28	
    29	    [Fact]
    30	    public void ValidatePayload_WithValidPayload_ReturnsSuccess()
    31	    {
    32	        var bridgeDir = Path.Combine(_tempDirectory, "Bridge");
    33	        Directory.CreateDirectory(bridgeDir);
    34	        var bridgeDllPath = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
    35	        var bridgeContent = Encoding.UTF8.GetBytes("Fake Bridge DLL Content");
    36	        File.WriteAllBytes(bridgeDllPath, bridgeContent);
    37	
    38	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDllPath);
    39	
    40	        var manifest = new PayloadManifest
```

</details>

## Alert #147 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/147
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
     8	namespace TiaAgent.Cli.Tests.Payload;
     9	
    10	public sealed class PayloadValidatorTests : IDisposable
    11	{
    12	    private readonly string _tempDirectory;
    13	
    14	    public PayloadValidatorTests()
    15	    {
    16	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadValidatorTests_" + Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_tempDirectory);
    18	    }
    19	
    20	    public void Dispose()
    21	    {
    22	        if (Directory.Exists(_tempDirectory))
    23	        {
    24	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
```

</details>

## Alert #146 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/146
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadManifestTests.cs:95-95`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
    87	        loaded.Files.Should().HaveCount(1);
    88	        loaded.Files[0].RelativePath.Should().Be("Bridge/TiaAgent.Bridge.dll");
    89	    }
    90	
    91	    [Fact]
    92	    public void PayloadLocator_GetBundledPayloadDirectory_ShouldResolvePaths()
    93	    {
    94	        var customPath = _tempDirectory;
    95	        var subPayload = Path.Combine(customPath, "payload");
    96	        Directory.CreateDirectory(subPayload);
    97	
    98	        var resolved = PayloadLocator.GetBundledPayloadDirectory(customPath);
    99	        resolved.Should().Be(subPayload);
   100	    }
   101	
   102	    [Fact]
   103	    public void PayloadLocator_GetBundledPayloadDirectory_WithNoPayloadSubdirectory_ShouldFallbackToBasePath()
```

</details>

## Alert #145 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/145
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadManifestTests.cs:78-78`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
    70	                    Sha256Hash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    71	                    SizeBytes = 1024
    72	                }
    73	            }
    74	        };
    75	
    76	        PayloadStore.WriteManifest(_tempDirectory, manifest);
    77	
    78	        File.Exists(Path.Combine(_tempDirectory, PayloadStore.ManifestFileName)).Should().BeTrue();
    79	
    80	        var loaded = PayloadStore.ReadManifest(_tempDirectory);
    81	        loaded.Should().NotBeNull();
    82	        loaded.SchemaVersion.Should().Be(1);
    83	        loaded.ProductVersion.Should().Be("0.2.0-beta.1");
    84	        loaded.CommitSha.Should().Be("abc1234");
    85	        loaded.Components.Should().ContainKey("bridge");
    86	        loaded.Components["bridge"].RelativePath.Should().Be("Bridge/TiaAgent.Bridge.dll");
```

</details>

## Alert #144 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/144
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadManifestTests.cs:15-15`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
     7	namespace TiaAgent.Cli.Tests.Payload;
     8	
     9	public sealed class PayloadManifestTests : IDisposable
    10	{
    11	    private readonly string _tempDirectory;
    12	
    13	    public PayloadManifestTests()
    14	    {
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadManifestTests_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
```

</details>

## Alert #143 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/143
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:105-105`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    97	
    98	        act.Should().Throw<InvalidDataException>()
    99	           .WithMessage("*malformed JSON*");
   100	    }
   101	
   102	    [Fact]
   103	    public void ManifestStore_ReadEmptyFile_ShouldThrowInvalidDataException()
   104	    {
   105	        var manifestPath = Path.Combine(_tempDirectory, "empty.json");
   106	        File.WriteAllText(manifestPath, "   ");
   107	
   108	        Action act = () => ManifestStore.Read<CurrentManifest>(manifestPath);
   109	
   110	        act.Should().Throw<InvalidDataException>()
   111	           .WithMessage("*is empty*");
   112	    }
   113	}
```

</details>

## Alert #142 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/142
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:93-93`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    85	
    86	        loaded.Should().NotBeNull();
    87	        loaded.ActiveVersion.Should().BeEmpty();
    88	    }
    89	
    90	    [Fact]
    91	    public void ManifestStore_ReadMalformedFile_ShouldThrowInvalidDataException()
    92	    {
    93	        var manifestPath = Path.Combine(_tempDirectory, "corrupt.json");
    94	        File.WriteAllText(manifestPath, "{ invalid json format: ");
    95	
    96	        Action act = () => ManifestStore.Read<CurrentManifest>(manifestPath);
    97	
    98	        act.Should().Throw<InvalidDataException>()
    99	           .WithMessage("*malformed JSON*");
   100	    }
   101	
```

</details>

## Alert #141 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/141
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:82-82`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    74	        loaded.SchemaVersion.Should().Be(1);
    75	        loaded.ActiveVersion.Should().Be("0.2.0-beta.1");
    76	        loaded.ActivatedBy.Should().Be("cli-test");
    77	    }
    78	
    79	    [Fact]
    80	    public void ManifestStore_ReadMissingFile_ShouldReturnNewDefaultInstance()
    81	    {
    82	        var manifestPath = Path.Combine(_tempDirectory, "nonexistent.json");
    83	
    84	        var loaded = ManifestStore.Read<CurrentManifest>(manifestPath);
    85	
    86	        loaded.Should().NotBeNull();
    87	        loaded.ActiveVersion.Should().BeEmpty();
    88	    }
    89	
    90	    [Fact]
```

</details>

## Alert #140 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/140
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:60-60`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    52	        Directory.Exists(layout.LogsPath).Should().BeTrue();
    53	        Directory.Exists(layout.RuntimePath).Should().BeTrue();
    54	        Directory.Exists(layout.CachePath).Should().BeTrue();
    55	    }
    56	
    57	    [Fact]
    58	    public void ManifestStore_WriteAtomicAndRead_ShouldPersistDataCorrectly()
    59	    {
    60	        var manifestPath = Path.Combine(_tempDirectory, "current.json");
    61	        var manifest = new CurrentManifest
    62	        {
    63	            SchemaVersion = 1,
    64	            ActiveVersion = "0.2.0-beta.1",
    65	            ActivatedBy = "cli-test",
    66	        };
    67	
    68	        ManifestStore.WriteAtomic(manifestPath, manifest);
```

</details>

## Alert #139 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/139
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:40-40`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
    44	    public void Layout_EnsureDirectoriesExist_ShouldCreateAllDirectories()
    45	    {
    46	        var layout = new TiaAgentLayout(_tempDirectory);
    47	
    48	        layout.EnsureDirectoriesExist();
```

</details>

## Alert #138 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/138
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:39-39`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
    44	    public void Layout_EnsureDirectoriesExist_ShouldCreateAllDirectories()
    45	    {
    46	        var layout = new TiaAgentLayout(_tempDirectory);
    47	
```

</details>

## Alert #137 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/137
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:38-38`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
    44	    public void Layout_EnsureDirectoriesExist_ShouldCreateAllDirectories()
    45	    {
    46	        var layout = new TiaAgentLayout(_tempDirectory);
```

</details>

## Alert #136 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/136
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:37-37`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
    44	    public void Layout_EnsureDirectoriesExist_ShouldCreateAllDirectories()
    45	    {
```

</details>

## Alert #135 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/135
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:36-36`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    28	    [Fact]
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
    44	    public void Layout_EnsureDirectoriesExist_ShouldCreateAllDirectories()
```

</details>

## Alert #134 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/134
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:35-35`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    27	
    28	    [Fact]
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
    43	    [Fact]
```

</details>

## Alert #133 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/133
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:34-34`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    26	    }
    27	
    28	    [Fact]
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
    32	
    33	        layout.RootPath.Should().Be(_tempDirectory);
    34	        layout.VersionsPath.Should().Be(Path.Combine(_tempDirectory, "versions"));
    35	        layout.ConfigPath.Should().Be(Path.Combine(_tempDirectory, "config.json"));
    36	        layout.CurrentManifestPath.Should().Be(Path.Combine(_tempDirectory, "current.json"));
    37	        layout.InstallationsManifestPath.Should().Be(Path.Combine(_tempDirectory, "installations.json"));
    38	        layout.LogsPath.Should().Be(Path.Combine(_tempDirectory, "logs"));
    39	        layout.RuntimePath.Should().Be(Path.Combine(_tempDirectory, "runtime"));
    40	        layout.CachePath.Should().Be(Path.Combine(_tempDirectory, "cache"));
    41	    }
    42	
```

</details>

## Alert #132 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/132
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:15-15`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
     7	namespace TiaAgent.Cli.Tests.Layout;
     8	
     9	public class ManifestStoreTests : IDisposable
    10	{
    11	    private readonly string _tempDirectory;
    12	
    13	    public ManifestStoreTests()
    14	    {
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "TiaAgentTest_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
```

</details>

## Alert #131 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/131
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionCommandTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 106 lines

<details><summary>Current code context</summary>

```text
    11	public sealed class VersionCommandTests : IDisposable
    12	{
    13	    private readonly string _tempDirectory;
    14	    private readonly string _customRoot;
    15	
    16	    public VersionCommandTests()
    17	    {
    18	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionCommandTests_" + Guid.NewGuid().ToString("N"));
    19	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    20	
    21	        Directory.CreateDirectory(_tempDirectory);
    22	        Directory.CreateDirectory(_customRoot);
    23	    }
    24	
    25	    public void Dispose()
    26	    {
    27	        if (Directory.Exists(_tempDirectory))
```

</details>

## Alert #130 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/130
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionCommandTests.cs:18-18`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 106 lines

<details><summary>Current code context</summary>

```text
    10	
    11	public sealed class VersionCommandTests : IDisposable
    12	{
    13	    private readonly string _tempDirectory;
    14	    private readonly string _customRoot;
    15	
    16	    public VersionCommandTests()
    17	    {
    18	        _tempDirectory = Path.Combine(Path.GetTempPath(), "VersionCommandTests_" + Guid.NewGuid().ToString("N"));
    19	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    20	
    21	        Directory.CreateDirectory(_tempDirectory);
    22	        Directory.CreateDirectory(_customRoot);
    23	    }
    24	
    25	    public void Dispose()
    26	    {
```

</details>

## Alert #129 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/129
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:350-350`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   342	        var addinDir = Path.Combine(payloadDir, "AddIn");
   343	        Directory.CreateDirectory(bridgeDir);
   344	        Directory.CreateDirectory(addinDir);
   345	
   346	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   347	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge DLL Content");
   348	        File.WriteAllBytes(bridgeDll, bridgeContent);
   349	
   350	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   351	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content");
   352	        File.WriteAllBytes(addinFile, addinContent);
   353	
   354	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
   355	        var addinHash = PayloadStore.ComputeSha256(addinFile);
   356	
   357	        var manifest = new PayloadManifest
   358	        {
```

</details>

## Alert #128 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/128
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:346-346`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   338	
   339	    private static void CreateDummyPayload(string payloadDir, string version)
   340	    {
   341	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   342	        var addinDir = Path.Combine(payloadDir, "AddIn");
   343	        Directory.CreateDirectory(bridgeDir);
   344	        Directory.CreateDirectory(addinDir);
   345	
   346	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   347	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge DLL Content");
   348	        File.WriteAllBytes(bridgeDll, bridgeContent);
   349	
   350	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
   351	        var addinContent = Encoding.UTF8.GetBytes("AddIn Content");
   352	        File.WriteAllBytes(addinFile, addinContent);
   353	
   354	        var bridgeHash = PayloadStore.ComputeSha256(bridgeDll);
```

</details>

## Alert #127 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/127
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:342-342`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   334	        stdout.ToString().Should().Contain("Successfully installed");
   335	        // Should not have deployed to UserAddIns since no .addin was present
   336	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeFalse();
   337	    }
   338	
   339	    private static void CreateDummyPayload(string payloadDir, string version)
   340	    {
   341	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   342	        var addinDir = Path.Combine(payloadDir, "AddIn");
   343	        Directory.CreateDirectory(bridgeDir);
   344	        Directory.CreateDirectory(addinDir);
   345	
   346	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   347	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge DLL Content");
   348	        File.WriteAllBytes(bridgeDll, bridgeContent);
   349	
   350	        var addinFile = Path.Combine(addinDir, "TiaAgent-0.2.0.addin");
```

</details>

## Alert #126 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/126
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:341-341`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   333	        exitCode.Should().Be(0);
   334	        stdout.ToString().Should().Contain("Successfully installed");
   335	        // Should not have deployed to UserAddIns since no .addin was present
   336	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeFalse();
   337	    }
   338	
   339	    private static void CreateDummyPayload(string payloadDir, string version)
   340	    {
   341	        var bridgeDir = Path.Combine(payloadDir, "Bridge");
   342	        var addinDir = Path.Combine(payloadDir, "AddIn");
   343	        Directory.CreateDirectory(bridgeDir);
   344	        Directory.CreateDirectory(addinDir);
   345	
   346	        var bridgeDll = Path.Combine(bridgeDir, "TiaAgent.Bridge.dll");
   347	        var bridgeContent = Encoding.UTF8.GetBytes("Bridge DLL Content");
   348	        File.WriteAllBytes(bridgeDll, bridgeContent);
   349	
```

</details>

## Alert #125 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/125
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:169-169`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   161	        using var stdout = new StringWriter();
   162	        var exitCode = UninstallCommand.Execute(uninstallOptions, stdout, TextWriter.Null);
   163	
   164	        exitCode.Should().Be(0);
   165	        stdout.ToString().Should().Contain("Successfully uninstalled TIA Agent version(s): 0.2.0-beta.1");
   166	
   167	        var layout = new TiaAgentLayout(_customRoot);
   168	        Directory.Exists(layout.GetVersionPath("0.2.0-beta.1")).Should().BeFalse();
   169	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeFalse();
   170	    }
   171	
   172	    [Fact]
   173	    public void UninstallCommand_WithAllFlag_UninstallsAllVersions()
   174	    {
   175	        var installOptions = new InstallOptions
   176	        {
   177	            Version = "0.2.0-beta.1",
```

</details>

## Alert #124 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/124
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:125-125`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   117	
   118	        exitCode.Should().Be(0);
   119	        stdout.ToString().Should().Contain("Successfully installed TIA Agent v0.2.0-beta.1");
   120	    }
   121	
   122	    [Fact]
   123	    public void InstallCommand_WithInvalidPayload_ReturnsError()
   124	    {
   125	        var emptyPayloadDir = Path.Combine(_tempDirectory, "empty_payload");
   126	        Directory.CreateDirectory(emptyPayloadDir);
   127	
   128	        var options = new InstallOptions
   129	        {
   130	            PayloadDir = emptyPayloadDir,
   131	            CustomRoot = _customRoot,
   132	            UserAddInsDir = _userAddInsDir
   133	        };
```

</details>

## Alert #123 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/123
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:78-78`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    70	
    71	        var installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    72	        installations.Versions.Should().ContainKey("0.2.0-beta.1");
    73	
    74	        var versionDir = layout.GetVersionPath("0.2.0-beta.1");
    75	        Directory.Exists(versionDir).Should().BeTrue();
    76	        File.Exists(Path.Combine(versionDir, "Bridge", "TiaAgent.Bridge.dll")).Should().BeTrue();
    77	
    78	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    79	    }
    80	
    81	    [Fact]
    82	    public void InstallCommand_AlreadyInstalled_UpdatesActiveAndReturnsZero()
    83	    {
    84	        var options = new InstallOptions
    85	        {
    86	            Version = "0.2.0-beta.1",
```

</details>

## Alert #122 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/122
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:76-76`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    68	        var current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    69	        current.ActiveVersion.Should().Be("0.2.0-beta.1");
    70	
    71	        var installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
    72	        installations.Versions.Should().ContainKey("0.2.0-beta.1");
    73	
    74	        var versionDir = layout.GetVersionPath("0.2.0-beta.1");
    75	        Directory.Exists(versionDir).Should().BeTrue();
    76	        File.Exists(Path.Combine(versionDir, "Bridge", "TiaAgent.Bridge.dll")).Should().BeTrue();
    77	
    78	        File.Exists(Path.Combine(_userAddInsDir, "TiaAgent-0.2.0.addin")).Should().BeTrue();
    79	    }
    80	
    81	    [Fact]
    82	    public void InstallCommand_AlreadyInstalled_UpdatesActiveAndReturnsZero()
    83	    {
    84	        var options = new InstallOptions
```

</details>

## Alert #121 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/121
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    16	    private readonly string _userAddInsDir;
    17	    private readonly string _payloadDir;
    18	
    19	    public InstallerCommandTests()
    20	    {
    21	        _tempDirectory = Path.Combine(Path.GetTempPath(), "InstallerCommandTests_" + Guid.NewGuid().ToString("N"));
    22	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    24	        _payloadDir = Path.Combine(_tempDirectory, "payload");
    25	
    26	        Directory.CreateDirectory(_tempDirectory);
    27	        Directory.CreateDirectory(_customRoot);
    28	        Directory.CreateDirectory(_userAddInsDir);
    29	        Directory.CreateDirectory(_payloadDir);
    30	
    31	        CreateDummyPayload(_payloadDir, "0.2.0-beta.1");
    32	    }
```

</details>

## Alert #120 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/120
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:23-23`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    15	    private readonly string _customRoot;
    16	    private readonly string _userAddInsDir;
    17	    private readonly string _payloadDir;
    18	
    19	    public InstallerCommandTests()
    20	    {
    21	        _tempDirectory = Path.Combine(Path.GetTempPath(), "InstallerCommandTests_" + Guid.NewGuid().ToString("N"));
    22	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    24	        _payloadDir = Path.Combine(_tempDirectory, "payload");
    25	
    26	        Directory.CreateDirectory(_tempDirectory);
    27	        Directory.CreateDirectory(_customRoot);
    28	        Directory.CreateDirectory(_userAddInsDir);
    29	        Directory.CreateDirectory(_payloadDir);
    30	
    31	        CreateDummyPayload(_payloadDir, "0.2.0-beta.1");
```

</details>

## Alert #119 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/119
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:22-22`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    14	    private readonly string _tempDirectory;
    15	    private readonly string _customRoot;
    16	    private readonly string _userAddInsDir;
    17	    private readonly string _payloadDir;
    18	
    19	    public InstallerCommandTests()
    20	    {
    21	        _tempDirectory = Path.Combine(Path.GetTempPath(), "InstallerCommandTests_" + Guid.NewGuid().ToString("N"));
    22	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    24	        _payloadDir = Path.Combine(_tempDirectory, "payload");
    25	
    26	        Directory.CreateDirectory(_tempDirectory);
    27	        Directory.CreateDirectory(_customRoot);
    28	        Directory.CreateDirectory(_userAddInsDir);
    29	        Directory.CreateDirectory(_payloadDir);
    30	
```

</details>

## Alert #118 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/118
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    13	{
    14	    private readonly string _tempDirectory;
    15	    private readonly string _customRoot;
    16	    private readonly string _userAddInsDir;
    17	    private readonly string _payloadDir;
    18	
    19	    public InstallerCommandTests()
    20	    {
    21	        _tempDirectory = Path.Combine(Path.GetTempPath(), "InstallerCommandTests_" + Guid.NewGuid().ToString("N"));
    22	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    23	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    24	        _payloadDir = Path.Combine(_tempDirectory, "payload");
    25	
    26	        Directory.CreateDirectory(_tempDirectory);
    27	        Directory.CreateDirectory(_customRoot);
    28	        Directory.CreateDirectory(_userAddInsDir);
    29	        Directory.CreateDirectory(_payloadDir);
```

</details>

## Alert #117 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/117
- Location: `tests/TiaAgent.Cli.Tests/Commands/DoctorCommandTests.cs:21-21`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 136 lines

<details><summary>Current code context</summary>

```text
    13	    private readonly string _tempDirectory;
    14	    private readonly string _customRoot;
    15	    private readonly string _userAddInsDir;
    16	
    17	    public DoctorCommandTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "DoctorCommandTests_" + Guid.NewGuid().ToString("N"));
    20	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	
    23	        Directory.CreateDirectory(_tempDirectory);
    24	        Directory.CreateDirectory(_customRoot);
    25	        Directory.CreateDirectory(_userAddInsDir);
    26	    }
    27	
    28	    public void Dispose()
    29	    {
```

</details>

## Alert #116 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/116
- Location: `tests/TiaAgent.Cli.Tests/Commands/DoctorCommandTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 136 lines

<details><summary>Current code context</summary>

```text
    12	{
    13	    private readonly string _tempDirectory;
    14	    private readonly string _customRoot;
    15	    private readonly string _userAddInsDir;
    16	
    17	    public DoctorCommandTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "DoctorCommandTests_" + Guid.NewGuid().ToString("N"));
    20	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	
    23	        Directory.CreateDirectory(_tempDirectory);
    24	        Directory.CreateDirectory(_customRoot);
    25	        Directory.CreateDirectory(_userAddInsDir);
    26	    }
    27	
    28	    public void Dispose()
```

</details>

## Alert #115 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/115
- Location: `tests/TiaAgent.Cli.Tests/Commands/DoctorCommandTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 136 lines

<details><summary>Current code context</summary>

```text
    11	public sealed class DoctorCommandTests : IDisposable
    12	{
    13	    private readonly string _tempDirectory;
    14	    private readonly string _customRoot;
    15	    private readonly string _userAddInsDir;
    16	
    17	    public DoctorCommandTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "DoctorCommandTests_" + Guid.NewGuid().ToString("N"));
    20	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    21	        _userAddInsDir = Path.Combine(_tempDirectory, "UserAddIns");
    22	
    23	        Directory.CreateDirectory(_tempDirectory);
    24	        Directory.CreateDirectory(_customRoot);
    25	        Directory.CreateDirectory(_userAddInsDir);
    26	    }
    27	
```

</details>

## Alert #114 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/114
- Location: `tests/TiaAgent.Cli.Tests/Commands/ConfigCommandTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 183 lines

<details><summary>Current code context</summary>

```text
    12	public sealed class ConfigCommandTests : IDisposable
    13	{
    14	    private readonly string _tempDirectory;
    15	    private readonly string _customRoot;
    16	
    17	    public ConfigCommandTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ConfigCommandTests_" + Guid.NewGuid().ToString("N"));
    20	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    21	
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
```

</details>

## Alert #113 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/113
- Location: `tests/TiaAgent.Cli.Tests/Commands/ConfigCommandTests.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 183 lines

<details><summary>Current code context</summary>

```text
    11	
    12	public sealed class ConfigCommandTests : IDisposable
    13	{
    14	    private readonly string _tempDirectory;
    15	    private readonly string _customRoot;
    16	
    17	    public ConfigCommandTests()
    18	    {
    19	        _tempDirectory = Path.Combine(Path.GetTempPath(), "ConfigCommandTests_" + Guid.NewGuid().ToString("N"));
    20	        _customRoot = Path.Combine(_tempDirectory, "TiaAgentRoot");
    21	
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
```

</details>

## Alert #112 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/112
- Location: `tests/TiaAgent.Bridge.Tests/TokenProviderTests.cs:18-18`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    10	{
    11	    private readonly string _testDir;
    12	    private readonly string _testTokenFile;
    13	
    14	    public TokenProviderTests()
    15	    {
    16	        _testDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_testDir);
    18	        _testTokenFile = Path.Combine(_testDir, "bridge.token");
    19	    }
    20	
    21	    [Fact]
    22	    public void Token_IsNotEmpty()
    23	    {
    24	        var provider = new TokenProvider();
    25	        provider.Token.Should().NotBeNullOrEmpty();
    26	    }
```

</details>

## Alert #111 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/111
- Location: `tests/TiaAgent.Bridge.Tests/TokenProviderTests.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
     8	
     9	public class TokenProviderTests
    10	{
    11	    private readonly string _testDir;
    12	    private readonly string _testTokenFile;
    13	
    14	    public TokenProviderTests()
    15	    {
    16	        _testDir = Path.Combine(Path.GetTempPath(), "TiaAgentTests", Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_testDir);
    18	        _testTokenFile = Path.Combine(_testDir, "bridge.token");
    19	    }
    20	
    21	    [Fact]
    22	    public void Token_IsNotEmpty()
    23	    {
    24	        var provider = new TokenProvider();
```

</details>

## Alert #110 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/110
- Location: `tests/TiaAgent.Bridge.Tests/RuntimeConfigTests.cs:20-20`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
    12	public class RuntimeConfigTests : IDisposable
    13	{
    14	    private readonly string _tempDir;
    15	    private readonly BridgeLogger _logger = new();
    16	    private static readonly JsonSerializerOptions s_jsonOptions = new() { PropertyNameCaseInsensitive = true };
    17	
    18	    public RuntimeConfigTests()
    19	    {
    20	        _tempDir = Path.Combine(Path.GetTempPath(), $"TiaAgentTest_{Guid.NewGuid():N}");
    21	        Directory.CreateDirectory(_tempDir);
    22	    }
    23	
    24	    public void Dispose()
    25	    {
    26	        if (Directory.Exists(_tempDir))
    27	            Directory.Delete(_tempDir, true);
    28	        GC.SuppressFinalize(this);
```

</details>

## Alert #109 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/109
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:67-67`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    59	        securityPermissions.Should().NotContain("System.Security.Permissions.SecurityPermission.SkipVerification");
    60	    }
    61	
    62	    private static string FindRepositoryRoot()
    63	    {
    64	        var directory = new DirectoryInfo(AppContext.BaseDirectory);
    65	        while (directory is not null)
    66	        {
    67	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
    68	            {
    69	                return directory.FullName;
    70	            }
    71	
    72	            directory = directory.Parent;
    73	        }
    74	
    75	        throw new DirectoryNotFoundException("Could not locate the repository root.");
```

</details>

## Alert #108 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/108
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:75-75`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    67	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
    68	            {
    69	                return directory.FullName;
    70	            }
    71	
    72	            directory = directory.Parent;
    73	        }
    74	
    75	        throw new DirectoryNotFoundException("Could not locate the repository root.");
    76	    }
    77	}
```

</details>

## Alert #107 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/107
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:62-62`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    54	        securityPermissions.Should().Contain("System.Security.Permissions.UIPermission");
    55	        securityPermissions.Should().Contain("System.Security.Permissions.FileIOPermission");
    56	        securityPermissions.Should().Contain("System.Security.Permissions.EnvironmentPermission");
    57	        securityPermissions.Should().Contain("System.Security.Permissions.SecurityPermission.UnmanagedCode");
    58	        securityPermissions.Should().Contain("System.Net.WebPermission");
    59	        securityPermissions.Should().NotContain("System.Security.Permissions.SecurityPermission.SkipVerification");
    60	    }
    61	
    62	    private static string FindRepositoryRoot()
    63	    {
    64	        var directory = new DirectoryInfo(AppContext.BaseDirectory);
    65	        while (directory is not null)
    66	        {
    67	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
    68	            {
    69	                return directory.FullName;
    70	            }
```

</details>

## Alert #106 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/106
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:49-49`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    41	        var document = XDocument.Load(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    42	
    43	        document.Descendants().Where(element => element.Name.LocalName == "UnrestrictedAccess").Should().BeEmpty();
    44	
    45	        var tiaPermissions = document.Descendants()
    46	            .FirstOrDefault(element => element.Name.LocalName == "TIAPermissions")
    47	            ?.Elements().Select(element => element.Name.LocalName).ToList() ?? new List<string>();
    48	        tiaPermissions.Should().NotBeEmpty();
    49	
    50	        var securityPermissions = document.Descendants()
    51	            .FirstOrDefault(element => element.Name.LocalName == "SecurityPermissions")
    52	            ?.Elements().Select(element => element.Name.LocalName).ToList() ?? new List<string>();
    53	
    54	        securityPermissions.Should().Contain("System.Security.Permissions.UIPermission");
    55	        securityPermissions.Should().Contain("System.Security.Permissions.FileIOPermission");
    56	        securityPermissions.Should().Contain("System.Security.Permissions.EnvironmentPermission");
    57	        securityPermissions.Should().Contain("System.Security.Permissions.SecurityPermission.UnmanagedCode");
```

</details>

## Alert #105 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/105
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:37-37`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    29	        var codeowners = File.ReadAllText(Path.Combine(root, ".github", "CODEOWNERS"));
    30	        var issueConfig = File.ReadAllText(Path.Combine(root, ".github", "ISSUE_TEMPLATE", "config.yml"));
    31	
    32	        codeowners.Should().Contain("/SECURITY.md");
    33	        codeowners.Should().Contain("/.github/");
    34	        issueConfig.Should().Contain("Security Vulnerability Report");
    35	    }
    36	
    37	    [Fact]
    38	    public void AddIn_manifest_maintains_least_privilege()
    39	    {
    40	        var root = FindRepositoryRoot();
    41	        var document = XDocument.Load(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    42	
    43	        document.Descendants().Where(element => element.Name.LocalName == "UnrestrictedAccess").Should().BeEmpty();
    44	
    45	        var tiaPermissions = document.Descendants()
```

</details>

## Alert #104 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/104
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
    20	        content.Should().Contain("docs/spec/SECURITY_MODEL.md");
    21	        content.Should().Contain("Security > Advisories > New draft security advisory");
    22	        content.Should().Contain("security@industrix.com.br");
    23	    }
    24	
    25	    [Fact]
    26	    public void Repository_security_ownership_and_private_reporting_are_configured()
    27	    {
    28	        var root = FindRepositoryRoot();
    29	        var codeowners = File.ReadAllText(Path.Combine(root, ".github", "CODEOWNERS"));
    30	        var issueConfig = File.ReadAllText(Path.Combine(root, ".github", "ISSUE_TEMPLATE", "config.yml"));
    31	
    32	        codeowners.Should().Contain("/SECURITY.md");
    33	        codeowners.Should().Contain("/.github/");
    34	        issueConfig.Should().Contain("Security Vulnerability Report");
    35	    }
    36	
```

</details>

## Alert #103 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/103
- Location: `tests/TiaAgent.ArchitectureTests/RepositoryHealthAndSecurityTests.cs:13-13`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 77 lines

<details><summary>Current code context</summary>

```text
     5	namespace TiaAgent.ArchitectureTests;
     6	
     7	public sealed class RepositoryHealthAndSecurityTests
     8	{
     9	    [Fact]
    10	    public void Security_policy_and_authoritative_model_exist()
    11	    {
    12	        var root = FindRepositoryRoot();
    13	        var securityMdPath = Path.Combine(root, "SECURITY.md");
    14	        var securityModelPath = Path.Combine(root, "docs", "spec", "SECURITY_MODEL.md");
    15	
    16	        File.Exists(securityMdPath).Should().BeTrue();
    17	        File.Exists(securityModelPath).Should().BeTrue();
    18	
    19	        var content = File.ReadAllText(securityMdPath);
    20	        content.Should().Contain("docs/spec/SECURITY_MODEL.md");
    21	        content.Should().Contain("Security > Advisories > New draft security advisory");
```

</details>

## Alert #102 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/102
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:119-119`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
   111	        targets.Should().Contain(".addin.tmp");
   112	    }
   113	
   114	    private static string FindRepositoryRoot()
   115	    {
   116	        var directory = new DirectoryInfo(AppContext.BaseDirectory);
   117	        while (directory is not null)
   118	        {
   119	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
   120	            {
   121	                return directory.FullName;
   122	            }
   123	
   124	            directory = directory.Parent;
   125	        }
   126	
   127	        throw new DirectoryNotFoundException("Could not locate the repository root.");
```

</details>

## Alert #101 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/101
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:117-117`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
   109	        packTarget.Should().Contain("AddInTempPackagePath");
   110	        packTarget.Should().Contain("Move-Item");
   111	        targets.Should().Contain(".addin.tmp");
   112	    }
   113	
   114	    private static string FindRepositoryRoot()
   115	    {
   116	        var directory = new DirectoryInfo(AppContext.BaseDirectory);
   117	        while (directory is not null)
   118	        {
   119	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
   120	            {
   121	                return directory.FullName;
   122	            }
   123	
   124	            directory = directory.Parent;
   125	        }
```

</details>

## Alert #100 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/100
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:90-90`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    82	        targets.Should().NotContain("TiaAgent-$(AddInManifestVersion).addin");
    83	    }
    84	
    85	    [Fact]
    86	    public void Beta_rc_and_stable_have_distinct_AddIn_artifact_names()
    87	    {
    88	        var artifactNames = new[]
    89	        {
    90	            "TiaAgent-0.3.0-beta.1.addin",
    91	            "TiaAgent-0.3.0-rc.1.addin",
    92	            "TiaAgent-0.3.0.addin"
    93	        };
    94	
    95	        artifactNames.Should().OnlyHaveUniqueItems();
    96	    }
    97	
    98	    [Fact]
```

</details>

## Alert #99 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/99
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:72-72`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    64	            File.ReadAllText(sourceFile).Should().NotContain("<FileVersion>0.0.0.0</FileVersion>");
    65	        }
    66	    }
    67	
    68	    [Fact]
    69	    public void Siemens_manifest_version_is_numeric_while_artifact_version_preserves_prerelease()
    70	    {
    71	        var root = FindRepositoryRoot();
    72	        var config = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    73	        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));
    74	
    75	        config.Should().Contain("<Version>__ADDIN_MANIFEST_VERSION__</Version>");
    76	        ProductVersionLiteral.IsMatch(config).Should().BeFalse();
    77	
    78	        targets.Should().Contain("<AddInManifestVersion>");
    79	        targets.Should().Contain("<ArtifactVersion>$(Version)</ArtifactVersion>");
    80	        targets.Should().Contain("Replace('__ADDIN_MANIFEST_VERSION__', '$(AddInManifestVersion)')");
```

</details>

## Alert #98 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/98
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:73-73`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    65	        }
    66	    }
    67	
    68	    [Fact]
    69	    public void Siemens_manifest_version_is_numeric_while_artifact_version_preserves_prerelease()
    70	    {
    71	        var root = FindRepositoryRoot();
    72	        var config = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
    73	        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));
    74	
    75	        config.Should().Contain("<Version>__ADDIN_MANIFEST_VERSION__</Version>");
    76	        ProductVersionLiteral.IsMatch(config).Should().BeFalse();
    77	
    78	        targets.Should().Contain("<AddInManifestVersion>");
    79	        targets.Should().Contain("<ArtifactVersion>$(Version)</ArtifactVersion>");
    80	        targets.Should().Contain("Replace('__ADDIN_MANIFEST_VERSION__', '$(AddInManifestVersion)')");
    81	        targets.Should().Contain("TiaAgent-$(ArtifactVersion).addin");
```

</details>

## Alert #97 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/97
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:51-51`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    43	            document.Descendants("Version").Should().BeEmpty($"{projectFile} must inherit Version from Directory.Build.props");
    44	            document.Descendants("PackageVersion").Should().BeEmpty($"{projectFile} must inherit PackageVersion from Directory.Build.props");
    45	            document.Descendants("ProductVersion").Should().BeEmpty($"{projectFile} must inherit ProductVersion from Directory.Build.props");
    46	            document.Descendants("AssemblyVersion").Should().BeEmpty($"{projectFile} must inherit AssemblyVersion from Directory.Build.props");
    47	            document.Descendants("FileVersion").Should().BeEmpty($"{projectFile} must inherit FileVersion from Directory.Build.props");
    48	            document.Descendants("InformationalVersion").Should().BeEmpty($"{projectFile} must inherit InformationalVersion from Directory.Build.props");
    49	        }
    50	    }
    51	
    52	    [Fact]
    53	    public void Release_sources_do_not_contain_fixed_zero_assembly_versions()
    54	    {
    55	        var root = FindRepositoryRoot();
    56	        var sourceFiles = Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
    57	            .Where(path => path.EndsWith(".props", StringComparison.OrdinalIgnoreCase)
    58	                || path.EndsWith(".targets", StringComparison.OrdinalIgnoreCase)
    59	                || path.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
```

</details>

## Alert #96 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/96
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:50-50`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    42	            var document = XDocument.Load(projectFile);
    43	            document.Descendants("Version").Should().BeEmpty($"{projectFile} must inherit Version from Directory.Build.props");
    44	            document.Descendants("PackageVersion").Should().BeEmpty($"{projectFile} must inherit PackageVersion from Directory.Build.props");
    45	            document.Descendants("ProductVersion").Should().BeEmpty($"{projectFile} must inherit ProductVersion from Directory.Build.props");
    46	            document.Descendants("AssemblyVersion").Should().BeEmpty($"{projectFile} must inherit AssemblyVersion from Directory.Build.props");
    47	            document.Descendants("FileVersion").Should().BeEmpty($"{projectFile} must inherit FileVersion from Directory.Build.props");
    48	            document.Descendants("InformationalVersion").Should().BeEmpty($"{projectFile} must inherit InformationalVersion from Directory.Build.props");
    49	        }
    50	    }
    51	
    52	    [Fact]
    53	    public void Release_sources_do_not_contain_fixed_zero_assembly_versions()
    54	    {
    55	        var root = FindRepositoryRoot();
    56	        var sourceFiles = Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
    57	            .Where(path => path.EndsWith(".props", StringComparison.OrdinalIgnoreCase)
    58	                || path.EndsWith(".targets", StringComparison.OrdinalIgnoreCase)
```

</details>

## Alert #95 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/95
- Location: `tests/TiaAgent.ArchitectureTests/ProductVersionConsistencyTests.cs:18-18`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    10	    private static readonly Regex ProductVersionLiteral = new(
    11	        @"(?<![A-Za-z0-9])\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+)?(?![A-Za-z0-9])",
    12	        RegexOptions.Compiled | RegexOptions.CultureInvariant);
    13	
    14	    [Fact]
    15	    public void DirectoryBuildProps_defines_one_product_version_and_derives_all_dotnet_versions()
    16	    {
    17	        var root = FindRepositoryRoot();
    18	        var document = XDocument.Load(Path.Combine(root, "Directory.Build.props"));
    19	        var versions = document.Descendants("Version").ToArray();
    20	
    21	        versions.Should().ContainSingle();
    22	        versions[0].Value.Should().Be("0.0.0-dev");
    23	        versions[0].Attribute("Condition")?.Value.Should().Contain("$(Version)");
    24	
    25	        document.Descendants("PackageVersion").Single().Value.Should().Be("$(Version)");
    26	        document.Descendants("ProductVersion").Single().Value.Should().Be("$(Version)");
```

</details>

## Alert #94 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/94
- Location: `tests/TiaAgent.ArchitectureTests/DependencyTests.cs:18-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 205 lines

<details><summary>Current code context</summary>

```text
    10	    {
    11	        // Load the AddIn assembly by path to avoid cross-framework ProjectReference (NU1702).
    12	        // The AddIn targets net48; this test project targets net8.0.
    13	        var baseDir = AppContext.BaseDirectory;
    14	        var addInPath = Path.Combine(baseDir, "..", "..", "..", "..", "..",
    15	            "src", "TiaAgent.AddIn", "bin", "Release", "net48", "TiaAgent.AddIn.dll");
    16	        if (!File.Exists(addInPath))
    17	        {
    18	            addInPath = Path.Combine(baseDir, "..", "..", "..", "..", "..",
    19	                "src", "TiaAgent.AddIn", "bin", "Debug", "net48", "TiaAgent.AddIn.dll");
    20	        }
    21	        return Assembly.LoadFrom(Path.GetFullPath(addInPath));
    22	    }
    23	    [Fact]
    24	    public void Contracts_ShouldNotReferenceSiemens()
    25	    {
    26	        var assembly = typeof(TiaAgent.Contracts.Abstractions.IClock).Assembly;
    27	        var references = assembly.GetReferencedAssemblies();
```

</details>

## Alert #93 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/93
- Location: `tests/TiaAgent.ArchitectureTests/DependencyTests.cs:14-15`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 205 lines

<details><summary>Current code context</summary>

```text
     6	
     7	public class DependencyTests
     8	{
     9	    private static Assembly LoadAddInAssembly()
    10	    {
    11	        // Load the AddIn assembly by path to avoid cross-framework ProjectReference (NU1702).
    12	        // The AddIn targets net48; this test project targets net8.0.
    13	        var baseDir = AppContext.BaseDirectory;
    14	        var addInPath = Path.Combine(baseDir, "..", "..", "..", "..", "..",
    15	            "src", "TiaAgent.AddIn", "bin", "Release", "net48", "TiaAgent.AddIn.dll");
    16	        if (!File.Exists(addInPath))
    17	        {
    18	            addInPath = Path.Combine(baseDir, "..", "..", "..", "..", "..",
    19	                "src", "TiaAgent.AddIn", "bin", "Debug", "net48", "TiaAgent.AddIn.dll");
    20	        }
    21	        return Assembly.LoadFrom(Path.GetFullPath(addInPath));
    22	    }
    23	    [Fact]
```

</details>

## Alert #92 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/92
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:62-62`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    54	        buildScriptContent.Should().Contain("TiaAgent.Cli.$ProductVersion.nupkg");
    55	    }
    56	
    57	    private static string FindRepositoryRoot()
    58	    {
    59	        var directory = new DirectoryInfo(AppContext.BaseDirectory);
    60	        while (directory is not null)
    61	        {
    62	            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
    63	            {
    64	                return directory.FullName;
    65	            }
    66	
    67	            directory = directory.Parent;
    68	        }
    69	
    70	        throw new DirectoryNotFoundException("Could not locate the repository root.");
```

</details>

## Alert #91 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/91
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:26-26`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
    18	
    19	        buildScriptContent.Should().Contain("payload-manifest.json");
    20	        buildScriptContent.Should().Contain("Bridge\\TiaAgent.Bridge.dll");
    21	        buildScriptContent.Should().Contain("ResponseCenter\\TiaAgent.ResponseCenter.exe");
    22	        buildScriptContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
    23	        buildScriptContent.Should().Contain("TiaAgent-$ProductVersion.addin");
    24	        buildScriptContent.Should().Contain("THIRD_PARTY_NOTICES.md");
    25	        buildScriptContent.Should().Contain("Siemens.*.dll");
    26	    }
    27	
    28	    [Fact]
    29	    public void Response_center_is_the_single_task_result_ui()
    30	    {
    31	        var root = FindRepositoryRoot();
    32	        var solutionContent = File.ReadAllText(Path.Combine(root, "TiaAgent.sln"));
    33	        var addInUiPath = Path.Combine(root, "src", "TiaAgent.AddIn", "Ui");
    34	
```

</details>

## Alert #90 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/90
- Location: `tests/TiaAgent.ArchitectureTests/PayloadBundlingTests.cs:14-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 72 lines

<details><summary>Current code context</summary>

```text
     6	public sealed class PayloadBundlingTests
     7	{
     8	    [Fact]
     9	    public void Cli_package_includes_the_complete_installation_payload()
    10	    {
    11	        var root = FindRepositoryRoot();
    12	        var csprojContent = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.Cli", "TiaAgent.Cli.csproj"));
    13	        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));
    14	
    15	        csprojContent.Should().Contain("tools/net8.0/any/payload/");
    16	        csprojContent.Should().Contain("payload\\**\\*");
    17	        csprojContent.Should().Contain("Pack=\"true\"");
    18	
    19	        buildScriptContent.Should().Contain("payload-manifest.json");
    20	        buildScriptContent.Should().Contain("Bridge\\TiaAgent.Bridge.dll");
    21	        buildScriptContent.Should().Contain("ResponseCenter\\TiaAgent.ResponseCenter.exe");
    22	        buildScriptContent.Should().Contain("src\\TiaAgent.ResponseCenter\\TiaAgent.ResponseCenter.csproj");
```

</details>

## Alert #89 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/89
- Location: `src/TiaAgent.Cli/Payload/PayloadValidator.cs:88-88`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 118 lines

<details><summary>Current code context</summary>

```text
    80	        foreach (var fileEntry in manifest.Files)
    81	        {
    82	            if (string.IsNullOrWhiteSpace(fileEntry.RelativePath))
    83	            {
    84	                errors.Add("Payload manifest contains a file entry with an empty relative path.");
    85	                continue;
    86	            }
    87	
    88	            var fullPath = Path.Combine(payloadDirectory, fileEntry.RelativePath.Replace('/', Path.DirectorySeparatorChar));
    89	            if (!File.Exists(fullPath))
    90	            {
    91	                errors.Add($"Missing payload file: '{fileEntry.RelativePath}'.");
    92	                continue;
    93	            }
    94	
    95	            var fileInfo = new FileInfo(fullPath);
    96	            if (fileInfo.Length != fileEntry.SizeBytes)
```

</details>

## Alert #88 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/88
- Location: `src/TiaAgent.Cli/Payload/PayloadValidator.cs:24-24`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 118 lines

<details><summary>Current code context</summary>

```text
    16	            return PayloadValidationResult.Failure("Payload directory path cannot be null or empty.");
    17	        }
    18	
    19	        if (!Directory.Exists(payloadDirectory))
    20	        {
    21	            return PayloadValidationResult.Failure($"Payload directory does not exist: {payloadDirectory}");
    22	        }
    23	
    24	        var manifestPath = Path.Combine(payloadDirectory, PayloadStore.ManifestFileName);
    25	        if (!File.Exists(manifestPath))
    26	        {
    27	            return PayloadValidationResult.Failure($"Payload manifest file missing: {manifestPath}");
    28	        }
    29	
    30	        PayloadManifest manifest;
    31	        try
    32	        {
```

</details>

## Alert #87 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/87
- Location: `src/TiaAgent.Cli/Payload/PayloadStore.cs:35-35`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 51 lines

<details><summary>Current code context</summary>

```text
    27	    {
    28	        if (string.IsNullOrWhiteSpace(payloadDirectory))
    29	        {
    30	            throw new ArgumentException("Payload directory cannot be null or empty.", nameof(payloadDirectory));
    31	        }
    32	
    33	        ArgumentNullException.ThrowIfNull(manifest);
    34	
    35	        var manifestPath = Path.Combine(payloadDirectory, ManifestFileName);
    36	        ManifestStore.WriteAtomic(manifestPath, manifest);
    37	    }
    38	
    39	    public static string ComputeSha256(string filePath)
    40	    {
    41	        if (!File.Exists(filePath))
    42	        {
    43	            throw new FileNotFoundException($"File not found for hash calculation: {filePath}", filePath);
```

</details>

## Alert #86 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/86
- Location: `src/TiaAgent.Cli/Payload/PayloadStore.cs:22-22`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 51 lines

<details><summary>Current code context</summary>

```text
    14	
    15	    public static PayloadManifest ReadManifest(string payloadDirectory)
    16	    {
    17	        if (string.IsNullOrWhiteSpace(payloadDirectory))
    18	        {
    19	            throw new ArgumentException("Payload directory cannot be null or empty.", nameof(payloadDirectory));
    20	        }
    21	
    22	        var manifestPath = Path.Combine(payloadDirectory, ManifestFileName);
    23	        return ManifestStore.Read<PayloadManifest>(manifestPath);
    24	    }
    25	
    26	    public static void WriteManifest(string payloadDirectory, PayloadManifest manifest)
    27	    {
    28	        if (string.IsNullOrWhiteSpace(payloadDirectory))
    29	        {
    30	            throw new ArgumentException("Payload directory cannot be null or empty.", nameof(payloadDirectory));
```

</details>

## Alert #85 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/85
- Location: `src/TiaAgent.Cli/Payload/PayloadLocator.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 36 lines

<details><summary>Current code context</summary>

```text
    20	            if (Directory.Exists(customPayload))
    21	            {
    22	                return customPayload;
    23	            }
    24	            return customBasePath;
    25	        }
    26	
    27	        var baseDir = AppContext.BaseDirectory;
    28	        var subDir = Path.Combine(baseDir, "payload");
    29	        if (Directory.Exists(subDir))
    30	        {
    31	            return subDir;
    32	        }
    33	
    34	        return baseDir;
    35	    }
    36	}
```

</details>

## Alert #84 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/84
- Location: `src/TiaAgent.Cli/Payload/PayloadLocator.cs:19-19`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 36 lines

<details><summary>Current code context</summary>

```text
    11	    /// <summary>
    12	    /// Returns the absolute path to the payload directory.
    13	    /// Checks custom path, then AppContext.BaseDirectory/payload, and falls back to AppContext.BaseDirectory.
    14	    /// </summary>
    15	    public static string GetBundledPayloadDirectory(string? customBasePath = null)
    16	    {
    17	        if (!string.IsNullOrWhiteSpace(customBasePath))
    18	        {
    19	            var customPayload = Path.Combine(customBasePath, "payload");
    20	            if (Directory.Exists(customPayload))
    21	            {
    22	                return customPayload;
    23	            }
    24	            return customBasePath;
    25	        }
    26	
    27	        var baseDir = AppContext.BaseDirectory;
```

</details>

## Alert #83 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/83
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:42-42`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
    38	        {
    39	            throw new ArgumentException("Version cannot be null or empty.", nameof(version));
    40	        }
    41	
    42	        return Path.Combine(VersionsPath, version);
    43	    }
    44	
    45	    public void EnsureDirectoriesExist()
    46	    {
    47	        Directory.CreateDirectory(RootPath);
    48	        Directory.CreateDirectory(VersionsPath);
    49	        Directory.CreateDirectory(LogsPath);
    50	        Directory.CreateDirectory(RuntimePath);
```

</details>

## Alert #82 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/82
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:33-33`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
    38	        {
    39	            throw new ArgumentException("Version cannot be null or empty.", nameof(version));
    40	        }
    41	
```

</details>

## Alert #81 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/81
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:32-32`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
    38	        {
    39	            throw new ArgumentException("Version cannot be null or empty.", nameof(version));
    40	        }
```

</details>

## Alert #80 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/80
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:31-31`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
    38	        {
    39	            throw new ArgumentException("Version cannot be null or empty.", nameof(version));
```

</details>

## Alert #79 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/79
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:30-30`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    22	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
    38	        {
```

</details>

## Alert #78 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/78
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:29-29`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    21	        {
    22	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
    37	        if (string.IsNullOrWhiteSpace(version))
```

</details>

## Alert #77 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/77
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:28-28`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    20	        else
    21	        {
    22	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
    36	    {
```

</details>

## Alert #76 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/76
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:27-27`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    19	        }
    20	        else
    21	        {
    22	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
    32	    public string RuntimePath => Path.Combine(RootPath, "runtime");
    33	    public string CachePath => Path.Combine(RootPath, "cache");
    34	
    35	    public string GetVersionPath(string version)
```

</details>

## Alert #75 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/75
- Location: `src/TiaAgent.Cli/Layout/TiaAgentLayout.cs:23-23`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 53 lines

<details><summary>Current code context</summary>

```text
    15	    {
    16	        if (!string.IsNullOrWhiteSpace(customRootPath))
    17	        {
    18	            RootPath = customRootPath;
    19	        }
    20	        else
    21	        {
    22	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    23	            RootPath = Path.Combine(localAppData, "TiaAgent");
    24	        }
    25	    }
    26	
    27	    public string VersionsPath => Path.Combine(RootPath, "versions");
    28	    public string ConfigPath => Path.Combine(RootPath, "config.json");
    29	    public string CurrentManifestPath => Path.Combine(RootPath, "current.json");
    30	    public string InstallationsManifestPath => Path.Combine(RootPath, "installations.json");
    31	    public string LogsPath => Path.Combine(RootPath, "logs");
```

</details>

## Alert #74 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/74
- Location: `src/TiaAgent.Cli/Commands/UninstallCommand.cs:81-81`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 172 lines

<details><summary>Current code context</summary>

```text
    73	            stdout.WriteLine("No installed TIA Agent versions found.");
    74	            return 0;
    75	        }
    76	
    77	        var userAddInsDir = options.UserAddInsDir;
    78	        if (string.IsNullOrWhiteSpace(userAddInsDir))
    79	        {
    80	            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    81	            userAddInsDir = Path.Combine(appData, "Siemens", "Automation", "Portal V21", "UserAddIns");
    82	        }
    83	
    84	        var uninstalledVersions = new List<string>();
    85	
    86	        foreach (var ver in targetVersions.Distinct(StringComparer.OrdinalIgnoreCase))
    87	        {
    88	            try
    89	            {
```

</details>

## Alert #73 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/73
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:567-567`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   559	        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
   560	        var extensions = isWindows ? new[] { "", ".exe", ".cmd", ".bat" } : new[] { "" };
   561	
   562	        var paths = pathEnv.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);
   563	        foreach (var dir in paths)
   564	        {
   565	            foreach (var ext in extensions)
   566	            {
   567	                var fullPath = Path.Combine(dir, executableName + ext);
   568	                if (File.Exists(fullPath))
   569	                {
   570	                    return true;
   571	                }
   572	            }
   573	        }
   574	
   575	        return false;
```

</details>

## Alert #72 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/72
- Location: `src/TiaAgent.Cli/Commands/DoctorCommand.cs:415-415`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 611 lines

<details><summary>Current code context</summary>

```text
   407	                Recommendation = "Install TIA Portal V21 on Windows or set TiaPublicApiDir."
   408	            });
   409	        }
   410	
   411	        var userAddInsDir = customUserAddInsDir;
   412	        if (string.IsNullOrWhiteSpace(userAddInsDir))
   413	        {
   414	            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
   415	            userAddInsDir = Path.Combine(appData, "Siemens", "Automation", "Portal V21", "UserAddIns");
   416	        }
   417	
   418	        if (Directory.Exists(userAddInsDir))
   419	        {
   420	            report.Checks.Add(new DoctorCheckResult
   421	            {
   422	                Category = "Siemens",
   423	                Name = "UserAddIns Directory",
```

</details>

## Alert #71 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/71
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:241-241`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   233	        foreach (var file in Directory.GetFiles(sourceDir))
   234	        {
   235	            var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
   236	            File.Copy(file, destFile, overwrite);
   237	        }
   238	
   239	        foreach (var subDir in Directory.GetDirectories(sourceDir))
   240	        {
   241	            var destSubDir = Path.Combine(destinationDir, Path.GetFileName(subDir));
   242	            CopyDirectory(subDir, destSubDir, overwrite);
   243	        }
   244	    }
   245	}
```

</details>

## Alert #70 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/70
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:235-235`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   227	        Directory.Delete(dir, recursive: true);
   228	    }
   229	
   230	    private static void CopyDirectory(string sourceDir, string destinationDir, bool overwrite)
   231	    {
   232	        Directory.CreateDirectory(destinationDir);
   233	        foreach (var file in Directory.GetFiles(sourceDir))
   234	        {
   235	            var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
   236	            File.Copy(file, destFile, overwrite);
   237	        }
   238	
   239	        foreach (var subDir in Directory.GetDirectories(sourceDir))
   240	        {
   241	            var destSubDir = Path.Combine(destinationDir, Path.GetFileName(subDir));
   242	            CopyDirectory(subDir, destSubDir, overwrite);
   243	        }
```

</details>

## Alert #69 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/69
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:219-219`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   211	                Directory.Delete(dir, recursive: true);
   212	                return;
   213	            }
   214	            catch (IOException) when (attempt < maxRetries)
   215	            {
   216	                stdout.WriteLine($"Directory locked, retrying deletion ({attempt}/{maxRetries})...");
   217	                System.Threading.Thread.Sleep(1000);
   218	            }
   219	            catch (UnauthorizedAccessException) when (attempt < maxRetries)
   220	            {
   221	                stdout.WriteLine($"Directory locked, retrying deletion ({attempt}/{maxRetries})...");
   222	                System.Threading.Thread.Sleep(1000);
   223	            }
   224	        }
   225	
   226	        // Final attempt — let it throw if it fails
   227	        Directory.Delete(dir, recursive: true);
```

</details>

## Alert #68 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/68
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:203-203`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   195	
   196	        var defaultConfigJson = """
   197	        {
   198	          "defaultRuntime": "opencode"
   199	        }
   200	        """;
   201	        File.WriteAllText(configPath, defaultConfigJson);
   202	    }
   203	
   204	    private static void TryDeleteDirectory(string dir, TextWriter stdout, TextWriter stderr)
   205	    {
   206	        const int maxRetries = 3;
   207	        for (int attempt = 1; attempt <= maxRetries; attempt++)
   208	        {
   209	            try
   210	            {
   211	                Directory.Delete(dir, recursive: true);
```

</details>

## Alert #67 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/67
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:202-202`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   194	        }
   195	
   196	        var defaultConfigJson = """
   197	        {
   198	          "defaultRuntime": "opencode"
   199	        }
   200	        """;
   201	        File.WriteAllText(configPath, defaultConfigJson);
   202	    }
   203	
   204	    private static void TryDeleteDirectory(string dir, TextWriter stdout, TextWriter stderr)
   205	    {
   206	        const int maxRetries = 3;
   207	        for (int attempt = 1; attempt <= maxRetries; attempt++)
   208	        {
   209	            try
   210	            {
```

</details>

## Alert #66 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/66
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:197-197`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   189	            if (File.Exists(settingsExample))
   190	            {
   191	                File.Copy(settingsExample, configPath, overwrite: true);
   192	                return;
   193	            }
   194	        }
   195	
   196	        var defaultConfigJson = """
   197	        {
   198	          "defaultRuntime": "opencode"
   199	        }
   200	        """;
   201	        File.WriteAllText(configPath, defaultConfigJson);
   202	    }
   203	
   204	    private static void TryDeleteDirectory(string dir, TextWriter stdout, TextWriter stderr)
   205	    {
```

</details>

## Alert #65 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/65
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:188-188`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   180	        if (File.Exists(configPath))
   181	        {
   182	            return;
   183	        }
   184	
   185	        var payloadConfigDir = Path.Combine(payloadDir, "config");
   186	        if (Directory.Exists(payloadConfigDir))
   187	        {
   188	            var settingsExample = Path.Combine(payloadConfigDir, "settings.example.json");
   189	            if (File.Exists(settingsExample))
   190	            {
   191	                File.Copy(settingsExample, configPath, overwrite: true);
   192	                return;
   193	            }
   194	        }
   195	
   196	        var defaultConfigJson = """
```

</details>

## Alert #64 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/64
- Location: `src/TiaAgent.Cli/Commands/InstallCommand.cs:185-185`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 245 lines

<details><summary>Current code context</summary>

```text
   177	
   178	    private static void EnsureDefaultConfig(string configPath, string payloadDir)
   179	    {
   180	        if (File.Exists(configPath))
   181	        {
   182	            return;
   183	        }
   184	
   185	        var payloadConfigDir = Path.Combine(payloadDir, "config");
   186	        if (Directory.Exists(payloadConfigDir))
   187	        {
   188	            var settingsExample = Path.Combine(payloadConfigDir, "settings.example.json");
   189	            if (File.Exists(settingsExample))
   190	            {
   191	                File.Copy(settingsExample, configPath, overwrite: true);
   192	                return;
   193	            }
```

</details>

## Alert #63 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/63
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeConfigLoader.cs:113-113`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 115 lines

<details><summary>Current code context</summary>

```text
   105	    }
   106	
   107	    /// <summary>
   108	    /// Gets the path to the runtime configuration file.
   109	    /// </summary>
   110	    public static string GetConfigPath()
   111	    {
   112	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
   113	        return Path.Combine(localAppData, "TiaAgent", "config.json");
   114	    }
   115	}
```

</details>

## Alert #62 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/62
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:18-18`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
    10	    private readonly string _tokenFilePath;
    11	    private readonly string _token;
    12	
    13	    public TokenProvider()
    14	    {
    15	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    16	        var tiaAgentDir = Path.Combine(localAppData, "TiaAgent");
    17	        Directory.CreateDirectory(tiaAgentDir);
    18	        _tokenFilePath = Path.Combine(tiaAgentDir, "bridge.token");
    19	        _token = LoadOrCreateToken();
    20	    }
    21	
    22	    public string Token => _token;
    23	
    24	    public string TokenFilePath => _tokenFilePath;
    25	
    26	    public bool Validate(string? bearerToken)
```

</details>

## Alert #61 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/61
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
     8	public sealed class TokenProvider
     9	{
    10	    private readonly string _tokenFilePath;
    11	    private readonly string _token;
    12	
    13	    public TokenProvider()
    14	    {
    15	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    16	        var tiaAgentDir = Path.Combine(localAppData, "TiaAgent");
    17	        Directory.CreateDirectory(tiaAgentDir);
    18	        _tokenFilePath = Path.Combine(tiaAgentDir, "bridge.token");
    19	        _token = LoadOrCreateToken();
    20	    }
    21	
    22	    public string Token => _token;
    23	
    24	    public string TokenFilePath => _tokenFilePath;
```

</details>

## Alert #60 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/60
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:377-377`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   369	            return;
   370	
   371	        try
   372	        {
   373	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
   374	            var configDir = Path.Combine(localAppData, "TiaAgent");
   375	            Directory.CreateDirectory(configDir);
   376	
   377	            _generatedMcpConfigPath = Path.Combine(configDir, "claude-mcp.json");
   378	
   379	            // Claude MCP config format
   380	            var config = new
   381	            {
   382	                mcpServers = new
   383	                {
   384	                    tia_portal = new
   385	                    {
```

</details>

## Alert #59 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/59
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:374-374`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   366	    private void EnsureMcpConfigGenerated()
   367	    {
   368	        if (_generatedMcpConfigPath != null && File.Exists(_generatedMcpConfigPath))
   369	            return;
   370	
   371	        try
   372	        {
   373	            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
   374	            var configDir = Path.Combine(localAppData, "TiaAgent");
   375	            Directory.CreateDirectory(configDir);
   376	
   377	            _generatedMcpConfigPath = Path.Combine(configDir, "claude-mcp.json");
   378	
   379	            // Claude MCP config format
   380	            var config = new
   381	            {
   382	                mcpServers = new
```

</details>

## Alert #58 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/58
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:240-240`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   232	        // --- Response sanity checks ---
   233	        var responseError = ValidateResponse(response, request.Action);
   234	        if (responseError != null)
   235	        {
   236	            _logger.Warn($"ClaudeCodeRuntime: response validation failed: {responseError}");
   237	            return new AgentTaskResult
   238	            {
   239	                Success = false,
   240	                Error = responseError,
   241	                ErrorCode = "RUNTIME_INVALID_RESPONSE",
   242	                RuntimeId = Id,
   243	                RuntimeMode = "cli"
   244	            };
   245	        }
   246	
   247	        return new AgentTaskResult
   248	        {
```

</details>

## Alert #57 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/57
- Location: `src/TiaAgent.Bridge/Program.cs:251-251`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
   243	            if (mcpCommand == null)
   244	            {
   245	                // Fallback: check if tia-mcp is on PATH (bare name)
   246	                var pathVar = Environment.GetEnvironmentVariable("PATH");
   247	                if (!string.IsNullOrEmpty(pathVar))
   248	                {
   249	                    foreach (var dir in pathVar.Split(Path.PathSeparator))
   250	                    {
   251	                        var candidate = Path.Combine(dir.Trim(), "tia-mcp.exe");
   252	                        if (File.Exists(candidate))
   253	                        {
   254	                            mcpCommand = candidate;
   255	                            break;
   256	                        }
   257	                    }
   258	                }
   259	            }
```

</details>

## Alert #56 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/56
- Location: `src/TiaAgent.Bridge/Program.cs:236-236`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
   228	            // hang until the timeout expires. File-existence check is sufficient.
   229	            string? mcpCommand = null;
   230	            var dotnetToolsDir = Path.Combine(
   231	                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
   232	                ".dotnet", "tools");
   233	
   234	            if (Directory.Exists(dotnetToolsDir))
   235	            {
   236	                var mcpExePath = Path.Combine(dotnetToolsDir, "tia-mcp.exe");
   237	                if (File.Exists(mcpExePath))
   238	                {
   239	                    mcpCommand = mcpExePath;
   240	                }
   241	            }
   242	
   243	            if (mcpCommand == null)
   244	            {
```

</details>

## Alert #55 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/55
- Location: `src/TiaAgent.Bridge/Program.cs:230-232`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
   222	        if (claudeConfig?.Enabled != false)
   223	        {
   224	            // Find tia-mcp for MCP config generation.
   225	            // Prefer the .NET global tools copy (stable path) over bare name.
   226	            // Do NOT spawn tia-mcp to test --version: it is a stdio MCP server
   227	            // that blocks reading stdin, causing Process.Start + WaitForExit to
   228	            // hang until the timeout expires. File-existence check is sufficient.
   229	            string? mcpCommand = null;
   230	            var dotnetToolsDir = Path.Combine(
   231	                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
   232	                ".dotnet", "tools");
   233	
   234	            if (Directory.Exists(dotnetToolsDir))
   235	            {
   236	                var mcpExePath = Path.Combine(dotnetToolsDir, "tia-mcp.exe");
   237	                if (File.Exists(mcpExePath))
   238	                {
   239	                    mcpCommand = mcpExePath;
   240	                }
```

</details>

## Alert #54 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/54
- Location: `src/TiaAgent.Bridge/Diagnostics/BridgeLogger.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 40 lines

<details><summary>Current code context</summary>

```text
     8	    private readonly string _logFilePath;
     9	    private readonly object _lock = new();
    10	
    11	    public BridgeLogger()
    12	    {
    13	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    14	        var tiaAgentDir = Path.Combine(localAppData, "TiaAgent");
    15	        Directory.CreateDirectory(tiaAgentDir);
    16	        _logFilePath = Path.Combine(tiaAgentDir, "bridge.log");
    17	    }
    18	
    19	    public void Info(string message) => WriteLog("INFO", message);
    20	    public void Warn(string message) => WriteLog("WARN", message);
    21	    public void Error(string message, Exception? ex = null) => WriteLog("ERROR", ex != null ? $"{message}: {ex.Message}" : message);
    22	    public void Debug(string message) => WriteLog("DEBUG", message);
    23	    public void Startup(string message) => WriteLog("STARTUP", message);
    24	
```

</details>

## Alert #53 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/53
- Location: `src/TiaAgent.Bridge/Diagnostics/BridgeLogger.cs:14-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 40 lines

<details><summary>Current code context</summary>

```text
     6	public sealed class BridgeLogger
     7	{
     8	    private readonly string _logFilePath;
     9	    private readonly object _lock = new();
    10	
    11	    public BridgeLogger()
    12	    {
    13	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    14	        var tiaAgentDir = Path.Combine(localAppData, "TiaAgent");
    15	        Directory.CreateDirectory(tiaAgentDir);
    16	        _logFilePath = Path.Combine(tiaAgentDir, "bridge.log");
    17	    }
    18	
    19	    public void Info(string message) => WriteLog("INFO", message);
    20	    public void Warn(string message) => WriteLog("WARN", message);
    21	    public void Error(string message, Exception? ex = null) => WriteLog("ERROR", ex != null ? $"{message}: {ex.Message}" : message);
    22	    public void Debug(string message) => WriteLog("DEBUG", message);
```

</details>

## Alert #52 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/52
- Location: `src/TiaAgent.Bridge/Configuration/BridgeConfig.cs:101-101`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 103 lines

<details><summary>Current code context</summary>

```text
    93	#pragma warning disable CA1846
    94	        return long.TryParse(json.Substring(start, idx - start), out var val) ? val : null;
    95	#pragma warning restore CA1846
    96	    }
    97	
    98	    private static string GetConfigPath()
    99	    {
   100	        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
   101	        return Path.Combine(localAppData, "TiaAgent", "bridge.json");
   102	    }
   103	}
```

</details>

## Alert #51 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/51
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:17-17`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
     9	
    10	/// <summary>
    11	/// File-based logger for the TIA Portal Add-In.
    12	/// Writes to %LOCALAPPDATA%\TiaAgent\logs\addin-YYYYMMDD.log.
    13	///
    14	/// Design constraints:
    15	/// - ALL operations are best-effort: a logging failure must NEVER prevent Add-In loading.
    16	/// - LogDir is resolved lazily to avoid TypeInitializationException from static field
    17	///   initializer calling Environment.GetFolderPath() before EnvironmentPermission is available.
    18	/// - The Log() method silently disables file logging on first failure and falls back to no-op.
    19	/// - Startup() catches all exceptions and never throws.
    20	/// </summary>
    21	public static class AddInLogger
    22	{
    23	    // Lazy initialization: resolved on first Log() call, not at class load time.
    24	    // This prevents TypeInitializationException when EnvironmentPermission is not yet granted.
    25	    private static string? _logDir;
```

</details>

## Alert #50 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/50
- Location: `src/TiaAgent.AddIn/Diagnostics/AddInLogger.cs:13-15`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **yes**
- Current file length: 318 lines

<details><summary>Current code context</summary>

```text
     5	using System.Security.Cryptography;
     6	using System.Threading;
     7	
     8	namespace TiaAgent.AddIn.Diagnostics;
     9	
    10	/// <summary>
    11	/// File-based logger for the TIA Portal Add-In.
    12	/// Writes to %LOCALAPPDATA%\TiaAgent\logs\addin-YYYYMMDD.log.
    13	///
    14	/// Design constraints:
    15	/// - ALL operations are best-effort: a logging failure must NEVER prevent Add-In loading.
    16	/// - LogDir is resolved lazily to avoid TypeInitializationException from static field
    17	///   initializer calling Environment.GetFolderPath() before EnvironmentPermission is available.
    18	/// - The Log() method silently disables file logging on first failure and falls back to no-op.
    19	/// - Startup() catches all exceptions and never throws.
    20	/// </summary>
    21	public static class AddInLogger
    22	{
    23	    // Lazy initialization: resolved on first Log() call, not at class load time.
```

</details>

## Alert #49 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/49
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:155-155`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #48 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/48
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:64-64`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #47 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/47
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:16-16`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #46 — cs/path-combine

- Rule: `cs/path-combine`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/46
- Location: `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:12-14`
- Message: Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.

- Current file exists on `main`: **no**

## Alert #45 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/45
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadValidatorTests.cs:24-24`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 231 lines

<details><summary>Current code context</summary>

```text
    16	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadValidatorTests_" + Guid.NewGuid().ToString("N"));
    17	        Directory.CreateDirectory(_tempDirectory);
    18	    }
    19	
    20	    public void Dispose()
    21	    {
    22	        if (Directory.Exists(_tempDirectory))
    23	        {
    24	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    25	        }
    26	        GC.SuppressFinalize(this);
    27	    }
    28	
    29	    [Fact]
    30	    public void ValidatePayload_WithValidPayload_ReturnsSuccess()
    31	    {
    32	        var bridgeDir = Path.Combine(_tempDirectory, "Bridge");
```

</details>

## Alert #44 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/44
- Location: `tests/TiaAgent.Cli.Tests/Payload/PayloadManifestTests.cs:23-23`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 117 lines

<details><summary>Current code context</summary>

```text
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "PayloadManifestTests_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    24	        }
    25	    }
    26	
    27	    [Fact]
    28	    public void PayloadManifest_DefaultValues_ShouldMatchSchema()
    29	    {
    30	        var manifest = new PayloadManifest();
    31	
```

</details>

## Alert #43 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/43
- Location: `tests/TiaAgent.Cli.Tests/Layout/ManifestStoreTests.cs:23-23`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 113 lines

<details><summary>Current code context</summary>

```text
    15	        _tempDirectory = Path.Combine(Path.GetTempPath(), "TiaAgentTest_" + Guid.NewGuid().ToString("N"));
    16	        Directory.CreateDirectory(_tempDirectory);
    17	    }
    18	
    19	    public void Dispose()
    20	    {
    21	        if (Directory.Exists(_tempDirectory))
    22	        {
    23	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    24	        }
    25	        GC.SuppressFinalize(this);
    26	    }
    27	
    28	    [Fact]
    29	    public void Layout_Paths_ShouldBeSubdirectoriesOfRoot()
    30	    {
    31	        var layout = new TiaAgentLayout(_tempDirectory);
```

</details>

## Alert #42 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/42
- Location: `tests/TiaAgent.Cli.Tests/Commands/VersionCommandTests.cs:29-29`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 106 lines

<details><summary>Current code context</summary>

```text
    21	        Directory.CreateDirectory(_tempDirectory);
    22	        Directory.CreateDirectory(_customRoot);
    23	    }
    24	
    25	    public void Dispose()
    26	    {
    27	        if (Directory.Exists(_tempDirectory))
    28	        {
    29	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    30	        }
    31	        GC.SuppressFinalize(this);
    32	    }
    33	
    34	    [Fact]
    35	    public void VersionCommand_Default_OutputsVersionString()
    36	    {
    37	        var options = new VersionOptions
```

</details>

## Alert #41 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/41
- Location: `tests/TiaAgent.Cli.Tests/Commands/InstallerCommandTests.cs:38-38`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
    30	
    31	        CreateDummyPayload(_payloadDir, "0.2.0-beta.1");
    32	    }
    33	
    34	    public void Dispose()
    35	    {
    36	        if (Directory.Exists(_tempDirectory))
    37	        {
    38	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    39	        }
    40	        GC.SuppressFinalize(this);
    41	    }
    42	
    43	    [Fact]
    44	    public void InstallCommand_WithValidPayload_InstallsSuccessfully()
    45	    {
    46	        var options = new InstallOptions
```

</details>

## Alert #40 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/40
- Location: `tests/TiaAgent.Cli.Tests/Commands/DoctorCommandTests.cs:32-32`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 136 lines

<details><summary>Current code context</summary>

```text
    24	        Directory.CreateDirectory(_customRoot);
    25	        Directory.CreateDirectory(_userAddInsDir);
    26	    }
    27	
    28	    public void Dispose()
    29	    {
    30	        if (Directory.Exists(_tempDirectory))
    31	        {
    32	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    33	        }
    34	        GC.SuppressFinalize(this);
    35	    }
    36	
    37	    [Fact]
    38	    public void DoctorCommand_WithEmptyRoot_ReturnsZeroWithWarnings()
    39	    {
    40	        var options = new DoctorOptions
```

</details>

## Alert #39 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/39
- Location: `tests/TiaAgent.Cli.Tests/Commands/ConfigCommandTests.cs:30-30`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 183 lines

<details><summary>Current code context</summary>

```text
    22	        Directory.CreateDirectory(_tempDirectory);
    23	        Directory.CreateDirectory(_customRoot);
    24	    }
    25	
    26	    public void Dispose()
    27	    {
    28	        if (Directory.Exists(_tempDirectory))
    29	        {
    30	            try { Directory.Delete(_tempDirectory, recursive: true); } catch { }
    31	        }
    32	        GC.SuppressFinalize(this);
    33	    }
    34	
    35	    [Fact]
    36	    public void ConfigCommand_List_DisplaysDefaultConfiguration()
    37	    {
    38	        var options = new ConfigOptions
```

</details>

## Alert #38 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/38
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230-230`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **no**

## Alert #37 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/37
- Location: `src/TiaAgent.Cli/Layout/ManifestStore.cs:86-86`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 91 lines

<details><summary>Current code context</summary>

```text
    78	            File.WriteAllText(tempPath, json);
    79	
    80	            File.Move(tempPath, filePath, overwrite: true);
    81	        }
    82	        catch
    83	        {
    84	            if (File.Exists(tempPath))
    85	            {
    86	                try { File.Delete(tempPath); } catch { }
    87	            }
    88	            throw;
    89	        }
    90	    }
    91	}
```

</details>

## Alert #36 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/36
- Location: `src/TiaAgent.Cli/Commands/VersionCommand.cs:76-76`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    68	                    installedVersions.Add(new VersionDetail
    69	                    {
    70	                        Version = ver,
    71	                        InstalledAt = meta.InstalledAt,
    72	                        CommitSha = meta.CommitSha
    73	                    });
    74	                }
    75	            }
    76	            catch { }
    77	        }
    78	
    79	        var report = new VersionReport
    80	        {
    81	            ProductVersion = productVersion,
    82	            ActiveVersion = activeVersion,
    83	            InstalledVersions = installedVersions,
    84	            ConfigPath = layout.ConfigPath,
```

</details>

## Alert #35 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/35
- Location: `src/TiaAgent.Cli/Commands/VersionCommand.cs:57-57`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 129 lines

<details><summary>Current code context</summary>

```text
    49	        string? activeVersion = null;
    50	        if (File.Exists(layout.CurrentManifestPath))
    51	        {
    52	            try
    53	            {
    54	                var current = ManifestStore.Read<CurrentManifest>(layout.CurrentManifestPath);
    55	                activeVersion = current.ActiveVersion;
    56	            }
    57	            catch { }
    58	        }
    59	
    60	        var installedVersions = new List<VersionDetail>();
    61	        if (File.Exists(layout.InstallationsManifestPath))
    62	        {
    63	            try
    64	            {
    65	                var installations = ManifestStore.Read<InstallationsManifest>(layout.InstallationsManifestPath);
```

</details>

## Alert #33 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/33
- Location: `src/TiaAgent.Bridge/Tasks/TaskManager.cs:92-92`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 341 lines

<details><summary>Current code context</summary>

```text
    84	        // Also tell the runtime to cancel
    85	        if (!string.IsNullOrEmpty(entry.RuntimeId))
    86	        {
    87	            try
    88	            {
    89	                var runtime = _runtimeRegistry.GetRuntime(entry.RuntimeId);
    90	                _ = runtime.CancelAsync(taskId, CancellationToken.None);
    91	            }
    92	            catch { }
    93	        }
    94	
    95	        return true;
    96	    }
    97	
    98	    private async Task ExecuteTaskAsync(TaskEntry entry, CancellationToken cancellationToken)
    99	    {
   100	        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
```

</details>

## Alert #32 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/32
- Location: `src/TiaAgent.Bridge/Sessions/SessionManager.cs:59-59`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 69 lines

<details><summary>Current code context</summary>

```text
    51	        sessionId = null;
    52	        return false;
    53	    }
    54	
    55	    public void Dispose()
    56	    {
    57	        foreach (var kvp in _sessions)
    58	        {
    59	            try { _openCodeClient.AbortSessionAsync(kvp.Value.SessionId).GetAwaiter().GetResult(); } catch { }
    60	        }
    61	        _sessions.Clear();
    62	    }
    63	
    64	    private sealed class SessionEntry
    65	    {
    66	        public string SessionId { get; init; } = null!;
    67	        public DateTime CreatedAt { get; init; }
```

</details>

## Alert #31 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/31
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:54-54`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
    46	                var existing = File.ReadAllText(_tokenFilePath).Trim();
    47	                if (!string.IsNullOrEmpty(existing))
    48	                    return existing;
    49	            }
    50	        }
    51	        catch { }
    52	
    53	        var token = GenerateToken();
    54	        try { File.WriteAllText(_tokenFilePath, token); } catch { }
    55	        return token;
    56	    }
    57	
    58	    private static string GenerateToken()
    59	    {
    60	        Span<byte> bytes = stackalloc byte[32];
    61	        RandomNumberGenerator.Fill(bytes);
    62	        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
```

</details>

## Alert #30 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/30
- Location: `src/TiaAgent.Bridge/Security/TokenProvider.cs:51-51`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 64 lines

<details><summary>Current code context</summary>

```text
    43	        {
    44	            if (File.Exists(_tokenFilePath))
    45	            {
    46	                var existing = File.ReadAllText(_tokenFilePath).Trim();
    47	                if (!string.IsNullOrEmpty(existing))
    48	                    return existing;
    49	            }
    50	        }
    51	        catch { }
    52	
    53	        var token = GenerateToken();
    54	        try { File.WriteAllText(_tokenFilePath, token); } catch { }
    55	        return token;
    56	    }
    57	
    58	    private static string GenerateToken()
    59	    {
```

</details>

## Alert #29 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/29
- Location: `src/TiaAgent.Bridge/Runtime/RuntimeRegistry.cs:151-151`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 156 lines

<details><summary>Current code context</summary>

```text
   143	    }
   144	
   145	    public void Dispose()
   146	    {
   147	        foreach (var runtime in _runtimes.Values)
   148	        {
   149	            if (runtime is IDisposable disposable)
   150	            {
   151	                try { disposable.Dispose(); } catch { }
   152	            }
   153	        }
   154	        _runtimes.Clear();
   155	    }
   156	}
```

</details>

## Alert #28 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/28
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:209-209`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
   201	                };
   202	            }
   203	
   204	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stdout.decoded", decodedStdout));
   205	            _logger.Info(TextPayloadDiagnostics.DescribeText("2.process.stderr.decoded", decodedStderr));
   206	
   207	            // Progress reporting is observational only; decodedStdout remains the source of truth.
   208	            if (progress != null)
   209	            {
   210	                var stdoutLines = decodedStdout.Split(s_newlineSeparators, StringSplitOptions.None);
   211	                foreach (var line in stdoutLines)
   212	                    progress.Report(line);
   213	            }
   214	
   215	            var exitCode = process.ExitCode;
   216	            _logger.Info($"ProcessRunner: process exited with code {exitCode}");
   217	
```

</details>

## Alert #27 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/27
- Location: `src/TiaAgent.Bridge/Runtime/OpenCodeRuntime.cs:382-382`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 397 lines

<details><summary>Current code context</summary>

```text
   374	                        else if (root.TryGetProperty("text", out var textProp))
   375	                            lastContent = textProp.GetString();
   376	                    }
   377	                }
   378	
   379	                if (root.TryGetProperty("result", out var resultProp) && resultProp.ValueKind == JsonValueKind.String)
   380	                    lastContent = resultProp.GetString();
   381	            }
   382	            catch (JsonException) { }
   383	        }
   384	
   385	        return lastContent ?? ProcessRunner.StripAnsiEscapes(stdout.Trim());
   386	    }
   387	
   388	    private static string EscapeShellArg(string arg) => RuntimeHelpers.EscapeShellArg(arg);
   389	
   390	    #endregion
```

</details>

## Alert #26 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/26
- Location: `src/TiaAgent.Bridge/Runtime/ProcessRunner.cs:107-107`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 324 lines

<details><summary>Current code context</summary>

```text
    99	                var stdinBytes = Encoding.UTF8.GetBytes(stdinContent);
   100	                _logger.Info($"ProcessRunner: writing {stdinBytes.Length} UTF-8 bytes to stdin");
   101	
   102	                await process.StandardInput.BaseStream.WriteAsync(stdinBytes.AsMemory(), cancellationToken)
   103	                    .ConfigureAwait(false);
   104	                await process.StandardInput.BaseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
   105	            }
   106	
   107	            try { process.StandardInput.Close(); } catch { }
   108	
   109	            _logger.Info($"ProcessRunner: process started (PID={process.Id})");
   110	
   111	            using var timeoutCts = new CancellationTokenSource(timeout);
   112	            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
   113	                cancellationToken, timeoutCts.Token);
   114	
   115	            // BOUNDARY 1: read stdout and stderr concurrently as raw bytes.
```

</details>

## Alert #25 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/25
- Location: `src/TiaAgent.Bridge/Runtime/ClaudeCodeRuntime.cs:502-502`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 505 lines

<details><summary>Current code context</summary>

```text
   494	
   495	    public void Dispose()
   496	    {
   497	        _processRunner.Dispose();
   498	
   499	        // Clean up generated MCP config
   500	        if (_generatedMcpConfigPath != null && File.Exists(_generatedMcpConfigPath))
   501	        {
   502	            try { File.Delete(_generatedMcpConfigPath); } catch { }
   503	        }
   504	    }
   505	}
```

</details>

## Alert #24 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/24
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:82-82`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    74	    }
    75	
    76	    public async Task AbortSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    77	    {
    78	        try
    79	        {
    80	            await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/abort", null, cancellationToken).ConfigureAwait(false);
    81	        }
    82	        catch { }
    83	    }
    84	
    85	    public void Dispose() => _httpClient.Dispose();
    86	
    87	    /// <summary>
    88	    /// Reads HTTP response content as a string using explicit UTF-8 encoding.
    89	    /// Prevents encoding corruption when the server response lacks a charset
    90	    /// in the Content-Type header.
```

</details>

## Alert #23 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/23
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:216-216`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   208	        var defaultRuntimeId = _runtimeRegistry.GetDefaultRuntimeId();
   209	        IAgentRuntime? defaultRuntime = null;
   210	        RuntimeAvailabilityResult? availability = null;
   211	        try
   212	        {
   213	            defaultRuntime = _runtimeRegistry.GetRuntime(defaultRuntimeId);
   214	            availability = await defaultRuntime.CheckAvailabilityAsync(CancellationToken.None).ConfigureAwait(false);
   215	        }
   216	        catch { }
   217	
   218	        var healthJson = $"{{\"service\":\"tia-agent-bridge\",\"status\":\"healthy\",\"version\":\"1.0.0\",\"instanceId\":\"{EscapeJson(instanceId)}\",\"runtimeId\":\"{EscapeJson(defaultRuntimeId)}\",\"runtimeDisplayName\":\"{EscapeJson(defaultRuntime?.DisplayName ?? defaultRuntimeId)}\",\"runtimeAvailable\":{(availability?.Available == true ? "true" : "false")},\"runtimeVersion\":\"{EscapeJson(availability?.Version ?? "")}\"}}";
   219	        await WriteJsonResponseAsync(response, 200, healthJson).ConfigureAwait(false);
   220	    }
   221	
   222	    private async Task HandleCreateTaskAsync(HttpListenerRequest request, HttpListenerResponse response)
   223	    {
   224	        var body = await ReadRequestBodyAsync(request).ConfigureAwait(false);
```

</details>

## Alert #22 — cs/empty-catch-block

- Rule: `cs/empty-catch-block`
- Severity: **note**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-08-02T16:11:50Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/22
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:176-176`
- Message: Poor error handling: empty catch block.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
   168	        }
   169	        catch (Exception ex)
   170	        {
   171	            _logger.Error($"Error handling {method} {path}", ex);
   172	            try
   173	            {
   174	                await WriteJsonResponseAsync(response, 500, "{\"error\":\"Internal server error\"}").ConfigureAwait(false);
   175	            }
   176	            catch { }
   177	        }
   178	        finally
   179	        {
   180	            response.Close();
   181	        }
   182	    }
   183	
   184	    private (bool success, string errorType, string message) AuthenticateRequest(HttpListenerRequest request)
```

</details>

## Alert #21 — cs/constant-condition

- Rule: `cs/constant-condition`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/21
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:155-155`
- Message: Condition is always true because of ... == ....
Condition is always true because of ... == ....

- Current file exists on `main`: **no**

## Alert #20 — cs/constant-condition

- Rule: `cs/constant-condition`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/20
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:131-131`
- Message: Condition is always false because of ... == ....

- Current file exists on `main`: **no**

## Alert #19 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/19
- Location: `tests/TiaAgent.Bridge.Tests/TaskRoutingTests.cs:176-176`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 223 lines

<details><summary>Current code context</summary>

```text
   168	
   169	    [Fact]
   170	    public async Task CreateTask_UnknownRuntime_ReturnsError()
   171	    {
   172	        StartBridge();
   173	        SetValidAuthToken();
   174	
   175	        var body = @"{""contractVersion"":""1.0"",""correlationId"":""test-123"",""action"":""explain"",""agentId"":""tia-explain"",""userMessage"":""test"",""runtime"":""nonexistent""}";
   176	        using var content = new StringContent(body, Encoding.UTF8, "application/json");
   177	        var createResponse = await _httpClient!.PostAsync("/v1/tasks", content);
   178	        createResponse.StatusCode.Should().Be(HttpStatusCode.Accepted);
   179	
   180	        // Wait for task to fail
   181	        await Task.Delay(200);
   182	
   183	        var createJson = await createResponse.Content.ReadAsStringAsync();
   184	        var taskId = ExtractJsonString(createJson, "taskId");
```

</details>

## Alert #18 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/18
- Location: `tests/TiaAgent.Bridge.Tests/TaskRoutingTests.cs:83-83`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 223 lines

<details><summary>Current code context</summary>

```text
    75	    [Fact]
    76	    public async Task TaskStatus_ContainsRuntimeMetadata()
    77	    {
    78	        StartBridge();
    79	        SetValidAuthToken();
    80	
    81	        // Create a task
    82	        var body = @"{""contractVersion"":""1.0"",""correlationId"":""test-123"",""action"":""explain"",""agentId"":""tia-explain"",""userMessage"":""test""}";
    83	        using var content = new StringContent(body, Encoding.UTF8, "application/json");
    84	        var createResponse = await _httpClient!.PostAsync("/v1/tasks", content);
    85	        createResponse.StatusCode.Should().Be(HttpStatusCode.Accepted);
    86	
    87	        var createJson = await createResponse.Content.ReadAsStringAsync();
    88	        var taskId = ExtractJsonString(createJson, "taskId");
    89	        taskId.Should().NotBeNullOrEmpty();
    90	
    91	        // Wait a moment for the task to execute (fake runtime is instant)
```

</details>

## Alert #17 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/17
- Location: `tests/TiaAgent.Bridge.Tests/TaskRoutingTests.cs:69-69`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 223 lines

<details><summary>Current code context</summary>

```text
    61	
    62	    [Fact]
    63	    public async Task CreateTask_WithoutRuntimeOverride_UsesDefault()
    64	    {
    65	        StartBridge();
    66	        SetValidAuthToken();
    67	
    68	        var body = @"{""contractVersion"":""1.0"",""correlationId"":""test-123"",""action"":""explain"",""agentId"":""tia-explain"",""userMessage"":""test""}";
    69	        using var content = new StringContent(body, Encoding.UTF8, "application/json");
    70	        var response = await _httpClient!.PostAsync("/v1/tasks", content);
    71	
    72	        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    73	    }
    74	
    75	    [Fact]
    76	    public async Task TaskStatus_ContainsRuntimeMetadata()
    77	    {
```

</details>

## Alert #16 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/16
- Location: `tests/TiaAgent.Bridge.Tests/TaskRoutingTests.cs:56-56`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 223 lines

<details><summary>Current code context</summary>

```text
    48	
    49	    [Fact]
    50	    public async Task CreateTask_WithRuntimeOverride_RoutesToCorrectRuntime()
    51	    {
    52	        StartBridge();
    53	        SetValidAuthToken();
    54	
    55	        var body = @"{""contractVersion"":""1.0"",""correlationId"":""test-123"",""action"":""explain"",""agentId"":""tia-explain"",""userMessage"":""test"",""runtime"":""mimo""}";
    56	        using var content = new StringContent(body, Encoding.UTF8, "application/json");
    57	        var response = await _httpClient!.PostAsync("/v1/tasks", content);
    58	
    59	        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    60	    }
    61	
    62	    [Fact]
    63	    public async Task CreateTask_WithoutRuntimeOverride_UsesDefault()
    64	    {
```

</details>

## Alert #15 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/15
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:132-132`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
   124	
   125	    [Fact]
   126	    public async Task TaskEndpoint_Returns202_WithValidAuth_AndValidBody()
   127	    {
   128	        StartBridge();
   129	        SetValidAuthToken();
   130	
   131	        var body = @"{""contractVersion"":""1.0"",""correlationId"":""test-123"",""action"":""explain"",""agentId"":""tia-explain"",""userMessage"":""test""}";
   132	        using var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
   133	        var response = await _httpClient!.PostAsync("/v1/tasks", content);
   134	
   135	        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
   136	    }
   137	
   138	    [Fact]
   139	    public async Task DiagnosticsEndpoint_ReturnsAuthTokenFingerprint()
   140	    {
```

</details>

## Alert #14 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/14
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:119-119`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
   111	    }
   112	
   113	    [Fact]
   114	    public async Task TaskEndpoint_Returns400_WithValidAuth_ButBadBody()
   115	    {
   116	        StartBridge();
   117	        SetValidAuthToken();
   118	
   119	        using var content = new StringContent("not-json", System.Text.Encoding.UTF8, "application/json");
   120	        var response = await _httpClient!.PostAsync("/v1/tasks", content);
   121	
   122	        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
   123	    }
   124	
   125	    [Fact]
   126	    public async Task TaskEndpoint_Returns202_WithValidAuth_AndValidBody()
   127	    {
```

</details>

## Alert #13 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/13
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:105-105`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
    97	
    98	    [Fact]
    99	    public async Task TaskEndpoint_Returns401_WithInvalidToken()
   100	    {
   101	        StartBridge();
   102	        _httpClient!.DefaultRequestHeaders.Authorization =
   103	            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "totally-wrong-token");
   104	
   105	        using var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
   106	        var response = await _httpClient.PostAsync("/v1/tasks", content);
   107	
   108	        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
   109	        var body = await response.Content.ReadAsStringAsync();
   110	        body.Should().Contain("\"errorType\":\"invalid\"");
   111	    }
   112	
   113	    [Fact]
```

</details>

## Alert #12 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/12
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:86-89`
- Message: Disposable 'HttpRequestMessage' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
    78	    }
    79	
    80	    [Fact]
    81	    public async Task TaskEndpoint_Returns401_WithEmptyBearerToken()
    82	    {
    83	        StartBridge();
    84	        _httpClient!.DefaultRequestHeaders.Authorization = null;
    85	
    86	        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/tasks")
    87	        {
    88	            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
    89	        };
    90	        request.Headers.Add("Authorization", "Bearer ");
    91	
    92	        var response = await _httpClient.SendAsync(request);
    93	        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    94	        var body = await response.Content.ReadAsStringAsync();
    95	        body.Should().Contain("\"errorType\":\"malformed\"");
    96	    }
    97	
```

</details>

## Alert #11 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/11
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:68-71`
- Message: Disposable 'HttpRequestMessage' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
    60	    }
    61	
    62	    [Fact]
    63	    public async Task TaskEndpoint_Returns401_WithMalformedHeader()
    64	    {
    65	        StartBridge();
    66	        _httpClient!.DefaultRequestHeaders.Authorization = null;
    67	
    68	        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/tasks")
    69	        {
    70	            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
    71	        };
    72	        request.Headers.Add("Authorization", "Basic dXNlcjpwYXNz");
    73	
    74	        var response = await _httpClient.SendAsync(request);
    75	        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    76	        var body = await response.Content.ReadAsStringAsync();
    77	        body.Should().Contain("\"errorType\":\"malformed\"");
    78	    }
    79	
```

</details>

## Alert #10 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/10
- Location: `tests/TiaAgent.Bridge.Tests/BridgeAuthTests.cs:54-54`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 240 lines

<details><summary>Current code context</summary>

```text
    46	        response.StatusCode.Should().Be(HttpStatusCode.OK);
    47	    }
    48	
    49	    [Fact]
    50	    public async Task TaskEndpoint_Returns401_WithoutAuthHeader()
    51	    {
    52	        StartBridge();
    53	
    54	        using var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
    55	        var response = await _httpClient!.PostAsync("/v1/tasks", content);
    56	
    57	        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    58	        var body = await response.Content.ReadAsStringAsync();
    59	        body.Should().Contain("\"errorType\":\"missing\"");
    60	    }
    61	
    62	    [Fact]
```

</details>

## Alert #9 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/9
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:113-113`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #8 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/8
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:92-92`
- Message: Disposable 'HttpRequestMessage' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #7 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/7
- Location: `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:62-62`
- Message: Disposable 'HttpRequestMessage' is created but not disposed.

- Current file exists on `main`: **no**

## Alert #6 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/6
- Location: `src/TiaAgent.Bridge/Program.cs:68-68`
- Message: Disposable 'CancellationTokenSource' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 276 lines

<details><summary>Current code context</summary>

```text
    60	
    61	        // Load runtime configuration
    62	        var configLoader = new RuntimeConfigLoader(logger);
    63	        var runtimeConfig = configLoader.Load();
    64	
    65	        // Create and populate the runtime registry
    66	        var runtimeRegistry = new RuntimeRegistry(runtimeConfig, logger);
    67	
    68	        // Register all known runtime adapters
    69	        RegisterRuntimes(runtimeRegistry, runtimeConfig, config, logger);
    70	
    71	        // Log registered runtimes
    72	        var allRuntimes = runtimeRegistry.GetAllRuntimes();
    73	        logger.Startup($"Registered runtimes: {string.Join(", ", allRuntimes.Select(r => $"{r.Id} ({r.DisplayName})"))}");
    74	        logger.Startup($"Default runtime: {runtimeRegistry.GetDefaultRuntimeId()}");
    75	
    76	        // Check availability of all runtimes
```

</details>

## Alert #5 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/5
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:66-66`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    58	                RawJson = $"Connection failed to {url}: {ex.Message}"
    59	            };
    60	        }
    61	    }
    62	
    63	    public async Task<MessageResponse> SendMessageAsync(string sessionId, string message, CancellationToken cancellationToken = default)
    64	    {
    65	        var payload = $"{{\"message\":\"{EscapeJson(message)}\"}}";
    66	        using var content = new StringContent(payload, Encoding.UTF8, "application/json");
    67	        var response = await _httpClient.PostAsync($"{_baseUrl}/sessions/{sessionId}/messages", content, cancellationToken).ConfigureAwait(false);
    68	        var body = await ReadResponseUtf8Async(response, cancellationToken).ConfigureAwait(false);
    69	        return new MessageResponse
    70	        {
    71	            Success = response.IsSuccessStatusCode,
    72	            RawJson = body
    73	        };
    74	    }
```

</details>

## Alert #4 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/4
- Location: `src/TiaAgent.Bridge/OpenCode/OpenCodeClient.cs:40-40`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 141 lines

<details><summary>Current code context</summary>

```text
    32	        {
    33	            return new HealthResponse { Available = false, Error = ex.Message };
    34	        }
    35	    }
    36	
    37	    public async Task<SessionResponse> CreateSessionAsync(string agentId, string prompt, CancellationToken cancellationToken = default)
    38	    {
    39	        var payload = $"{{\"agent\":\"{EscapeJson(agentId)}\",\"prompt\":\"{EscapeJson(prompt)}\"}}";
    40	        var url = $"{_baseUrl}/sessions";
    41	        try
    42	        {
    43	            using var content = new StringContent(payload, Encoding.UTF8, "application/json");
    44	            var response = await _httpClient.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
    45	            var body = await ReadResponseUtf8Async(response, cancellationToken).ConfigureAwait(false);
    46	            return new SessionResponse
    47	            {
    48	                Success = response.IsSuccessStatusCode,
```

</details>

## Alert #3 — cs/local-not-disposed

- Rule: `cs/local-not-disposed`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/3
- Location: `src/TiaAgent.AddIn/Bridge/AgentBridgeClient.cs:83-83`
- Message: Disposable 'StringContent' is created but not disposed.

- Current file exists on `main`: **yes**
- Current file length: 606 lines

<details><summary>Current code context</summary>

```text
    75	
    76	    private void ConfigureAuthentication(HttpClient client, AddInConfig config)
    77	    {
    78	        if (!string.IsNullOrEmpty(config.AuthToken))
    79	        {
    80	            client.DefaultRequestHeaders.Authorization =
    81	                new AuthenticationHeaderValue("Bearer", config.AuthToken);
    82	            AddInLogger.Info($"Bridge auth configured: Bearer token loaded ({TokenFingerprint(config.AuthToken!)})");
    83	        }
    84	        else
    85	        {
    86	            AddInLogger.Warn("Bridge auth token not found — requests to Bridge will be rejected");
    87	        }
    88	    }
    89	
    90	    private HttpClient CreateHttpClient(AddInConfig config)
    91	    {
```

</details>

## Alert #2 — cs/dispose-not-called-on-throw

- Rule: `cs/dispose-not-called-on-throw`
- Severity: **warning**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/2
- Location: `tests/TiaAgent.Runtime.Tests/PortAllocatorTests.cs:71-71`
- Message: Dispose missed if exception is thrown by call to method Start.
Dispose missed if exception is thrown by call to method Stop.

- Current file exists on `main`: **yes**
- Current file length: 78 lines

<details><summary>Current code context</summary>

```text
    63	    private static bool IsPortAvailable(int port)
    64	    {
    65	        try
    66	        {
    67	            using var listener = new System.Net.Sockets.TcpListener(
    68	                System.Net.IPAddress.Loopback, port);
    69	            listener.Start();
    70	            listener.Stop();
    71	            return true;
    72	        }
    73	        catch
    74	        {
    75	            return false;
    76	        }
    77	    }
    78	}
```

</details>

## Alert #1 — cs/user-controlled-bypass

- Rule: `cs/user-controlled-bypass`
- Severity: **high**
- Tool: CodeQL
- Instance state: `open`
- Most recent ref: `refs/heads/main`
- Most recent commit: `2e200ed5ac4bbfcc0f5c7e93f3c42616b4025ce2`
- Created: 2026-07-23T02:17:27Z
- Updated: 2026-07-23T15:19:42Z
- Alert: https://github.com/industrix-com-br/tia-portal-code-agent/security/code-scanning/1
- Location: `src/TiaAgent.Bridge/Api/BridgeController.cs:104-104`
- Message: This condition guards a sensitive action, but a user-provided value controls it.
This condition guards a sensitive action, but a user-provided value controls it.
This condition guards a sensitive action, but a user-provided value controls it.

- Current file exists on `main`: **yes**
- Current file length: 704 lines

<details><summary>Current code context</summary>

```text
    96	    private async Task HandleRequestAsync(HttpListenerContext context)
    97	    {
    98	        var request = context.Request;
    99	        var response = context.Response;
   100	        response.Headers.Add("Access-Control-Allow-Origin", "*");
   101	
   102	        var path = request.Url?.AbsolutePath ?? "/";
   103	        var method = request.HttpMethod?.ToUpperInvariant() ?? "GET";
   104	
   105	        try
   106	        {
   107	            // Bearer token authentication — only exempt well-known public endpoints.
   108	            // The public-endpoint check uses server-side constants so the bypass
   109	            // decision is not controlled by user-supplied data (CWE-807).
   110	            if (!IsPublicEndpoint(path))
   111	            {
   112	                var (authenticated, errorType, errorMessage) = AuthenticateRequest(request);
```

</details>

